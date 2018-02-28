using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	bool hasPressedEnter;
	public GameObject mainMenuPanel;
    public GameObject keyboardChoicePanel;
    public GameObject pressEnterObj;

	public AudioSource audioS;
	public AudioClip mouseOverSnd;
	public AudioClip startGameSnd;



    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Keyboard"))
        {
            keyboardChoicePanel.SetActive(true);
        }
        else
        {

            keyboardChoicePanel.SetActive(false);
        }
    }

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
        keyboardChoicePanel.SetActive(false);
	}

	public void PlayerMouseOverSnd()
	{
		audioS.PlayOneShot (mouseOverSnd);

	}

    public void showOption()
    {
        keyboardChoicePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        Invoke("activatePressEnter", 0.1f);
        pressEnterObj.SetActive(true);
    }

    public void activatePressEnter()
    {
        hasPressedEnter = false;
    }


    

}
