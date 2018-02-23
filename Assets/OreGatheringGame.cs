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
	public int currentScore;
	public int scorePointCount;
	int recquiredScore;
	int actualRound;

	public float scrollSpeed;
	float initialScrollSpeed;
	public Transform leftBorder;
	public Transform rightBorder;
	public Rigidbody2D detectionCursor;
	public OreGameDetectionArea bonusArea;

	bool gameInProgress;
	bool gameIsFinished;
	public bool isPlaying;
	public bool isHeadingRight = true;

	Vector2 detectionCursorStartPos;
	void Awake()
	{
		detectionCursorStartPos = detectionCursor.transform.localPosition;
		initialScrollSpeed = scrollSpeed;
	}

	void OnEnable()
	{
		Invoke("Initialize",0.1f );
	}

	void OnDisable()
	{

		if (playerController) //nécessaire dans ce cas précis...
		{
			CustomInputManager.instance.ShowHideActionButtonVisual (true);

			endOreGamePanel.SetActive (false);
			OreCanvasObj.SetActive (false);
			playerController.enabled = true;
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
				this.enabled = false;
			}
		
		}
	}

	void FixedUpdate()
	{
		if (!isPlaying) 
		{
			return;
		}
		if (isHeadingRight) {
			detectionCursor.AddForce (leftBorder.right * Time.fixedDeltaTime* scrollSpeed, ForceMode2D.Impulse);
		} else {
			detectionCursor.AddForce (-leftBorder.right * Time.fixedDeltaTime* scrollSpeed, ForceMode2D.Impulse);

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
		playerScoreTxt.text = currentScore.ToString ();
		actualRound = 0;
		scrollSpeed = initialScrollSpeed;
		playerController.enabled = false; 
		playerController.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		gameIsFinished = false;
		gameInProgress = false;
		detectionCursor.transform.localPosition = detectionCursorStartPos;
		detectionCursor.velocity = Vector2.zero;

	}
	
	
	public void BeginGameSession()
	{
		isPlaying = true;
		endOreGamePanel.SetActive (false);

		gameInProgress = true;
		bonusArea.isActive = true;
	}
	public void EndGameSession()
	{
		isPlaying = false;
		bonusArea.isActive = false;
		gameIsFinished = true;
		endOreGamePanel.SetActive (true);

		if (currentScore == 0) 
		{
			endGameTxt.text = "Better try again...";
			effectsAudioS.PlayOneShot (defeatSnd);
			return;
		}
		effectsAudioS.PlayOneShot (victorySnd);

		if (currentScore > 5) 
		{
			endGameTxt.text = "AMAZING!";
			return;
		}
		if (currentScore > 3) 
		{
			endGameTxt.text = "AMAZING!";
			return;
		}
		if (currentScore >= 1) 
		{
			endGameTxt.text = "Well done.";
			return;
		}
		//provisoire: a remove:
//		gameInProgress = true;

	}

	public void ChangeCursorDirection()
	{
		isHeadingRight = !isHeadingRight;
		detectionCursor.velocity = Vector2.zero;
		ActualizeScore ();
		bonusArea.areaImg.CrossFadeAlpha (1, 1f, true);

	}

	void ActualizeScore()
	{
		actualRound++;
		recquiredScore ++;
		playerScoreTxt.text = currentScore.ToString();
		scrollSpeed *= 2;
		if (currentScore < recquiredScore) 
		{
			EndGameSession ();
			return;
		}
		bonusArea.isActive = true;
		bonusArea.transform.localPosition = new Vector2(Random.Range(-360f,360f),0);
	}
}
