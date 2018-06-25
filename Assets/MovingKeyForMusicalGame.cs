using UnityEngine;
using UnityEngine.UI;

public class MovingKeyForMusicalGame : MonoBehaviour
{
    public bool isActive;
    public bool isMoving;
    public bool hasScoreAPoint;
    public AudioClip keySound;
    public Image keyToPressImg;
    public KeyCode expectedKey;
    public float moveSpeed;

    //ce jeu supporte 4 touches pas plus hein, sinon faut revoir un peu le code!
    public KeyCode[] possibleKeys;

    public AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isActive)
        {
            if (Input.GetKey(expectedKey))
            {
                if (!hasScoreAPoint)
                {
                    GiveAPoint();
                }
                //				InGameManager.instance.playerController.GetComponent<Animator> ().SetBool ("MiningHit", true);
                //				InGameManager.instance.miningHitParticle.GetComponent <ParticleSystem> ().Play ();
            }
            else
            //si tu te trompe de touche:
            if (Input.GetKey(possibleKeys[0]) && possibleKeys[0] != expectedKey || Input.GetKey(possibleKeys[1]) && possibleKeys[1] != expectedKey || Input.GetKey(possibleKeys[2]) && possibleKeys[2] != expectedKey || Input.GetKey(possibleKeys[3]) && possibleKeys[3] != expectedKey)
            {
                audioS.PlayOneShot(MusicalGame.instance.myMusicGame.errorKey);
                isActive = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed * Screen.width / 100, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EndYourTravel();
    }

    public void MusicalKeyConstructor(Sprite inputVisual, KeyCode inputToPress, AudioClip soundOfKey, float speed)
    {
        keyToPressImg.sprite = inputVisual;
        expectedKey = inputToPress;
        keySound = soundOfKey;
        moveSpeed = speed;
        keyToPressImg.CrossFadeAlpha(1, 1f, true);

        isActive = true;
        isMoving = true;
    }

    private void EndYourTravel()
    {
        //faire ici ce qu'on doit faire quand on a fini notre chemin.
        if (hasScoreAPoint)
        {
            MusicalGame.instance.ChangeMusicalGameScore(1);
        }
        else
        {
            MusicalGame.instance.ChangeMusicalGameScore(0);
        }
        GetBackIntoTheList();
    }

    private void GetBackIntoTheList()
    {
        isActive = false;
        isMoving = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        MusicalGame.instance.keyPool.Add(gameObject);
        transform.position = MusicalGame.instance.keyStartPosition.position;
        hasScoreAPoint = false;
        //		gameObject.SetActive (false);
    }

    private void GiveAPoint()
    {
        isActive = false;

        hasScoreAPoint = true;
        audioS.PlayOneShot(keySound);
        keyToPressImg.CrossFadeAlpha(0, .1f, true);
        //		CustomInputManager.instance.ShowHideActionButtonVisual (false);

        //		InGameManager.instance.OreGame.currentScore++;
        //		InGameManager.instance.OreGame.PlayerPressedKey ();
        //		InGameManager.instance.OreGame.totalSessionScore++;
        //		InGameManager.instance.OreGame.playerScoreTxt.text = InGameManager.instance.OreGame.totalSessionScore.ToString ();
    }
}