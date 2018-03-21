using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantationSpotEnhanced : MonoBehaviour {

	//refonte du plantation spot pour qu'il se réfère plus aux scriptable object et pour différencier "plantation spot" et "plantes"...
	//C'était pas bien clair tout ca :/
	//voir aussi pour un nouveau systeme de menu des graines hein.

	//sert a la sauvegarde! Doit etre configurer et être different de tout autre ID pour pas que ca plane xD nul...
	public int persistentID;

	//Le biome ou je me trouve:
	public BiomeEnum spotBiome;

	//la plante qui est planté ici. Peut etre vide hein.
	public PlantObject plantSO;

	//tout ce qui est son:
	public AudioClip planterSnd;
	public AudioClip growUpSnd;
	public AudioSource plantAudioS;

	//l'outliner
	public cakeslice.Outline outliner;

	//les visuels en enfant de l'objet.
	public GameObject debrisObj;
	public GameObject lopinNoSeedObj;
	public GameObject lopinSeedObj;

	//le type de plante
	public PlantTypeEnum plantType;

	//l'état actuel du spot (planter / vide ? )
	public PlantStateEnum actualPlantState;

	//les visuels de la plante a ses différentes étapes : hérité du SO.
	GameObject babyVisual;
	GameObject teenageVisual;
	GameObject grownupVisual;

	//Le canvas du menu de plantation de graine : changer ca, faire des menu pour les 3 possibilités.
	//référencer ca en fonction du biome du coup!
	//ne pas foutre un canvas par spot, c'est psycho.
	public GameObject plantTypeCanvas;
	//le menu est-il ouvert?
	bool isPlantTypeMenuOpened;
	//on est sur quelle icone de graine dans le menu la ? 
	public int currentPlantTypeIndex = 1;

	//gestion des visuels du menu.
	//voir pour modifié ca, pour que le sprite soit pris sur le SO.
	public Image plantTypeSelectorImg;

	//icone pour quitter le menu de selec de graine:
	public Sprite leaveMenuIcon;

	//effet de particule en enfant qui s'active quand jneed de l'eau mec.
	public GameObject needWaterParticules;

	//gère ma croissance...Gère surtout l'arrosage pour le moment hein..Ne nous voilons pas la face XD
	public PlantGrowthCycleManager plantGrowth;
	public WaterIconManager waterIcon;

	//est ce que je donne des essences si on fait espace sur moi ? 
	public bool giveEssence;
	//combien d'essences je donne par récolte??
	public int nbrOfGivenEssence;
	//je fais quoi comme son qd on me récolte?
	public AudioClip recolteSnd;

	//gestion des timers de pousse.
	public float timeToGrow;
	float growthStartTime;
	float timeSpentGrowing;
	bool isGrowing;

	public Animator growthAnimator;





	void Start()
	{
		outliner.enabled = false;

		//		//juste pour voir si les SO marchent bien... : oui.
		//		if (plantSO) {
		//			GameObject GO = Instantiate (plantSO.babyModel);
		//			GO.transform.localScale = new Vector3 (plantSO.scale, plantSO.scale, plantSO.scale);
		//			GO.transform.parent = transform;
		//			GO.transform.localPosition = Vector3.zero;
		//		}
	}

	void Update()
	{
		if (isGrowing) 
		{
			if (Time.time > growthStartTime + timeToGrow) 
			{
				//faire evoluer la plante
				ChangePlantState();
				growthStartTime = Time.time;
			}
		}

		if (isPlantTypeMenuOpened) 
		{
			if (Input.GetKeyDown (CustomInputManager.instance.actionKey)) 
			{
				switch (spotBiome) 
				{
				case BiomeEnum.plain:

					switch (currentPlantTypeIndex) {
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

				default:
					Debug.Log ("wrong biome on this spot");
					break;
				}
				SelectPlantType (currentPlantTypeIndex);
				InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime("Plant", layer:-1, fixedTime:2);
				plantAudioS.PlayOneShot (planterSnd);
				InGameManager.instance.cleanParticle.GetComponent<ParticleSystem> ().Play ();
				HidePlantTypeMenu();
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
			{if (currentPlantTypeIndex == 3) 
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
		switch (spotBiome) 
		{
		case BiomeEnum.plain:
			switch (currentPlantTypeIndex) 
			{
			case 0:
				plantTypeSelectorImg.sprite = PlantCollection.instance.airFlower.plantIcon;
				break;
			case 1:
				plantTypeSelectorImg.sprite = PlantCollection.instance.airBush.plantIcon;
				break;
			case 2:
				plantTypeSelectorImg.sprite = PlantCollection.instance.airTree.plantIcon;
				break;
			case 3:
				plantTypeSelectorImg.sprite = leaveMenuIcon;
				break;
//			case 4:
//				plantTypeSelectorImg.sprite = PlantCollection.instance.airTree.plantIcon;
//				break;
//			case 5:
//				plantTypeSelectorImg.sprite = PlantCollection.instance.airTree.plantIcon;
//				break;
//			case 6:
//				plantTypeSelectorImg.sprite = PlantCollection.instance.airTree.plantIcon;
//				break;
//			case 7:
//				plantTypeSelectorImg.sprite = PlantCollection.instance.airTree.plantIcon;
//				break;
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

	public void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyDown (CustomInputManager.instance.actionKey) && other.tag == "Player" &&!isPlantTypeMenuOpened) 
		{
			//si t'es pas encore une plante, fait ton taff normalement...
			if (plantType == PlantTypeEnum.none) 
			{
				ChangePlantState ();
			} 
			else 
			{
				if (giveEssence) 
				{
					InGameManager.instance.playerController.GetComponent<Animator>().PlayInFixedTime("Cleaning", layer: -1, fixedTime: 1);
					giveEssence = false;
					plantAudioS.PlayOneShot(growUpSnd);
					InGameManager.instance.cleanParticle.GetComponent<ParticleSystem> ().Play ();

					ResourcesManager.instance.ChangeEssence (nbrOfGivenEssence);
					return;
				}
				if (!isGrowing) 
				{
					WaterThePlant ();
				}
			}

		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !isGrowing ||other.tag == "Player" && giveEssence ) 
		{
			ListenForAction ();


		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
			StopListeningForAction ();
			InGameManager.instance.cleanParticle.GetComponent<ParticleSystem> ().Stop ();
			InGameManager.instance.waterParticle.GetComponent<ParticleSystem> ().Stop ();

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
		if (ResourcesManager.instance.haveSeed())
		{
			plantTypeCanvas.SetActive(true);
			isPlantTypeMenuOpened = true;
			InGameManager.instance.isPlanting = true;
			InGameManager.instance.playerController.isActive = false;
		}
	}

	public void HidePlantTypeMenu()
	{
		plantTypeCanvas.SetActive (false);
		isPlantTypeMenuOpened = false;
		InGameManager.instance.isPlanting = false;

		InGameManager.instance.playerController.isActive = true;

	}

	//faire pousser/choisir la plante etc...
	public void ChangePlantState()
	{
		switch (actualPlantState)
		{
		case PlantStateEnum.debris:
			actualPlantState = PlantStateEnum.lopin;
			ResourcesManager.instance.ChangeRawOre(Random.Range(1, 6));
			debrisObj.SetActive(false);
			//			lopinNoSeedObj.SetActive (true);
			InGameManager.instance.playerController.GetComponent<Animator>().PlayInFixedTime("Cleaning", layer: -1, fixedTime: 1);
			plantAudioS.PlayOneShot(planterSnd);
			InGameManager.instance.cleanParticle.GetComponent<ParticleSystem>().Play();
			break;
		case PlantStateEnum.lopin:
			Invoke("ShowPlantTypeMenu", 0.1f);
			//			l'animation est à la sortie de menu graine line94
			break;
		case PlantStateEnum.seed:
			actualPlantState = PlantStateEnum.baby;
			//			lopinSeedObj.SetActive (false);
			babyVisual.SetActive(true);
			growthAnimator.SetBool("baby", true);

			//			InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime("Plant", layer:-1, fixedTime:2);
			//			plantAudioS.PlayOneShot (growUpSnd);
			break;
		case PlantStateEnum.baby:
			actualPlantState = PlantStateEnum.teenage;
			babyVisual.SetActive(false);
			teenageVisual.SetActive(true);
			growthAnimator.SetBool("teenage", true);

			//			InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime("Plant", layer:-1, fixedTime:2);
			//			plantAudioS.PlayOneShot (growUpSnd);
			break;
		case PlantStateEnum.teenage:
			actualPlantState = PlantStateEnum.grownup;
			teenageVisual.SetActive(false);
			grownupVisual.SetActive(true);
			growthAnimator.SetBool("grownup", true);
			growthAnimator.SetFloat ("growthspeed", 100f);

			//			InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime("Plant", layer:-1, fixedTime:2);
			//			plantAudioS.PlayOneShot (growUpSnd);
			break;
		case PlantStateEnum.grownup:
			if (plantType != PlantTypeEnum.flower) {
				giveEssence = true;
			}
			break;
		default:

			break;
		}
	}

	//faire pousser/choisir la plante etc...
	public void loadPlantState(PlantStateEnum state)
	{
		actualPlantState = state;
		switch (actualPlantState)
		{
		case PlantStateEnum.debris:
			break;
		case PlantStateEnum.lopin:
			debrisObj.SetActive(false);
			break;
		case PlantStateEnum.seed:
			debrisObj.SetActive(false);
			babyVisual.SetActive(false);
			teenageVisual.SetActive(false);
			grownupVisual.SetActive(false);
			growthAnimator.SetBool("teenage", false);
			growthAnimator.SetBool("baby", false);
			growthAnimator.SetBool("grownup", false);
			break;
		case PlantStateEnum.baby:
			debrisObj.SetActive(false);
			babyVisual.SetActive(true);
			teenageVisual.SetActive(false);
			grownupVisual.SetActive(false);
			growthAnimator.SetBool("teenage", false);
			growthAnimator.SetBool("baby", true);
			growthAnimator.SetBool("grownup", false);
			break;
		case PlantStateEnum.teenage:
			debrisObj.SetActive(false);
			babyVisual.SetActive(false);
			teenageVisual.SetActive(true);
			grownupVisual.SetActive(false);
			growthAnimator.SetBool("teenage", true);
			growthAnimator.SetBool("baby", false);
			growthAnimator.SetBool("grownup", false);
			break;
		case PlantStateEnum.grownup:
			debrisObj.SetActive(false);
			babyVisual.SetActive(false);
			teenageVisual.SetActive(false);
			grownupVisual.SetActive(true);
			growthAnimator.SetBool("baby", false);
			growthAnimator.SetBool("grownup", true);
			growthAnimator.SetBool("teenage", false);

			break;
		default:

			break;
		}
	}

	// choisir la graine
	/// <summary>
	/// Selects the type of the plant.
	/// </summary>
	/// <param name="index">Index.</param>
	public void SelectPlantType(int index)
	{
		switch (spotBiome) 
		{
		case BiomeEnum.plain:
			switch (index) 
			{
			//t'es une fleur
			case 0:
				ResourcesManager.instance.ChangeFlowerSeed (-1);
				plantSO = PlantCollection.instance.airFlower;

				timeToGrow = 60f;
				growthAnimator.SetFloat ("growthspeed", 16f);
				plantType = PlantTypeEnum.flower;
				
				break;

			//t'es un bush
			case 1:
				ResourcesManager.instance.ChangeBushSeed (-1);
				plantSO = PlantCollection.instance.airBush;

				timeToGrow = 120f;
				nbrOfGivenEssence = 1;
				growthAnimator.SetFloat ("growthspeed", 8.3f);
				plantType = PlantTypeEnum.bush;
				break;


			//t'es un arbre
			case 2:
				ResourcesManager.instance.ChangeTreeSeed (-1);
				plantSO = PlantCollection.instance.airTree;

				timeToGrow = 300f;
				nbrOfGivenEssence = 3;
				growthAnimator.SetFloat ("growthspeed", 3.3f);
				plantType = PlantTypeEnum.tree;

				break;

			default:
				Debug.Log ("t'es une plante inconnu mec!");
				break;
			}
			break;

		default:
			break;
		}

		actualPlantState = PlantStateEnum.seed;
		babyVisual = plantSO.babyModel;
		teenageVisual = plantSO.teenageModel;
		grownupVisual = plantSO.grownupModel;
		lopinSeedObj.SetActive(true);
		growthStartTime = Time.time;
		RecquireWater();



	}
	// choisir la graine
	/// <summary>
	/// Selects the type of the plant.
	/// </summary>
	/// <param name="index">Index.</param>
	public void SelectPlantType(PlantTypeEnum index)
	{
		switch (index)
		{
		//t'es un bush
		case PlantTypeEnum.bush:
			timeToGrow = 120f;
			growthAnimator.SetFloat("growthspeed", 8.3f);
//			babyVisual = bush1Obj;
//			teenageVisual = bush2Obj;
//			grownupVisual = bush3Obj;
			plantType = PlantTypeEnum.bush;
			break;

			//t'es une fleur
		case PlantTypeEnum.flower:
			timeToGrow = 60f;
			growthAnimator.SetFloat("growthspeed", 16f);

//			babyVisual = flower1Obj;
//			teenageVisual = flower2Obj;
//			grownupVisual = flower3Obj;
			plantType = PlantTypeEnum.flower;

			break;

			//t'es un arbre
		case PlantTypeEnum.tree:
			timeToGrow = 300f;
			growthAnimator.SetFloat("growthspeed", 3.3f);

//			babyVisual = tree1Obj;
//			teenageVisual = tree2Obj;
//			grownupVisual = tree3Obj;
			plantType = PlantTypeEnum.tree;

			break;

		default:
			Debug.Log("t'es une plante inconnu mec!");
			break;
		}

		actualPlantState = PlantStateEnum.seed;
		//		lopinNoSeedObj.SetActive (false);
		lopinSeedObj.SetActive(true);
		growthStartTime = Time.time;
		RecquireWater();
	}
	public void RecquireWater()
	{
		timeSpentGrowing = Time.time - growthStartTime;
		isGrowing = false;
		needWaterParticules.SetActive (true);
		waterIcon.activate(plantType, actualPlantState);
	}
	public void WaterThePlant()
	{
		growthStartTime = Time.time - timeSpentGrowing;
		isGrowing = true;
		needWaterParticules.SetActive (false);
		plantGrowth.StartCoroutine (plantGrowth.StartGrowing ());

		//jouer ici les sons et anim lié au fait d'arroser:
		InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime ("Water", layer: -1, fixedTime: 2);
		plantAudioS.PlayOneShot (growUpSnd);
		InGameManager.instance.waterParticle.GetComponent<ParticleSystem> ().Play ();
		waterIcon.gameObject.SetActive(false);


	}
}
