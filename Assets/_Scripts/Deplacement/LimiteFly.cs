using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteFly : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InGameManager.instance.playerController.InFlyingZone = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InGameManager.instance.playerController.InFlyingZone = true;
        }
    }
}
