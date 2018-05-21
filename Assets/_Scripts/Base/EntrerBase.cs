using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrerBase : MonoBehaviour {

    public GameObject BaseCanvas;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ListenForAction();


        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopListeningForAction();

        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" )
        {
            if (Input.GetKeyDown(CustomInputManager.instance.actionKey) && InGameManager.instance.playerController.canDoAction)
            {
                BaseCanvas.SetActive(true);
                InGameManager.instance.playerController.disableMovement();
            }
        }
    }

    void ListenForAction()
    {
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
    }
    void StopListeningForAction()
    {

        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);


    }
}
