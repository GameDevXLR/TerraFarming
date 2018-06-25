using UnityEngine;

public class OreGatheringGame : MonoBehaviour
{
    //	public GameObject OreCanvasObj;
    //	public Text playerScoreTxt;
    //	public Text endGameTxt;
    //	public GameObject endOreGamePanel;
    //
    //	public AudioSource effectsAudioS;
    //	public AudioSource MiningAudioS;
    //	public AudioClip victorySnd;
    //	public AudioClip defeatSnd;
    //
    //	public  ThirdPersonUserControl playerController;
    //	public Animator playerAnimator;
    //	public int currentScore;
    //	public int scorePointCount;
    //	int recquiredScore;
    //	public int totalSessionScore;
    //	int actualRound;
    //
    //	public float scrollSpeed;
    //	float initialScrollSpeed;
    //	public Transform leftBorder;
    //	public Transform rightBorder;
    //	public Rigidbody2D detectionCursor;
    //
    //	public OreGameDetectionArea mainBonusArea;
    //	public GameObject[] bonusAreasObj;
    //	public int chanceOfActivatingArea=50;
    //	private int defaultChanceOfActivating;
    //
    //	bool gameInProgress;
    //	bool gameIsFinished;
    //	public bool isPlaying;
    //	public bool isHeadingRight = true;
    //
    //	Vector2 detectionCursorStartPos;
    //
    //	public bool hasClic;
    //	bool hasReleasedActionKey;
    //	float lastClicTime;
    //	public float timeBetweenClic;
    //
    //	bool firstRound;
    //
    //	void Awake()
    //	{
    //		detectionCursorStartPos = detectionCursor.transform.localPosition;
    //		initialScrollSpeed = scrollSpeed;
    //		OreCanvasObj.SetActive (false);
    //		defaultChanceOfActivating = chanceOfActivatingArea;
    //	}
    //
    //	void OnEnable()
    //	{
    ////		Invoke("Initialize",0.5f );
    //		Initialize();
    //	}
    //
    //	void OnDisable()
    //	{
    //
    //		if (playerController) //nécessaire dans ce cas précis...
    //		{
    //			CustomInputManager.instance.ShowHideActionButtonVisual (false);
    //
    //			endOreGamePanel.SetActive (false);
    //			OreCanvasObj.SetActive (false);
    //			playerController.isActive= true;
    //		}
    //	}
    //
    //	void Update()
    //	{
    //
    //		if (!hasReleasedActionKey && Input.GetKeyUp (CustomInputManager.instance.actionKey))
    //		{
    //			hasReleasedActionKey = true;
    //			lastClicTime = Time.time;
    //
    //		}
    //		if (hasClic)
    //		{
    //			if (hasReleasedActionKey)
    //			{
    //				if (Time.time > lastClicTime + timeBetweenClic)
    //				{
    //					hasClic = false;
    //				}
    //			}
    //		}
    //		if(Input.GetKeyDown (CustomInputManager.instance.actionKey))
    //		{
    //			if (!gameInProgress)
    //			{
    //				BeginGameSession();
    //			}
    //			if (gameIsFinished)
    //			{
    //				CustomInputManager.instance.ShowHideActionButtonVisual (false);
    //
    //				this.enabled = false;
    //			}
    //
    //		}
    //	}
    //
    //
    //	void Initialize()
    //	{
    //		firstRound = true;
    //		CustomInputManager.instance.ShowHideActionButtonVisual (false);
    //		chanceOfActivatingArea = defaultChanceOfActivating;
    //		isHeadingRight = true;
    //		OreCanvasObj.SetActive (true);
    //		endOreGamePanel.SetActive (true);
    //		endGameTxt.text = "Press key to begin";
    //		currentScore = 0;
    //		recquiredScore = 0;
    //		totalSessionScore = 0;
    //		playerScoreTxt.text = totalSessionScore.ToString ();
    //		actualRound = 0;
    //		scrollSpeed = initialScrollSpeed;
    //		playerController.isActive = false;
    ////		playerController.GetComponent<Rigidbody> ().velocity = Vector3.zero;
    //		gameIsFinished = false;
    //		gameInProgress = false;
    //		detectionCursor.transform.localPosition = detectionCursorStartPos;
    //		detectionCursor.velocity = Vector2.zero;
    //
    //	}
    //
    //
    //	public void BeginGameSession()
    //	{
    //		hasClic = false;
    //		hasReleasedActionKey = true;
    //		isPlaying = true;
    //		detectionCursor.velocity = new Vector2 (scrollSpeed * Screen.width/100, 0);
    //		endOreGamePanel.SetActive (false);
    //		playerAnimator.SetBool ("IsMining", true);
    //		MiningAudioS.GetComponent<AudioSource> ().Play ();
    //		gameInProgress = true;
    //		ChangeBonusAreas ();
    //		InGameManager.instance.miningChargeParticle.GetComponent <ParticleSystem> ().gameObject.SetActive (true);
    //		InGameManager.instance.miningChargeParticle.GetComponent <ParticleSystem> ().Play ();
    //
    //	}
    //
    //	public void EndGameSession()
    //	{
    //
    //		detectionCursor.velocity = Vector2.zero;
    //		isPlaying = false;
    //		gameIsFinished = true;
    //		endOreGamePanel.SetActive (true);
    //		MiningAudioS.GetComponent<AudioSource> ().Stop ();
    //		playerAnimator.SetBool ("IsMining", false);
    //		//ResourcesManager.instance.ChangeRawOre (totalSessionScore);
    //		CustomInputManager.instance.ShowHideActionButtonVisual (true);
    //		InGameManager.instance.OreGame.playerController.transform.GetChild (0).gameObject.SetActive (false);
    //		InGameManager.instance.miningChargeParticle.GetComponent <ParticleSystem> ().gameObject.SetActive(false);
    //
    //		if (totalSessionScore == 0)
    //		{
    //			endGameTxt.text = "Better try again...";
    //			effectsAudioS.PlayOneShot (defeatSnd);
    //			return;
    //		}
    //		effectsAudioS.PlayOneShot (victorySnd);
    //		InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime ("Victory", layer: -1, fixedTime: 2);
    //		InGameManager.instance.InterfaceAnimator.GetComponent<Animator> ().Play("ScaleOreIco");
    //		if (totalSessionScore > 20)
    //		{
    //			endGameTxt.text = "AMAZING!";
    //			return;
    //		}
    //		if (totalSessionScore > 10)
    //		{
    //			endGameTxt.text = "Great work!";
    //			return;
    //		}
    //		if (totalSessionScore >= 1)
    //		{
    //			endGameTxt.text = "Well done.";
    //			return;
    //		}
    //		//provisoire: a remove:
    ////		gameInProgress = true;
    //
    //	}
    //
    //	public void PlayerPressedKey()
    //	{
    //		hasClic = true;
    //		hasReleasedActionKey = false;
    //	}
    //
    //	public void ChangeCursorDirection()
    //	{
    //
    //		ActualizeScore ();
    //		isHeadingRight = !isHeadingRight;
    //		detectionCursor.velocity = -detectionCursor.velocity;
    ////		bonusArea.areaImg.CrossFadeAlpha (1, 1f, true);
    //
    //	}
    //
    //	void ActualizeScore()
    //	{
    //		actualRound++;
    //
    //		//droit a l'erreur retiré pour le moment:
    ////		if (recquiredScore > 1)
    ////		{
    ////			//droit a une erreure!
    ////			recquiredScore--;
    ////		}
    //
    //		playerScoreTxt.text = totalSessionScore.ToString();
    //		if (actualRound == 1 ||actualRound == 3||actualRound == 6 ||actualRound == 8||actualRound == 10) {
    ////			effectsAudioS.PlayOneShot (victorySnd);
    //			chanceOfActivatingArea += 10;
    //			if (chanceOfActivatingArea > 100)
    //			{
    //				chanceOfActivatingArea = 100;
    //			}
    //			detectionCursor.velocity  *= 1.5f;
    //		}
    //		if (currentScore < recquiredScore)
    //		{
    //			EndGameSession ();
    //			return;
    //		}
    //		currentScore = 0;
    //		recquiredScore = 0;
    //		ChangeBonusAreas ();
    //		//		bonusArea.transform.localPosition = new Vector2(Random.Range(-360f,360f),0);
    //	}
    //
    //	void ChangeBonusAreas()
    //	{
    //
    //		if (firstRound)
    //		{
    //			chanceOfActivatingArea = 100;
    //		}
    //
    //		foreach (GameObject obj in bonusAreasObj)
    //		{
    //			obj.GetComponent<OreGameDetectionArea> ().isActive = false;
    //			obj.GetComponent<Image> ().enabled = false;
    //
    //			if (Random.Range (0, 101) < chanceOfActivatingArea)
    //			{
    //				obj.GetComponent<Image> ().enabled = true;
    //				obj.GetComponent<Image> ().CrossFadeAlpha (1, .1f, true);
    //
    //				obj.GetComponent<OreGameDetectionArea> ().isActive = true;
    //				recquiredScore++;
    //			}
    //
    //		}
    //		if (firstRound)
    //		{
    //			chanceOfActivatingArea = defaultChanceOfActivating;
    //			firstRound = false;
    //		}
    //		if (recquiredScore == 0)
    //		{
    //			recquiredScore++;
    //			mainBonusArea.GetComponent<OreGameDetectionArea> ().isActive = true;
    //			mainBonusArea.GetComponent<Image> ().enabled = true;
    //			mainBonusArea.GetComponent<Image> ().CrossFadeAlpha (1, .1f, true);
    //
    //		}
    //	}
}