using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {
    public Manager _manager;
    public MovingPlatformPlayer MPP;
    public MovingPlatformPlayer MPP2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "P1")
        {
            MPP.RespawnPlatform();
            _manager.RespawnPlayer1();
        }
        else if (other.transform.tag =="P2")
        {
            _manager.RespawnPlayer2();
            MPP.RespawnPlatform();
           MPP2.RespawnPlatform();

        }
    }
}
