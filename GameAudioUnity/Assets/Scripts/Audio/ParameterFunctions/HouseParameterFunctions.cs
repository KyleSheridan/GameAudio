using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseParameterFunctions : MonoBehaviour
{
    public void PianoParams(int numChordProgressions)
    {
        int num = Random.Range(0, numChordProgressions) + 1;

        ActivityManager.Instance.eventInstance.setParameterByName("ChordProgression", num);
    }

    public void PianoChangeModifiers()
    {
        ActivityManager.Instance.currentActivity.modifiers[0].SetAmount(5);

        Debug.Log(ActivityManager.Instance.currentActivity.modifiers[0].amount);

        ActivityManager.Instance.currentActivity.modifiers.Add(new StatModifier(StatType.Intelligence, 1));
    }
}