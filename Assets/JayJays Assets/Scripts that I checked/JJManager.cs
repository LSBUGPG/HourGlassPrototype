using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JJManager : MonoBehaviour {
    #region Variables

    //Accessing the two players Camera
    public GameObject player1Camera;
    public GameObject player2Camera;

   public MonoBehaviour[] Player1Items;
   public MonoBehaviour[] Player2Items;

    //public GameObject[] blocksRotating;

    public GameObject Player1;
    public GameObject Player2;

    //Accessing the two players Rigidbody2D
    public Rigidbody2D rb1;
    public Rigidbody2D rb2;

    public GameObject CheckpointP1;
    public GameObject CheckpointP2;

    //Accessing the two players scripts
    public Movement MV;
    public PlayerTwo P2;

    [Header ("Time between turns")]
    public float TimeLeft;
    float NewTimeLeft;

    [Header("Maxium turns the players can do")]
    public float MaxTurns;
    //float Turns;

    public Text Turnstext;
    public Text Timetext;

    #endregion

    void Start () {
        // This makes the NewTimeLeft the same value as TimeLeft
        NewTimeLeft = TimeLeft;

        //This section makes the Player1 start with control
        //Player1Items = GameObject.FindGameObjectsWithTag("P1");
        // Player2Items = GameObject.FindGameObjectsWithTag("P2");


        MV.enabled = true;
        P2.enabled = false;

        //player1Camera.SetActive(true);
        //player2Camera.SetActive(false);

        foreach (MonoBehaviour wi1 in Player1Items)
        {
            wi1.enabled = true;
        }
        foreach (MonoBehaviour wi2 in Player2Items)
        {
            wi2.enabled = true;
        }
    }


	
	// Update is called once per frame
	void Update () {
        //This checks if all of the turns have been completed by checking turns is greater than or equal too maxturns. Else the timer carries on
        if (MaxTurns == 0)
        {
            //MV.enabled = false;
            //P2.enabled = false;
            RespawnPlayers();
        }
        else
        {
            Timetext.text = TimeLeft.ToString("00");
            TimeLeft -= Time.deltaTime;
        }

        Turnstext.text = "" + MaxTurns;
        

        //This Section checks if the Timeleft is less than 0, If so the players controls switch and adds one more value to the float turns. Then resets the TimeLeft to NewTimeLeft.
            if (TimeLeft<0)
         {
             TimeLeft = NewTimeLeft;
            MaxTurns = MaxTurns -1;
       
             if (MV.enabled == true)
            {
                foreach (MonoBehaviour wi1 in Player1Items)
                {
                    wi1.enabled = false;
                }
                foreach (MonoBehaviour wi1 in Player2Items)
                {
                    wi1.enabled= true;
                }
                //player1Camera.SetActive(false);
                //player2Camera.SetActive(true);
                MV.enabled = false;
                P2.enabled = true;
                rb1.constraints = RigidbodyConstraints2D.FreezeAll;
                rb2.constraints = RigidbodyConstraints2D.None;
                rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
             else
             {
                foreach (MonoBehaviour wi1 in Player1Items)
                {
                    wi1.enabled = true;
                }
                foreach (MonoBehaviour wi1 in Player2Items)
                {
                    wi1.enabled = false;
                }
               // player1Camera.SetActive(true);
               // player2Camera.SetActive(false);
                MV.enabled = true;
                P2.enabled = false;
                rb1.constraints = RigidbodyConstraints2D.None;
                rb1.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb2.constraints = RigidbodyConstraints2D.FreezeAll;
            }
         }


        /*foreach (GameObject blocks in blocksRotating)
        {
            blocks.transform.Rotate(Vector3.forward * -2);
        }*/
    }

    //This is called when all of the turns have been completed as a result the level will reload.
    public void RespawnPlayers()
    {

        //Debug.Log("Players Respawned");
        //Player1.transform.position = CheckpointP1.transform.position;
        //Player2.transform.position = CheckpointP2.transform.position;

       // TimeLeft = NewTimeLeft;
        //Turns = 0;
        SceneManager.LoadScene("MainScene");

    }
    public void RespawnPlayer1()
    {

        Player1.transform.position = CheckpointP1.transform.position;

    
    }
    public void RespawnPlayer2()
    {

        Player2.transform.parent = null;
       

        Player2.transform.position = CheckpointP2.transform.position;
    }


}
    

