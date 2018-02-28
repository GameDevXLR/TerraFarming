using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainDialogue : MonoBehaviour {

    public CaptainDialogueUI dialogueUI;

    public Collider collide;

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ListenForAction();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(CustomInputManager.instance.actionKey))
        {
            dialogueUI.nextStep();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopListeningForAction();
        }
    }

    public void activate()
    {
        collide.enabled = true;
    }
    public void unactivate()
    {
        collide.enabled = false;
    }

    void ListenForAction()
    {
        //faire les changements d'apparence de la caillasse;
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
    }
    void StopListeningForAction()
    {
        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
    }
}
