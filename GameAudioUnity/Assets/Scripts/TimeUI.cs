using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TMP_Text timeUI;
    public TMP_Text dayUI;

    public ActivityInfo Activity1;
    public ActivityInfo Activity2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //TimeOfDay.Instance.IncreaseTime(1);
            ActivityManager.Instance.OpenMenu(Activity1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //TimeOfDay.Instance.IncreaseTime(5);
            ActivityManager.Instance.OpenMenu(Activity2);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(TimeOfDay.Instance.Time <= 12)
        {
            timeUI.text = "Time: " + TimeOfDay.Instance.Time.ToString() + "am";
        }
        else
        {
            timeUI.text = "Time: " + (TimeOfDay.Instance.Time - 12).ToString() + "pm";
        }
        dayUI.text = "Day: " + TimeOfDay.Instance.Day.ToString();
    }
}
