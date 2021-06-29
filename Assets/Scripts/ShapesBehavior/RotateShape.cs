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


    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;
        
        transform.Rotate(Vector3.up, -rotX, Space.World);
        transform.Rotate(Vector3.right, -rotY, Space.World);
        // transform.RotateAround(Vector3.up, -rotX);
        // transform.RotateAround(Vector3.right, -rotY);
    }
}

