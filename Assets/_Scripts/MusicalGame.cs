﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicalGame : MonoBehaviour 
{
	//Il suffit d'activer le script pour lancer le jeu.


	//jeu musical jouant une piste en fond sur laquelle le joueur doit "placer" des notes dans un interval régulier.
	//il gagne des points si il réussi une note, en perd si il fail.
	//le rythme d'arrivé des notes a jouer est constant basé sur une variable fixe.
	//il peut y avoir des "blancs" entre des notes
	//la mélodie est pré écrite mais les touches a appuyer sont random.
	//les notes débarquent (le son est préderminé ainsi que le rythme mais pas la touche sur laquelle on doit appuyer)
	//un choix entre plusieurs mélodies.
	//Différentes mélodie pour chaque minerai.
	public static MusicalGame instance;

	[Header("Gestion de la musique")]
	public AudioClip backgroundMusic;
	[Tooltip("Le son jouer en cas d'erreur.")]public AudioClip errorKey;
	[Tooltip("Tous les accords que tu peux jouer.")]public AudioClip[] keys;

	public float timeBetweenKeys;

	[Tooltip("La vitesse de défilement des touches dans le minigame.")]
	public float keySpeed;

	[Tooltip("Le code qui défini un blanc. Ne doit pas etre une possibilité de 'Keys' juste au dessus!!")]
	public int whiteKeyCode;

	[Tooltip("Ca c'est ta partition en gros! Tu place des index de 'keys' plus haut ou alors un blanc.")]
	public int[] keyTrack;

	public AudioSource audioSBackground;
	public AudioSource audioSKeys;

	[Header ("Gestion du minijeu lui-meme.")]
	public Canvas oreGameCanvas;
	public Transform keyStartPosition;
	public List<GameObject> keyPool;
	public int score;
	public Text scoreTxt;

	[Header("Le panneau des scores:")]
	public GameObject scorePanel;
	public Text mistakesCountDisplay;
	public Text comboCountDisplay;
	public Text commentForPlayer;
	bool lastInputWasMistake;
	int numberOfMistakes;
	int currentCombo;
	int longestCombo;

	[Header("Attention! L'ordre de ces 2 array doivent être le meme!")]

	[Tooltip("Les touches du clavier a utiliser.")]
	public KeyCode[] allInputs;
	[Tooltip("Les icones des touches du clavier.")]
	public Sprite[] allSpritesForInput;

	
	//private fields...
	bool isPlaying;
	bool scoreMenuOpen;
	int currentPos;
	float musicLenght;
	float startTime;
	float lastKeyTime;
	int tmpkey;
	KeyCode expectedInput;
	Sprite expectedSprite;
	AudioClip expectedSnd;

	#region monoBehaviour
	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		} else 
		{
			Debug.Log ("oups ya comme qui dirait 2 musical game la!");
		}
	}

	void Start () 
	{
		musicLenght = backgroundMusic.length;
	}

	public void OnEnable()
	{
		oreGameCanvas.enabled = true;
		StartPlayingTheGame ();
	}
	public void OnDisable()
	{
		oreGameCanvas.enabled = false;
		score = 0;
		scoreTxt.text = score.ToString();

	}
	void Update () 
	{
		if (scoreMenuOpen) 
		{
			if (Input.GetKeyDown (CustomInputManager.instance.actionKey) || Input.GetKeyDown (KeyCode.Escape)) 
			{
				CloseTheGame ();
			}
		}
			
		if (isPlaying) 
		{
			if (Time.time > lastKeyTime + timeBetweenKeys) 
			{
				if (currentPos >= keyTrack.Length) 
				{
					StopAddingKeysToPlay ();
				} else 
				{
					tmpkey = keyTrack [currentPos];
					if (tmpkey == whiteKeyCode) 
					{
						//					audioSKeys.PlayOneShot (white);

					} else 
					{
						expectedSnd = keys [tmpkey];
						SelectTheNextKey ();
					}
					currentPos++;
					lastKeyTime = Time.time;
				}
			}
			if (Time.time > startTime + musicLenght) 
			{
				isPlaying = false;
			}
			if (Input.GetKeyDown (KeyCode.Escape)) 
			{
				StopAddingKeysToPlay ();
			}
		} 

	}

	#endregion



	//lancer une partie:
	void StartPlayingTheGame ()
	{
		InGameManager.instance.playerController.isActive = false;

		InGameManager.instance.miningChargeParticle.GetComponent <ParticleSystem> ().gameObject.SetActive (true);
		InGameManager.instance.miningChargeParticle.GetComponent <ParticleSystem> ().Play ();
		InGameManager.instance.playerController.GetComponent<Animator>().SetBool ("IsMining", true);

		isPlaying = true;
		audioSBackground.PlayOneShot (backgroundMusic);
		startTime = Time.time;
		lastKeyTime = Time.time;
		currentCombo = 0;
		longestCombo = 0;
		numberOfMistakes = 0;
		lastInputWasMistake = true;
	}

	//paramétrage de la prochaine touche a appuyer et ce qu'elle fera.
	void SelectTheNextKey()
	{
		int i = Random.Range (0, allInputs.Length);
		expectedInput = allInputs [i];
		expectedSprite = allSpritesForInput [i];
		AddAKeyToPlay ();
	}

	//ajout de la touche dans le jeu. 
	void AddAKeyToPlay()
	{
		GameObject go = keyPool [0];
		go.SetActive (true);
		go.GetComponent<MovingKeyForMusicalGame> ().MusicalKeyConstructor (expectedSprite, expectedInput, expectedSnd, keySpeed);
		keyPool.RemoveAt (0);

	}

	//quand on a fini de faire toutes les touches, ca s'arrete ^^
	void StopAddingKeysToPlay()
	{
		isPlaying = false;
		currentPos = 0;
		InGameManager.instance.miningChargeParticle.GetComponent <ParticleSystem> ().gameObject.SetActive (false);
		InGameManager.instance.playerController.GetComponent<Animator>().SetBool ("IsMining", false);
		InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime ("Victory", layer: -1, fixedTime: 2);

		//un peu aprés ca affiche le menu des scores.
		Invoke ("ShowScoreMenu", 1f);
	}

	//a optimiser tout ca.
	void ShowScoreMenu()
	{
		//on donne des points bonus pour le plus long combo?
		ChangeMusicalGameScore(longestCombo);

		scoreMenuOpen = true;
		InGameManager.instance.playerController.isActive = true; 
		scorePanel.SetActive (true);
		Debug.Log("Game is over. Your score: "+ score.ToString());
		Invoke ("CloseTheGame", 3f);
		comboCountDisplay.text = longestCombo.ToString ();
		mistakesCountDisplay.text = numberOfMistakes.ToString();
		ResourcesManager.instance.ChangeRawOre(score);

		if (score > 20) 
				{
			commentForPlayer.text = "AMAZING!";
					return;
				}
		if (score > 10) 
				{
			commentForPlayer.text = "Great work!";
					return;
				}
		if (score >= 1) 
				{
			commentForPlayer.text = "Well done.";
					return;
				}
		//on donne la récompense
		
	}

	void CloseTheGame()
	{
		scoreMenuOpen = false;
		scorePanel.SetActive (false);
		this.enabled = false;
	}

	//changement du score.
	public void ChangeMusicalGameScore(int change)
	{
		//décompte des fautes et longueur du combo.
		if (change < 0) {
			InGameManager.instance.playerController.GetComponent<Animator> ().SetBool("MiningHit", false);
			numberOfMistakes++;
			lastInputWasMistake = true;
		} else {
			InGameManager.instance.playerController.GetComponent<Animator> ().SetBool("MiningHit", true);

			if (currentCombo > 3) 
			{
				//faire ici des bonus de combo?
			}
			
			if (lastInputWasMistake) 
			{
				if (currentCombo > longestCombo) 
				{
					longestCombo = currentCombo;
				}
				currentCombo = 0;
				lastInputWasMistake = false;
			}
			currentCombo++;
		}

		score += change;
		if (score <= 0) 
		{
			score = 0;
		}
		scoreTxt.text = score.ToString ();
	}
}
