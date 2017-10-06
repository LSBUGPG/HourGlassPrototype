using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {

    public Manager _manager;
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.name == "Top Player")
        {
            _manager.CheckpointP1 = gameObject;
        }
        else if (other.name == "Bot Player")
        {
            _manager.CheckpointP2 = gameObject;
        }
    }
}
