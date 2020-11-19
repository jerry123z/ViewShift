using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    int levelIndex;
    public int index;
    private GameObject pauseScreen;
    bool isMoving = false;

    private GameObject restart;
    private GameObject contBtn;
    private GameObject main;
    private GameObject exit;
    void Start()
    {
        pauseScreen = GameObject.Find("Pause Screen/PauseScreenCanvas/PauseScreen");
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        index = 0;
        restart = GameObject.Find("/Pause Screen/PauseScreenCanvas/PauseScreen/MenuWindow/Restart");
        contBtn = GameObject.Find("/Pause Screen/PauseScreenCanvas/PauseScreen/MenuWindow/Continue");
        main = GameObject.Find("/Pause Screen/PauseScreenCanvas/PauseScreen/MenuWindow/Main Screen");
        exit = GameObject.Find("/Pause Screen/PauseScreenCanvas/PauseScreen/MenuWindow/QuitButton");
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void returnMain()
    {
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void pausing()
    {
        if (Time.timeScale > 0)
        {
            print("true");
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            print(GameObject.Find("/Pause Screen/MenuManager"));
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0)
            {
                pauseScreen.SetActive(true);
                initialPause();
                Time.timeScale = 0;
            }
            else
            {
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
            }
        }
        float yAxis = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Return))
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
        if (index == 1)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(restart);
        }
        else if (index == 2)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(contBtn);
        }
        else if (index == 3)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(main);
        }
        else if (index == 4)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(exit);
        }
    }

    void functions(int ind)
    {
        switch (ind)
        {
            case 4:
                quit();
                break;
            case 3:
                returnMain();
                break;
            case 2:
                pausing();
                break;
            case 1:
                restartGame();
                break;
        }
    }

    void initialPause()
    {
        index = 1;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restart);
    }

    public IEnumerator ResetMove(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        isMoving = false;
    }
}
