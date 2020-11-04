using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject pauseScreen;

    void Start()
    {
        pauseScreen = GameObject.Find("Pause Screen/PauseScreenCanvas/PauseScreen");
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void pausing()
    {
        if(Time.timeScale > 0)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale > 0)
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
