using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnEnable() 
	{
		transform.LookAt (InGameManager.instance.playerController.transform.position);
	}
}
