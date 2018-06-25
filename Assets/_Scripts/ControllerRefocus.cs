using UnityEngine;
using UnityEngine.EventSystems;

//using UnityEngine.UI;
// If there is no selected item, set the selected item to the event system's first selected item
public class ControllerRefocus : MonoBehaviour
{
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            //			EventSystem.current.currentSelectedGameObject.GetComponent<Button>().navigation.
        }
    }
}