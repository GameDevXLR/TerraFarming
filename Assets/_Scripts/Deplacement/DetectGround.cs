using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour {

    public int numberOfDetectObj = 0;
    public LayerMask layer;
    private void OnTriggerEnter(Collider other)
    {
        if ((layer.value & 1 << other.gameObject.layer) != 0)
        {

            Debug.Log(other.gameObject.name);
            if (numberOfDetectObj == 0)
                InGameManager.instance.playerController.isGrounded = true;
            numberOfDetectObj++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((layer.value & 1 << other.gameObject.layer) != 0)
        {
            numberOfDetectObj--;
            if (numberOfDetectObj == 0)
                InGameManager.instance.playerController.isGrounded = false;
        }
    }
}
