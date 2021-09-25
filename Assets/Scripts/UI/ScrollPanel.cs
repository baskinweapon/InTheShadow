using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLevel;

    [SerializeField] private Image[] starImages;
    [SerializeField] private TextMeshProUGUI timeLabel;
    [SerializeField] private TMP_Dropdown dificultDropDown;
    
    [SerializeField] private Sprite starImage;
    [SerializeField] private Sprite yellowStarImage;

    public void SetData(LevelDataScriptble levelData)
    {
        nameLevel.text = levelData.nameLevel;
        TimeSpan time = TimeSpan.FromSeconds(levelData.time);
        timeLabel.text = time.ToString("hh':'mm':'ss");
        dificultDropDown.value = levelData.dificult;
        for (int i = 0; i < levelData.score; i++)
        {
            starImages[i].sprite = yellowStarImage;
        }
    }
}
