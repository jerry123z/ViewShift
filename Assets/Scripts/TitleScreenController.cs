using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    private GameObject keyMap;

    private void Start()
    {
        keyMap = GameObject.Find("/Title Screen/KeyMapping");
        if (keyMap)
        {
            keyMap.SetActive(false);
        }
    }
    public void startGame()
    {
        SceneManager.LoadScene("AlphaLevel");
    }

    public void option()
    {
        keyMap.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            keyMap.SetActive(false);
        }
    }
}
