using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager instance;
    public PlayableDirector director;
    public PlayableAsset cutscene1;
    public Animator captainAnimator;
    public Animator playAnimator;
    public Transform StartPosTr;
    public GameObject canvasParentObj;
    //	public Camera questcam;

    private bool isPlayingClip;
    private float clipDuration;

    private PlayableAsset clipToPlay;

    //	Vector3 pos;
    //	Quaternion rot;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //	void Start()
    //	{
    //		pos = InGameManager.instance.playerController.GetComponent<Transform> ().position;
    //		rot = InGameManager.instance.playerController.GetComponent<Transform> ().rotation;
    //	}
    private void Update()
    {
        ////Permet de rejouer le dernier clip jouer ou le clip placé manuellement dans la hierarchy.
        //if (Input.GetKeyDown(KeyCode.Alpha1) && !isPlayingClip)
        //{
        //    LaunchCinematic(cutscene1, StartPosTr);
        //}

        //Vérifie si un clip est en cours, si oui, s'assure de le finir quand il faut.
        if (isPlayingClip)
        {
            if (clipDuration < 0)
            {
                EndClip();
            }
            else
            {
                clipDuration -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Launchs the cinematic.
    /// </summary>
    /// <param name="clip">Clip to play.</param>
    /// <param name="playerStartPos">Player start position and rotation based on a transform.</param>
    public void LaunchCinematic(PlayableAsset clip, Transform playerStartPos)
    {
        clipToPlay = clip;
        StartPosTr = playerStartPos;
        director.playableAsset = clipToPlay;
        PrepareAndStartClip();
    }

    private void PrepareAndStartClip()
    {
        isPlayingClip = true;
        clipDuration = (float)director.duration;
        //		Camera.main.GetComponent<OutlineEffect> ().outlineCamera = questcam;
        InGameManager.instance.playerController.disableMovement();
        InGameManager.instance.playerController.GetComponent<BehaviourController>().enabled = false;
        //		InGameManager.instance.playerController.shadowObject.SetActive(false);

        canvasParentObj.SetActive(false);
        InGameManager.instance.playerController.transform.parent.GetComponent<Transform>().SetPositionAndRotation(StartPosTr.position, StartPosTr.rotation);
        InGameManager.instance.playerController.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        director.Play(clipToPlay);
    }

    public void EndClip()
    {
        //		Camera.main.GetComponent<OutlineEffect> ().outlineCamera = Camera.main;

        isPlayingClip = false;
        InGameManager.instance.playerController.enableMovement();
        InGameManager.instance.playerController.GetComponent<BehaviourController>().enabled = true;
        //		InGameManager.instance.playerController.shadowObject.SetActive(true);

        canvasParentObj.SetActive(true);
    }
}