using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public enum Type { Goal, Obstacle};
    public Type currentType; //Types of doors that exit.

    bool isOpen;
    public bool isUsed;

    Collider2D coll;
    SpriteRenderer sr;

    void Start() //Gets doors collider and mesh renderer.
    {
        coll = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        if (isOpen) //If the door is set to being opened it acts accordingly.
        {
            if (currentType == Type.Obstacle) //If it is a obstacle door, it removes the collision and turns off the renderer.
            {
                coll.enabled = false;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.25f);
            }
            if (currentType == Type.Goal) //If it is a door goal it changes the collider to a trigger and changes color to red.
            {
                coll.isTrigger = true;
                sr.color = new Color(1, 0.92f, 0.016f, 0.75f);
            }
        }
        else //If the door is closed then it is reset.
        {
            if (currentType == Type.Obstacle)
            {
                coll.enabled = true;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
            if (currentType == Type.Goal)
            {
                coll.isTrigger = false;
                sr.color = Color.white;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collision) //If a player is touching the door then it sets to being used.
    {
		if (collision.transform.tag == "P2"|| collision.transform.tag == "P1")
        {
            isUsed = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision) //If the player leaves it resets.
    {
		if (collision.transform.tag == "P2"|| collision.transform.tag == "P1")
        {
            isUsed = false;
        }
    }

    public void OpenDoor() //Method for opening the door.
    {
        isOpen = true;
    }

    public void CloseDoor() //Method for closing the door.
    {
        isOpen = false;
    }
}
