using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantCollection : MonoBehaviour 
{

	//Contient la liste de toutes les plantes du jeu ainsi que le nombre de graines associés en possession.
	//Note:Faut faire un système de sauvegarde de cette collection!
	public static PlantCollection instance;

	[Header("A COMPLETER")]
	public PlantObject[] allPlantObjects;
	public GameObject genericSeed;
	public GameObject plantObjectUI;
	public RectTransform collectionContentUI;

	[Header("Gérer automatiquement: liste des UI par biome.")]
	//permet de gérer l'affichage des UI dans la collection:
	public List<GameObject> plainUIObjects = new List<GameObject>();
	public List<GameObject> craterUIObjects= new List<GameObject>();
	public List<GameObject> caveUIObjects= new List<GameObject>();

	bool plainUIVisible = true;
	bool craterUIVisible = true;
	bool caveUIVisible = true;

	[Header("Plaines: air")]
	public PlantObject airFlower;
	public int airFlowerSeeds;
	public Text airFlowerDisplay;

	public PlantObject airBush;
	public int airBushSeeds;
	public Text airBushDisplay;

	public PlantObject airTree;
	public int airTreeSeeds;
	public Text airTreeDisplay;


	[Header("Grottes: lumière")]
	public PlantObject caveFlower;
	public int caveFlowerSeeds;
	public Text caveFlowerDisplay;

	public PlantObject caveBush;
	public int caveBushSeeds;
	public Text caveBushDisplay;

	public PlantObject caveTree;
	public int caveTreeSeeds;
	public Text caveTreeDisplay;


	[Header("Cratères: eau")]
	public PlantObject craterFlower;
	public int craterFlowerSeeds;
	public Text craterFlowerDisplay;

	public PlantObject craterBush;
	public int craterBushSeeds;
	public Text craterBushDisplay;

	public PlantObject craterTree;
	public int craterTreeSeeds;
	public Text craterTreeDisplay;

	//Ici les hybrids:
	[Header("air+lumière")]
	public PlantObject airCaveFlower;
	public int airCaveFlowerSeeds;
	public Text airCaveFlowerDisplay;

	public PlantObject airCaveBush;
	public int airCaveBushSeeds;
	public Text airCaveBushDisplay;

	public PlantObject airCaveTree;
	public int airCaveTreeSeeds;
	public Text airCaveTreeDisplay;

	[Header("air+eau")]
	public PlantObject airCraterFlower;
	public int airCraterFlowerSeeds;
	public Text airCraterFlowerDisplay;

	public PlantObject airCraterBush;
	public int airCraterBushSeeds;
	public Text airCraterBushDisplay;

	public PlantObject airCraterTree;
	public int airCraterTreeSeeds;
	public Text airCraterTreeDisplay;

	[Header("lumière+eau")]
	public PlantObject caveCraterFlower;
	public int caveCraterFlowerSeeds;
	public Text caveCraterFlowerDisplay;

	public PlantObject caveCraterBush;
	public int caveCraterBushSeeds;
	public Text caveCraterBushDisplay;

	public PlantObject caveCraterTree;
	public int caveCraterTreeSeeds;
	public Text caveCraterTreeDisplay;

	//ici les spécials triple type (par exemple)
	[Header("air+lumière+eau")]
	public PlantObject airCaveCraterFlower;
	public int airCaveCraterFlowerSeeds;
	public Text airCaveCraterFlowerDisplay;

	public PlantObject airCaveCraterBush;
	public int airCaveCraterBushSeeds;
	public Text airCaveCraterBushDisplay;

	public PlantObject airCaveCraterTree;
	public int airCaveCraterTreeSeeds;
	public Text airCaveCraterTreeDisplay;


	#region monoBehaviour

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

//	void Start()
//	{
//		PopulateCollection ();
//	}
	#endregion

	#region Utilitaire de gestion de UI de la collection
//	public void PopulateCollection()
//	{
//		foreach (var PO in allPlantObjects) 
//		{
//			GameObject go = Instantiate (plantObjectUI);
//			go.transform.SetParent(collectionContentUI);
//			go.transform.localScale = Vector3.one;
//			go.GetComponent<PlantItemUI> ().myPlant = PO;
//		}
//	}
	public void ShowHidePlainUI()
	{
		plainUIVisible = !plainUIVisible;
		foreach (var go in plainUIObjects) 
		{
			go.SetActive (plainUIVisible);
		}
	}

	public void ShowHideCraterUI()
	{
		craterUIVisible = !craterUIVisible;
		foreach (var go in craterUIObjects) 
		{
			go.SetActive (craterUIVisible);
		}
	}
	public void ShowHideCaveUI()
	{
		caveUIVisible = !caveUIVisible;
		foreach (var go in caveUIObjects) 
		{
			go.SetActive (caveUIVisible);
		}
	}


	public void ShowAllUI()
	{
		plainUIVisible= false;
		craterUIVisible= false;
		caveUIVisible= false;
		ShowHidePlainUI ();
		ShowHideCraterUI ();
		ShowHideCaveUI ();
	}

	#endregion

	#region actualisation de chaque quantité de plante...
	#region AIR

	public void airFlowerAdd(int i)
	{
		airFlowerSeeds += i;
		airFlowerDisplay.text = airFlowerSeeds.ToString ();
	}
	public void airBushAdd(int i)
	{
		airBushSeeds += i;
		airBushDisplay.text = airBushSeeds.ToString ();
	}
	public void airTreeAdd(int i)
	{
		airTreeSeeds += i;
		airTreeDisplay.text = airTreeSeeds.ToString ();
	}
	#endregion

	#region EAU
	public void craterFlowerAdd(int i)
	{
		craterFlowerSeeds += i;
		craterFlowerDisplay.text = craterFlowerSeeds.ToString ();
	}
	public void craterBushAdd(int i)
	{
		craterBushSeeds += i;
		craterBushDisplay.text = craterBushSeeds.ToString ();
	}
	public void craterTreeAdd(int i)
	{
		craterTreeSeeds += i;
		craterTreeDisplay.text = craterTreeSeeds.ToString ();
	}
	#endregion

	#region LUMIERE
	public void caveFlowerAdd(int i)
	{
		caveFlowerSeeds += i;
		caveFlowerDisplay.text = caveFlowerSeeds.ToString ();
	}
	public void caveBushAdd(int i)
	{
		caveBushSeeds += i;
		caveBushDisplay.text = caveBushSeeds.ToString ();
	}
	public void caveTreeAdd(int i)
	{
		caveTreeSeeds += i;
		caveTreeDisplay.text = caveTreeSeeds.ToString ();
	}
	#endregion

	#region AIR+LUMIERE
	public void airCaveFlowerAdd(int i)
	{
		airCaveFlowerSeeds += i;
		airCaveFlowerDisplay.text = airCaveFlowerSeeds.ToString ();
	}
	public void airCaveBushAdd(int i)
	{
		airCaveBushSeeds += i;
		airCaveBushDisplay.text = airCaveBushSeeds.ToString ();
	}
	public void airCaveTreeAdd(int i)
	{
		airCaveTreeSeeds += i;
		airCaveTreeDisplay.text = airCaveTreeSeeds.ToString ();
	}
	#endregion
	#region AIR+EAU
	public void airCraterFlowerAdd(int i)
	{
		airCraterFlowerSeeds += i;
		airCraterFlowerDisplay.text = airCraterFlowerSeeds.ToString ();
	}
	public void airCraterBushAdd(int i)
	{
		airCraterBushSeeds += i;
		airCraterBushDisplay.text = airCraterBushSeeds.ToString ();
	}
	public void airCraterTreeAdd(int i)
	{
		airCraterTreeSeeds += i;
		airCraterTreeDisplay.text = airCraterTreeSeeds.ToString ();
	}
	#endregion
	#region LUMIERE+EAU
	public void caveCraterFlowerAdd(int i)
	{
		caveCraterFlowerSeeds += i;
		caveCraterFlowerDisplay.text = caveCraterFlowerSeeds.ToString ();
	}
	public void caveCraterBushAdd(int i)
	{
		caveCraterBushSeeds += i;
		caveCraterBushDisplay.text = caveCraterBushSeeds.ToString ();
	}
	public void caveCraterTreeAdd(int i)
	{
		caveCraterTreeSeeds += i;
		caveCraterTreeDisplay.text = caveCraterTreeSeeds.ToString ();
	}
	#endregion
	#region AIR+LUMIERE+EAU
	public void airCaveCraterFlowerAdd(int i)
	{
		airCaveCraterFlowerSeeds += i;
		airCaveCraterFlowerDisplay.text = airCaveCraterFlowerSeeds.ToString ();
	}
	public void airCaveCraterBushAdd(int i)
	{
		airCaveCraterBushSeeds += i;
		airCaveCraterBushDisplay.text = airCaveCraterBushSeeds.ToString ();
	}
	public void airCaveCraterTreeAdd(int i)
	{
		airCaveCraterTreeSeeds += i;
		airCaveCraterTreeDisplay.text = airCaveCraterTreeSeeds.ToString ();
	}
	#endregion
	#endregion
}
