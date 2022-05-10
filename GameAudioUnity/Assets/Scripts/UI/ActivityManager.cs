using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct ActivityInfo
{
    public string title;
    [TextArea]
    public string description;
    [TextArea]
    public string effects;
    public int hours;
} 

public class ActivityManager : MonoBehaviour
{
    private static ActivityManager _instance;

    public static ActivityManager Instance { get { return _instance; } }

    public GameObject canvas;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text effectsText;
    public TMP_Text hoursText;

    public GameObject interactText;

    public bool menuOpen { get; private set; }

    private ActivityInfo currentActivity;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            menuOpen = false;
        }
    }

    public void OpenMenu(ActivityInfo info)
    {
        if(canvas.activeInHierarchy || menuOpen) { return; }

        currentActivity = info;

        canvas.SetActive(true);

        titleText.text = info.title;
        descriptionText.text = info.description;
        effectsText.text = info.effects;
        if(info.hours < 24)
        {
            hoursText.text = info.hours + " Hours";
        }
        else
        {
            hoursText.text = "Next Day";
        }

        menuOpen = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMenu()
    {
        if(!canvas.activeInHierarchy) { return; }

        canvas.SetActive(false);

        menuOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetMenuOpen(bool val)
    {
        menuOpen = val;
        if (val)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AcceptActivity()
    {
        if (!canvas.activeInHierarchy) { return; }

        TimeOfDay.Instance.IncreaseTime(currentActivity.hours);

        //set stats

        CloseMenu();
    }

    private void OnDestroy()
    {
        if (this == _instance)
        {
            _instance = null;
        }
    }
}
