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

    public static Action OnOpenAllLevels;
    
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

    private List<bool> saverIsOpen = new List<bool>();
    public void ChangeAllLevel(bool state)
    {
        if (state)
        {
            for (int i = 0; i < GameManager.Instance.levelCount; i++)
            {
                var levelData = GameManager.Instance.GetLevelData(i);
                saverIsOpen.Add(levelData.isOpen);
                levelData.isOpen = true;
            }
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.levelCount; i++)
            {
                var levelData = GameManager.Instance.GetLevelData(i);
                levelData.isOpen = saverIsOpen[i];
            }
            saverIsOpen.Clear();
        }
        OnOpenAllLevels?.Invoke();
        PlayerPrefs.SetInt("ChangeLVL", state ? 1 : 0);
    }
}
