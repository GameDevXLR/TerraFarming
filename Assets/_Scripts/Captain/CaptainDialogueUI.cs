using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptainDialogueUI : MonoBehaviour
{
    public string step1;
    public int oreNeed = 10;
    public string step2;
    public int essenceNeed = 1;
    public string step3;
    public string step4;
    public List<string> dialogueNoTriggerList;

    public Text dialogueText;

    public CaptainDialogue captain;

    public cakeslice.Outline essenceOutline;
    public List<cakeslice.Outline> seedOutlineList;

    private int step;
    private int maxStep = 4;

    private int previousSeedFlower;
    private int previousSeedBush;
    private int previousSeedTree;

    private void Start()
    {
        if (PlayerPrefs.HasKey("stepGame"))
        {
            step = PlayerPrefs.GetInt("stepGame");
        }
        else
        {
            step = 0;
            PlayerPrefs.SetInt("stepGame", 0);
        }
        if (PlayerPrefs.GetString("Game") == "new")
        {
            step = 0;
        }
        if (step < maxStep)
        {
            captain.unactivate();
            dialogueText.text = getStep(step);
        }
        else
        {
            enabled = false;
            captain.activate();
            step = Random.Range(0, dialogueNoTriggerList.Count - 1);
            dialogueText.text = dialogueNoTriggerList[step];
        }
    }

    private void Update()
    {
        if (step < maxStep)
        {
            getNextStepAction(step);
        }
        else
        {
            captain.activate();
            step = Random.Range(0, dialogueNoTriggerList.Count - 1);
            dialogueText.text = dialogueNoTriggerList[step];
            enabled = false;
        }
    }

    private string getStep(int index)
    {
        switch (index)
        {
            case 0:
                return step1;

            case 1:
                essenceOutline.enabled = true;
                return step2;

            case 2:
                essenceOutline.enabled = false;
                outlineActivate(true);
                return step3;

            case 3:
                outlineActivate(false);
                return step4;

            default:
                return " ";
        }
    }

    private void getNextStepAction(int index)
    {
        switch (index)
        {
            case 0:
                step1Action();
                break;

            case 1:
                step2Action();
                break;

            case 2:
                step3Action();
                break;

            case 3:
                step4Action();
                break;

            default:
                break;
        }
    }

    private void step1Action()
    {
        if (ResourcesManager.instance.rawOre >= oreNeed)
        {
            step++;
            dialogueText.text = getStep(step);
            PlayerPrefs.SetInt("stepGame", step);
        }
    }

    private void step2Action()
    {
        if (ResourcesManager.instance.essence >= essenceNeed)
        {
            step++;
            dialogueText.text = getStep(step);
            PlayerPrefs.SetInt("stepGame", step);
        }
    }

    private void step3Action()
    {
        if (ResourcesManager.instance.flowerSeed > 0 || ResourcesManager.instance.bushSeed > 0 || ResourcesManager.instance.treeSeed > 0)
        {
            step++;
            dialogueText.text = getStep(step);
            saveSeed();
            PlayerPrefs.SetInt("stepGame", step);
        }
    }

    private void step4Action()
    {
        if (!compareSeed(PlantTypeEnum.bush, previousSeedBush) || !compareSeed(PlantTypeEnum.flower, previousSeedFlower) || !compareSeed(PlantTypeEnum.tree, previousSeedTree))
        {
            step++;
            dialogueText.text = getStep(step);
            PlayerPrefs.SetInt("stepGame", step);
        }
        saveSeed();
    }

    private void saveSeed()
    {
        previousSeedBush = ResourcesManager.instance.GetSeedQuantity(PlantTypeEnum.bush);
        previousSeedFlower = ResourcesManager.instance.GetSeedQuantity(PlantTypeEnum.flower);
        previousSeedTree = ResourcesManager.instance.GetSeedQuantity(PlantTypeEnum.tree);
    }

    private bool compareSeed(PlantTypeEnum seed, int seedPrevious)
    {
        return ResourcesManager.instance.GetSeedQuantity(seed) >= seedPrevious;
    }

    public void nextStep()
    {
        step++;
        if (step >= dialogueNoTriggerList.Count)
            step = 0;
        dialogueText.text = dialogueNoTriggerList[step];
    }

    public void outlineActivate(bool activate)
    {
        foreach (cakeslice.Outline outline in seedOutlineList)
        {
            outline.enabled = activate;
        }
    }
}