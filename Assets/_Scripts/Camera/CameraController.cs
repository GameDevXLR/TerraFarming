using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

#region editor variables
    public GameObject focus;

    public Vector3 offset;
    public float stepZoom;

    public float minDistance;
    public float maxDistance;

    public float smooth;

    public Text verticalText;
    public Text horizontalText;


    #endregion

    #region other variables

    private float distance = 1;
    public static CameraController instance;

    private float targetAngle = 0;
    const float rotationAmount = 1.0f;
    private float v;
    private float h;
    private float z;

    #endregion
#region unity methods

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            setVerticalUI(v);
            setHorizontalUI(h);
        }
    }

    private void Start()
    {

        offset = Quaternion.Euler(V, -H, Z) * new Vector3(0, 0, 1);
        transform.position = focus.transform.position - offset * Distance;
        transform.LookAt(focus.transform);
    }

    private void Update()
    {
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            Distance -= stepZoom;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            Distance += stepZoom;
        }

        Distance = Mathf.Clamp(Distance, minDistance, maxDistance);

        //transform.position = Vector3.Lerp(transform.position, focus.transform.position + offset * distance, smooth * Time.deltaTime );
       
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            H -= rotationAmount;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            H += rotationAmount;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            V -= rotationAmount;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            V += rotationAmount;
        }

        moveSmoothlyCam();
       
    }
#endregion
    public void moveSmoothlyCam()
    {
        offset = Quaternion.Euler(V, -H, Z) * new Vector3(0, 0, 1);
        transform.position = Vector3.Lerp(transform.position, focus.transform.position - offset * Distance, smooth * Time.deltaTime);
        //transform.LookAt(focus.transform);
        Vector3 lTargetDir = focus.transform.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * smooth);
    }

    public void moveCam()
    {
        offset = Quaternion.Euler(V, -H, Z) * new Vector3(0, 0, 1);
        transform.position = focus.transform.position - offset * Distance;
        transform.LookAt(focus.transform);
    }


#region getter/setter
    public float V
    {
        get
        {
            return v;
        }

        set
        {
            v = Mathf.Clamp(value, 0, 90);
            setVerticalUI(v);
        }
    }

    public float H
    {
        get
        {
            return h;
        }

        set
        {
            h = (value < 0) ? value + 360 : (value > 360) ? value - 360 : value;
            setHorizontalUI(h);
        }
    }

    public float Z
    {
        get
        {
            return z;
        }

        set
        {
            z = value;
        }
    }

    public float Distance
    {
        get
        {
            return distance;
        }

        set
        {
            distance = value;
        }
    }

    #endregion

    #region UI
    public void setHorizontalUI(float value)
    {
        if (horizontalText != null)
        {
            horizontalText.text = value.ToString();
        }
    }
    public void setVerticalUI(float value)
    {
        if (verticalText != null)
        {
            verticalText.text = value.ToString();
        }
    }
    #endregion

}
