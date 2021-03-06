using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityInteract : MonoBehaviour
{
    public string menuSoundsName;

    public List<ActivityInfo> activities;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            ActivityManager.Instance.interactText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E) && !ActivityManager.Instance.menuOpen)
            {
                ActivityManager.Instance.OpenMenu(activities, menuSoundsName);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            ActivityManager.Instance.interactText.SetActive(false);
        }
    }
}
