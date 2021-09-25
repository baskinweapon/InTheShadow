using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> hideObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneController.instance.OnChangeScene += Hide;
    }

    public void Hide()
    {
        foreach (var obj in hideObjects)
        {
            obj.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        SceneController.instance.OnChangeScene -= Hide;
    }
}
