using System.Collections;
using System.Collections.Generic;
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

	[Header("A compléter lors du spawn")]
	public PlantObject myPlant;

	[Header("gestion du bouton")]
	Button thisButton;

	void Start()
	{
		InitializeTheItem ();
	}

	public void InitializeTheItem()
	{
		//propriétés générales.
		plantName.text = myPlant.plantName;
		plantIcon.sprite = myPlant.plantIcon;
		thisButton = GetComponent<Button> ();
		thisButton.onClick.AddListener (PlantThis );

		//gestion des biomes.
		if (myPlant.biome1 == BiomeEnum.plain ||myPlant.biome2 == BiomeEnum.plain ||myPlant.biome3 == BiomeEnum.plain) 
		{
			isPlainIcon.SetActive (true);
			PlantCollection.instance.plainUIObjects.Add (gameObject);
		}
		if (myPlant.biome1 == BiomeEnum.crater ||myPlant.biome2 == BiomeEnum.crater ||myPlant.biome3 == BiomeEnum.crater) 
		{
			isCraterIcon.SetActive (true);
			PlantCollection.instance.craterUIObjects.Add (gameObject);

		}
		if (myPlant.biome1 == BiomeEnum.cave ||myPlant.biome2 == BiomeEnum.cave ||myPlant.biome3 == BiomeEnum.cave) 
		{
			isCaveIcon.SetActive (true);
			PlantCollection.instance.caveUIObjects.Add (gameObject);

		}
	}

	//utiliser pour planter une graine de ce type particulier. En cliquant sur l'objet, ca plante si on est devant un plantation spot.
	public void PlantThis()
	{
		//je m'assure ici qu'on est bien en train de planter
		if (PlantationManager.instance.plantationSpot != null) 
		{
			if (seeds > 0) {
				PlantationManager.instance.plantationSpot.PlantSeedHere (myPlant);
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
		seedCount.text = seeds.ToString ();
	}
}
