using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Level : MonoBehaviour
{
    private float _levelTime;
    [SerializeField] private int objectsCount = 1;

    [SerializeField] private Transform[] objects;

    [Range(0, 100)]
    [SerializeField]
    private float timeTreeStars;

    private LevelData _levelData;
    private bool end = false;

    public static Action OnFinishLevel;
    public static Action OnEnd;
    public static Action<VisualEffect, GameObject> OnMultyObjectFinish;
    public static Action<int> OnSetDifficult;

    private int difficult;
    
    private void Start()
    {
        OnEnd += End;
        OnFinishLevel += WinGame;
        OnMultyObjectFinish += MultyObjectFinish;
        _levelData = GameManager.Instance.GetLevelData(SceneController.instance.GetLevelID());
        _levelTime = 0;
        difficult = _levelData.dificult;
        OnSetDifficult?.Invoke(difficult);
    }

    private List<GameObject> visuals = new List<GameObject>();
    private List<VisualEffect> vfxs = new List<VisualEffect>();
    
    private void MultyObjectFinish(VisualEffect vfx, GameObject visual)
    {
        visuals.Add(visual);
        vfxs.Add(vfx);
        if (visuals.Count == objectsCount)
        {
            end = true;
            for (int i = 0; i < visuals.Count; i++)
            {
                visuals[i].SetActive(false);
                vfxs[i].enabled = true;
                Destroy(vfxs[i].gameObject, 5f);
            }
            WinGame();
        }
    }

    private void End()
    {
        end = true;
    }
    
    private void WinGame()
    {
        AudioManager.Instance.PlayWinLevelAudio();
        end = true;
        _levelData.time = _levelTime;
        _levelData.score = SetStars();
        var nextLevel = GameManager.Instance.GetLevelData(SceneController.instance.GetLevelID() + 1);
        if (nextLevel != null)
            nextLevel.isOpen = true;
        StartCoroutine(ReturnToMenu());
    }
    
    private IEnumerator ReturnToMenu()
    {
        Menu.OnFinishLevel?.Invoke();
        yield return new WaitForSeconds(5f);
        SceneController.instance.LoadMenu();
    }

    private Coroutine _coroutine;
    private void SwapPositions()
    {
        if (_coroutine != null)
            return;
        if (objects.Length >= 2)
        {
           _coroutine =  StartCoroutine(SmoothCoroutine());
        }
    }

    private Vector3 vel;
    IEnumerator SmoothCoroutine()
    {
        var pos = objects[0].position;
        var pos2 = objects[1].position;
        while (objects[1].position != pos)
        {
            objects[0].position = Vector3.Lerp(objects[0].position, pos2, 0.2f);
            objects[1].position = Vector3.Lerp(objects[1].position, pos, 0.2f);
            yield return null;
        }

        _coroutine = null;
    }
    
    void Update()
    {
        if (GameManager.Instance._GameState == GameState.Play)
            _levelTime += Time.deltaTime;
        if (end)
            return;
        if (difficult != 2)
            return;
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
            SwapPositions();
    }

    private int SetStars()
    {
        if (_levelTime <= timeTreeStars)
            return 3;
        else if (_levelTime <= timeTreeStars * 2)
            return 2;
        else
            return 1;
    }

    private void OnDestroy()
    {
        OnEnd -= End;
        OnMultyObjectFinish -= MultyObjectFinish;
        OnFinishLevel -= WinGame;
    }
}
