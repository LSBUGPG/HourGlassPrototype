using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private float Value = 0;
    public GameObject ITEM;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "P2")
        {

            StartCoroutine("DoSomething");
        }
    }
    IEnumerator DoSomething()
    {
        yield return new WaitForSeconds(1f);
        ITEM.GetComponent<MeshRenderer>().material.color = new Color(Value, Value, Value, Value);
        ITEM.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        ITEM.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
        ITEM.GetComponent<BoxCollider2D>().enabled = true;
    }
}
