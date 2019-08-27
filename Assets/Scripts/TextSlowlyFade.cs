using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSlowlyFade : MonoBehaviour
{
    [SerializeField]
    private float FadeDuration = 3;
    [SerializeField]
    private Text text = null;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!text)
        {
            text.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color prevColor = text.color;
        prevColor.a -= Time.deltaTime / FadeDuration;
        text.color = prevColor;
    }
}
