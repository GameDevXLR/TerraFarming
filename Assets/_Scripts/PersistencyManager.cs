using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistencyManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (gameObject);
//		Cursor.visible = false;
//		Cursor.lockState = CursorLockMode.Locked;
	}
	

}
