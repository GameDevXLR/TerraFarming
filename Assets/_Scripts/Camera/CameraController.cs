using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

#region editor variables
    public GameObject focus;

    public Vector3 offset;
    public float distance = 1;
    public float stepZoom;

    public float minDistance;
    public float maxDistance;

    public float smooth;
    #endregion

    #region other variables
    #endregion

    private float targetAngle = 0;
    const float rotationAmount = 1.0f;

    private void Start()
    {
        offset = transform.transform.position - focus.transform.position;
    }

    private void Update()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            distance -= stepZoom;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            distance += stepZoom;
        }

        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        //transform.position = Vector3.Lerp(transform.position, focus.transform.position + offset * distance, smooth * Time.deltaTime );
       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetAngle -= 45.0f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetAngle += 45.0f;
        }
        if (targetAngle != 0)
            {
                Rotate();
            }

    }


    protected void Rotate()
    {

        if (targetAngle > 0)
        {
            transform.RotateAround(focus.transform.position, Vector3.up, -rotationAmount);
            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0)
        {
            transform.RotateAround(focus.transform.position, Vector3.up, rotationAmount);
            targetAngle += rotationAmount;
        }

    }
}
