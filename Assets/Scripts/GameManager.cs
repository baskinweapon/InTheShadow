using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_TYPE
{
    GAME_START,
    GAME_PLAY,
    GAME_PAUSE,
};

public class GameManager : MonoBehaviour
{
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
}
