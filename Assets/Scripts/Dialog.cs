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
    public Camera mainCam;
    public GameObject finishLine;
    bool finished;
    Vector3 playerPos;
    Vector3 cameraDirection;
    Coroutine typeCoroutine = null;
    Coroutine wait = null;
    Coroutine freezing = null;

    public AudioClip typing;
    private AudioSource audioSource;
    public float volume = 0.2f;
    public GameObject rock;

    private void Start()
    {
        dialog = GameObject.Find("/Dialog/Dialogues");
        player = GameObject.Find("/Player"); 
        finishLine = GameObject.Find("/Anchors/finish line");
        cubes = GameObject.Find("/Level Objects/Standing Places").GetComponentsInChildren<Transform>();
        mainCam = GameObject.Find("/Main Camera").GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();

        for(int a = 0; a < cubes.Length-1; a++)
        {
            cubes[a] = cubes[a + 1];
        }
        Array.Resize(ref cubes, cubes.Length - 1);
        finished = false;
        cameraDirection = mainCam.transform.forward;
        dialog.SetActive(true);
        continueBtn.SetActive(false);
        removeAllPlaces();
        Invoke("freeze", 0.7f);
        if (typeCoroutine != null)
        {
            StopCoroutine(typeCoroutine);
        }
        audioSource.PlayOneShot(typing, volume); 
        typeCoroutine = StartCoroutine(Type());

    }

    private void Update()
    {
        playerPos = player.transform.position;
        if (textDisplay.text == sentences[index])
        {
            continueBtn.SetActive(true);
        }
        if (!dialog.activeSelf && wait == null && finished == false)
        {
            player.GetComponent<PlayerMover>().enabled = true;
            print(lineIndex);
            if(lineIndex == 4)
            {
                wait = StartCoroutine(WaitForSelection());
            } else if (lineIndex == 5)
            {
                wait = StartCoroutine(WaitForRotation());
            } else
            {
                wait = StartCoroutine(WaitForPosition());
            }
            
            freezing = null;
            Time.timeScale = 1;
        } else if (!dialog.activeSelf && wait == null && finished)
        {
            player.GetComponent<PlayerMover>().enabled = true;
            Time.timeScale = 1;
        }

        if (Input.GetButtonDown("Jump") && continueBtn.activeSelf && dialog.activeSelf)
        {
            NextSentence();
        }
        
    }

    IEnumerator Type()
    {
        if(index != 0)
        {
            player.GetComponent<PlayerMover>().enabled = false;
        }
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
            else {
                audioSource.PlayOneShot(typing, volume);
            }
            textDisplay.text = "";
            if (typeCoroutine != null)
            {
                StopCoroutine(typeCoroutine);
            }
            typeCoroutine = StartCoroutine(Type());
            
        } else
        {
            dialog.SetActive(false);
            finished = true;
        }
    }

    IEnumerator WaitForPosition()
    {
        print("waiting");
        Transform cube = cubes[lineIndex-1];
        cube.gameObject.SetActive(true);
        Vector3 designatedPos = cube.position;
        yield return new WaitUntil(() => checkPosition(designatedPos, playerPos));
        if(freezing == null)
        {
            audioSource.PlayOneShot(typing, volume);
            freezing = StartCoroutine(freezeAndActive());
        }
    }

    IEnumerator WaitForRotation()
    {
        print("waitingRotate");
        yield return new WaitUntil(() => rotate());
        if (freezing == null)
        {
            audioSource.PlayOneShot(typing, volume);
            freezing = StartCoroutine(freezeAndActive());
        }
    }

    IEnumerator WaitForSelection()
    {
        print("Waiting Select");
        yield return new WaitUntil(() => selection());
        if (freezing == null)
        {
            audioSource.PlayOneShot(typing, volume);
            freezing = StartCoroutine(freezeAndActive());
        }
    }

    public bool selection()
    {
        RelativeRotatorData rrd = rock.GetComponent<RelativeRotatorData>();
        return rrd.willRotate;
    }

    public bool rotate()
    {
        Vector3 facing = mainCam.transform.forward;
        float curDirZ = -1 * Mathf.Round(cameraDirection.z * 10.0f) * 0.1f;
        float facingZ = Mathf.Round(facing.z * 10.0f) * 0.1f;
        bool result = facingZ == curDirZ;
        return result;
    }

    public bool checkPosition(Vector3 designatedPos, Vector3 playerPos)
    {
        bool x = ((designatedPos.x - cubeSize) < playerPos.x) && (playerPos.x < (designatedPos.x + cubeSize))
            && (designatedPos.y < playerPos.y) && (playerPos.y < (designatedPos.y + 2 * cubeSize))
            && ((designatedPos.z - cubeSize) < playerPos.z) && (playerPos.z < (designatedPos.z + cubeSize));
        
        return x;
    }

    IEnumerator freezeAndActive()
    {
        yield return new WaitForSeconds(0.3f);
        wait = null;
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        textDisplay.text = "";
        dialog.SetActive(true);
        continueBtn.SetActive(false);
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

    public void freeze()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
    }
}
