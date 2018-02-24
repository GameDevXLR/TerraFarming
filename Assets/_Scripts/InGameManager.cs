using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class InGameManager : MonoBehaviour {

	public OreGatheringGame OreGame;
	public static InGameManager instance;
	public ThirdPersonUserControl playerController;
	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else 
		{
			Destroy (gameObject);
		}
		
	}
    
}
