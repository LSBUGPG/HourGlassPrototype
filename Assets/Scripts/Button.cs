using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public Door[] target;
    public enum Type { Hold, Switch }; //Types of buttons.
    public Type currentType;
    bool isDown;

    void Update()
    {
        if (isDown) //If the button is pressed then opens the attached door / doors. 
        {
            for (int i = 0; i < target.Length; i++)
            {
                target[i].OpenDoor();
            }
        }
        else //If the button is not pressed then it closes them.
        {
            for (int i = 0; i < target.Length; i++)
            {
                target[i].CloseDoor();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "P1" || other.tag == "P2")
        {
            if (currentType == Type.Switch) //If the player presses a switch then it is set to the opposite value. For example, if the switch is on then the switch is set to off.
            {
                isDown = !isDown;
            }

            if (currentType == Type.Hold) //If the player touches a hold button then the hold button is set to true.
            {
                isDown = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentType == Type.Hold && other.tag == "P1" || currentType == Type.Hold && other.tag == "P2") //If the player leaves, the hold button is set to false.
        {
            isDown = false;
        }
    }
}
