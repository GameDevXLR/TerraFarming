using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseManager : MonoBehaviour 
{

	public static BaseManager instance;
	public Action<int> OnDayChange;

    public GameObject baseCanvas;

	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

    public void ExitInDay(int day)
    {
        baseCanvas.SetActive(false);
        DayManager.instance.CurrentDay += day;
        InGameManager.instance.playerController.enableMovement();
		PlantationManager.instance.SpeedUpAllGrowth ();

    }


}
