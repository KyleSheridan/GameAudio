using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCollisionFunctions : MonoBehaviour
{
    public enum ShopSounds
    {
        Inside,
        Outside, 
        Mid
    }

    public ShopAmbience shop;

    public ShopSounds type;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            switch (type)
            {
                case ShopSounds.Inside:
                    shop.SetInsinde();
                    break;
                case ShopSounds.Mid:
                    shop.SetMid();
                    break;
                case ShopSounds.Outside:
                    shop.SetOutside();
                    break;
                default:
                    break;
            }
        }
    }
}
