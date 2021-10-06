using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Play,
    Pause,
}

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private List<LevelDataScriptble> levelData;

    private float _gameTime;
    public int levelCount;

    public GameState _GameState = GameState.Play;
    
    
    public static GameManager Instance
    {
        get => instance;
        set {}
    }

    private static GameManager instance = null;

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
        levelCount = levelData.Count;
    }

    private void Update()
    {
        if (_GameState == GameState.Play)
            _gameTime += Time.deltaTime;
    }

    public float GetGameTime()
    {
        return _gameTime;
    }
    
    

    public LevelDataScriptble GetLevelData(int id)
    {
        if (id >= 0 && id < levelData.Count)
            return levelData[id];
        return null;
    }

    public void StartNewGame()
    {
        for (int i = 0; i < levelData.Count; i++)
        {
            var level = levelData[i];
            level.dificult = 0;
            level.score = 0;
            level.time = 0;
            level.isOpen = i > 0 ? false : true;
        }
    }
}
