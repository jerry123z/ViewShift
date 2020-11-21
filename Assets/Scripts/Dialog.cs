using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject continueBtn;
    public GameObject dialog;
    public string[] sentences;
    public int[] lineBreaks;
    public GameObject cube;
    public GameObject player;
    private int index;
    private int lineIndex;
    public float cubeSize;
    public float typingSpeed;
    private bool typing;

    private void Start()
    {
        dialog = GameObject.Find("/Dialog/Dialogues");
        player = GameObject.Find("/Player");
        cube = GameObject.Find("/Level Objects/Standing Place");
        dialog.SetActive(true);
        continueBtn.SetActive(false);
        typing = false;
        if (!typing)
        {
            StartCoroutine(Type());
        }
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            typing = false;
            continueBtn.SetActive(true);
        }
        if (!dialog.activeSelf)
        {
            StartCoroutine(WaitForPosition());
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Return) && continueBtn.activeSelf)
        {
            NextSentence();
        }
    }

    IEnumerator Type()
    {
        print(1);
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        typing = true;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueBtn.SetActive(false);
        if (index < sentences.Length-1)
        {
            index += 1;
            if (index == lineBreaks[lineIndex])
            {
                typing = false;
                dialog.SetActive(false);
            }
            textDisplay.text = "";
            if (!typing)
            {
                StartCoroutine(Type());
            }
        } else
        {
            textDisplay.text = "";
            continueBtn.SetActive(false);
        }
    }

    IEnumerator WaitForPosition()
    {
        Vector3 designatedPos = cube.transform.position;
        Vector3 playerPos = player.transform.position;
        yield return new WaitUntil(() => ((designatedPos.x - cubeSize) < playerPos.x) && (playerPos.x < (designatedPos.x + cubeSize)) 
            && (designatedPos.y < playerPos.y) && (playerPos.y < (designatedPos.y + 2*cubeSize)) 
            && ((designatedPos.z - cubeSize) < playerPos.z) && (playerPos.z < (designatedPos.z + cubeSize)) );
        Invoke("freezeAndActive", 0.5f);
    }

    public void freezeAndActive()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        textDisplay.text = "";
        dialog.SetActive(true);
        if (!typing)
        {
            StartCoroutine(Type());
        }
    }
}
