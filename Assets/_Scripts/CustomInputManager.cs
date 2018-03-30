using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager : MonoBehaviour {

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
        if(instance == null)
        {
            instance = this;
            if(PlayerPrefs.GetString("Keyboard") != "azerty")
            {
                forwardkey = KeyCode.W;
                rightKey = KeyCode.A;
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
            direction.z = 1;
        }
        if (Input.GetKey(backwardKey))
        {
            direction.z -= 1;
        }
        if (Input.GetKey(leftKey))
        {
            direction.x = 1;
        }
        if (Input.GetKey(rightKey))
        {
            direction.x -= 1;
        }

        return direction;
    }
}
