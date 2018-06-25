using UnityEngine;

public class PlayerBaseCanvas : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        BaseManager.instance.baseCanvas = gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}