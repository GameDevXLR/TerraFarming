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

//	void FixedUpdate()
//	{
//		if (!isPlaying || !gameInProgress) 
//		{
//			return;
//		}
//		if (isHeadingRight) {
//			detectionCursor.AddForce (leftBorder.right * Time.fixedDeltaTime* scrollSpeed, ForceMode2D.Impulse);
//			Debug.Log (detectionCursor.velocity);
//		} else {
//			detectionCursor.AddForce (-leftBorder.right * Time.fixedDeltaTime* scrollSpeed, ForceMode2D.Impulse);
//
//		}
//	}

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
		bonusArea.isActive = true;
	}
	public void EndGameSession()
	{

		detectionCursor.velocity = Vector2.zero;

		isPlaying = false;
		bonusArea.isActive = false;
		gameIsFinished = true;
		endOreGamePanel.SetActive (true);
		playerAnimator.SetBool ("IsMining", false);
		ResourcesManager.instance.ChangeRawOre (currentScore);
		CustomInputManager.instance.ShowHideActionButtonVisual (true);

		if (currentScore == 0) 
		{
			endGameTxt.text = "Better try again...";
			effectsAudioS.PlayOneShot (defeatSnd);
			return;
		}
		effectsAudioS.PlayOneShot (victorySnd);

		if (currentScore > 7) 
		{
			endGameTxt.text = "AMAZING!";
			return;
		}
		if (currentScore > 5) 
		{
			endGameTxt.text = "Great work!";
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

		ActualizeScore ();
		isHeadingRight = !isHeadingRight;
		detectionCursor.velocity = -detectionCursor.velocity;
		bonusArea.areaImg.CrossFadeAlpha (1, 1f, true);

	}

	void ActualizeScore()
	{
		actualRound++;
		recquiredScore ++;
		playerScoreTxt.text = currentScore.ToString();
		if (actualRound == 3 ||actualRound == 5||actualRound == 7 ||actualRound == 9||actualRound == 11) {
			effectsAudioS.PlayOneShot (victorySnd);

			detectionCursor.velocity  *= 1.5f;
		}
		if (currentScore < recquiredScore) 
		{
			EndGameSession ();
			return;
		}
		bonusArea.isActive = true;
		bonusArea.transform.localPosition = new Vector2(Random.Range(-360f,360f),0);
	}
}
