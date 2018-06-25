using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private bool hasPressedEnter;
    public GameObject mainMenuPanel;
    public GameObject keyboardChoicePanel;
    public GameObject pressEnterObj;

    public LoadingScreenControl loadingScreenControl;

    public Button continueButton;

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

        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            continueButton.interactable = true;
        }
    }

    public void QuitGame()
    {
        //		GetComponent<AudioSource> ().PlayOneShot (clic1Snd);

        Application.Quit();
    }

    public void StartNewGame()
    {
        PlayerPrefs.SetString("Game", "new");
        audioS.PlayOneShot(startGameSnd);
        mainMenuPanel.SetActive(false);
        loadingScreenControl.LoadScreen(1);
        //SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ContinueGame()
    {
        PlayerPrefs.SetString("Game", "continue");
        audioS.PlayOneShot(startGameSnd);
        mainMenuPanel.SetActive(false);
        loadingScreenControl.LoadScreen(1);
        //SceneManager.LoadScene (1, LoadSceneMode.Single);
    }

    public void Update()
    {
        if (!hasPressedEnter)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                ShowMainMenu();
            }
        }
    }

    public void ShowMainMenu()
    {
        hasPressedEnter = true;
        mainMenuPanel.SetActive(true);
        pressEnterObj.SetActive(false);
        keyboardChoicePanel.SetActive(false);
    }

    public void PlayerMouseOverSnd()
    {
        audioS.PlayOneShot(mouseOverSnd);
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