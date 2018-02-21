using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public KeyCode forwardkey = KeyCode.Z;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.Q;
    public KeyCode leftKey = KeyCode.D;

    public static InputManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
