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

    public int numShows = 2;

    public List<string> showNames;

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
        float fmVal = fmKnob.value * numShows;

        radioEvent.setParameterByName("RadioChannelSelector", fmVal);
        radioEvent.setParameterByName("RadioVolume", volKnob.value);

        fmText.text = fmVal.ToString();
        volText.text = (volKnob.value * 100).ToString();

        for (int i = 0; i < numShows; i++)
        {
            if(fmVal >= i + 0.2 && fmVal <= i + 0.8)
            {
                stationText.text = showNames[i];
                return;
            }
        }

        stationText.text = "No Signal...";
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
