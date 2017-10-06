using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    //Accessing the two players Camera
    public GameObject player1Camera;
    public GameObject player2Camera;

    public MonoBehaviour[] Player1Items;
    public MonoBehaviour[] Player2Items;

    //public GameObject[] blocksRotating;

    public GameObject Player1;
    public GameObject Player2;

    //Accessing the two players Rigidbody2D
    Rigidbody2D rb1;
    Rigidbody2D rb2;

    public GameObject CheckpointP1;
    public GameObject CheckpointP2;

    //Accessing the two players scripts
    BasicMovement MV1;
    BasicMovement MV2;
    Vector2 tempVelocity1;
    Vector2 tempVelocity2;
    

    [Header("Time between turns")]
    public float TimeLeft;
    float NewTimeLeft;

    [Header("Maxium turns the players can do")]
    public float MaxTurns;
    //float Turns;

    public Text Turnstext;
    public Text Timetext;

    [Header("Canvases with UI for losing and winning")]
    List<Door> goalDoors = new List<Door>();
    public Canvas loseCanvas;
    public Canvas winCanvas;
    public Canvas tutorialCanvas;

    public float tutorialTimer = 4;

    // Use this for initialization
    void Start () {

        rb1 = Player1.GetComponent<Rigidbody2D>();
        rb2 = Player2.GetComponent<Rigidbody2D>();

        MV1 = Player1.GetComponent<BasicMovement>();
        MV2 = Player2.GetComponent<BasicMovement>();

        // This makes the NewTimeLeft the same value as TimeLeft
        NewTimeLeft = TimeLeft;

        MV1.enabled = true;
        MV2.enabled = false;

        foreach (MonoBehaviour wi1 in Player1Items)
        {
            wi1.enabled = true;
        }
        foreach (MonoBehaviour wi2 in Player2Items)
        {
            wi2.enabled = true;
        }

        if (winCanvas.enabled)
        {
            winCanvas.enabled = false;
        }
       if (loseCanvas.enabled)
        {
            loseCanvas.enabled = false;
        }
        if (tutorialCanvas.enabled)
        {
            tutorialCanvas.enabled = false;
        }

        foreach (Door door in FindObjectsOfType<Door>()) //Finds all the goal doors in the scene.
        {
            if (door.currentType == Door.Type.Goal)
            {
                goalDoors.Add(door);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        tutorialTimer -= Time.deltaTime;

        if (tutorialTimer < 0)
        {
            tutorialCanvas.enabled = false;
        }
        else
        {
            tutorialCanvas.enabled = true;
        }

        //This checks if all of the turns have been completed by checking turns is greater than or equal too maxturns. Else the timer carries on
        if (MaxTurns == 0)
        {
            MV1.enabled = false;
            MV2.enabled = false;
            loseCanvas.enabled = true;
        }
        else
        {
            Timetext.text = TimeLeft.ToString("00");
            TimeLeft -= Time.deltaTime;
        }

        Turnstext.text = MaxTurns.ToString();

        //This Section checks if the Timeleft is less than 0, If so the players controls switch and adds one more value to the float turns. Then resets the TimeLeft to NewTimeLeft.
        if (TimeLeft < 0)
        {
            TimeLeft = NewTimeLeft;
            MaxTurns = MaxTurns - 1;

            if (MV1.enabled == true)
            {
                foreach (MonoBehaviour wi1 in Player1Items)
                {
                    wi1.enabled = false;
                }
                foreach (MonoBehaviour wi1 in Player2Items)
                {
                    wi1.enabled = true;
                }
                //player1Camera.SetActive(false);
                //player2Camera.SetActive(true);
                tempVelocity1 = rb1.velocity;
                MV1.enabled = false;
                MV2.enabled = true;
                rb1.constraints = RigidbodyConstraints2D.FreezeAll;
                rb2.constraints = RigidbodyConstraints2D.None;
                rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb2.velocity = tempVelocity2;
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
                tempVelocity2 = rb2.velocity;
                MV1.enabled = true;
                MV2.enabled = false;
                rb1.constraints = RigidbodyConstraints2D.None;
                rb1.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb2.constraints = RigidbodyConstraints2D.FreezeAll;
                rb1.velocity = tempVelocity1;
            }
        }

        if (isPlayersAtDoor()) //If all the players are at the goal doors then they win.
        {
            winCanvas.enabled = true;
            Debug.Log("Game over! You win!");
        }
}
    bool isPlayersAtDoor() //Checks if all the player are touching the goal door.
    {
        for (int i = 0; i < goalDoors.Count; i++)
        {
            if (goalDoors[i].isUsed == false) 
            {
                return false; //Returns false if one or more doors are not being used.
            }

        }
            return true; //Returns true if all the players are using there goal doors.
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
        Player1.transform.parent = null;

        Player1.transform.position = CheckpointP1.transform.position;


    }
    public void RespawnPlayer2()
    {

        Player2.transform.parent = null;


        Player2.transform.position = CheckpointP2.transform.position;
    }

    public void RestartLevel()
    {
        Player1.transform.parent = null;
        MV1.enabled = true;
        Player1.transform.position = CheckpointP1.transform.position;

        Player2.transform.parent = null;
        MV2.enabled = true;
        Player2.transform.position = CheckpointP2.transform.position;
    }
}
