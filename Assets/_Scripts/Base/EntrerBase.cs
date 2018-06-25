using UnityEngine;

public class EntrerBase : MonoBehaviour
{
    public GameObject BaseCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ListenForAction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopListeningForAction();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(CustomInputManager.instance.actionKey) && InGameManager.instance.playerController.canDoAction)
            {
                BaseCanvas.SetActive(true);
                InGameManager.instance.playerController.disableMovement();
            }
        }
    }

    private void ListenForAction()
    {
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
    }

    private void StopListeningForAction()
    {
        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
    }
}