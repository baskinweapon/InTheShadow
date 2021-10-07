using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [SerializeField] private GameObject panPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Button startLevelButton;
    [SerializeField] private MainMenuStart mainMenuStart;

    [Range(1, 50)]
    public int panCount;
    [Range(0, 500)]
    public int panOffset;
    [Range(0f, 50f)]
    public float snapSpeed;
    [Range(0f, 500f)]
    public float scaleOffset;
    [Range(0f, 50f)]
    public float scaleSpeed;
    

    private GameObject[] instPans;
    private Vector2[] pansPosition;
    
    private RectTransform contentRect;
    private Vector2 contentVector;

    private Vector2[] pansScale;
    
    private int selectedID = 0;
    private bool isScrooling;

    private int maxUnlockLevel = 6;
    
    public void Start()
    {
        instPans = new GameObject[panCount];
        pansPosition = new Vector2[panCount];
        contentRect = content.GetComponent<RectTransform>();
        pansScale = new Vector2[panCount];

        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, content, false);
            var scroolPanel = instPans[i].GetComponent<ScrollPanel>();
            scroolPanel.SetData(GameManager.Instance.GetLevelData(i));

            if (!scroolPanel.IsOpenLevel())
            {
                scroolPanel.SetLockedLevel();
                mainMenuStart.planets[i].GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                maxUnlockLevel = i;
            }
            
            if (i >= 2)
            {
                scroolPanel.AddOptionsDropDown();
            }
            if (i == 0) continue;
            var x = instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x +
                    panOffset;
            var y = instPans[i].transform.localPosition.y;
            instPans[i].transform.localPosition = new Vector2(x, y);
            pansPosition[i] = -instPans[i].transform.localPosition;
        }
        MainMenuStart.OnChooseLvl?.Invoke(selectedID);
    }

    private int prevSelectedID = 1;
    private void FixedUpdate()
    {
        var nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            var distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPosition[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedID = i;
                if (prevSelectedID != selectedID)
                {
                    prevSelectedID = selectedID;
                }
            }

            var scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];
        }

        if (isScrooling) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPosition[selectedID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool state)
    {
        if (state)
        {
            startLevelButton.interactable = false;
            AudioManager.Instance.PlaySwipeAudio();
        }
        else
        {
            MainMenuStart.OnChooseLvl?.Invoke(selectedID);
            if (selectedID <= maxUnlockLevel)
                startLevelButton.interactable = true;
        }
        isScrooling = state;
    }
    
}
