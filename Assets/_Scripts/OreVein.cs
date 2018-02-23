using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVein : MonoBehaviour 
{
	public cakeslice.Outline outliner;
	public int gamesAvailable = 2;

	public OreVein nextVeinToActivate;

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
			if (Input.GetKeyDown (CustomInputManager.instance.actionKey)) {
				if (gamesAvailable > 0) {
					InGameManager.instance.OreGame.enabled = true;
					gamesAvailable--;
					if (gamesAvailable == 0) 
					{
						StopListeningForAction ();
						nextVeinToActivate.gamesAvailable += Random.Range(1,4);
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
