using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!pauseScreen.activeInHierarchy)
            {
                SetPauseScreen(true);
            }
            else
            {
                SetPauseScreen(false);
            }
        }
    }

    public void SetPauseScreen(bool val)
    {
        pauseScreen.SetActive(val);
        ActivityManager.Instance.SetMenuOpen(val);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
