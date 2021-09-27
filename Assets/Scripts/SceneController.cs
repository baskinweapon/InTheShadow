using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

public class SceneController : MonoBehaviour
{
    public static SceneController instance
    {
        get => _instance;
        set {}
    }

    private static SceneController _instance;

    private int level_id;
    public int menu;
    
    private Action OnLoadMenu;
    public Action OnChangeScene;
    
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Canvas canvasLoadScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI pressAnyButton;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        DontDestroyOnLoad(loadingScreen);
        startPosLoadingScreen = loadingScreen.transform.position;
        DontDestroyOnLoad(canvasLoadScreen.gameObject);
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
        LoadingScreen(menu);
    }

    public void LoadLevel(int scene_id)
    {
        level_id = scene_id;
        GameManager.Instance.GetLevelData(scene_id);
        var id = scene_id + 2; 
        LoadingScreen(id);
    }

    private Vector3 startPosLoadingScreen;
    private void LoadingScreen(int scene_id)
    {
        OnChangeScene?.Invoke();
        Camera _camera = Camera.main;
        if (_camera != null)
        { 
            _camera.cullingMask = 1 << LayerMask.NameToLayer("LoadingScreen");
            _camera.transform.position = Vector3.zero;
            _camera.transform.rotation = Quaternion.identity;
        }
        loadingScreen.SetActive(true);
        loadingScreen.transform.position = startPosLoadingScreen;
        canvasLoadScreen.gameObject.SetActive(true);
        StartCoroutine(LoadAsync(scene_id));
    }
    
    private IEnumerator LoadAsync(int scene_id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene_id);
        
        asyncLoad.allowSceneActivation = false;
        
        while (!asyncLoad.isDone)
        {
            slider.value = asyncLoad.progress;
            if (asyncLoad.progress >= .9f && !asyncLoad.allowSceneActivation)
            {
                slider.value = 1;
                StartCoroutine(PressAnyButttonAnim());
                loadingScreen.transform.position += Vector3.back * Time.deltaTime;
                if (Input.anyKeyDown || loadingScreen.transform.position.z < -.5f)
                {
                    asyncLoad.allowSceneActivation = true;
                    loadingScreen.SetActive(false);
                    canvasLoadScreen.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }

    private float _time;
    private int _sign = 1;
    private IEnumerator PressAnyButttonAnim()
    {
        while (true)
        {
            _time += _sign * Time.deltaTime * 0.001f;
            pressAnyButton.alpha = _time;
            if (_time >= 1)
                _sign = -1;
            if (_time <= 0)
                _sign = 1;
            yield return null;
        }
    }

    public int GetLevelID()
    {
        return level_id;
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
}
