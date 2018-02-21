using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject focus;

    public Vector3 offset;
    public float distance = 1;


    private void Start()
    {
        offset = transform.transform.position - focus.transform.position;
    }

    private void Update()
    {
        transform.position = focus.transform.position + offset * distance;
    }
}
