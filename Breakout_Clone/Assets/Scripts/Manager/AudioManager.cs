using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    private float savedVolume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }

        Play("BGM");
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);


        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void Mute()
    {
        mixerGroup.audioMixer.GetFloat("MyExposedParam", out savedVolume);
        mixerGroup.audioMixer.SetFloat("MyExposedParam", -80f);
    }
    public void Unmute()
    {
        mixerGroup.audioMixer.SetFloat("MyExposedParam", savedVolume);
    }

}
