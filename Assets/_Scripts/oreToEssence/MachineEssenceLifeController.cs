using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class MachineEssenceLifeController : MonoBehaviour {

    public cakeslice.Outline outliner;
    public bool hasEnoughtOre = false;
    public OreToEssenceUI interfaceMachine;
    public OreToEssenceGame game;
    public int nbrOreForEssence;
	public AudioClip miniGameFail;

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
        hasEnoughtOre = ResourcesManager.instance.rawOre >= nbrOreForEssence;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(CustomInputManager.instance.actionKey) )
        {
            if (!interfaceMachine.isActive )
            {
                interfaceMachine.activate(ResourcesManager.instance.rawOre, nbrOreForEssence, SimuleSynthetizeEssence());
				if (hasEnoughtOre) {
					game.activate (nbrOreForEssence);
				} else {
					GetComponent<AudioSource> ().PlayOneShot (miniGameFail);
				}
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

    public int SimuleSynthetizeEssence()
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
