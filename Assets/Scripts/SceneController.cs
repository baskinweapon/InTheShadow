using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    
    public int menu;

    public Action OnLoadMenu;
    
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
