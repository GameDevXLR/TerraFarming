using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager : MonoBehaviour {

    public KeyCode forwardkey = KeyCode.Z;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.Q;
    public KeyCode leftKey = KeyCode.D;
	public KeyCode actionKey = KeyCode.Space;

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
        }
    }

	public void ShowHideActionButtonVisual(bool show)
	{
		actionButtonVisual.SetActive (show);
		if (show) 
		{
			actionBtnAudioS.PlayOneShot (hideActionBtnSnd);
		} 
//		else 
//		{
//			actionBtnAudioS.PlayOneShot (hideActionBtnSnd);
//
//		}
	}
}
