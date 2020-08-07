using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public GameObject Object;
    public Sound[] sounds;

    public static AudioManager instance;    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void PlayDelayed(string name,float delay)
    {
        Create(name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found!");
            return;
        }

        if (!(s.source.isPlaying))
        {
            s.source.PlayDelayed(delay);
        }
        Destroy(s.source, s.source.clip.length + delay);
    }
    public void Play (string name)
    {
        Create(name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
            Debug.LogWarning("Sound" + name + "not found!");
            return;
		}

        if (!(s.source.isPlaying))
        {
            s.source.Play();
        }
    }
    public void Destroy(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found!");
            return;
        }
        Destroy(s.source);
    }   
    public void PlayAndDestroy(string name)
    {
        Create(name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found!");
            return;
        }
        if (!(s.source.isPlaying))
        {
            s.source.Play();
        }
        Destroy(s.source,s.source.clip.length);
    }
    private void Create(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        AudioSource[] temp = gameObject.GetComponents<AudioSource>();

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] == s.source)
            {
                return;
            }
        }

        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found!");
            return;
        }
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.mute = s.mute;
    }
}
