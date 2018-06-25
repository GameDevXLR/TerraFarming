using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public bool rotationActivated = true;
    public Transform cam;
    private Vector3 standardPos = new Vector3(0, 180, 0);

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        this.transform.LookAt(cam.position);
        if (rotationActivated)
        {
            this.transform.Rotate(standardPos);
        }
    }
}