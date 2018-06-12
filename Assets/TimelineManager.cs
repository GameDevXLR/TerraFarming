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
	public Animator playAnimator;
	public Transform StartPosTr;
	public GameObject canvasParentObj;

	bool isPlayingClip;
	public float clipDuration;
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
		if (Input.GetKeyDown (KeyCode.Alpha1) && !isPlayingClip) 
		{
			PrepareForClip ();
			//			captainAnimator.SetBool ("iswalking", true);
			InGameManager.instance.playerController.transform.parent.GetComponent<Transform>().SetPositionAndRotation(StartPosTr.position,StartPosTr.rotation);
			InGameManager.instance.playerController.transform.SetPositionAndRotation (Vector3.zero, Quaternion.identity);
			director.Play (cutscene1);
		}
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

	void PrepareForClip()
	{
		isPlayingClip = true;
		clipDuration =(float) director.duration;
		InGameManager.instance.playerController.disableMovement();
		canvasParentObj.SetActive (false);

	}
	public void EndClip()
	{
		isPlayingClip = false;
		InGameManager.instance.playerController.enableMovement();
		canvasParentObj.SetActive (true);

	}
}