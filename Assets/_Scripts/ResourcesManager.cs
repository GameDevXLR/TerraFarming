using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager : MonoBehaviour
{
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

    private void Awake()
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
        rawOreDisplay.text = rawOre.ToString();
        launchAnimation("isOre", qty);
        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().Play("ScaleOreIco");
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
        essenceDisplay.text = essence.ToString();
        launchAnimation("isEssence", qty);
        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().Play("ScaleEssenceIco");
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

    public int GetSeedQuantity(PlantTypeEnum seed)
    {
        switch (seed)
        {
            case PlantTypeEnum.bush:
                return bushSeed;

            case PlantTypeEnum.tree:
                return treeSeed;

            case PlantTypeEnum.flower:
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

    //prend en compte le biome de la machine ^^
    //	public void setRessourceQuantity(ressourceEnum ress, int qty, BiomeEnum biome)
    //	{
    //		switch (ress)
    //		{
    //		case ressourceEnum.bush:
    //			switch (biome)
    //			{
    //			case BiomeEnum.plain:
    //				PlantCollection.instance.airBushAdd (qty);
    //				break;
    //			case BiomeEnum.crater:
    //				PlantCollection.instance.craterBushAdd(qty);
    //				break;
    //			case BiomeEnum.cave:
    //				PlantCollection.instance.caveBushAdd(qty);
    //				break;
    //			default:
    //				break;
    //			}
    //			ChangeBushSeed(qty);
    //			break;
    //		case ressourceEnum.tree:
    //			switch (biome)
    //			{
    //			case BiomeEnum.plain:
    //				PlantCollection.instance.airTreeAdd (qty);
    //				break;
    //			case BiomeEnum.crater:
    //				PlantCollection.instance.craterTreeAdd(qty);
    //				break;
    //			case BiomeEnum.cave:
    //				PlantCollection.instance.caveTreeAdd(qty);
    //				break;
    //			default:
    //				break;
    //			}
    //			ChangeTreeSeed(qty);
    //			break;
    //		case ressourceEnum.flower:
    //			switch (biome)
    //			{
    //			case BiomeEnum.plain:
    //				PlantCollection.instance.airFlowerAdd (qty);
    //				break;
    //			case BiomeEnum.crater:
    //				PlantCollection.instance.craterFlowerAdd(qty);
    //				break;
    //			case BiomeEnum.cave:
    //				PlantCollection.instance.caveFlowerAdd(qty);
    //				break;
    //			default:
    //				break;
    //			}
    //			ChangeFlowerSeed(qty);
    //			break;
    //		case ressourceEnum.ore:
    //			ChangeRawOre(qty);
    //			break;
    //		case ressourceEnum.essence:
    //			ChangeEssence(qty);
    //			break;
    //		default:
    //			break;
    //		}
    //	}

    //	//prend en compte jusqu'a 3 biomes
    //	public void setRessourceQuantity(ressourceEnum ress, int qty, BiomeEnum biome1, BiomeEnum biome2, BiomeEnum biome3)
    //	{
    //		switch (ress)
    //		{
    //		#region ressource = bush
    //
    //		case ressourceEnum.bush:
    //			switch (biome1)
    //			{
    //			case BiomeEnum.plain:
    //				switch (biome2)
    //				{
    //				case BiomeEnum.crater:
    //					switch (biome3)
    //					{
    //					case BiomeEnum.cave:
    //						PlantCollection.instance.airCaveCraterBushAdd(qty);
    //
    //						break;
    //					case BiomeEnum.none:
    //						PlantCollection.instance.airCraterBushAdd(qty);
    //
    //						break;
    //					default:
    //					break;
    //					}
    //					break;
    //				case BiomeEnum.cave:
    //					PlantCollection.instance.airCaveBushAdd(qty);
    //
    //					break;
    //				case BiomeEnum.none:
    //					PlantCollection.instance.airBushAdd(qty);
    //					break;
    //				default:
    //				break;
    //				}
    //				break;
    //			case BiomeEnum.crater:
    //				switch (biome2)
    //				{
    //				case BiomeEnum.cave:
    //					PlantCollection.instance.caveCraterBushAdd(qty);
    //
    //				break;
    //				case BiomeEnum.none:
    //					PlantCollection.instance.craterBushAdd(qty);
    //					break;
    //				}
    //				break;
    //			case BiomeEnum.cave:
    //				PlantCollection.instance.caveBushAdd(qty);
    //				break;
    //			default:
    //				break;
    //			}
    //			ChangeBushSeed(qty);
    //			break;
    //			#endregion
    //
    //			#region ressource = tree
    //
    //		case ressourceEnum.tree:
    //			switch (biome1)
    //			{
    //			case BiomeEnum.plain:
    //				switch (biome2)
    //				{
    //				case BiomeEnum.crater:
    //					switch (biome3)
    //					{
    //					case BiomeEnum.cave:
    //						PlantCollection.instance.airCaveCraterTreeAdd(qty);
    //
    //						break;
    //					case BiomeEnum.none:
    //						PlantCollection.instance.airCraterTreeAdd(qty);
    //
    //						break;
    //					default:
    //						break;
    //					}
    //					break;
    //				case BiomeEnum.cave:
    //					PlantCollection.instance.airCaveTreeAdd(qty);
    //
    //					break;
    //				case BiomeEnum.none:
    //					PlantCollection.instance.airTreeAdd(qty);
    //					break;
    //				default:
    //					break;
    //				}
    //				break;
    //			case BiomeEnum.crater:
    //				switch (biome2)
    //				{
    //				case BiomeEnum.cave:
    //					PlantCollection.instance.caveCraterTreeAdd(qty);
    //
    //					break;
    //				case BiomeEnum.none:
    //					PlantCollection.instance.craterTreeAdd(qty);
    //					break;
    //				}
    //				break;
    //			case BiomeEnum.cave:
    //				PlantCollection.instance.caveTreeAdd(qty);
    //				break;
    //			default:
    //				break;
    //			}
    //			ChangeTreeSeed(qty);
    //			break;
    //			#endregion
    //
    //			#region ressource = flower
    //
    //		case ressourceEnum.flower:
    //			switch (biome1)
    //			{
    //			case BiomeEnum.plain:
    //				switch (biome2)
    //				{
    //				case BiomeEnum.crater:
    //					switch (biome3)
    //					{
    //					case BiomeEnum.cave:
    //						PlantCollection.instance.airCaveCraterFlowerAdd(qty);
    //
    //						break;
    //					case BiomeEnum.none:
    //						PlantCollection.instance.airCraterFlowerAdd(qty);
    //
    //						break;
    //					default:
    //						break;
    //					}
    //					break;
    //				case BiomeEnum.cave:
    //					PlantCollection.instance.airCaveFlowerAdd(qty);
    //
    //					break;
    //				case BiomeEnum.none:
    //					PlantCollection.instance.airFlowerAdd(qty);
    //					break;
    //				default:
    //					break;
    //				}
    //				break;
    //			case BiomeEnum.crater:
    //				switch (biome2)
    //				{
    //				case BiomeEnum.cave:
    //					PlantCollection.instance.caveCraterFlowerAdd(qty);
    //
    //					break;
    //				case BiomeEnum.none:
    //					PlantCollection.instance.craterFlowerAdd(qty);
    //					break;
    //				}
    //				break;
    //			case BiomeEnum.cave:
    //				PlantCollection.instance.caveFlowerAdd(qty);
    //				break;
    //			default:
    //				break;
    //			}
    //			ChangeFlowerSeed(qty);
    //			break;
    //			#endregion
    //
    //		case ressourceEnum.ore:
    //			ChangeRawOre(qty);
    //			break;
    //		case ressourceEnum.essence:
    //			ChangeEssence(qty);
    //			break;
    //		default:
    //			break;
    //		}
    //	}

    //prend en compte un PlantObject et une quantité uniquement:
    public void setRessourceQuantity(PlantObject PO, int qty)
    {
        switch (PO.plantType)
        {
            case PlantTypeEnum.flower:
                ChangeFlowerSeed(qty);

                break;

            case PlantTypeEnum.bush:
                ChangeBushSeed(qty);

                break;

            case PlantTypeEnum.tree:
                ChangeTreeSeed(qty);

                break;

            default:
                break;
        }
        PlantCollection.instance.AddSeed(PO, qty);
    }

    public bool haveSeed()
    {
        return flowerSeed > 0 | bushSeed > 0 | treeSeed > 0;
    }

    public void launchAnimation(string anim, int qty)
    {
        //        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().SetBool(anim, true);
        //        InGameManager.instance.InterfaceAnimator.GetComponent<Animator>().SetBool(anim, false);
        //dDebug.Log("coucou");
    }
}