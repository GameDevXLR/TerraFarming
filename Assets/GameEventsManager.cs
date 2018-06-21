using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameEventsManager : MonoBehaviour {

	//Ce script doit servir a lancé des evenements (exemple la cinématique d'intro)
	public static GameEventsManager instance;

	public bool playIntroCinematic = true;
	public bool playIntroMiningCinematic = true;
	public bool playIntroSeedCinematic = true;
	public bool playIntroSeedPlantedCinematic = true;
	public bool playIntroNewDay = true;

	[Header("La cinématique d'intro arrivé de pipon:")]
	public PlayableAsset introPA;
	public Transform introStartPosTr;
	[Header("cinematique tuto fin de premier minage:")]
	public PlayableAsset miningPA;
	public Transform introMiningStartPosTr;
	//temporaire :je met ici provisoirement : 
	public GameObject machineToActivate;
	[Header("cinematique tuto graine creer:")]
	public PlayableAsset seedPA;
	public Transform introSeedStartPosTr;
	public AlchimieGame gameThatActivateTheIntro;
	public PlantationSpotEnhanced plantationSpotToActivate;
	[Header("cinematique tuto graine planter:")]
	public PlayableAsset plantedPA;
	public Transform introSeedPlantedStartPosTr;
	[Header("cinematique tuto nouvelle machine jour2:")]
	public PlayableAsset newDayPA;
	public Transform introNewDayStartPosTr;
	public GameObject machineToActivate2;

	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}
	void Start()
	{
		if (playIntroSeedCinematic) {
			
			gameThatActivateTheIntro.willPlayIntroVideo = true;
		}
		if(playIntroSeedPlantedCinematic){
			
			plantationSpotToActivate.willShowSeedPlantedCinematic = true;
		}
	}

	//la premiere vidéo quand on se co sans avoir load le jeu : 
	public void StartIntroCinematic()
	{
		if (playIntroCinematic) {
			TimelineManager.instance.LaunchCinematic (introPA, introStartPosTr);
		}
	}
	//arrive aprés avoir fait un premier jeu de minage:
	public void StartIntroCinematicMining()
	{
		if (playIntroMiningCinematic) {
			TimelineManager.instance.LaunchCinematic (miningPA, introMiningStartPosTr);
			machineToActivate.SetActive (true);
			//si on a pas gagné de minerai, on nous en donne 1 XD
			if (ResourcesManager.instance.rawOre < 1) {
				ResourcesManager.instance.ChangeRawOre (1);
			}
		}
	}
	//aprés avoir créer une premiere graine. Ca débloque le plantationSpot
	public void StartIntroCinematicSeedCreated()
	{
		if (playIntroSeedCinematic) {
			TimelineManager.instance.LaunchCinematic (seedPA, introSeedStartPosTr);
			plantationSpotToActivate.canBeUsed = true;
		}
	}

	//Aprés avoir planté la premiere graine. nous dit d'aller a la base dodo et revenir demain.
	public void StartIntroCinematicSeedPlanted()
	{
		if (playIntroSeedPlantedCinematic) {
			TimelineManager.instance.LaunchCinematic (plantedPA, introSeedPlantedStartPosTr);
		}
	}

	//aprés avoir dodo dans la base, une nouvelle machine est dispo.
	public void StartIntroCineNewDay()
	{
		if (playIntroNewDay) {
			//activer la deuxieme machine a graine (cratere)
			machineToActivate2.SetActive (true);
			TimelineManager.instance.LaunchCinematic (newDayPA, introNewDayStartPosTr);
		}
	}

	//lancé quand la premiere plante devient adulte.
	public void StartCineFirstFlowerHasGrown()
	{
		
	}
		
}
