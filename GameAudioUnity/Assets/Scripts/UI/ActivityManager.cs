using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public struct ActivityInfo
{
    [Header("Info")]
    public string title;
    [TextArea]
    public string description;
    [TextArea]
    public string effects;

    public int hours;

    [Header("Sound")]
    public string soundName;

    public float soundTime;

    public UnityEvent parameterFunctions;

    [Header("Effects")]
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

    public ActivityInfo currentActivity;
    private List<ActivityInfo> activityList;

    public FMOD.Studio.EventInstance eventInstance { get; private set; }
    public FMOD.Studio.EventInstance menuEventInstance { get; private set; }

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

    public void ResumeGame()
    {
        menuEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        CloseMenu();

        FadeToBlack.Intstance.FadeIn();
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

    public void OpenMenu(List<ActivityInfo> activities, string menuSound)
    {
        if (activitySelectMenu.activeInHierarchy || menuOpen) { return; }

        menuEventInstance = FMODUnity.RuntimeManager.CreateInstance(menuSound);

        menuEventInstance.start();

        activityList = activities;

        activitySelectMenu.SetActive(true);

        for (int i = 0; i < activities.Count; i++)
        {
            buttons[i].SetActive(true);
            texts[i].text = activityList[i].title;
        }

        FadeToBlack.Intstance.FadeSoundOut();

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

        menuEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

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

        CloseActivity();

        StartCoroutine(ActivitySound());

        for (int i = 0; i < currentActivity.modifiers.Count; i++)
        {
            PlayerStats.Instance.ModifyStat(currentActivity.modifiers[i]);
        }
    }

    void PlaySound()
    {
        eventInstance.start();
        eventInstance.release();
    }

    private IEnumerator ActivitySound()
    {
        menuOpen = true;

        int modifierNum = currentActivity.modifiers.Count;

        FadeToBlack.Intstance.FadeOut();

        eventInstance = FMODUnity.RuntimeManager.CreateInstance(currentActivity.soundName);

        if(currentActivity.parameterFunctions != null)
        {
            currentActivity.parameterFunctions.Invoke();
        }

        yield return new WaitForSeconds(1f);

        PlaySound();

        yield return new WaitForSeconds(currentActivity.soundTime);

        FadeToBlack.Intstance.FadeIn();

        while(currentActivity.modifiers.Count > modifierNum)
        {
            currentActivity.modifiers.RemoveAt(currentActivity.modifiers.Count - 1);
        }

        menuOpen = false;
    }

    private void OnDestroy()
    {
        if (this == _instance)
        {
            _instance = null;
        }
    }
}
