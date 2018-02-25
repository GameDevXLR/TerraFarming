using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowthCycleManager : MonoBehaviour 
{
	//est activé sur le plant spot une fois que ya une plante de planté xD.


	bool isWatered;
	float wateredTime;

	private PlantationSpot plantSpot;
	// Use this for initialization
	void Start () 
	{
		plantSpot = GetComponent<PlantationSpot> ();
	}
	
	public IEnumerator StartGrowing()
	{
		wateredTime = 150;
		isWatered = true;
		while (isWatered) 
		{
			wateredTime--;
			yield return new WaitForSecondsRealtime (1f);	
			if (wateredTime <= 0) 
			{
				isWatered = false;
				plantSpot.RecquireWater ();
				//dire a plantspot qu'il faut de l'eau!
			}
		}
	}
}
