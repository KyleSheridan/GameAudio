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

    public List<StatModifier> modifiers;
} 

public class ActivityManager : MonoBehaviour
{
    private static ActivityManager _instance;

    public static ActivityManager Instance { get { return _instance; } }

    public GameObject activitySelectMenu;
    public GameObject activityMenu;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text effectsText;
    public TMP_Text hoursText;

    public GameObject interactText;

    public List<GameObject> buttons;
    public List<TMP_Text> texts;

    public TMP_Text activityDescriptionText;

    public bool menuOpen { get; private set; }

    private bool activityOpen = false;

    private ActivityInfo currentActivity;
    private List<ActivityInfo> activityList;

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

    public void SelectActivity(int index)
    {
        CloseMenu();

        OpenActivity(activityList[index]);
    }

    public void OnHover(int index)
    {
        activityDescriptionText.text = activityList[index].description;
    }

    public void OpenMenu(List<ActivityInfo> activities)
    {
        if (activitySelectMenu.activeInHierarchy || menuOpen) { return; }

        activityList = activities;

        activitySelectMenu.SetActive(true);

        for (int i = 0; i < activities.Count; i++)
        {
            buttons[i].SetActive(true);
            texts[i].text = activityList[i].title;
        }

        SetMenuOpen(true);
    }

    public void CloseMenu()
    {
        if (!activitySelectMenu.activeInHierarchy) { return; }

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetActive(false);
        }

        activitySelectMenu.SetActive(false);

        SetMenuOpen(false);
    }

    public void OpenActivity(ActivityInfo info)
    {
        if(activityMenu.activeInHierarchy || activityOpen) { return; }

        currentActivity = info;

        activityMenu.SetActive(true);

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

        SetMenuOpen(true);

        activityOpen = true;
    }

    public void CloseActivity()
    {
        if(!activityMenu.activeInHierarchy) { return; }

        activityMenu.SetActive(false);

        SetMenuOpen(false);

        activityOpen = false;
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
        if (!activityMenu.activeInHierarchy) { return; }

        TimeOfDay.Instance.IncreaseTime(currentActivity.hours);

        for (int i = 0; i < currentActivity.modifiers.Count; i++)
        {
            PlayerStats.Instance.ModifyStat(currentActivity.modifiers[i]);
        }

        CloseActivity();
    }

    private void OnDestroy()
    {
        if (this == _instance)
        {
            _instance = null;
        }
    }
}
