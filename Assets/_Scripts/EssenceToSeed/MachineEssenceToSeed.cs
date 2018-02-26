using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineEssenceToSeed : MonoBehaviour {

    #region Editor Variables

    public cakeslice.Outline outliner, outlinerSeed;

    public EssenceToSeedGame game;


    #endregion


    #region Monobehaviour Methods

    // Use this for initialization
    void Start () {
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
    #endregion

    #region other Methods
    void ListenForAction()
    {
        //faire les changements d'apparence de la caillasse;
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
        outliner.enabled = true;
        outlinerSeed.enabled = true;
        

    }
    void StopListeningForAction()
    {
        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
        outliner.enabled = false;
        outlinerSeed.enabled = false;
        game.enabled = false;
    }
    #endregion
}
