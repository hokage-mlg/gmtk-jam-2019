using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DynamicLightInOut : MonoBehaviour
{
    private new Light2D light;
    private float startIntensity;
    [SerializeField]
    private float lifeSpanIn = 0.25f;
    [SerializeField]
    private float lifeSpanOut = 0.25f;
    private float lifeSpanLeftIn;
    private float lifeSpanLeftOut;

    [SerializeField]
    private bool shouldFadeOut = true;

    // Start is called before the first frame update
    void Awake()
    {
        light = GetComponent<Light2D>();
        startIntensity = light.intensity;
        lifeSpanLeftIn = lifeSpanIn;
        lifeSpanLeftOut = lifeSpanOut;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeSpanLeftIn > 0)
        {
            lifeSpanLeftIn -= Time.deltaTime;
            light.intensity = Mathf.Lerp(startIntensity, 0, lifeSpanLeftIn / lifeSpanIn);
        }
        else if (shouldFadeOut)
        {
            lifeSpanLeftOut -= Time.deltaTime;
            light.intensity = Mathf.Lerp(0, startIntensity, lifeSpanLeftOut / lifeSpanOut);
        }
    }

    public void FadeOut()
    {
        shouldFadeOut = true;
    }
}
