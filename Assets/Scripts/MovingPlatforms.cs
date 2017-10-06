using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour {

    //Gets the platform we are moving
    public GameObject platform;
    //Adds a slider to make the movespeed of the platform easier to calculate
    [Range (.1f,5f)]
    public float moveSpeed;
    //Gets the position of the current point the platform is going too
    private Transform currentPoint;
    //Gets an array of points of where you want the platforms to move
    public Transform[] points;
    //Gives a vlaue of what number in the array the platform is going to
    private int pointSelection;


	// Use this for initialization
	void Start () {
        //assigns the currentpoints to the points that are selected for the platform to go too.
        currentPoint = points[pointSelection];
	}
	
	// Update is called once per frame
	void Update () {

        //This is code for the platform to move
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

        //checks if the platform position is the same as the currentpoint position. If so adds a value to point selection so the platform knows what point is next on the array to go to.
        if (platform.transform.position == currentPoint.position)
        {
            pointSelection++;
            //If the pointSelection reaches the end of the list the pointSelection is set to 0 (Resets)
            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }
            currentPoint = points[pointSelection];
        }

    }
    }
