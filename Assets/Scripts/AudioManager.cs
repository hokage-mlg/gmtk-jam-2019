using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class AudioManager : MonoBehaviour
{
    public SetVol sliderValue;
    public static Sound[] sounds;
    public Sound[] soundsToRegister;
    public static AudioManager instance;
    public static float slideVal;
    // Name -> (time since last sound, maximum value)
    public static Dictionary<string, Vector2> Clips = new Dictionary<string, Vector2>();

    private const float lowestSoundValue = 0.3f;

    void Awake()
    {
        sounds = soundsToRegister;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static float GetVolume(string name, float volume)
    {
        if (Clips.ContainsKey(name))
        {
            float ltp = Clips[name].x;         
            volume = Mathf.Lerp(lowestSoundValue, Clips[name].y, Mathf.Clamp(Time.time - ltp, 0, 1));          
            Clips[name] = new Vector2(Time.time, Clips[name].y);
        }
        else
        {
            Clips.Add(name, new Vector2(Time.time, volume));
        }
        return volume;
    }

    public static void Play(string name, AudioSource source)
    {
        var slideV = new SetVol();
        slideV.SetLevel(slideVal);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source = source;
        s.source.clip = s.clip;
        s.source.volume = GetVolume(name, s.volume)*slideVal;
        s.source.pitch = s.pitch;
        s.source.panStereo = s.stereoPan;
        s.source.spatialBlend = s.spatialBlend;
        s.source.reverbZoneMix = s.reverbZoneMix;
        s.source.loop = s.loop;
        s.source.mute = s.mute;
        s.source.bypassEffects = s.bypassEffects;
        s.source.bypassReverbZones = s.bypassReverbZones;
        s.source.playOnAwake = s.playOnAwake;
        s.source.dopplerLevel = s.dopplerLevel;
        s.source.spread = s.spread;
        s.source.minDistance = s.minDistance;
        s.source.maxDistance = s.maxDistance;

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (CharacterLife.isDeath == true)
        {
            s.source.volume = s.volume / 2;
        }
        s.source.Play();
    }
    public static void Pause(string name, AudioSource source)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source = source;
        s.source.Pause();
    }
    public static bool isPlaying(string name, AudioSource source)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source = source;
        if (s.source.isPlaying)
            return true;
        else
            return false;
    }
}
