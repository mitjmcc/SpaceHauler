using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] music;
    public AudioSource musicSource;
    public int track = 0;
    public bool loopSingle;

    private Settings settings;

    void Start()
    {
        track = track % music.Length;
        musicSource.clip = music[track];
        musicSource.Play();
    }

    void Update()
    {
        if (!musicSource.isPlaying)
        {
            if (!loopSingle)
            {
                track = (track + 1) % music.Length;
            }
            musicSource.clip = music[track];
            musicSource.Play();
        }
        musicSource.volume = settings.volume;
    }

}
