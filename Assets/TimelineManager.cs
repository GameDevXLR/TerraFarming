using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour 
{

	public static TimelineManager instance;

	public PlayableDirector director;
	public PlayableAsset cutscene1;

	public Animator captainAnimator;

	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) 
		{
//			captainAnimator.SetBool ("iswalking", true);
			director.Play (cutscene1);
		}
	}
}
