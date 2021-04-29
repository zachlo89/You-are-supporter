using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public float allVolume = .5f;
    public float backgroundVolume = .5f;
    public float effectsVolume = 1f;
    private void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            BackgroundVolume(PlayerPrefs.GetFloat("MusicVolume"));
        } else
        {
            BackgroundVolume(.1f);
        }
        if (PlayerPrefs.HasKey("EffectVolume"))
        {
            EffectVolume(PlayerPrefs.GetFloat("EffectVolume"));
        }
        else
        {
            EffectVolume(1f);
        }
        Play("Lobby");
    }



    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }

    public void StopPlaying()
    {
        foreach(Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void AllVolume(float volume)
    {
        foreach(Sound s in sounds)
        {
            s.source.volume = volume;
        }
    }

    public void BackgroundVolume(float volume)
    {
        backgroundVolume = volume;
        Sound s = Array.Find(sounds, sound => sound.name == "BattleSoundTrack");
        s.source.volume = volume;
        s = Array.Find(sounds, sound => sound.name == "Lobby");
        s.source.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void EffectVolume(float volume)
    {
        effectsVolume = volume;
        AllVolume(volume);
        BackgroundVolume(backgroundVolume);
        PlayerPrefs.SetFloat("EffectVolume", volume);
    }
}
