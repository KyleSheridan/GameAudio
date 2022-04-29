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
        Time = 9;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseTime(int amount)
    {
        Time += amount;

        if(Time > 22)
        {
            NextDay();
        }
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

        Time = 9;
    }

    private void OnDestroy()
    {
        if(this == _instance)
        {
            _instance = null;
        }
    }
}
