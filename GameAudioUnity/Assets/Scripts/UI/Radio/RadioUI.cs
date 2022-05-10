using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioUI : MonoBehaviour
{
    public GameObject radioUI;

    [SerializeField] FMODUnity.StudioEventEmitter radioAudio;

    [SerializeField] FMOD.Studio.EventInstance radioEvent;

    [SerializeField] CircleSlider fmKnob;

    [SerializeField] CircleSlider volKnob;

    [SerializeField] TMP_Text stationText;

    [SerializeField] TMP_Text fmText;

    [SerializeField] TMP_Text volText;

    private void Start()
    {
        radioEvent = radioAudio.EventInstance;
    }

    void Update()
    {
        SetUI();

        if(!radioUI.activeInHierarchy) { return; }

        SetValues();
    }

    void SetValues()
    {
        float fmVal = fmKnob.value * 2;

        radioEvent.setParameterByName("RadioChannelSelector", fmVal);
        radioEvent.setParameterByName("RadioVolume", volKnob.value);

        fmText.text = fmVal.ToString();
        volText.text = (volKnob.value * 100).ToString();

        if(fmVal >= 0.2 && fmVal <= 0.8)
        {
            stationText.text = "Radio 1";
        } 
        else if (fmVal >= 1.2 && fmVal <= 1.8)
        {
            stationText.text = "Radio 1xtra";
        }
        else
        {
            stationText.text = "No Signal...";
        }
    }

    void SetUI()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (radioUI.activeInHierarchy)
            {
                ActivityManager.Instance.SetMenuOpen(false);
                radioUI.SetActive(false);
            }
            else
            {
                if(ActivityManager.Instance.menuOpen) { return; }
                ActivityManager.Instance.SetMenuOpen(true);
                radioUI.SetActive(true);
            }
        }
    }
}
