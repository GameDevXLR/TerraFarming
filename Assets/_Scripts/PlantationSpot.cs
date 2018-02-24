﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantationSpot : MonoBehaviour {

	public cakeslice.Outline outliner;
	public AudioSource audioS;
	public GameObject debrisObj;
	public GameObject lopinNoSeedObj;
	public GameObject lopinSeedObj;
	public GameObject bush1Obj;
	public GameObject bush2Obj;
	public GameObject bush3Obj;
	public GameObject flower1Obj;
	public GameObject flower2Obj;
	public GameObject flower3Obj;
	public GameObject tree1Obj;
	public GameObject tree2Obj;
	public GameObject tree3Obj;

	public PlantType plantType;
	public PlantState actualPlantState;
	GameObject babyVisual;
	GameObject teenageVisual;
	GameObject grownupVisual;

	float timeToGrow;
	float growthStartTime;
	bool isReadyToEvolve;

	public GameObject plantTypeCanvas;
	bool isPlantTypeMenuOpened;
	public int currentPlantTypeIndex;

	public Image plantTypeSelectorImg;
	public Sprite flowerSelectorIcon;
	public Sprite bushSelectorIcon;
	public Sprite treeSelectorIcon;

	public enum PlantType
	{
		none,
		flower,
		bush,
		tree
	}

	public enum PlantState
	{
		debris,
		lopin,
		seed,
		baby,
		teenage,
		grownup
	}

	void Start()
	{
		outliner.enabled = false;
		audioS = GetComponent<AudioSource> ();

	}

	void Update()
	{
		if (isPlantTypeMenuOpened) 
		{
			if (Input.GetKeyDown (CustomInputManager.instance.actionKey)) 
			{
				switch (currentPlantTypeIndex) 
				{
				case 0:
					if (ResourcesManager.instance.bushSeed <= 0) {
						return;
					}
					break;
				case 1:
					if (ResourcesManager.instance.flowerSeed <= 0) {
						return;
					}
					break;
				case 2:
					if (ResourcesManager.instance.treeSeed <= 0) {
						return;
					}
					break;
				default:
					break;
				}
				SelectPlantType (currentPlantTypeIndex);
				HidePlantTypeMenu();
			}
			//faire défiler les graines:
			if (Input.GetKeyDown (CustomInputManager.instance.leftKey)) 
			{
				if (currentPlantTypeIndex==0) 
				{
					currentPlantTypeIndex = 3;
				}
					ChangePlantTypeIndex (true);
			}
			if (Input.GetKeyDown (CustomInputManager.instance.rightKey)) 
			{if (currentPlantTypeIndex == 2) 
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
		switch (currentPlantTypeIndex) 
		{
		case 0:
			plantTypeSelectorImg.sprite = bushSelectorIcon;
			break;
		case 1:
			plantTypeSelectorImg.sprite = flowerSelectorIcon;
			break;
		case 2:
			plantTypeSelectorImg.sprite = treeSelectorIcon;
			break;
		default:
			Debug.Log ("planage sur l'icone la!");
			break;
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyDown (CustomInputManager.instance.actionKey) && other.tag == "Player" &&!isPlantTypeMenuOpened) 
		{
			ChangePlantState ();
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			ListenForAction ();
			
			
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
			StopListeningForAction ();
			
		}
		
	}
	
	void ListenForAction()
	{
		
		//faire les changements d'apparence
		CustomInputManager.instance.ShowHideActionButtonVisual (true);
		outliner.enabled = true;

	}
	void StopListeningForAction()
	{
		
		//arreter les effets visuels
		CustomInputManager.instance.ShowHideActionButtonVisual (false);
		outliner.enabled = false;

	}

	public void ShowPlantTypeMenu()
	{
		plantTypeCanvas.SetActive (true);
		isPlantTypeMenuOpened = true;
		InGameManager.instance.playerController.isActive = false;
	}

	public void HidePlantTypeMenu()
	{
		plantTypeCanvas.SetActive (false);
		isPlantTypeMenuOpened = false;
		InGameManager.instance.playerController.isActive = true;

	}

	//faire pousser/choisir la plante etc...
	public void ChangePlantState()
	{	
		switch (actualPlantState) {
		case PlantState.debris:
			actualPlantState = PlantState.lopin;
			ResourcesManager.instance.ChangeRawOre (Random.Range (1, 6));
			debrisObj.SetActive (false);
			lopinNoSeedObj.SetActive (true);
			InGameManager.instance.playerController.GetComponent<Animator> ().Play("Plant");
			Debug.Log ("clean terrain");
			break;
		case PlantState.lopin:
			Invoke("ShowPlantTypeMenu",0.1f);
			InGameManager.instance.playerController.GetComponent<Animator> ().Play("Plant");
			Debug.Log ("Graine plantée");
			break;
		case PlantState.seed:
			actualPlantState = PlantState.baby;
			lopinSeedObj.SetActive (false);
			babyVisual.SetActive (true);
			InGameManager.instance.playerController.GetComponent<Animator> ().Play("Plant");
			Debug.Log ("Baby plante");
			break;
		case PlantState.baby:
			actualPlantState = PlantState.teenage;
			babyVisual.SetActive (false);
			teenageVisual.SetActive (true);
			InGameManager.instance.playerController.GetComponent<Animator> ().Play("Plant");
			Debug.Log ("Teenage plante");
			break;
		case PlantState.teenage:
			actualPlantState = PlantState.grownup;
			teenageVisual.SetActive (false);
			grownupVisual.SetActive (true);
			InGameManager.instance.playerController.GetComponent<Animator> ().Play("Plant");
			Debug.Log ("Grownup plante");
			break;
		case PlantState.grownup:
			break;
		default:
			
			break;		}
	}

	// choisir la graine
	/// <summary>
	/// Selects the type of the plant.
	/// </summary>
	/// <param name="index">Index.</param>
	public void SelectPlantType(int index)
	{
		switch (index) 
		{
			//t'es un bush
		case 0:
			ResourcesManager.instance.ChangeBushSeed(-1);
			babyVisual = bush1Obj;
			teenageVisual = bush2Obj;
			grownupVisual = bush3Obj;
			plantType = PlantType.bush;
			break;

			//t'es une fleur
		case 1:
			ResourcesManager.instance.ChangeFlowerSeed(-1);

			babyVisual = flower1Obj;
			teenageVisual = flower2Obj;
			grownupVisual = flower3Obj;
			plantType = PlantType.flower;

			break;

			//t'es un arbre
		case 2:
			ResourcesManager.instance.ChangeTreeSeed(-1);

			babyVisual = tree1Obj;
			teenageVisual = tree2Obj;
			grownupVisual = tree3Obj;
			plantType = PlantType.tree;

			break;

		default:
			Debug.Log ("t'es une plante inconnu mec!");
			break;
		}
		actualPlantState = PlantState.seed;
		lopinNoSeedObj.SetActive (false);
		lopinSeedObj.SetActive (true);
	}

}
