using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantationSpot : MonoBehaviour {

	public GameObject debrisObj;
	public GameObject lopinNoSeedObj;
	public GameObject lopinSeedObj;
	public GameObject bush1Obj;
	public GameObject bush2Obj;
	public GameObject bush3Obj;
	public PlantState actualPlantState;


	public enum PlantState
	{
		debris,
		lopin,
		seed,
		baby,
		teenage,
		grownup
	}

	public void ChangePlantState()
	{
		switch (actualPlantState) {
		case PlantState.debris:
			actualPlantState = PlantState.lopin;
			debrisObj.SetActive (false);
			lopinNoSeedObj.SetActive (true);
			break;
		case PlantState.lopin:
			actualPlantState = PlantState.seed;
			lopinNoSeedObj.SetActive (false);
			lopinSeedObj.SetActive (true);
			break;
		case PlantState.seed:
			actualPlantState = PlantState.baby;
			lopinSeedObj.SetActive (false);
			bush1Obj.SetActive (true);
			break;
		case PlantState.baby:
			actualPlantState = PlantState.teenage;
			bush1Obj.SetActive (false);
			bush2Obj.SetActive (true);
			break;
		case PlantState.teenage:
			actualPlantState = PlantState.grownup;
			bush2Obj.SetActive (false);
			bush3Obj.SetActive (true);
			break;
		case PlantState.grownup:
			break;
		default:
			break;
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyDown (CustomInputManager.instance.actionKey) && other.tag == "Player") 
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
		
			//faire les changements d'apparence de la caillasse;
			CustomInputManager.instance.ShowHideActionButtonVisual (true);

	}
	void StopListeningForAction()
	{

		//arreter les effets visuels
		CustomInputManager.instance.ShowHideActionButtonVisual (false);

	}

}
