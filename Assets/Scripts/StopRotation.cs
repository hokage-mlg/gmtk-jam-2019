using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0);

    private static Vector3 zeroVector = new Vector3(0, 0, 0);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.eulerAngles = zeroVector;
        transform.position = transform.parent.position + offset;
    }
}
