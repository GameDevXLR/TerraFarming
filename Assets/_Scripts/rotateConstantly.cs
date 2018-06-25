using UnityEngine;

public class rotateConstantly : MonoBehaviour
{
    public float rotateSpeed; //set it in the  inspector

    private void Update()
    {
        rotate();
    }

    private void rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }
}