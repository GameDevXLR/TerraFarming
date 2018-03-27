using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("miaou");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Miaou2");
    }
}
