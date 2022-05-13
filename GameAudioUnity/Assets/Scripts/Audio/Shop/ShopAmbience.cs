using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAmbience : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter shopAudio;
    private FMOD.Studio.EventInstance instance;

    bool increaseParam = false;
    bool decreaseParam = false;

    float currentVal = 0f;
    float desiredVal = 0f;

    float blendSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        shopAudio = GetComponent<FMODUnity.StudioEventEmitter>();
        instance = shopAudio.EventInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (increaseParam)
        {
            currentVal += blendSpeed * Time.deltaTime;

            if (currentVal >= desiredVal) {
                currentVal = desiredVal;
                increaseParam = false;
                instance.setParameterByName("InsideOutsideShop", currentVal);
                return; 
            }

            instance.setParameterByName("InsideOutsideShop", currentVal);
        }
        else if (decreaseParam)
        {
            currentVal -= blendSpeed * Time.deltaTime;

            if(currentVal <= desiredVal) {
                currentVal = desiredVal;
                decreaseParam = false;
                instance.setParameterByName("InsideOutsideShop", currentVal);
                return; 
            }

            instance.setParameterByName("InsideOutsideShop", currentVal);
        }
    }

    public void SetInsinde()
    {
        increaseParam = true;
        decreaseParam = false;
        desiredVal = 1f;
    }

    public void SetOutside()
    {
        decreaseParam = true;
        increaseParam = false;

        desiredVal = 0f;
    }

    public void SetMid()
    {
        if (currentVal > 0.5f)
        {
            decreaseParam = true;
            increaseParam = false;
        }
        else
        {
            increaseParam = true;
            decreaseParam = false;
        }

        desiredVal = 0.5f;
    }
}
