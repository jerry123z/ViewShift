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
    private Transform[] cubes;
    public GameObject player;
    private int index;
    private int lineIndex;
    public float cubeSize;
    public float typingSpeed;
    Coroutine typeCoroutine = null;

    private void Start()
    {
        dialog = GameObject.Find("/Dialog/Dialogues");
        player = GameObject.Find("/Player"); 
        cubes = GameObject.Find("/Level Objects/Standing Places").GetComponentsInChildren<Transform>();
        for(int a = 0; a < cubes.Length-1; a++)
        {
            cubes[a] = cubes[a + 1];
        }
        Array.Resize(ref cubes, cubes.Length - 1);
        dialog.SetActive(true);
        continueBtn.SetActive(false);
        removeAllPlaces();
        if (typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
        }
        typeCoroutine = StartCoroutine(Type());
        
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
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
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
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
                lineIndex += 1;
                removeAllPlaces();
                dialog.SetActive(false);
            }
            textDisplay.text = "";
            if (typeCoroutine != null)
            {
                StopCoroutine(typeCoroutine);
            }
            typeCoroutine = StartCoroutine(Type());
        } else
        {
            textDisplay.text = "";
            continueBtn.SetActive(false);
        }
    }

    IEnumerator WaitForPosition()
    {
        Transform cube = cubes[lineIndex-1];
        cube.gameObject.SetActive(true);
        Vector3 designatedPos = cube.position;
        Vector3 playerPos = player.transform.position;
        yield return new WaitUntil(() => ((designatedPos.x - cubeSize) < playerPos.x) && (playerPos.x < (designatedPos.x + cubeSize)) 
            && (designatedPos.y < playerPos.y) && (playerPos.y < (designatedPos.y + 2*cubeSize)) 
            && ((designatedPos.z - cubeSize) < playerPos.z) && (playerPos.z < (designatedPos.z + cubeSize)) );
        Invoke("freezeAndActive", 0.3f);
    }

    public void freezeAndActive()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        textDisplay.text = "";
        dialog.SetActive(true);
        if (typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
        }
        typeCoroutine = StartCoroutine(Type());
    }

    public void removeAllPlaces()
    {
        foreach (Transform cube in cubes)
        {
            cube.gameObject.SetActive(false);
        }
    }
}
