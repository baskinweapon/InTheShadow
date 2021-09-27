using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class ShapeBehavior : MonoBehaviour
{
    [SerializeField] private VisualEffect alphaEffect;
    [SerializeField] private GameObject visual;
    [SerializeField] private Light pointLight;
    
    private Collider _collider;
    private Vector3 pos;

    [SerializeField]
    private bool multyObject;

    private void Start()
    {
        pos = transform.position;
        alphaEffect.enabled = false;
        _collider = GetComponent<Collider>();
    }

    private bool end;
    private void Update()
    {
        if (end)
            return;
        if (transform.rotation.x >= -0.1f && transform.rotation.x <= 0.1f &&
            transform.rotation.y >= -0.1f && transform.rotation.y <= 0.1f &&
            transform.rotation.z >= -0.1f && transform.rotation.z <= 0.1f)
        {
            if (multyObject && Vector3.Distance(pos, transform.position) <= 0.1f)
            {
                StartCoroutine(WinGameCoroutine());
                _collider.enabled = false;
                end = true;
            }
            else if (!multyObject)
            {
                StartCoroutine(WinGameCoroutine());
                _collider.enabled = false;
                end = true;
            }
        }
    }

    private float time;
    private IEnumerator WinGameCoroutine()
    {
        Level.OnEnd?.Invoke();
        while (time <= .05f)
        {
            time += Time.deltaTime * 0.01f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime);
            pointLight.spotAngle = 21f + 500f * time;
            yield return null;
        }

        if (multyObject)
        {
            Level.OnMultyObjectFinish(alphaEffect, visual);
            yield break;
        }
        
        AudioManager.Instance.PlayWinLevelAudio();
        visual.SetActive(false);
        alphaEffect.enabled = true;
        Destroy(alphaEffect.gameObject, 5f);
        
        Level.OnFinishLevel?.Invoke();
    }

    
}
