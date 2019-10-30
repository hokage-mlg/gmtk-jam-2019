using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField]
    float cameraMoveSpeed = 2f;

    [SerializeField]
    private Vector4 CameraMoveBound = new Vector4(-18, 18, -15, 15);
        

    private Func<Vector3> GetCameraFollowPositionFunc;
    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;
        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);
            if (distanceAfterMoving > distance)
            {
                newCameraPosition = cameraFollowPosition;
            }
            transform.position = new Vector3(Mathf.Clamp(newCameraPosition.x, CameraMoveBound.x, CameraMoveBound.y), 
                Mathf.Clamp(newCameraPosition.y, CameraMoveBound.z, CameraMoveBound.w), transform.position.z);
        }
    }
}