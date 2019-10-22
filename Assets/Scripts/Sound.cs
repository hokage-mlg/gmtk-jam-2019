using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;
    [Range(-1f, 1f)]
    public float stereoPan;
    [Range(0f, 1f)]
    public float spatialBlend;
    [Range(0f, 1.1f)]
    public float reverbZoneMix;
    public bool loop;
    public bool mute;
    public bool bypassEffects;
    public bool bypassReverbZones;
    public bool playOnAwake;
    [Range(0f, 5f)]
    public float dopplerLevel;
    [Range(0f, 360f)]
    public float spread;
    public float minDistance;
    public float maxDistance;
    
    [HideInInspector]
    public AudioSource source;
}
