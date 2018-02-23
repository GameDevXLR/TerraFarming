using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

	public OreGatheringGame OreGame;
	public static InGameManager instance;
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl playerController;

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
