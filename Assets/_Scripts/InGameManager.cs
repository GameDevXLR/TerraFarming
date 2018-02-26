using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class InGameManager : MonoBehaviour {

	public OreGatheringGame OreGame;
	public static InGameManager instance;
	public ThirdPersonUserControl playerController;
	public ParticleSystem cleanParticle;
	public ParticleSystem waterParticle;
	public ParticleSystem miningChargeParticle;
	public ParticleSystem miningHitParticle;

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
