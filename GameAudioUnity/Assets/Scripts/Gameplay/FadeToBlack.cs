using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    private static FadeToBlack _instance;

    public static FadeToBlack Intstance { get { return _instance; } }

    [SerializeField] FMODUnity.StudioEventEmitter radioAudio;

    private FMOD.Studio.EventInstance radioEvent;

    public CanvasGroup blackScreen;

    public float screenFadeSpeed = 10f;
    public float radioFadeSpeed = 10f;

    bool fadeIn = false;
    bool fadeOut = false;

    private float desiredVolume;
    private float currentVolume;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        radioEvent = radioAudio.EventInstance;
    }

    void Update()
    {
        if (fadeIn)
        {
            blackScreen.alpha -= screenFadeSpeed * Time.deltaTime;
            currentVolume += radioFadeSpeed * Time.deltaTime;

            if (currentVolume > desiredVolume) currentVolume = desiredVolume;

            radioEvent.setParameterByName("RadioVolume", currentVolume);
            if(blackScreen.alpha <= 0)
            {
                blackScreen.alpha = 0;
                fadeIn = false;
            }
        }
        else if (fadeOut)
        {
            blackScreen.alpha += screenFadeSpeed * Time.deltaTime;
            currentVolume -= radioFadeSpeed * Time.deltaTime;

            radioEvent.setParameterByName("RadioVolume", currentVolume);

            if (blackScreen.alpha >= 1)
            {
                blackScreen.alpha = 1;
                fadeOut = false;
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
        fadeOut = false;
    }

    public void FadeOut()
    {
        fadeOut = true;
        fadeIn = false;

        radioEvent.getParameterByName("RadioVolume", out desiredVolume);
        currentVolume = desiredVolume;
    }
}
