using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus worldMusic;
    FMOD.Studio.Bus menuMusic;
    FMOD.Studio.Bus worldSFX;
    FMOD.Studio.Bus menuSFX;
    FMOD.Studio.Bus Master;
    float musicVolume = 0.5f;
    float sfxVolume = 0.5f;
    float masterVolume = 1f;

    float soundWaitTime = .6f;
    bool canPlaySound = true;

    void Awake()
    {
        worldMusic = FMODUnity.RuntimeManager.GetBus("bus:/World/Music");
        worldSFX = FMODUnity.RuntimeManager.GetBus("bus:/World/SFX");
        menuMusic = FMODUnity.RuntimeManager.GetBus("bus:/Menu/Music");
        menuSFX = FMODUnity.RuntimeManager.GetBus("bus:/Menu/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
        SFXVolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance("event:/UI/SFXVolumeTest");
    }

    // Update is called once per frame
    void Update()
    {
        worldMusic.setVolume(musicVolume);
        menuMusic.setVolume(musicVolume);
        worldSFX.setVolume(sfxVolume);
        menuSFX.setVolume(sfxVolume);
        Master.setVolume(masterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        masterVolume = newMasterVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING && canPlaySound)
        {
            SFXVolumeTestEvent.start();
            StartCoroutine(SoundWait());
        }
    }
    public void MusicVolumeLevel(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
    }
    public void SFXVolumeLevel(float newSFXVolume)
    {
        sfxVolume = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING && canPlaySound)
        {
            SFXVolumeTestEvent.start();
            StartCoroutine(SoundWait());
        }
    }

    IEnumerator SoundWait()
    {
        canPlaySound = false;

        yield return new WaitForSeconds(soundWaitTime);

        canPlaySound = true;
    }
}
