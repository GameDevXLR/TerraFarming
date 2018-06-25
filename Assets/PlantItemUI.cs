using UnityEngine;
using UnityEngine.UI;

public class PlantItemUI : MonoBehaviour
{
    [Header("infos a afficher")]
    public Text plantName;

    public Text seedCount;
    public int seeds;
    public Image plantIcon;
    public GameObject isPlainIcon;
    public GameObject isCraterIcon;
    public GameObject isCaveIcon;

    public Image isNotAvailable;

    [Header("A compléter lors du spawn")]
    public PlantObject myPlant;

    [Header("gestion du bouton")]
    private Button thisButton;

    private void Start()
    {
        InitializeTheItem();
    }

    public void InitializeTheItem()
    {
        //propriétés générales.
        plantName.text = myPlant.plantName;
        plantIcon.sprite = myPlant.plantIcon;
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(PlantThis);

        //gestion des biomes.
        if (myPlant.biome1 == BiomeEnum.plain || myPlant.biome2 == BiomeEnum.plain || myPlant.biome3 == BiomeEnum.plain)
        {
            isPlainIcon.SetActive(true);
            PlantCollection.instance.plainUIObjects.Add(gameObject);
        }
        if (myPlant.biome1 == BiomeEnum.crater || myPlant.biome2 == BiomeEnum.crater || myPlant.biome3 == BiomeEnum.crater)
        {
            isCraterIcon.SetActive(true);
            PlantCollection.instance.craterUIObjects.Add(gameObject);
        }
        if (myPlant.biome1 == BiomeEnum.cave || myPlant.biome2 == BiomeEnum.cave || myPlant.biome3 == BiomeEnum.cave)
        {
            isCaveIcon.SetActive(true);
            PlantCollection.instance.caveUIObjects.Add(gameObject);
        }

        //Gestion des types
        switch (myPlant.plantType)
        {
            case PlantTypeEnum.flower:
                PlantCollection.instance.flowerUIObjects.Add(gameObject);
                break;

            case PlantTypeEnum.bush:
                PlantCollection.instance.bushUIObjects.Add(gameObject);
                break;

            case PlantTypeEnum.tree:
                PlantCollection.instance.treeUIObjects.Add(gameObject);
                break;

            default:
                break;
        }
    }

    //utiliser pour planter une graine de ce type particulier. En cliquant sur l'objet, ca plante si on est devant un plantation spot.
    public void PlantThis()
    {
        //je m'assure ici qu'on est bien en train de planter
        if (PlantationManager.instance.plantationSpot != null)
        {
            if (seeds > 0)
            {
                PlantationManager.instance.plantationSpot.PlantSeedHere(myPlant);
            }
        }
        //on pourrait définir ici quoi faire si on est pas en train de planter mais qu'on clic dessus ?
        //exemple : dans la collection ?
        else
        {
        }
    }

    //appelé par le ressourcemanager quand necessaire. No stress
    public void ActualizeSeedUI(int i)
    {
        seeds += i;
        seedCount.text = seeds.ToString();
        if (seeds == 0)
        {
            isNotAvailable.enabled = true;
            PlantCollection.instance.notAvailableUIObjects.Add(gameObject);
        }
        else
        {
            isNotAvailable.enabled = false;
            PlantCollection.instance.notAvailableUIObjects.Remove(gameObject);
            if (PlantCollection.instance.collectionOpen)
            {
                if (PlantCollection.instance.plainUIVisible && PlantCollection.instance.plainUIObjects.Contains(gameObject))
                {
                    gameObject.SetActive(true);
                }
                if (PlantCollection.instance.craterUIVisible && PlantCollection.instance.craterUIObjects.Contains(gameObject))
                {
                    gameObject.SetActive(true);
                }
                if (PlantCollection.instance.caveUIVisible && PlantCollection.instance.caveUIObjects.Contains(gameObject))
                {
                    gameObject.SetActive(true);
                }
            }
        }
    }
}