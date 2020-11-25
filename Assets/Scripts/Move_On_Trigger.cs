using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_On_Trigger : MonoBehaviour
{
    public bool secondPos;
    public Vector3 whereToMove;
    private Vector3 normalPos;

    // Start is called before the first frame update
    void Start()
    {
        secondPos = false;
        normalPos = transform.localPosition;
    }

    public void ToNewPos()
    {
        secondPos = true;
        transform.localPosition = whereToMove;
    }

    public void ToOldPos()
    {
        secondPos = false;
        transform.localPosition = normalPos;
    }
}
