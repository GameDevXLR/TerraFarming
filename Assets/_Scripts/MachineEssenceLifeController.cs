using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class MachineEssenceLifeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GetComponent<OutlineEffect>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("player detect on " + name);
        }
    }
}
