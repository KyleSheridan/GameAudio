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
}
