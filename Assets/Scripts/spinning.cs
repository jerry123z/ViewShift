﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinning : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,90f*Time.deltaTime,0f);
    }
}
