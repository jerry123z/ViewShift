using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class RelativeRotatorSystem : ComponentSystem
{
    Camera_Controller cameraController;

    protected override void OnUpdate()
    {
        var camera = GameObject.Find("Main Camera");
        cameraController = camera.GetComponent<Camera_Controller>();
        if (cameraController.isRotating) {
            Entities.ForEach((ref Translation translation) => {
                translation.Value = RotateAroundPoint(translation.Value, cameraController.center.position, new Vector3(0, 3f * cameraController.direction, 0));
            });
        }
    }
  
    private Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; 
        dir = Quaternion.Euler(angles) * dir; 
        point = dir + pivot; 
        return point;
    }
}
