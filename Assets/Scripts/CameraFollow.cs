using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public bool isTop;

    Camera thisCamera;
    float buffer;

    Vector3 position;

	// Use this for initialization
	void Start () {
        thisCamera = GetComponentInChildren<Camera>();
        buffer = thisCamera.orthographicSize;
        if (isTop)
        {
            position = new Vector3(0, buffer, -10);
        }
        else
        {
            position = new Vector3(0, -buffer, -10);
        }
	}
	
	// Update is called once per frame
	void Update () {
        position.z = -10;
        position.x = target.position.x;
        
        if (target.position.y > buffer && isTop || target.position.y < -buffer && !isTop)
        {
            position.y = target.position.y;
        }
        else
        {
            if (isTop)
            {
                position.y = buffer;
            }
            else
            {
                position.y = -buffer;
            }
        }

        transform.position = position;
	}
}
