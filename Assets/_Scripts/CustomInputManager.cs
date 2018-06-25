using UnityEngine;

public class CustomInputManager : MonoBehaviour
{
    public KeyCode forwardkey = KeyCode.Z;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.Q;
    public KeyCode leftKey = KeyCode.D;
    public KeyCode actionKey = KeyCode.Space;
    public KeyCode jumpKey = KeyCode.J;

    public static CustomInputManager instance;
    public GameObject actionButtonVisual;
    public AudioSource actionBtnAudioS;
    public AudioClip showActionBtnSnd;
    public AudioClip hideActionBtnSnd;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (PlayerPrefs.GetString("Keyboard") != "azerty")
            {
                forwardkey = KeyCode.W;
                rightKey = KeyCode.A;
            }
        }
    }

    public void ShowHideActionButtonVisual(bool show)
    {
        actionButtonVisual.SetActive(show);
        if (show)
        {
            actionBtnAudioS.PlayOneShot(hideActionBtnSnd);
        }
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = new Vector3();

        if (Input.GetKey(forwardkey))
        {
            //direction.z = 1;
            direction += CameraController.instance.transform.forward;
        }
        if (Input.GetKey(backwardKey))
        {
            direction -= CameraController.instance.transform.forward;
        }
        if (Input.GetKey(leftKey))
        {
            direction += CameraController.instance.transform.right;
        }
        if (Input.GetKey(rightKey))
        {
            direction -= CameraController.instance.transform.right;
        }

        direction.y = 0;

        return direction;
    }

    public bool IsMoving()
    {
        return Input.GetKey(forwardkey) || Input.GetKey(backwardKey) || Input.GetKey(leftKey) || Input.GetKey(rightKey);
    }
}