using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateConstantly : MonoBehaviour {

    public float rotateSpeed; //set it in the  inspector

    void Update()
    {
        rotate();
    }


    void rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }

}
