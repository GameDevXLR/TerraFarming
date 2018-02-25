using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class OreGatheringGame : MonoBehaviour 
{
	public GameObject OreCanvasObj;
	public Text playerScoreTxt;
	public Text endGameTxt;
	public GameObject endOreGamePanel;

	public AudioSource effectsAudioS;
	public AudioClip victorySnd;
	public AudioClip defeatSnd;

	public  ThirdPersonUserControl playerController;
	public Animator playerAnimator;
	public int currentScore;
	public int scorePointCount;
	int recquiredScore;
	public int totalSessionScore;
	int actualRound;

	public float scrollSpeed;
	float initialScrollSpeed;
	public Transform leftBorder;
	public Transform rightBorder;
	public Rigidbody2D detectionCursor;
	public OreGameDetectionArea bonusArea;

	public GameObject[] bonusAreasObj;
	public int chanceOfActivatingArea=50;

	bool gameInProgress;
	bool gameIsFinished;
	public bool isPlaying;
	public bool isHeadingRight = true;

	Vector2 detectionCursorStartPos;
	void Awake()
	{
		detectionCursorStartPos = detectionCursor.transform.localPosition;
		initialScrollSpeed = scrollSpeed;
		OreCanvasObj.SetActive (false);
	}

	void OnEnable()
	{
//		Invoke("Initialize",0.5f );
		Initialize();
	}

	void OnDisable()
	{

		if (playerController) //nécessaire dans ce cas précis...
		{
			CustomInputManager.instance.ShowHideActionButtonVisual (false);

			endOreGamePanel.SetActive (false);
			OreCanvasObj.SetActive (false);
			playerController.isActive= true;
		}
	}

	void Update()
	{
		if(Input.GetKeyDown (CustomInputManager.instance.actionKey))
		{
			if (!gameInProgress) 
			{
				BeginGameSession();
			}
			if (gameIsFinished) 
			{
				CustomInputManager.instance.ShowHideActionButtonVisual (false);

				this.enabled = false;
			}
		
		}
	}


	void Initialize()
	{
		CustomInputManager.instance.ShowHideActionButtonVisual (false);
		isHeadingRight = true;
		OreCanvasObj.SetActive (true);
		endOreGamePanel.SetActive (true);
		endGameTxt.text = "Press key to begin";
		currentScore = 0;
		recquiredScore = 0;
		totalSessionScore = 0;
		playerScoreTxt.text = totalSessionScore.ToString ();
		actualRound = 0;
		scrollSpeed = initialScrollSpeed;
		playerController.isActive = false; 
//		playerController.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameIsFinished = false;
		gameInProgress = false;
		detectionCursor.transform.localPosition = detectionCursorStartPos;
		detectionCursor.velocity = Vector2.zero;

	}
	
	
	public void BeginGameSession()
	{
		isPlaying = true;
		detectionCursor.velocity = new Vector2 (scrollSpeed, 0);
		endOreGamePanel.SetActive (false);
		playerAnimator.SetBool ("IsMining", true);
		gameInProgress = true;
		ChangeBonusAreas ();
	}
	public void EndGameSession()
	{

		detectionCursor.velocity = Vector2.zero;

		isPlaying = false;
		bonusArea.isActive = false;
		gameIsFinished = true;
		endOreGamePanel.SetActive (true);
		playerAnimator.SetBool ("IsMining", false);
		ResourcesManager.instance.ChangeRawOre (totalSessionScore);
		CustomInputManager.instance.ShowHideActionButtonVisual (true);
		InGameManager.instance.OreGame.playerController.transform.GetChild (0).gameObject.SetActive (false);

		if (totalSessionScore == 0) 
		{
			endGameTxt.text = "Better try again...";
			effectsAudioS.PlayOneShot (defeatSnd);
			return;
		}
		effectsAudioS.PlayOneShot (victorySnd);
		InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime ("Victory", layer: -1, fixedTime: 2);
		if (totalSessionScore > 25) 
		{
			endGameTxt.text = "AMAZING!";
			return;
		}
		if (totalSessionScore > 10) 
		{
			endGameTxt.text = "Great work!";
			return;
		}
		if (totalSessionScore >= 1) 
		{
			endGameTxt.text = "Well done.";
			return;
		}
		//provisoire: a remove:
//		gameInProgress = true;

	}

	public void ChangeCursorDirection()
	{

		ActualizeScore ();
		isHeadingRight = !isHeadingRight;
		detectionCursor.velocity = -detectionCursor.velocity;
		bonusArea.areaImg.CrossFadeAlpha (1, 1f, true);

	}

	void ActualizeScore()
	{
		actualRound++;
		if (recquiredScore > 1) 
		{
			//droit a une erreure!
			recquiredScore--;
		}
		playerScoreTxt.text = totalSessionScore.ToString();
		if (actualRound == 1 ||actualRound == 3||actualRound == 6 ||actualRound == 8||actualRound == 10) {
//			effectsAudioS.PlayOneShot (victorySnd);
			chanceOfActivatingArea += 10;
			if (chanceOfActivatingArea > 100) 
			{
				chanceOfActivatingArea = 100;
			}
			detectionCursor.velocity  *= 1.5f;
		}
		if (currentScore < recquiredScore) 
		{
			EndGameSession ();
			return;
		}
		currentScore = 0;
		recquiredScore = 0;
		ChangeBonusAreas ();
		//		bonusArea.transform.localPosition = new Vector2(Random.Range(-360f,360f),0);
	}

	void ChangeBonusAreas()
	{

		foreach (GameObject obj in bonusAreasObj) 
		{
			obj.GetComponent<OreGameDetectionArea> ().isActive = false;
			obj.GetComponent<Image> ().enabled = false;

			if (Random.Range (0, 101) < chanceOfActivatingArea) 
			{
				obj.GetComponent<Image> ().enabled = true;
				obj.GetComponent<OreGameDetectionArea> ().isActive = true;
				recquiredScore++;
			}
			
		}
		if (recquiredScore == 0) 
		{
			recquiredScore++;
			bonusAreasObj [0].GetComponent<OreGameDetectionArea> ().isActive = true;
			bonusAreasObj [0].GetComponent<Image> ().enabled = true;
		}
	}
}
