using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCollection : MonoBehaviour 
{

	//Contient la liste de toutes les plantes du jeu ainsi que le nombre de graines associés en possession.
	//Note:Faut faire un système de sauvegarde de cette collection!
	public static PlantCollection instance;

	[Header("Plaines: air")]
	public PlantObject airFlower;
	[HideInInspector]public int airFlowerSeeds;

	public PlantObject airBush;
	[HideInInspector]public int airBushSeeds;

	public PlantObject airTree;
	[HideInInspector]public int airTreeSeeds;

	[Header("Grottes: lumière")]
	public PlantObject caveFlower;
	[HideInInspector]public int caveFlowerSeeds;

	public PlantObject caveBush;
	[HideInInspector]public int caveBushSeeds;

	public PlantObject caveTree;
	[HideInInspector]public int caveTreeSeeds;


	//NOTE: faut continuer a complèter en dessous la...Mais bon pour test pour le moment ca ira xD

	[Header("Cratères: eau")]
	public PlantObject craterFlower;
	public PlantObject craterBush;
	public PlantObject craterTree;
	[Header("air+lumière")]
	public PlantObject airCaveFlower;
	public PlantObject airCaveBush;
	public PlantObject airCaveTree;
	[Header("air+eau")]
	public PlantObject airCraterFlower;
	public PlantObject airCraterBush;
	public PlantObject airCraterTree;
	[Header("lumière+eau")]
	public PlantObject caveCraterFlower;
	public PlantObject caveCraterBush;
	public PlantObject caveCraterTree;
	[Header("air+lumière+eau")]
	public PlantObject airCaveCraterFlower;
	public PlantObject airCaveCraterBush;
	public PlantObject airCaveCraterTree;


	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		} else 
		{
			Destroy (this.gameObject);
			Debug.Log ("oups!Ya 2 plantcollection la");
		}
	}

}
