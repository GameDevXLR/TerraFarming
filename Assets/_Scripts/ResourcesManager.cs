using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager : MonoBehaviour {


	public static ResourcesManager instance;


    public int rawOre;
	public int essence;
	public int flowerSeed;
	public int bushSeed;
	public int treeSeed;

	public Text rawOreDisplay;
	public Text essenceDisplay;
	public Text flowerSeedDisplay;
	public Text bushSeedDisplay;
	public Text treeSeedDisplay;


	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

    public void setRawOre(int qty)
    {
        rawOre = qty;
        rawOreDisplay.text = rawOre.ToString();
        launchAnimation("isOre", qty);
    }

	public void ChangeRawOre(int qty)
	{
		rawOre += qty;
		rawOreDisplay.text = rawOre.ToString ();
        launchAnimation("isOre", qty);
		InGameManager.instance.InterfaceAnimator.GetComponent<Animator> ().Play("ScaleOreIco");
    }


    public void setEssence(int qty)
    {
        essence = qty;
        essenceDisplay.text = essence.ToString();
        launchAnimation("isEssence", qty);
        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().Play("ScaleEssenceIco");
    }

	public void ChangeEssence(int qty)
	{
		essence += qty;
		essenceDisplay.text = essence.ToString ();
        launchAnimation("isEssence", qty);
		InGameManager.instance.InterfaceAnimator.GetComponent<Animator> ().Play("ScaleEssenceIco");
    }

    public void ChangeFlowerSeed(int qty)
    {
        flowerSeed += qty;
        flowerSeedDisplay.text = flowerSeed.ToString();
        launchAnimation("isFlower", qty);
        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().Play("ScaleFlowerIco");

    }
    public void setFlowerSeed(int qty)
    {
        flowerSeed = qty;
        flowerSeedDisplay.text = flowerSeed.ToString();
        launchAnimation("isFlower", qty);
        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().Play("ScaleFlowerIco");

    }


    public void ChangeBushSeed(int qty)
    {
        bushSeed += qty;
        bushSeedDisplay.text = bushSeed.ToString();
        launchAnimation("isBush", qty);
        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().Play("ScaleBushIco");

    }
    public void setBushSeed(int qty)
    {
        bushSeed = qty;
        bushSeedDisplay.text = bushSeed.ToString();
        launchAnimation("isBush", qty);
        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().Play("ScaleBushIco");

    }


    public void ChangeTreeSeed(int qty)
    {
        treeSeed += qty;
        treeSeedDisplay.text = treeSeed.ToString();
        launchAnimation("isTree", qty);
        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().Play("ScaleTreeIco");
    }
    public void setTreeSeed(int qty)
    {
        treeSeed = qty;
        treeSeedDisplay.text = treeSeed.ToString();
        launchAnimation("isTree", qty);
        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().Play("ScaleTreeIco");
    }

    public int GetSeedQuantity(seedEnum seed)
    {
        switch (seed)
        {
            case seedEnum.bush:
                return bushSeed;
            case seedEnum.tree:
                return treeSeed;
            case seedEnum.flower:
                return flowerSeed;
            default:
                return 0;
        }
    }


    public int GetRessourceQuantity(ressourceEnum ress)
    {
        switch (ress)
        {
            case ressourceEnum.bush:
                return bushSeed;
            case ressourceEnum.tree:
                return treeSeed;
            case ressourceEnum.flower:
                return flowerSeed;
            case ressourceEnum.ore:
                return rawOre;
            case ressourceEnum.essence:
                return essence;
            default:
                return 0;
        }
    }


    public void setRessourceQuantity(ressourceEnum ress, int qty)
    {
        switch (ress)
        {
            case ressourceEnum.bush:
                ChangeBushSeed(qty);
                break;
            case ressourceEnum.tree:
                ChangeTreeSeed(qty);
                break;
            case ressourceEnum.flower:
                ChangeFlowerSeed(qty);
                break;
            case ressourceEnum.ore:
                ChangeRawOre(qty);
                break;
            case ressourceEnum.essence:
                ChangeEssence(qty);
                break;
            default:
                break;
        }
    }


    public void launchAnimation(string anim, int qty)
    {
//        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().SetBool(anim, true);
//        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().SetBool(anim, false);
        //dDebug.Log("coucou");        
    }
}
