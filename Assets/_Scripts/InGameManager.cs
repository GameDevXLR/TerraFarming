using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameManager : MonoBehaviour {

	public OreGatheringGame OreGame;
	public static InGameManager instance;

	public ThirdPersonUserControl playerController;
	public Animator InterfaceAnimator;

	public ParticleSystem cleanParticle;
	public ParticleSystem waterParticle;
	public ParticleSystem miningChargeParticle;
	public ParticleSystem miningHitParticle;

	public Animator machineAnimator;
	public Animator machineBushController;

	public GameObject machineCanvas;
	public GameObject miningCanvas;
	public GameObject quitCanvas;
	public Button resumeBtn;
	public Button quitBtn;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else 
		{
			Destroy (gameObject);
		}
		
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (quitCanvas.activeSelf) 
			{
				HideQuitGameMenu ();
				return;
			}
			if (!machineCanvas.activeSelf && !miningCanvas.activeSelf) 
			{
				ShowQuitGameMenu ();
			}
		}
	}

	public void ShowQuitGameMenu()
	{
		quitCanvas.SetActive (true);
		EventSystem.current.SetSelectedGameObject(resumeBtn.gameObject);
		playerController.isActive= false;

	}

	public void HideQuitGameMenu()
	{
		quitCanvas.SetActive (false);
		playerController.isActive= true;

	}

	public void QuitTheGame()
	{
		Application.Quit();
	}


}
