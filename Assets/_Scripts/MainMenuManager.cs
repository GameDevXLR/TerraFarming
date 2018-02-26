using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	bool hasPressedEnter;
	public GameObject mainMenuPanel;
	public GameObject pressEnterObj;

	public AudioSource audioS;
	public AudioClip mouseOverSnd;
	public AudioClip startGameSnd;


	public void QuitGame()
	{
//		GetComponent<AudioSource> ().PlayOneShot (clic1Snd);

		Application.Quit ();
	}

	public void StartNewGame()
	{
		audioS.PlayOneShot (startGameSnd);
		SceneManager.LoadScene (1,LoadSceneMode.Single);
	}

	public void ContinueGame()
	{
		audioS.PlayOneShot (startGameSnd);

		SceneManager.LoadScene (1);
		//and co...
	}

	public void Update ()
	{
		if (!hasPressedEnter) 
		{
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) 
			{
				ShowMainMenu ();
			}
		}
	}
	public void ShowMainMenu()
	{
		hasPressedEnter = true;
		mainMenuPanel.SetActive (true);
		pressEnterObj.SetActive (false);
	}

	public void PlayerMouseOverSnd()
	{
		audioS.PlayOneShot (mouseOverSnd);

	}

}
