using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effects : MonoBehaviour {
    
    //Slight shade variation

    public float lowerLimitTimer = 0.1f;
    public float upperLimitTimer = 0.15f;

    public Canvas grainCanvas;
    public Image[] grains;
    public Image[] scratches;
    public Sprite[] vingettes;
    public Image currentVingette;
    int vingetteCount = 0;
    List<Image> spawnedGrain = new List<Image>();
    List<Image> spawnedScratches = new List<Image>();

    float timer;

    public Camera player1Cam;
    public Camera player2Cam;
    float shakeAmount = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        Shake(0.025f, 0.2f);

        if (currentVingette.sprite == vingettes[1])
        {
            currentVingette.sprite = vingettes[0];
        }
        else
        {
            currentVingette.sprite = vingettes[1];
        }
        


        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnGrain();
            SpawnScratches();
            int procentage = Random.Range(1, 10);
            if (procentage >= 7)
            {
                SpawnGrain();
            }
            
            timer = Random.Range(lowerLimitTimer, upperLimitTimer);
        }
	}

    void SpawnGrain()
    {
        for (int i = 0; i < spawnedGrain.Count - 1; i++)
        {
            int y = Random.Range(1, 10);
            if (y <= 9)
            {
                Destroy(spawnedGrain[i].gameObject);
                spawnedGrain.RemoveAt(i);
            }
        }

        int x = Random.Range(0, grains.Length - 1);

        spawnedGrain.Add(Instantiate(grains[x], grainCanvas.transform, false));
        spawnedGrain[spawnedGrain.Count - 1].GetComponent<RectTransform>().localPosition = (new Vector3(Random.Range(-400, 400), Random.Range(-250, 250), 0));
        spawnedGrain[spawnedGrain.Count - 1].GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
    }

    void SpawnScratches()
    {
        for (int i = 0; i < spawnedScratches.Count - 1; i++)
        {
            int y = Random.Range(1, 10);
            if (y <= 9)
            {
                Destroy(spawnedScratches[i].gameObject);
                spawnedScratches.RemoveAt(i);
            }
        } 

        int x = Random.Range(0, scratches.Length - 1);

        spawnedScratches.Add(Instantiate(scratches[x], grainCanvas.transform, false));
        spawnedScratches[spawnedScratches.Count - 1].GetComponent<RectTransform>().localPosition = (new Vector3(Random.Range(-400,400), 0, 0));
    }

    void Shake(float amount, float length) {
        shakeAmount = amount;
        InvokeRepeating("DoShake",0,0.1f);
        Invoke("StopShake", length);

    }

    void DoShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos1 = player1Cam.transform.position;
            Vector3 camPos2 = player2Cam.transform.position;

            float offestX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos1.x += offestX;
            camPos1.y += offsetY;
            camPos2.x += offestX;
            camPos2.y += offsetY;

            player1Cam.transform.position = camPos1;
            player2Cam.transform.position = camPos2;
        }
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        player1Cam.transform.localPosition = Vector3.zero;
        player2Cam.transform.localPosition = Vector3.zero;
    }
}
