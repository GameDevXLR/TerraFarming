using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour {

    public GameObject BaseCanvas;

    public void ExitInDay(int day)
    {
        BaseCanvas.SetActive(false);
        DayManager.instance.CurrentDay += day;
        InGameManager.instance.playerController.enableMovement();
    }
}
