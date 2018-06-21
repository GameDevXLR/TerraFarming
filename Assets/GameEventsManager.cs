using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameEventsManager : MonoBehaviour {

	//Ce script doit servir a lancé des evenements (exemple la cinématique d'intro)
	public static GameEventsManager instance;

	[Header("La cinématique d'intro:")]
	public PlayableAsset introPA;
	public Transform introStartPosTr;


	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	public void StartIntroCinematic()
	{
		TimelineManager.instance.LaunchCinematic (introPA, introStartPosTr);
	}

}
