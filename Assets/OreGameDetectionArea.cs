using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OreGameDetectionArea : MonoBehaviour 
{
	public bool isActive;
	public AudioSource oreAudioS;
	public AudioClip pointPlusSnd;
	public Image areaImg;

	void OnTriggerStay2D(Collider2D other)
	{
		if (!isActive) 
		{
			return;
		}
		if(Input.GetKeyDown(CustomInputManager.instance.actionKey))
			{
			GiveAPoint ();
			}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		CustomInputManager.instance.ShowHideActionButtonVisual (true);
	}
	void OnTriggerExit2D(Collider2D other)
	{
		CustomInputManager.instance.ShowHideActionButtonVisual (false);

	}

	void GiveAPoint()
	{
		isActive = false;
		oreAudioS.PlayOneShot (pointPlusSnd);
		areaImg.CrossFadeAlpha (0, 1f, true);
		CustomInputManager.instance.ShowHideActionButtonVisual (false);
		InGameManager.instance.OreGame.currentScore++;
		InGameManager.instance.OreGame.playerScoreTxt.text = InGameManager.instance.OreGame.currentScore.ToString ();
	}
}
