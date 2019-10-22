using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetVol : MonoBehaviour
{    
    public void SetLevel(float sliderValue)
    {
        Debug.LogWarning(sliderValue);      
        AudioManager.slideVal = sliderValue;
    }
}
