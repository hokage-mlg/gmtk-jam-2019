using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public CameraFollowScript cameraFollow;
    public Transform playerTransform;

    private void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }
}