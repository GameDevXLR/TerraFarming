using UnityEngine;

public class PersistencyManager : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //		Cursor.visible = false;
        //		Cursor.lockState = CursorLockMode.Locked;
    }
}