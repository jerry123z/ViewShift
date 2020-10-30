using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;

    private void OnMouseUp()
    {
        if (isStart)
        {
            SceneManager.LoadScene("demoScene2");
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.green;
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }
}
