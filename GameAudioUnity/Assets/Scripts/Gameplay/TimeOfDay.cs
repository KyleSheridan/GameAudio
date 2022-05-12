using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    private static TimeOfDay _instance;

    public static TimeOfDay Instance { get { return _instance; } }

    public enum DayOfWeek
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public int startDay = 9;

    public int finishDay = 20;

    public int Time { get; private set; }
    public DayOfWeek Day { get; private set; }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Day = DayOfWeek.Monday;
        Time = startDay;

        SetTimeOfDayParam();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            IncreaseTime(1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            IncreaseTime(5);
        }
    }

    public void SetTimeOfDayParam()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("TimeOfDay", Time);
    }

    public void IncreaseTime(int amount)
    {
        Time += amount;

        if(Time > finishDay)
        {
            NextDay();
        }

        SetTimeOfDayParam();
    }

    private void NextDay()
    {
        if(Day == DayOfWeek.Sunday)
        {
            Day = DayOfWeek.Monday;
        }
        else
        {
            Day += 1;
        }

        Time = startDay;
    }

    private void OnDestroy()
    {
        if(this == _instance)
        {
            _instance = null;
        }
    }
}
