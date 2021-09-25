using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private TMP_FontAsset fontOverObj;
    private TMP_FontAsset _standartFont;

    private void Start()
    {
        _standartFont = textMeshPro.font;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshPro.font = fontOverObj;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.font = _standartFont;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        textMeshPro.font = _standartFont;
    }
}
