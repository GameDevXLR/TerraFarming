using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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

    [Tooltip("Un array contenant TOUS les musicgameSO")]
    public MusicGameScriptableObject[] allMusicGame;

    [Tooltip("Le jeu musical selectionné.")]
    public MusicGameScriptableObject myMusicGame;

    public OreVein currentVein;
    public GameObject mineHitEffect;
    //	[Header("Gestion de la musique")]
    //	public AudioClip backgroundMusic;
    //	[Tooltip("Le son jouer en cas d'erreur.")]public AudioClip errorKey;
    //	[Tooltip("Tous les accords que tu peux jouer.")]public AudioClip[] keys;
    //
    //	public float timeBetweenKeys;
    //
    //	[Tooltip("La vitesse de défilement des touches dans le minigame.")]
    //	public float keySpeed;
    //
    //	[Tooltip("Le code qui défini un blanc. Ne doit pas etre une possibilité de 'Keys' juste au dessus!!")]
    //	public int whiteKeyCode;
    //
    //	[Tooltip("Ca c'est ta partition en gros! Tu place des index de 'keys' plus haut ou alors un blanc.")]
    //	public int[] keyTrack;

    public AudioSource audioSBackground;
    public AudioSource audioSKeys;

    [Header("Gestion du minijeu lui-meme.")]
    public AudioMixer mainMusicMixer;

    public GameObject oreGameCanvas;
    public Transform keyStartPosition;
    public List<GameObject> keyPool;
    public int score;
    public Text scoreTxt;

    [Header("Le panneau des scores:")]
    public GameObject scorePanel;

    public Text mistakesCountDisplay;
    public Text comboCountDisplay;
    public Text commentForPlayer;

    //	bool lastInputWasMistake;
    private int numberOfMistakes;

    private int currentCombo;
    private int longestCombo;
    private float tmpTime;

    [Header("Attention! L'ordre de ces 2 array doivent être le meme!")]
    [Tooltip("Les touches du clavier a utiliser.")]
    public KeyCode[] allInputs;

    [Tooltip("Les icones des touches du clavier.")]
    public Sprite[] allSpritesForInput;

    //private fields...
    private bool isPlaying;

    private bool scoreMenuOpen;
    private bool isAddingKeys;
    private int currentPos;
    private float musicLenght;
    private float startTime;
    private float lastKeyTime;
    private int tmpkey;
    private KeyCode expectedInput;
    private Sprite expectedSprite;
    private AudioClip expectedSnd;
    private int indexKey;
    [HideInInspector]
    public bool willStartCinematic;

    #region monoBehaviour

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("oups ya comme qui dirait 2 musical game la!");
        }
    }

    public void OnEnable()
    {
        StopAllCoroutines();
        StartPlayingTheGame();
    }

    public void OnDisable()
    {
        oreGameCanvas.SetActive(false);
        score = 0;
        scoreTxt.text = score.ToString();
    }

    private void Update()
    {
        if (scoreMenuOpen)
        {
            if (Input.GetKeyDown(CustomInputManager.instance.actionKey)
                || Input.GetKeyDown(KeyCode.Escape)
                || CustomInputManager.instance.IsMoving())
            {
                CloseTheGame();
            }
        }

        if (isPlaying)
        {
            if (isAddingKeys)
            {
                if (Time.time > lastKeyTime + myMusicGame.timeBetweenKeys)
                {
                    if (currentPos >= myMusicGame.keyTrack.Length)
                    {
                        isAddingKeys = false;
                    }
                    else
                    {
                        tmpkey = myMusicGame.keyTrack[currentPos];
                        if (tmpkey == myMusicGame.whiteKeyCode)
                        {
                            //					audioSKeys.PlayOneShot (white);
                        }
                        else
                        {
                            expectedSnd = myMusicGame.keys[tmpkey];
                            SelectTheNextKey();
                        }
                        currentPos++;
                        lastKeyTime = Time.time;
                    }
                }
            }
            if (Time.time > startTime + musicLenght)
            {
                ShowScoreMenu();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BrutalyStopTheGame();
            }
        }
    }

    #endregion monoBehaviour

    //lancer une partie:
    private void StartPlayingTheGame()
    {
        indexKey++;
        if (indexKey >= allMusicGame.Length) indexKey = 0;
        myMusicGame = allMusicGame[indexKey];
        musicLenght = myMusicGame.backgroundMusic.length;

        oreGameCanvas.SetActive(true);

        InGameManager.instance.playerController.enabled = false;
        StartCoroutine(ChangeMainMusicVolume(false));

        //		InGameManager.instance.miningChargeParticle.GetComponent <ParticleSystem> ().gameObject.SetActive (true);
        InGameManager.instance.miningChargeParticle.GetComponent<ParticleSystem>().Play();
        InGameManager.instance.miningCharge2Particle.GetComponent<ParticleSystem>().Play();
        InGameManager.instance.playerController.GetComponent<Animator>().SetBool(AnimeParameters.ismining.ToString(), true);

        isPlaying = true;
        isAddingKeys = true;
        audioSBackground.PlayOneShot(myMusicGame.backgroundMusic);
        startTime = Time.time;
        lastKeyTime = Time.time;
        currentPos = 0;
        currentCombo = 0;
        longestCombo = 0;
        numberOfMistakes = 0;

        //		lastInputWasMistake = true;
    }

    //paramétrage de la prochaine touche a appuyer et ce qu'elle fera.
    private void SelectTheNextKey()
    {
        int i = Random.Range(0, allInputs.Length);
        expectedInput = allInputs[i];
        expectedSprite = allSpritesForInput[i];
        AddAKeyToPlay();
    }

    //ajout de la touche dans le jeu.
    private void AddAKeyToPlay()
    {
        GameObject go = keyPool[0];
        //		go.SetActive (true);
        go.GetComponent<MovingKeyForMusicalGame>().MusicalKeyConstructor(expectedSprite, expectedInput, expectedSnd, myMusicGame.keySpeed);
        keyPool.RemoveAt(0);
    }

    //arreter le jeu en chien
    private void BrutalyStopTheGame()
    {
        isAddingKeys = false;
        musicLenght = 0;
    }

    //a optimiser tout ca.
    private void ShowScoreMenu()
    {
        isPlaying = false;
        InGameManager.instance.miningChargeParticle.GetComponent<ParticleSystem>().Stop();
        InGameManager.instance.miningCharge2Particle.GetComponent<ParticleSystem>().Stop();
        InGameManager.instance.playerController.GetComponent<Animator>().SetBool("ismining", false);
        InGameManager.instance.playerController.GetComponent<Animator>().SetBool("isvictory", true);
        //InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime ("Victory", layer: -1, fixedTime: 2);
        //on donne des points bonus pour le plus long combo?
        if (currentCombo > longestCombo)
        {
            longestCombo = currentCombo;
        }
        ChangeMusicalGameScore(longestCombo);

        scoreMenuOpen = true;
        InGameManager.instance.playerController.enabled = true;
        scorePanel.SetActive(true);
        //		Debug.Log("Game is over. Your score: "+ score.ToString());
        Invoke("CloseTheGame", 4f);
        comboCountDisplay.text = longestCombo.ToString();
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
        commentForPlayer.text = "Better give it another try...";

        //on donne la récompense
    }

    private void CloseTheGame()
    {
        CancelInvoke();
        //InGameManager.instance.playerController.GetComponent<Animator>().SetBool ("iswalking", true);
        if (currentVein.clip)
        {
            currentVein.faceTarg.gameObject.SetActive(false);
            currentVein.faceTarg.enabled = false;
        }
        scoreMenuOpen = false;
        scorePanel.SetActive(false);
        //arreter la musique de fond aussi
        audioSBackground.Stop();
        StartCoroutine(ChangeMainMusicVolume(true));
        this.enabled = false;
        if (willStartCinematic)
        {
            GameEventsManager.instance.StartIntroCinematicMining();
            willStartCinematic = false;
        }
    }

    //changement du score.
    public void ChangeMusicalGameScore(int change)
    {
        //décompte des fautes et longueur du combo.
        if (change <= 0)
        {
            if (isPlaying)
            {
                //				InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime("MiningFail", layer: -1, fixedTime: 2);
                StartCoroutine(PlayPartEffect(1, false));

                numberOfMistakes++;
                //				lastInputWasMistake = true;
                if (currentCombo > longestCombo)
                {
                    longestCombo = currentCombo;
                }
                currentCombo = 0;
                score += change;
            }
        }
        else
        {
            if (isPlaying)
            {
                StartCoroutine(PlayPartEffect(1, true));

                if (currentCombo > 3)
                {
                    //faire ici des bonus de combo?
                }

                //			if (lastInputWasMistake)
                //			{
                //				lastInputWasMistake = false;
                //			}
                currentCombo++;
            }
            score += change;
        }

        if (score <= 0)
        {
            score = 0;
        }
        scoreTxt.text = score.ToString();
    }

    private IEnumerator ChangeMainMusicVolume(bool Activate)
    {
        float i;
        mainMusicMixer.GetFloat("MainMusic", out i);
        if (Activate)
        {
            while (i < 0)
            {
                i += 1;
                mainMusicMixer.SetFloat("MainMusic", i);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (i > -40f)
            {
                i -= 1;
                mainMusicMixer.SetFloat("MainMusic", i);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private IEnumerator PlayPartEffect(float time, bool isPositive)
    {
        tmpTime = 0;
        if (isPositive)
        {
            InGameManager.instance.miningChargeParticle.GetComponent<ParticleSystem>().Play();
            InGameManager.instance.miningCharge2Particle.GetComponent<ParticleSystem>().Play();
            InGameManager.instance.miningHitParticle.GetComponent<ParticleSystem>().Play();
            InGameManager.instance.miningHitParticle2.GetComponent<ParticleSystem>().Play();
            InGameManager.instance.playerController.GetComponent<Animator>().PlayInFixedTime("MiningHit", layer: -1, fixedTime: 2);
            mineHitEffect.GetComponent<ParticleSystem>().Play();
            mineHitEffect.transform.position = currentVein.transform.position;
        }
        else
        {
            InGameManager.instance.miningChargeParticle.GetComponent<ParticleSystem>().Stop();
            InGameManager.instance.miningCharge2Particle.GetComponent<ParticleSystem>().Stop();
            InGameManager.instance.miningFailParticle.GetComponent<ParticleSystem>().Play();
            InGameManager.instance.miningFailParticle2.GetComponent<ParticleSystem>().Play();
        }
        while (time > tmpTime)
        {
            if (isPositive)
            {
                InGameManager.instance.miningHitParticle.transform.LookAt(new Vector3(currentVein.transform.position.x, 0f, currentVein.transform.position.z));
                InGameManager.instance.miningHitParticle2.transform.LookAt(new Vector3(currentVein.transform.position.x, 0f, currentVein.transform.position.z));
            }
            else
            {
                InGameManager.instance.miningFailParticle.transform.LookAt(new Vector3(currentVein.transform.position.x, 0f, currentVein.transform.position.z));
                InGameManager.instance.miningFailParticle2.transform.LookAt(new Vector3(currentVein.transform.position.x, 0f, currentVein.transform.position.z));
            }
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            tmpTime += Time.deltaTime;
        }
    }
}