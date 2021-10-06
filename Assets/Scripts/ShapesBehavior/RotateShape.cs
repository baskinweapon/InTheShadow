using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateShape : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationDamping;

    private float _rotationVelocity;
    private bool _dragged;

    [SerializeField] private bool hideTutor = false;

    private int difficult;
    private void OnEnable()
    {
        Level.OnSetDifficult += SetDifficult;
    }
    
    private void SetDifficult(int difficult)
    {
        this.difficult = difficult;
        if (difficult == 0)
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
        }

        if (difficult != 2 && SceneController.instance.GetLevelID() > 1)
        {
            HideCanvas.OnHideCanvas?.Invoke();
            hideTutor = true;
        }
    }

    private void OnMouseDrag()
    {
        if (!hideTutor)
        {
            HideCanvas.OnHideCanvas?.Invoke();
            hideTutor = true;
        }
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;
        
        
        transform.Rotate(Vector3.up, -rotX, Space.World);
        if (difficult == 0)
            return;
        transform.Rotate(Vector3.forward, rotY, Space.World);
    }

    private void OnDisable()
    {
        Level.OnSetDifficult -= SetDifficult;
    }
}

