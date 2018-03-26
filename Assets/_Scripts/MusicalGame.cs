using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalGame : MonoBehaviour 
{
	//jeu musical jouant une piste en fond sur laquelle le joueur doit "placer" des notes dans un interval régulier.
	//il gagne des points si il réussi une note, en perd si il fail.
	//le rythme d'arrivé des notes a jouer est constant basé sur une variable fixe.
	//il peut y avoir des "blancs" entre des notes
	//la mélodie est pré écrite mais les touches a appuyer sont random.

	public AudioClip backgroundMusic;
	public AudioClip errorKey;
	public AudioClip[] keys;
	public float timeBetweenKeys;

	public int errorKeyCode;
	public int whiteKeyCode;
	public int[] keyTrack;

	public AudioSource audioSBackground;
	public AudioSource audioSKeys;

	bool isPlaying;
	int currentPos;
	float musicLenght;
	float startTime;
	float lastKeyTime;
	int tmpkey;


	// Use this for initialization
	void Start () 
	{
		musicLenght = backgroundMusic.length;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isPlaying) 
		{
			if (Time.time > lastKeyTime + timeBetweenKeys) 
			{
				tmpkey = keyTrack [currentPos];
				if (tmpkey == whiteKeyCode) 
				{
					//					audioSKeys.PlayOneShot (white);

				} else 
				{
					audioSKeys.PlayOneShot (keys[tmpkey]);
				}
				currentPos++;
				lastKeyTime = Time.time;
			}
			if (Time.time > startTime + musicLenght) 
			{
				isPlaying = false;
			}
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				tmpkey = keyTrack [currentPos];
				if (tmpkey == whiteKeyCode) 
				{
//					audioSKeys.PlayOneShot (white);

				} else 
				{
					audioSKeys.PlayOneShot (keys[tmpkey]);
				}
				currentPos++;
			}
		} 
		else 
		{
			if (Input.GetKeyDown (KeyCode.M)) 
			{
				isPlaying = true;
				StartPlayingTheMusic ();
			}
		}
	}

	void StartPlayingTheMusic ()
	{
		audioSBackground.PlayOneShot (backgroundMusic);
		startTime = Time.time;
		lastKeyTime = Time.time;
	}
}
