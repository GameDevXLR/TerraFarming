using UnityEngine;

public class MachineEssenceToSeed : MonoBehaviour
{
    #region Editor Variables

    public cakeslice.Outline outliner, outlinerSeed;

    public EssenceToSeedGame game;

    #endregion Editor Variables

    #region Monobehaviour Methods

    // Use this for initialization
    private void Start()
    {
        outliner.enabled = false;
        outlinerSeed.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ListenForAction();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(CustomInputManager.instance.actionKey))
        {
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopListeningForAction();
        }
    }

    #endregion Monobehaviour Methods

    #region other Methods

    private void ListenForAction()
    {
        //faire les changements d'apparence de la caillasse;
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
        outliner.enabled = true;
        outlinerSeed.enabled = true;
    }

    private void StopListeningForAction()
    {
        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
        outliner.enabled = false;
        outlinerSeed.enabled = false;
        game.enabled = false;
    }

    #endregion other Methods
}