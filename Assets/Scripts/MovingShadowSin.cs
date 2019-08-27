using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obsolete, destroy when new shadow is added
/// </summary>
public class MovingShadowSin : MonoBehaviour
{
    private float startScale;

    void Start()
    {
        startScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float val = startScale - Mathf.Abs(Mathf.Sin(Time.time * 6)) / 4;
        transform.localScale = new Vector3(val, val, val);
    }
}
