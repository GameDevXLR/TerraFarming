using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItemUI : MonoBehaviour 
{

	[Header("infos a afficher")]
	public Text plantName;
	public Text seedCount;
	public Image plantIcon;
	public GameObject isPlainIcon;
	public GameObject isCraterIcon;
	public GameObject isCaveIcon;

	[Header("A compléter lors du spawn")]
	public PlantObject myPlant;

	void Start()
	{
		InitializeTheItem ();
	}

	public void InitializeTheItem()
	{
		plantName.text = myPlant.plantName;
		plantIcon.sprite = myPlant.plantIcon;

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

}
