using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void QuitGame()
	{
//		GetComponent<AudioSource> ().PlayOneShot (clic1Snd);

		Application.Quit ();
	}

	public void StartNewGame()
	{
		SceneManager.LoadScene (1);
	}

	public void ContinueGame()
	{
		SceneManager.LoadScene (1);
		//and co...
	}
}
