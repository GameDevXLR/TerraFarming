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
	public int airFlowerSeeds;

	public PlantObject airBush;
	public int airBushSeeds;

	public PlantObject airTree;
	public int airTreeSeeds;

	[Header("Grottes: lumière")]
	public PlantObject caveFlower;
	public int caveFlowerSeeds;

	public PlantObject caveBush;
	public int caveBushSeeds;

	public PlantObject caveTree;
	public int caveTreeSeeds;



	[Header("Cratères: eau")]
	public PlantObject craterFlower;
	public int craterFlowerSeeds;

	public PlantObject craterBush;
	public int craterBushSeeds;

	public PlantObject craterTree;
	public int craterTreeSeeds;

	//NOTE: faut continuer a complèter en dessous la...Mais bon pour test pour le moment ca ira xD
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
