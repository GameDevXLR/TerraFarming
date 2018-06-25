using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void OnEnable()
    {
        transform.LookAt(InGameManager.instance.playerController.transform.position);
    }
}