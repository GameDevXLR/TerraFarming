using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager : MonoBehaviour {

    public KeyCode forwardkey = KeyCode.Z;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.Q;
    public KeyCode leftKey = KeyCode.D;

    public static CustomInputManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
