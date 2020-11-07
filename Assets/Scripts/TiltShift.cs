using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TiltShift : MonoBehaviour
{
    private GameObject camera;
    private MotionBlur motionBlurLayer;

    void Start()
    {
        camera = GameObject.Find("Main Camera");
        PostProcessVolume vol = transform.GetComponent<PostProcessVolume>();
        vol.profile.TryGetSettings(out motionBlurLayer);
    }

    void Update()
    {
        Camera_Controller cameraController = camera.GetComponent<Camera_Controller>();
        if (cameraController.isRotating)
        {
            motionBlurLayer.enabled.value = true;
        }
        else
        {
            motionBlurLayer.enabled.value = false;
        }
    }
}
