using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    
    public int menu;
    public int firstLvl;
    public int secondLvl;
    public int thirdLvl;
    public int fourLvl;
    public int fiveLvl;
    public int sixLvl;

    public Action OnLoadMenu;
    public Action OnLoadFirstLvl;
    public Action OnLoadSecondLvl;
    public Action OnLoadThirdLvl;
    public Action OnLoadFourLvl;
    public Action OnLoadFiveLvl;
    public Action OnLoadSixLvl;
    
    private void Awake()
    {
        instance = this;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void Start()
    {
        LoadMenu();
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex == menu)
            OnLoadMenu?.Invoke();
        if (SceneManager.GetActiveScene().buildIndex == firstLvl)
            OnLoadFirstLvl?.Invoke();
        if (SceneManager.GetActiveScene().buildIndex == secondLvl)
            OnLoadSecondLvl?.Invoke();
            
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menu);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
}
