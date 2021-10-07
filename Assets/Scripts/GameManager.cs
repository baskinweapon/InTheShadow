using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum GameState
{
    Play,
    Pause,
}

[Serializable]
public class LevelData
{
    public string nameLevel;
    public bool isOpen;
    public float time;
    public int dificult;
    public int score;
}

public class GameManager : MonoBehaviour
{
    
    //[SerializeField] private List<LevelDataScriptble> levelData;
    
    private LevelData[] _levelData;

    private float _gameTime;
    public int levelCount = 6;

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
        string json = JsonHelper.ReadJsonFromFile(SaveDirectory.Path);
        if (json != null) _levelData = JsonHelper.FromJson<LevelData>(json);
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
    
    

    public LevelData GetLevelData(int id)
    {
        if (id >= 0 && id < _levelData.Length)
            return _levelData[id];
        return null;
    }

    public void StartNewGame()
    {
        for (int i = 0; i < _levelData.Length; i++)
        {
            var level = _levelData[i];
            level.dificult = 0;
            level.score = 0;
            level.time = 0;
            level.isOpen = i > 0 ? false : true;
        }
    }

    public void ReadFile()
    {
        
        if (File.Exists(SaveDirectory.Path))
        {
            var file = File.OpenRead(SaveDirectory.Path);
            print(file);
        }
    }

    public void WriteFile()
    {
        string path = SaveDirectory.Path;
        if (File.Exists(path))
        {
            File.Delete(path);
            var file = File.CreateText(path);
            
            var json = JsonHelper.ToJson(_levelData, true);
            file.Write(json);
            file.Close();
        }
        else
        {
            var file = File.CreateText(path);
            var json = JsonHelper.ToJson(_levelData, true);
            file.Write(json);
            file.Close();
        }
    }

    public void OpenAllLevels()
    {
        for (int i = 0; i < _levelData.Length; i++)
        {
            var level = _levelData[i];
            level.dificult = 0;
            level.score = 0;
            level.time = 0;
            level.isOpen = true;
        }
    }
}
