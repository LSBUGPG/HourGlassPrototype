﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : MonoBehaviour {
    //These are the values that will add force on the y or x axis. Movespeed is how fast the player moves and jumpHeight is how high the player jumps.

    public float moveSpeed;
    public float jumpHeight;

    //These variables are used to later to check if the player is on the ground.

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    public AudioSource Jump;

    //This set grounded (bool) to be a true or false. This checks an object to be hitting a layer and sets grounded to be true if the layer is hit.

    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    }

    
    void Update () {
        //This area is used to move the player is a button is pressed. Some need over conditions to run; For example the jump button, The player needs to press UpArrow and the player has to be grounded.

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            Jump.Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        }
        //This checks if the player presses RightArrow OR LeftArrow. If the player does the character will move

        if (Input.GetKey(KeyCode.RightArrow))
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
    //This checks if the player lands onto a moving platform. If it does the player will become a parent of the object so that the player will stay on the platform and not move.

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
}
