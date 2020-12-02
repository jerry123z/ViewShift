using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RelativeRotatorData : MonoBehaviour
{
    public bool usesGravity;
    public bool willRotate;

    void Start(){

        if (gameObject.GetComponent<Rigidbody>()) {
            usesGravity = gameObject.GetComponent<Rigidbody>().useGravity;
        } else{
            usesGravity = false;
        }

    }
}