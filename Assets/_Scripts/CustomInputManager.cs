using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager : MonoBehaviour {

    public KeyCode forwardkey = KeyCode.Z;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.Q;
    public KeyCode rightKey = KeyCode.D;
	public KeyCode actionKey = KeyCode.Space;
    public KeyCode jumpKey = KeyCode.J;

    public static CustomInputManager instance;
	public GameObject actionButtonVisual;
	public AudioSource actionBtnAudioS;
	public AudioClip showActionBtnSnd;
	public AudioClip hideActionBtnSnd;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            if(PlayerPrefs.GetString("Keyboard") != "azerty")
            {
                forwardkey = KeyCode.W;
                leftKey = KeyCode.A;
            }
        }
    }

	public void ShowHideActionButtonVisual(bool show)
	{

		actionButtonVisual.SetActive (show);
		if (show) 
		{
			actionBtnAudioS.PlayOneShot (hideActionBtnSnd);
		} 
	}

    public Vector3 getDirection()
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
        if (Input.GetKey(rightKey))
        {
            direction += CameraController.instance.transform.right;
        }
        if (Input.GetKey(leftKey))
        {
            direction -= CameraController.instance.transform.right;
        }

        direction.y = 0;

        return direction;
    }
}
