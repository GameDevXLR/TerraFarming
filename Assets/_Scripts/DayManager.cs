using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    private int _currentDay = 1;
    public DayStates _dayStates;
    public Text DisplayNumberDay;
    public Text dayStateDisplay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        DisplayNumberDay.text = _currentDay.ToString();
    }

    public int CurrentDay
    {
        get
        {
            return _currentDay;
        }

        set
        {
            //			//si c'est le deuxieme jour.
            //			if (value == 2)
            //			{
            //				GameEventsManager.instance.StartIntroCineNewDay ();
            //			}
            _currentDay = value;
            DisplayNumberDay.text = _currentDay.ToString();
        }
    }

    public DayStates DayStates
    {
        get
        {
            return _dayStates;
        }

        set
        {
            _dayStates = value;
            dayStateDisplay.text = _dayStates.ToString();
        }
    }
}