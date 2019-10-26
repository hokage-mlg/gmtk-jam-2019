using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAbsBubbleSound : MonoBehaviour
{   
    void Start()
    {
        var audio = GetComponent<AudioSource>();
        AudioManager.Play("Block", audio);
    } 
}
