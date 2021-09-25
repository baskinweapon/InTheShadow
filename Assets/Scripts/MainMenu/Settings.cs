using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle changeLevelsTogle;
    
    private void Start()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        changeLevelsTogle.isOn = PlayerPrefs.GetInt("ChangeLVL") > 0 ? true : false;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void ChangeVolume(float value)
    {
        AudioManager.Instance.ChangeVolume(value);
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void ChangeResolution(int value)
    {
        if (value == 0)
            Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
        else if (value == 1)
            Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        else if (value == 2)
            Screen.SetResolution(2560, 1440, FullScreenMode.FullScreenWindow);
        else if (value == 3)
            Screen.SetResolution(3840,2160, FullScreenMode.FullScreenWindow);
        PlayerPrefs.SetInt("Resolution", value);
    }

    public void ChangeAllLevel(bool state)
    {
        print(state ? "Open all Levels" : "Close levels");
        PlayerPrefs.SetInt("ChangeLVL", state ? 1 : 0);
    }
}
