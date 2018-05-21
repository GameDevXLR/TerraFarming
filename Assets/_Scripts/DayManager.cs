using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour {

    private int _currentDay = 1;
    private DayStates _dayStates;
    public static DayManager instance;
    public Text DisplayNumberDay;


    private void Awake()
    {
        if(instance == null)
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
        }
    }

    
}
