using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void restartGame()
    {
        SceneManager.LoadScene("demoScene2");
    }

    public void returnMain()
    {
        SceneManager.LoadScene("Title Scene");
    }

    public void quit()
    {
        Application.Quit();
    }
}
