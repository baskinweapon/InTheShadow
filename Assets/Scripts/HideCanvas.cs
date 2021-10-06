using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    public static Action OnHideCanvas;


    public void OnEnable()
    {
        OnHideCanvas += HideCanvasFunc;
    }

    public void HideCanvasFunc()
    {
        canvas.SetActive(false);
    }

    private void OnDestroy()
    {
        HideCanvasFunc();
        OnHideCanvas -= HideCanvasFunc;
    }
}
