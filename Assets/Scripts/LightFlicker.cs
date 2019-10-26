using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private Vector2 FlickerValueRange = new Vector2(-0.02f, 0.02f);
    [SerializeField]
    private Vector2 FlickerPeriodRange = new Vector2(0.25f, 0.25f);
    [SerializeField]
    private float SinFactor = 0.3f;

    // Start is called before the first frame update
    void Awake()
    {
        light = GetComponent<Light2D>();
        startingIntensity = light.intensity;
        timeToFlicker = 0;
        flickerValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = startingIntensity + Mathf.Sin(Time.time) * SinFactor + flickerValue;

        timeToFlicker -= Time.deltaTime;
        if (timeToFlicker <= 0)
        {
            flickerValue = Random.Range(FlickerValueRange.x, FlickerValueRange.y);
            timeToFlicker = Random.Range(FlickerPeriodRange.x, FlickerPeriodRange.y);
        }
    }

    new private Light2D light;
    private float startingIntensity;
    private float timeToFlicker;
    private float flickerValue;
}
