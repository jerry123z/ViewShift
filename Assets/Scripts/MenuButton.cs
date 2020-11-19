using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private GameObject pauseScreen;

    void Start()
    {
        pauseScreen = GameObject.Find("Pause Screen/PauseScreenCanvas/PauseScreen");
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        
    }
}
