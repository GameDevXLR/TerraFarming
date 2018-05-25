using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class OreVein : MonoBehaviour 
{
	public cakeslice.Outline outliner;
	public int gamesAvailable = 2;
	public PlayableAsset clip;
	public OreVein nextVeinToActivate;
	public GameObject isActiveParticules;
	void Start()
	{
		outliner.enabled = false;

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

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player" && !InGameManager.instance.OreGame.enabled)
		{
			if (Input.GetKeyDown (CustomInputManager.instance.actionKey) && InGameManager.instance.playerController.canDoAction) {
				if (gamesAvailable > 0) {
					InGameManager.instance.OreGame.enabled = true;
					InGameManager.instance.OreGame.currentVein = this;
					TimelineManager.instance.director.Play (clip);
					InGameManager.instance.playerController.transform.LookAt (new Vector3(this.transform.position.x,0f,this.transform.position.z));
					//active le laser pour voir:
					InGameManager.instance.playerController.transform.GetChild (0).gameObject.SetActive (true);
					gamesAvailable--;
					if (gamesAvailable == 0) 
					{
						isActiveParticules.SetActive (false);
						StopListeningForAction ();
						nextVeinToActivate.gamesAvailable += Random.Range(1,4);
						nextVeinToActivate.isActiveParticules.SetActive (true);

//						nextVeinToActivate.enabled = true;
						this.enabled = false;
					}
				}
			}
		}
	}

	void ListenForAction()
	{
		if (gamesAvailable > 0) {
			//faire les changements d'apparence de la caillasse;
			CustomInputManager.instance.ShowHideActionButtonVisual (true);
			outliner.enabled = true;
		}
	}
	void StopListeningForAction()
	{
		
			//arreter les effets visuels
			CustomInputManager.instance.ShowHideActionButtonVisual (false);
			outliner.enabled = false;


	}
}
