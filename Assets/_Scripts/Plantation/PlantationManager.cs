using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantationManager : MonoBehaviour
{
    //rajouter ici un gameobject pour chaque biome correspondant au canvas de plantage de graine.

    public static PlantationManager instance;

    public List<PlantationSpotEnhanced> plantationList = new List<PlantationSpotEnhanced>();

    [Header("gestion du menu de plantage de graine")]

    //nouveau canvas remplacant tous les autres.
    public Canvas plantSeedCanvas;

    public Transform flowerSeedContent;
    public Transform bushSeedContent;
    public Transform treeSeedContent;

    public List<GameObject> actualUIElements;
    public List<GameObject> notAvailablePlantsUI;
    private bool notAvailableVisible;

    public Image plainSeedImg;
    public Image craterSeedImg;
    public Image caveSeedImg;

    public bool isSeedMenuOpen;

    //le plantation spot avec lequel on interagit actuellement
    public PlantationSpotEnhanced plantationSpot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //		Invoke ("ShowNotAvailableItems", 1f);
        //		ShowNotAvailableItems (true);
    }

    private void Update()
    {
        if (isSeedMenuOpen && plantationSpot)
        {
            //annuler
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HidePlantTypeMenu();
            }
        }
    }

    public void SpeedUpAllGrowth()
    {
        foreach (var p in plantationList)
        {
            if (p.plantSO != null)
            {
                p.growthStartTime -= 55f;
            }
        }
    }

    #region SeedsMenus

    public void ShowPlantTypeMenu(PlantationSpotEnhanced spot)
    {
        if (PlantCollection.instance.collectionOpen)
        {
            PlantCollection.instance.ShowHideCollection();
        }
        PlayerUICanvas.instance.ResetPlantsUIColor();

        plantSeedCanvas.enabled = true;
        plantationSpot = spot;

        switch (plantationSpot.spotBiome)
        {
            case BiomeEnum.plain:
                plainSeedImg.enabled = true;
                craterSeedImg.enabled = false;
                caveSeedImg.enabled = false;
                foreach (GameObject go in PlantCollection.instance.plainUIObjects)
                {
                    //				go.transform.SetParent (plantSeedContent);
                    switch (go.GetComponent<PlantItemUI>().myPlant.plantType)
                    {
                        case PlantTypeEnum.flower:
                            go.transform.SetParent(flowerSeedContent);
                            break;

                        case PlantTypeEnum.bush:
                            go.transform.SetParent(bushSeedContent);
                            break;

                        case PlantTypeEnum.tree:
                            go.transform.SetParent(treeSeedContent);
                            break;

                        default:
                            break;
                    }
                    actualUIElements.Add(go);
                    if (go.GetComponent<PlantItemUI>().seeds == 0)
                    {
                        go.SetActive(false);
                        go.GetComponent<PlantItemUI>().isNotAvailable.enabled = true;
                        notAvailablePlantsUI.Add(go);
                    }
                    else
                    {
                        go.SetActive(true);
                    }
                    go.transform.localScale = Vector3.one;
                }
                break;

            case BiomeEnum.crater:
                plainSeedImg.enabled = false;
                craterSeedImg.enabled = true;
                caveSeedImg.enabled = false;
                foreach (GameObject go in PlantCollection.instance.craterUIObjects)
                {
                    //				go.transform.SetParent (plantSeedContent);
                    switch (go.GetComponent<PlantItemUI>().myPlant.plantType)
                    {
                        case PlantTypeEnum.flower:
                            go.transform.SetParent(flowerSeedContent);
                            break;

                        case PlantTypeEnum.bush:
                            go.transform.SetParent(bushSeedContent);
                            break;

                        case PlantTypeEnum.tree:
                            go.transform.SetParent(treeSeedContent);
                            break;

                        default:
                            break;
                    }
                    actualUIElements.Add(go);
                    if (go.GetComponent<PlantItemUI>().seeds == 0)
                    {
                        go.SetActive(false);
                        go.GetComponent<PlantItemUI>().isNotAvailable.enabled = true;
                        notAvailablePlantsUI.Add(go);
                    }
                    else
                    {
                        go.SetActive(true);
                    }
                    go.transform.localScale = Vector3.one;
                }
                break;

            case BiomeEnum.cave:
                plainSeedImg.enabled = false;
                craterSeedImg.enabled = false;
                caveSeedImg.enabled = true;
                foreach (GameObject go in PlantCollection.instance.caveUIObjects)
                {
                    //				go.transform.SetParent (plantSeedContent);
                    switch (go.GetComponent<PlantItemUI>().myPlant.plantType)
                    {
                        case PlantTypeEnum.flower:
                            go.transform.SetParent(flowerSeedContent);
                            break;

                        case PlantTypeEnum.bush:
                            go.transform.SetParent(bushSeedContent);
                            break;

                        case PlantTypeEnum.tree:
                            go.transform.SetParent(treeSeedContent);
                            break;

                        default:
                            break;
                    }
                    actualUIElements.Add(go);
                    if (go.GetComponent<PlantItemUI>().seeds == 0)
                    {
                        go.SetActive(false);
                        go.GetComponent<PlantItemUI>().isNotAvailable.enabled = true;
                        notAvailablePlantsUI.Add(go);
                    }
                    else
                    {
                        go.SetActive(true);
                    }
                    go.transform.localScale = Vector3.one;
                }
                break;

            default:
                break;
        }

        InGameManager.instance.isPlanting = true;
        InGameManager.instance.playerController.disableMovement();
        isSeedMenuOpen = true;
        ShowFlowerSeedContent();
    }

    public void ShowNotAvailableItems()
    {
        notAvailableVisible = !notAvailableVisible;
        foreach (var go in notAvailablePlantsUI)
        {
            go.SetActive(notAvailableVisible);
        }
    }

    public void ShowNotAvailableItems(bool show)
    {
        notAvailableVisible = show;
        foreach (var go in notAvailablePlantsUI)
        {
            go.SetActive(notAvailableVisible);
        }
    }

    public void HidePlantTypeMenu()
    {
        foreach (GameObject go in actualUIElements)
        {
            go.transform.SetParent(PlantCollection.instance.collectionContentUI);
            go.transform.localScale = Vector3.one;

            go.SetActive(true);
        }
        if (isSeedMenuOpen)
        {
            plantationSpot = null;
            plantSeedCanvas.enabled = false;
            InGameManager.instance.isPlanting = false;
            InGameManager.instance.playerController.enableMovement();
            isSeedMenuOpen = false;
        }
        actualUIElements.Clear();
        notAvailablePlantsUI.Clear();
    }

    public void ShowFlowerSeedContent()
    {
        flowerSeedContent.gameObject.SetActive(true);
        bushSeedContent.gameObject.SetActive(false);
        treeSeedContent.gameObject.SetActive(false);
    }

    public void ShowBushSeedContent()
    {
        flowerSeedContent.gameObject.SetActive(false);
        bushSeedContent.gameObject.SetActive(true);
        treeSeedContent.gameObject.SetActive(false);
    }

    public void ShowTreeSeedContent()
    {
        flowerSeedContent.gameObject.SetActive(false);
        bushSeedContent.gameObject.SetActive(false);
        treeSeedContent.gameObject.SetActive(true);
    }

    #endregion SeedsMenus

    #region SaveLoad

    public List<PlanteSave> savePlantation()
    {
        List<PlanteSave> planteSave = new List<PlanteSave>();
        for (int i = 0; i < plantationList.Count; i++)
        {
            PlanteSave save = new PlanteSave
            {
                index = i,
                plantType = plantationList[i].plantType,
                plantState = plantationList[i].actualPlantState
            };
            planteSave.Add(save);
        }
        return planteSave;
    }

    public void loadPlantation(List<PlanteSave> planteSave)
    {
        foreach (PlanteSave save in planteSave)
        {
            if (save.plantType != PlantTypeEnum.none)
            {
                //                plantationList[save.index].SelectPlantType(save.plantType);
                plantationList[save.index].RecquireWater();
            }

            plantationList[save.index].loadPlantState(save.plantState);
        }
    }

    #endregion SaveLoad
}