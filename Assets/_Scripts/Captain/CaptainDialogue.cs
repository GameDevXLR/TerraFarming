using NodeCanvas.DialogueTrees;
using UnityEngine;

public class CaptainDialogue : MonoBehaviour
{
    public DialogueTreeController dialogueUI;

    public Collider collide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ListenForAction();
        }
    }

    private void OnTriggerStay(Collider other)
    {
		if (other.tag == "Player" && Input.GetKeyDown(CustomInputManager.instance.actionKey) && GameEventsManager.instance.introIsOver)
        {
            dialogueUI.StartDialogue(DialogueCallback);
            unactivate();
			StopListeningForAction();

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
            Debug.Log("test");
            activate();
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

    private void ListenForAction()
    {
        //faire les changements d'apparence de la caillasse;
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
    }

    private void StopListeningForAction()
    {
        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
    }
}