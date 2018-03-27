using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectGround : MonoBehaviour {

    public bool isGrounded;

    int nbrGround = 0;
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        nbrGround++;
    }

    private void OnTriggerExit(Collider other)
    {
        nbrGround--;
        if(nbrGround == 0)
            isGrounded = false;

    }
}
