using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantationManager : MonoBehaviour {

	//rajouter ici un gameobject pour chaque biome correspondant au canvas de plantage de graine.

	public static PlantationManager instance;
    
	public List<PlantationSpot> plantationList;
    
	public GameObject plantSeedPlainCanvas;
	public GameObject plantSeedCraterCanvas;
	public GameObject plantSeedCaveCanvas;
	public Image plainSelectedSeedImg;
	public Image craterSelectedSeedImg;
	public Image caveSelectedSeedImg;

	public bool isSeedMenuOpen;
	public int currentPlantTypeIndex;
	//le plantation spot avec lequel on interagit actuellement
	public PlantationSpotEnhanced plantationSpot;

	//icone pour quitter le menu de selec de graine:
	public Sprite leaveMenuIcon;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
	void Update()
	{
		if (isSeedMenuOpen && plantationSpot) 
		{
			if (Input.GetKeyDown (CustomInputManager.instance.actionKey)) 
			{
				switch (plantationSpot.spotBiome) 
				{
				case BiomeEnum.plain:

					switch (currentPlantTypeIndex) 
					{
					case 1:
						if (PlantCollection.instance.airBushSeeds <= 0) {
							return;
						}
						break;
					case 0:
						if (PlantCollection.instance.airFlowerSeeds <= 0) {
							return;
						}
						break;
					case 2:
						if (PlantCollection.instance.airTreeSeeds <= 0) {
							return;
						}
						break;
					case 3:
						HidePlantTypeMenu ();
						return;
					default:
						break;
					}
					break;
				case BiomeEnum.crater:

					switch (currentPlantTypeIndex) 
					{
					case 1:
						if (PlantCollection.instance.craterBushSeeds <= 0) {
							return;
						}
						break;
					case 0:
						if (PlantCollection.instance.craterFlowerSeeds <= 0) {
							return;
						}
						break;
					case 2:
						if (PlantCollection.instance.craterTreeSeeds <= 0) {
							return;
						}
						break;
					case 3:
						HidePlantTypeMenu ();
						return;
					default:
						break;
					}
					break;
				case BiomeEnum.cave:

					switch (currentPlantTypeIndex) 
					{
					case 1:
						if (PlantCollection.instance.caveBushSeeds <= 0) {
							return;
						}
						break;
					case 0:
						if (PlantCollection.instance.caveFlowerSeeds <= 0) {
							return;
						}
						break;
					case 2:
						if (PlantCollection.instance.caveTreeSeeds <= 0) {
							return;
						}
						break;
					case 3:
						HidePlantTypeMenu ();
						return;
					default:
						break;
					}
					break;

				default:
					Debug.Log ("wrong biome on this spot");
					break;
				}
				plantationSpot.SelectPlantType (currentPlantTypeIndex);
				InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime ("Plant", layer: -1, fixedTime: 2);
				plantationSpot.plantAudioS.PlayOneShot (plantationSpot.planterSnd);
				InGameManager.instance.cleanParticle.GetComponent<ParticleSystem> ().Play ();
				HidePlantTypeMenu ();

			}
				//faire défiler les graines:
				if (Input.GetKeyDown (CustomInputManager.instance.leftKey )|| Input.GetKeyDown (KeyCode.LeftArrow)) 
				{

					if (currentPlantTypeIndex==0) 
					{
						currentPlantTypeIndex = 4;
					}
					ChangePlantTypeIndex (true);
				}
				if (Input.GetKeyDown (CustomInputManager.instance.rightKey)|| Input.GetKeyDown (KeyCode.RightArrow)) 
				{
				if (currentPlantTypeIndex == 3) 
					{
						currentPlantTypeIndex = -1;

					}
					ChangePlantTypeIndex (false);
				}

				//annuler
				if (Input.GetKeyDown (KeyCode.Escape)) 
				{
					HidePlantTypeMenu();
				}
		}
	}
	#region SeedsMenus

	public void ChangePlantTypeIndex(bool scrollLeft)
	{
		if (scrollLeft) 
		{
			currentPlantTypeIndex--;
		} else 
		{
			currentPlantTypeIndex++;
		}
		ActualizePlantTypeUI ();
	}

	public void ActualizePlantTypeUI()
	{
		switch (plantationSpot.spotBiome) 
		{
		case BiomeEnum.plain:
			switch (currentPlantTypeIndex) 
			{
			case 0:
				plainSelectedSeedImg.sprite = PlantCollection.instance.airFlower.plantIcon;
				break;
			case 1:
				plainSelectedSeedImg.sprite = PlantCollection.instance.airBush.plantIcon;
				break;
			case 2:
				plainSelectedSeedImg.sprite = PlantCollection.instance.airTree.plantIcon;
				break;
			case 3:
				plainSelectedSeedImg.sprite = leaveMenuIcon;
				break;
			default:
				Debug.Log ("planage sur l'icone la!");
				break;
			}
			break;
		case BiomeEnum.crater:
			switch (currentPlantTypeIndex) 
			{
			case 0:
				craterSelectedSeedImg.sprite = PlantCollection.instance.craterFlower.plantIcon;
				break;
			case 1:
				craterSelectedSeedImg.sprite = PlantCollection.instance.craterBush.plantIcon;
				break;
			case 2:
				craterSelectedSeedImg.sprite = PlantCollection.instance.craterTree.plantIcon;
				break;
			case 3:
				craterSelectedSeedImg.sprite = leaveMenuIcon;
				break;
			default:
				Debug.Log ("planage sur l'icone la!");
				break;
			}
			break;
		case BiomeEnum.cave:
			switch (currentPlantTypeIndex) 
			{
			case 0:
				caveSelectedSeedImg.sprite = PlantCollection.instance.caveFlower.plantIcon;
				break;
			case 1:
				caveSelectedSeedImg.sprite = PlantCollection.instance.caveBush.plantIcon;
				break;
			case 2:
				caveSelectedSeedImg.sprite = PlantCollection.instance.caveTree.plantIcon;
				break;
			case 3:
				caveSelectedSeedImg.sprite = leaveMenuIcon;
				break;
			default:
				Debug.Log ("planage sur l'icone la!");
				break;
			}
			break;

		default:
			Debug.Log ("wrong biome on this spot");
			break;
		}

	}


	public void ShowPlantTypeMenu(PlantationSpotEnhanced spot)
	{
		plantationSpot = spot;
		if (!isSeedMenuOpen)
		{
			switch (spot.spotBiome) 
			{
			case BiomeEnum.plain:
				plantSeedPlainCanvas.SetActive (true);
				break;
			case BiomeEnum.crater:
				plantSeedCraterCanvas.SetActive (true);
				break;
			case BiomeEnum.cave:
				plantSeedCaveCanvas.SetActive (true);
				break;
			default:
				break;
			}			
			InGameManager.instance.isPlanting = true;
			InGameManager.instance.playerController.isActive = false;
			isSeedMenuOpen = true;
			ActualizePlantTypeUI ();
		}
	}

	public void HidePlantTypeMenu()
	{
		if (isSeedMenuOpen) 
		{
			plantationSpot = null;
			plantSeedPlainCanvas.SetActive (false);
			plantSeedCraterCanvas.SetActive (false);
			plantSeedCaveCanvas.SetActive (false);
			InGameManager.instance.isPlanting = false;
			InGameManager.instance.playerController.isActive = true;
			isSeedMenuOpen = false;
		}
	}
	#endregion


	#region SaveLoad

    public List<PlanteSave> savePlantation()
    {
        List<PlanteSave> planteSave = new List<PlanteSave>();
        for (int i = 0; i < plantationList.Count; i++)
        {
            PlanteSave save = new PlanteSave {
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
        foreach(PlanteSave save in planteSave)
        {
            
            
			if (save.plantType != PlantTypeEnum.none)
            {

                plantationList[save.index].SelectPlantType(save.plantType);
                plantationList[save.index].RecquireWater();
            }

            plantationList[save.index].loadPlantState(save.plantState);

        }
    }
	#endregion
}
