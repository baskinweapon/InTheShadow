using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private float _gameTime;
    
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
    
    private void Update()
    {
        _gameTime += Time.deltaTime;
    }

    public float GetGameTime()
    {
        return _gameTime;
    }
}
