﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelineManager : MonoBehaviour 
{
	public static TimelineManager instance;
	public PlayableDirector director;
	public PlayableAsset cutscene1;
	public Animator captainAnimator;
	public Animator playAnimator;
	public Transform StartPosTr;
	public GameObject canvasParentObj;

	bool isPlayingClip;
	float clipDuration;

	PlayableAsset clipToPlay;
//	Vector3 pos;
//	Quaternion rot;
	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}
//	void Start()
//	{
//		pos = InGameManager.instance.playerController.GetComponent<Transform> ().position;
//		rot = InGameManager.instance.playerController.GetComponent<Transform> ().rotation;
//	}
	void Update()
	{
		//Permet de rejouer le dernier clip jouer ou le clip placé manuellement dans la hierarchy.
		if (Input.GetKeyDown (KeyCode.Alpha1) && !isPlayingClip) 
		{
			LaunchCinematic (cutscene1, StartPosTr);
		}

		//Vérifie si un clip est en cours, si oui, s'assure de le finir quand il faut.
		if(isPlayingClip)
		{
			if(clipDuration<0)
			{
				EndClip();
			}else
			{
				clipDuration -= Time.deltaTime;
			}
		}
	}
	/// <summary>
	/// Launchs the cinematic.
	/// </summary>
	/// <param name="clip">Clip to play.</param>
	/// <param name="playerStartPos">Player start position and rotation based on a transform.</param>
	public void LaunchCinematic( PlayableAsset clip, Transform playerStartPos)
	{
		clipToPlay = clip;
		StartPosTr = playerStartPos;
		director.playableAsset = clipToPlay;
		PrepareAndStartClip ();

	}

	void PrepareAndStartClip()
	{
		isPlayingClip = true;
		clipDuration =(float) director.duration;
		InGameManager.instance.playerController.disableMovement();
		InGameManager.instance.playerController.GetComponent<BehaviourController> ().enabled = false;

		canvasParentObj.SetActive (false);
		InGameManager.instance.playerController.transform.parent.GetComponent<Transform>().SetPositionAndRotation(StartPosTr.position,StartPosTr.rotation);
		InGameManager.instance.playerController.transform.SetPositionAndRotation (Vector3.zero, Quaternion.identity);
		director.Play (clipToPlay);

	}
	public void EndClip()
	{
		isPlayingClip = false;
		InGameManager.instance.playerController.enableMovement();
		InGameManager.instance.playerController.GetComponent<BehaviourController> ().enabled = true;
		canvasParentObj.SetActive (true);

	}
}