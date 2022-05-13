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

    public float screenFadeSpeed = 1f;
    public float soundFadeSpeed = 0.001f;

    FMOD.Studio.Bus worldSounds;

    bool fadeIn = false;
    bool fadeOut = false;

    bool fadeSoundIn = false;
    bool fadeSoundOut = false;

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

        worldSounds = FMODUnity.RuntimeManager.GetBus("bus:/World");
    }

    void Update()
    {
        if (fadeIn)
        {
            blackScreen.alpha -= screenFadeSpeed * Time.deltaTime;
            if(blackScreen.alpha <= 0)
            {
                blackScreen.alpha = 0;
                fadeIn = false;
            }
        }
        else if (fadeOut)
        {
            blackScreen.alpha += screenFadeSpeed * Time.deltaTime;

            if (blackScreen.alpha >= 1)
            {
                blackScreen.alpha = 1;
                fadeOut = false;
            }
        }

        if (fadeSoundIn)
        {
            currentVolume += soundFadeSpeed * Time.deltaTime;

            if (currentVolume > desiredVolume)
            {
                currentVolume = desiredVolume;
                worldSounds.setVolume(currentVolume);
                return;
            }

            worldSounds.setVolume(currentVolume);
        }
        else if (fadeSoundOut)
        {
            currentVolume -= soundFadeSpeed * Time.deltaTime;

            if (currentVolume < 0)
            {
                currentVolume = 0;
                worldSounds.setVolume(currentVolume);
                return;
            }

            worldSounds.setVolume(currentVolume);
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
        fadeOut = false;
        FadeSoundIn();
    }

    public void FadeOut()
    {
        fadeOut = true;
        fadeIn = false;
    }

    public void FadeSoundIn()
    {
        fadeSoundIn = true;
        fadeSoundOut = false;
        currentVolume = 0;
    }

    public void FadeSoundOut()
    {
        fadeSoundOut = true;
        fadeSoundIn = false;

        worldSounds.getVolume(out desiredVolume);
        currentVolume = desiredVolume;
    }
}
