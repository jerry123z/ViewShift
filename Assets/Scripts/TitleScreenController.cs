using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    private bool state;
    private GameObject keyMap;

    private void Start()
    {
        keyMap = GameObject.Find("/Canvas/KeyMapping");
        keyMap.SetActive(false);
        state = false;
    }
    public void startGame()
    {
        SceneManager.LoadScene("AlphaLevel");
    }

    public void option()
    {
        keyMap.SetActive(!state);
    }

    public void quitGame()
    {
        Application.Quit();
    } 
}
