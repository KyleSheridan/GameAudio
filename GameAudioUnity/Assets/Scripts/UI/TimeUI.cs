using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TMP_Text timeUI;
    public TMP_Text dayUI;

    // Update is called once per frame
    void Update()
    {
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
