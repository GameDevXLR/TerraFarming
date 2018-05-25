using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour 
{

	public static TimelineManager instance;

	public PlayableDirector director;

	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}
}
