using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScreenController : MonoBehaviour
{
    private GameObject keyMap;
    private GameObject optionScreen;
    private GameObject newGame;
    private GameObject options;
    private GameObject exit;
    private int index;
    public AudioClip moveSfx;
    public AudioClip selectSfx;
    protected AudioSource audioSource;

    bool isMoving;

    private void Start()
    {
        keyMap = GameObject.Find("/Title Screen/KeyMapping");
        optionScreen = GameObject.Find("/Title Screen/Option Screen");
        newGame = GameObject.Find("/Title Screen/New Game/Functionality");
        options = GameObject.Find("/Title Screen/Options/Functionality");
        exit = GameObject.Find("/Title Screen/Exit/Functionality");
        isMoving = false;
        index = 1;
        audioSource = GetComponent<AudioSource>();

        if (keyMap)
        {
            keyMap.SetActive(false);
        }
        if (optionScreen)
        {
            optionScreen.SetActive(false);
        }
        MoveSelector(index);
    }
    public void startGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void option()
    {
        optionScreen.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Select In View"))
        {
            optionScreen.SetActive(false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            audioSource.PlayOneShot(selectSfx, 0.2f);
            functions(index);
        }

        if (xAxis > 0)
        {
            Select("right");
        }
        else if (xAxis < 0)
        {
            Select("left");
        }
    }

    void Select(string direction)
    {
        if (isMoving == false)
        {
            
            isMoving = true;
            if (direction == "right" && index < 3)
            {
                audioSource.PlayOneShot(moveSfx, 0.2f);
                index += 1;
            }
            else if (direction == "left" && index > 1)
            {
                audioSource.PlayOneShot(moveSfx, 0.2f);
                index -= 1;
            }
            MoveSelector(index);

            StartCoroutine(ResetMove(0.3f));
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
