using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("AlphaLevel");
    }

    public void quitGame()
    {
        Application.Quit();
    } 
}
