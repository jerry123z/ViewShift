using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScreenController : MonoBehaviour
{
    private GameObject keyMap;
    private GameObject newGame;
    private GameObject options;
    private GameObject exit;
    private int index;

    bool isMoving = false;

    private void Start()
    {
        keyMap = GameObject.Find("/Title Screen/KeyMapping");
        newGame = GameObject.Find("/Title Screen/New Game/Functionality");
        options = GameObject.Find("/Title Screen/Options/Functionality");
        exit = GameObject.Find("/Title Screen/Exit/Functionality");
        index = 1;

        if (keyMap)
        {
            keyMap.SetActive(false);
        }
        MoveSelector(index);
    }
    public void startGame()
    {
        SceneManager.LoadScene("LevelSelect");
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
        float yAxis = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Select In View"))
        {
            keyMap.SetActive(false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            functions(index);
        }

        if (yAxis > 0)
        {
            Select("up");
        }
        else if (yAxis < 0)
        {
            Select("down");
        }
    }

    void Select(string direction)
    {
        if (isMoving == false)
        {
            isMoving = true;
            if (direction == "up")
            {
                index -= 1;
            }
            else if (direction == "down")
            {
                index += 1;
            }
            MoveSelector(index);

            StartCoroutine(ResetMove(0.2f));
        }
    }

    void MoveSelector(int index)
    {
        if(index == 1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(newGame);
        }
        else if (index == 2)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(options);
        }
        else if (index == 3)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(exit);
        }
    }

    public IEnumerator ResetMove(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        isMoving = false;
    }

    void functions(int ind)
    {
        if (ind == 1)
        {
            startGame();
        }
        else if(ind == 2)
        {
            option();
        }
        else if(ind == 3)
        {
            quitGame();
        }
    }
}
