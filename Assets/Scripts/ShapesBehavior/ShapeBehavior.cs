using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShapeBehavior : MonoBehaviour
{
    [SerializeField] private VisualEffect alphaEffect;
    [SerializeField] private GameObject visual;
    [SerializeField] private Material wallMaterial;

    private void Start()
    {
        alphaEffect.enabled = false;
    }

    private bool end;
    private void Update()
    {
        if (end)
            return;
        if (transform.rotation.x >= -0.5f && transform.rotation.x <= 0.5f &&
            transform.rotation.y >= -0.5f && transform.rotation.y <= 0.5f &&
            transform.rotation.z >= -0.5f && transform.rotation.z <= 0.5f)
        {
            StartCoroutine(WinGameCouroutine());
            end = true;
        }
    }

    private float time;
    private IEnumerator WinGameCouroutine()
    {
        while (transform.rotation != Quaternion.identity)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0,0 ,1), 1);
            yield return null;
        }
        
        visual.SetActive(false);
        alphaEffect.enabled = true;
        Destroy(alphaEffect.gameObject, 5f);
    }
}
