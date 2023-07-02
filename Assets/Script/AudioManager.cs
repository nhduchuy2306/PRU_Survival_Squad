using UnityEngine.Audio;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public static AudioManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (var s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        var s = Array.Find(Sounds, sound => sound.name == name);
        if (s is null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        var s = Array.Find(Sounds, sound => sound.name == name);
        if (s is null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        s.source.Stop();
    }

    public Sound GetSoundByName(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Not found");
            return null;
        }
        else
            return s;
    }

    public void ActiveSoundEffects()
    {
        foreach (var item in Sounds)
        {
            if (item.type == "sfx")
            {
                item.source.mute = false;
            }
        }
    }

    public void DeActiveAllSoundEffects()
    {
        foreach (var item in Sounds)
        {
            if (item.type == "sfx")
            {
                item.source.mute = true;
            }
        }
    }

    public void DeActiveAllTheme()
    {
        Sound[] s = Array.FindAll(Sounds, sound => sound.type == "theme");

        foreach (var item in s)
        {
            item.volume = 0;
            item.source.Stop();
        }
    }

    public void ActiveAllTheme()
    {
        Sound[] s = Array.FindAll(Sounds, sound => sound.type == "theme");

        foreach (var item in s)
        {
            item.volume = 1;
            item.source.Play();
        }
    }
}
