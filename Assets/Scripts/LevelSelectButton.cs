using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField]
    GameObject Selector;

    [SerializeField]
    GameObject[] row1;

    const int rows = 1;
    const int cols = 5;

    Vector2 positionIndex;
    GameObject currentSlot;

    bool isMoving = false;

    public GameObject[,] grid = new GameObject[rows, cols];
    public int level = 1;
    void Start()
    {
        AddRowToGrid(0, row1);
        positionIndex = new Vector2(0, 0);
        currentSlot = grid[0, 0];
    }

    public void AddRowToGrid(int index, GameObject[] row)
    {
        print("success");
        for(int i = 0; i < cols; i++)
        {
            grid[index, i] = row[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        if(xAxis > 0)
        {
            moveSelector("right");
        }
        else if(xAxis < 0)
        {
            moveSelector("left");
        }
        else if (yAxis > 0)
        {
            moveSelector("up");
        }
        else if (yAxis < 0)
        {
            moveSelector("down");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadLevel();
        }
    }

    public void moveSelector(string direction)
    {
        if (isMoving == false)
        {
            isMoving = true;
            if(direction == "right")
            {
                if(positionIndex.x < cols - 1)
                {
                    positionIndex.x += 1;
                    level += 1;
                }
                //else
                //{
                //    positionIndex.x = 0;
                //    positionIndex.y += 1;
                //}
            }
            else if (direction == "left")
            {
                if (positionIndex.x > 0)
                {
                    positionIndex.x -= 1;
                    level -= 1;
                }
                //else
                //{
                //    positionIndex.x = cols - 1;
                //    positionIndex.y -= 1;
                //}
            }
            else if (direction == "up")
            {
                if (positionIndex.y > 0)
                {
                    positionIndex.y -= 1;
                    level -= cols;
                    if (level < 0)
                    {
                        level = 0;
                    }
                }
                //else
                //{
                //    positionIndex.y = rows - 1;
                //    positionIndex.x -= 1;
                //}
            }
            else if (direction == "down")
            {
                if (positionIndex.y < rows - 1)
                {
                    positionIndex.y += 1;
                    level += cols;
                }
                //else
                //{
                //    positionIndex.y = 0;
                //    positionIndex.x += 1;
                //}
            }

            currentSlot = grid[(int)positionIndex.y, (int)positionIndex.x];
            Selector.transform.position = currentSlot.transform.position;

            Invoke("ResetMoving", 0.2f);
        }
    }

    public void ResetMoving()
    {
        isMoving = false;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(level);
    }
}
