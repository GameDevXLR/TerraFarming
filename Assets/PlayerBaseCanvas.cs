using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseCanvas : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
	{
		BaseManager.instance.baseCanvas = gameObject;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
