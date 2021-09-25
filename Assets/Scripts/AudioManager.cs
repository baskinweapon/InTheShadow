using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _cosmosSource;

    [SerializeField] private List<AudioClip> musicClips;
    [SerializeField] private AudioClip _startLevelAudio;
    [SerializeField] private AudioClip _finishLevelAudio;
    [SerializeField] private AudioClip _popUpAudio;
    [SerializeField] private AudioClip _swipeAudio;
    
    
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
        _cosmosSource.volume = _audioSource.volume;
    }
    

    public void ChangeVolume(float volume)
    {
        _audioSource.volume = volume;
        _cosmosSource.volume = volume;
    }

    public void PlayStartLEvelAudio()
    {
        _audioSource.PlayOneShot(_startLevelAudio);
    }

    public void PlayFinishLevelAudio()
    {
        _audioSource.PlayOneShot(_finishLevelAudio);
    }

    public void PlayPopUpAudio()
    {
        _audioSource.PlayOneShot(_popUpAudio);
    }

    public void PlaySwipeAudio()
    {
        _audioSource.PlayOneShot(_swipeAudio);
    }
}
