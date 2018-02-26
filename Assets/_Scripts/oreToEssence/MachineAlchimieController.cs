using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class MachineAlchimieController : MonoBehaviour {

    public List<Outline> outlinerList;
    public OreToEssenceUI interfaceMachine;
    public AlchimieGame game;

    private void Start()
    {
        interfaceMachine.unactivate();
        setActivationOutline(false);
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
            if (!interfaceMachine.isActive )
            {
                game.activate();
    //            interfaceMachine.activate(ResourcesManager.instance.rawOre, nbrOreForEssence, SimuleSynthetizeEssence());
    //if (hasEnoughtOre) {
    //	game.activate (nbrOreForEssence);
    //} else {
    //	GetComponent<AudioSource> ().PlayOneShot (miniGameFail);
    //}
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopListeningForAction();
            game.enabled = false;
        }
    }

    void ListenForAction()
    {
        //faire les changements d'apparence de la caillasse;
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
        setActivationOutline(true);
    }
    void StopListeningForAction()
    {
        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
        setActivationOutline(false);
        interfaceMachine.unactivate();
    }


    void setActivationOutline(bool isActivate)
    {
        foreach (Outline outliner in outlinerList)
        {
            outliner.enabled = isActivate;
        }
    }
    

}
