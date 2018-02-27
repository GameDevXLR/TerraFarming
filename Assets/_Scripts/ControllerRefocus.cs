using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.UI;
// If there is no selected item, set the selected item to the event system's first selected item
public class ControllerRefocus : MonoBehaviour
{
	void Update()
	{
		if (EventSystem.current.currentSelectedGameObject == null)
		{
			Debug.Log("Reselecting first input");
			EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
//			EventSystem.current.currentSelectedGameObject.GetComponent<Button>().navigation.
		}
	}
}