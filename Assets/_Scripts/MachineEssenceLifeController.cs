using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class MachineEssenceLifeController : MonoBehaviour {

    public cakeslice.Outline outliner;
    public bool menuIsOpen = false;
    public OreToEssenceUI interfaceMachine;
    public OreToEssenceGame game;
    public int nbrOreForEssence;

    private void Start()
    {
        interfaceMachine.unactivate();
        outliner.enabled = false;
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
            if (!interfaceMachine.isActive)
            {
                interfaceMachine.activate(ResourcesManager.instance.rawOre, nbrOreForEssence, SimuleSynthtizeEssence());
                game.enabled = true;
            }
            else
            {
                //SynthetizeEssence();
                //interfaceMachine.unactivate();
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
        outliner.enabled = true;
        
    }
    void StopListeningForAction()
    {

        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
        outliner.enabled = false;
        interfaceMachine.unactivate();


    }

    public int SimuleSynthtizeEssence()
    {
        return ResourcesManager.instance.rawOre / nbrOreForEssence;
    }

    public void SynthetizeEssence()
    {
        int rawOre = ResourcesManager.instance.rawOre;
        ResourcesManager.instance.ChangeEssence(rawOre / nbrOreForEssence);
        ResourcesManager.instance.setRawOre(rawOre % nbrOreForEssence);
    }

}
