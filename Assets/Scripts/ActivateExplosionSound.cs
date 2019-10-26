using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateExplosionSound : MonoBehaviour
{  
    void Start()
    {
        var audio = GetComponent<AudioSource>();
        AudioManager.Play("Explosion", audio);
    }
}
