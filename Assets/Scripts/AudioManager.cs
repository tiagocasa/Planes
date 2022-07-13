using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private float volumeGeral;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    private bool once_call;

    // Start is called before the first frame update
    void Awake()
    {
        if (!once_call)
        {
            DontDestroyOnLoad(this);
            once_call = true;
        }
        else
        {

            Destroy(gameObject);

        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixer;

        }

           

        FindObjectOfType<AudioManager>().Play("Musica");
        

    }
        
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();

    }

    //public void Volume(string name, float volume)
    //{
    //    sounds s = Array.Find(sounds, sound => sound.name == name);
    //    s.source.Volume();

    //}

}
