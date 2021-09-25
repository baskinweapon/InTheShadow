using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private List<AudioClip> musicClips;
    [SerializeField] private AudioClip _startLevelAudio;
    [SerializeField] private AudioClip _finishLevelAudio;
    
    public static AudioManager Instance
    {
        get => instance;
        set {}
    }

    private static AudioManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    private void Start()
    {
        _audioSource.volume = PlayerPrefs.GetFloat("Volume");
    }
    

    public void ChangeVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    public void PlayStartLEvelAudio()
    {
        _audioSource.PlayOneShot(_startLevelAudio);
    }

    public void PlayFinishLevelAudio()
    {
        _audioSource.PlayOneShot(_finishLevelAudio);
    }
}
