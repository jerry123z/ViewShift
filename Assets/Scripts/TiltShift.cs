using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TiltShift : MonoBehaviour
{
    //GameObject camera;
    private MotionBlur motionBlurLayer;
    public PostProcessResources postProcessResources;

    void Start()
    {
        PostProcessLayer postProcessLayer = Camera.main.gameObject.GetComponent<PostProcessLayer>();
        postProcessLayer.Init(postProcessResources);
        //postProcessLayer.volumeTrigger = Camera.main.transform;
        //postProcessLayer.volumeLayer = LayerMask.GetMask("RotationEffects");

        //camera = transform.parent.gameObject;
        PostProcessVolume vol = transform.GetComponent<PostProcessVolume>();
        vol.profile.TryGetSettings(out motionBlurLayer);
    }

    void Update()
    {
        if (transform.parent.gameObject.GetComponent<Camera_Controller>().isRotating)
        {
            motionBlurLayer.enabled.value = true;
        }
        else
        {
            motionBlurLayer.enabled.value = false;
        }
    }
}
