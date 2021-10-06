using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class ScrollPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLevel;
    [SerializeField] private GameObject stars;
    [SerializeField] private GameObject difficulty;
    [SerializeField] private GameObject bestTime;
    
    [SerializeField] private Image panelImage;
    
    
    [SerializeField] private Image[] starImages;
    [SerializeField] private TextMeshProUGUI timeLabel;
    [SerializeField] private TMP_Dropdown dificultDropDown;
    
    [SerializeField] private Sprite starImage;
    [SerializeField] private Sprite yellowStarImage;

    private LevelDataScriptble _levelData;
    private bool isOpen = false;

    public void SetData(LevelDataScriptble levelData)
    {
        isOpen = levelData.isOpen;
        if (!isOpen)
        {
            nameLevel.text = "Locked";
            return;
        }
        _levelData = levelData;
        nameLevel.text = levelData.nameLevel;
        TimeSpan time = TimeSpan.FromSeconds(levelData.time); 
        timeLabel.text = time.ToString("hh':'mm':'ss");
        dificultDropDown.value = levelData.dificult;
        for (int i = 0; i < levelData.score; i++)
        {
            starImages[i].sprite = yellowStarImage;
        }
    }

    public bool IsOpenLevel()
    {
        return isOpen;
    }

    public void SetLockedLevel()
    {
        stars.SetActive(false);
        bestTime.SetActive(false);
        difficulty.SetActive(false);
        panelImage.color = Color.red;
        
    }

    public void AddOptionsDropDown()
    {
        dificultDropDown.options.Add(new TMP_Dropdown.OptionData
        {
            text = "Hard"
        });
    }

    public void DifficultDropBox(int state)
    {
        _levelData.dificult = state;
    }
    
}
