using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;

public class CaptainDialogue : MonoBehaviour {

	public DialogueTreeController dialogueUI;

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
			dialogueUI.StartDialogue (DialogueCallback);
			unactivate ();
		}
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopListeningForAction();
        }
    }
	public void DialogueCallback(bool dialogueOver)
	{
		if (dialogueOver) 
		{
			Debug.Log ("test");
			activate ();
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
