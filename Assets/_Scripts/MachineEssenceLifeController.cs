using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class MachineEssenceLifeController : MonoBehaviour {

    public cakeslice.Outline outliner;
    public bool menuIsOpen = false;
    public OreToEssenceUI interfaceMachine;

    private void Start()
    {
        interfaceMachine.unacticate();

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
        if(other.tag == "Player" && Input.GetKeyDown(CustomInputManager.instance.actionKey) )
        {
            if(!interfaceMachine.isActive)
                interfaceMachine.activate();
            else
            {
                ResourcesManager.instance.SynthetizeEssence();
                interfaceMachine.unacticate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopListeningForAction();
        }
    }

    void ListenForAction()
    {
        //faire les changements d'apparence de la caillasse;
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
        outliner.enabled = true;
        
    }
    void StopListeningForAction()
    {

        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
        outliner.enabled = false;
        interfaceMachine.unacticate();


    }
}
