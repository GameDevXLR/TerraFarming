using cakeslice;
using System.Collections.Generic;
using UnityEngine;

public class MachineAlchimieController : MonoBehaviour
{
    public List<Outline> outlinerList;
    public OreToEssenceUI interfaceMachine;
    public AlchimieGame game;
    public ressourceEnum machineResource;
    public int minimumResourcesRecquired;
    public GameObject neededResourcesCanvas;
    public bool isListening;

    private void Start()
    {
        interfaceMachine.unactivate();
        setActivationOutline(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            neededResourcesCanvas.SetActive(true);
            switch (machineResource)
            {
                case ressourceEnum.ore:
                    if (ResourcesManager.instance.rawOre >= minimumResourcesRecquired)
                    {
                        ListenForAction();
                    }
                    break;

                case ressourceEnum.essence:
                    if (ResourcesManager.instance.essence >= minimumResourcesRecquired)
                    {
                        ListenForAction();
                    }
                    break;

                default:
                    Debug.Log("planage sur les resources ici");
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(CustomInputManager.instance.actionKey))
        {
            if (!interfaceMachine.isActive && isListening)
            {
                game.activate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            neededResourcesCanvas.SetActive(false);

            StopListeningForAction();
            game.enabled = false;
        }
    }

    private void ListenForAction()
    {
        //faire les changements d'apparence de la caillasse;
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
        setActivationOutline(true);
        isListening = true;
    }

    private void StopListeningForAction()
    {
        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
        setActivationOutline(false);
        interfaceMachine.unactivate();
        isListening = false;
    }

    private void setActivationOutline(bool isActivate)
    {
        foreach (Outline outliner in outlinerList)
        {
            outliner.enabled = isActivate;
        }
    }
}