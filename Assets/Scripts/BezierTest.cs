using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BezierTest : MonoBehaviour
{
    public Transform p0;
    public Transform p1;
    public Transform p2;
    public Transform p3;

    [Range(0, 1)]
    public float t;

    public float speed = 5f;


    public void TransformToCamera()
    {
        StopAllCoroutines();
        StartCoroutine(CourutineToCamera());
    }

    public void TransformToStartPosition()
    {
        StopAllCoroutines();
        StartCoroutine(CourutineToStartPosition());
    }

    IEnumerator CourutineToCamera()
    {
        while(t <= 1)
        {
            yield return null;
            t += Time.deltaTime / speed;
            transform.position = Bezier.GetPoint(p0.position, p1.position, p2.position, p3.position, t);
        }
    }

    IEnumerator CourutineToStartPosition()
    {
        while (t >= 0)
        {
            yield return null;
            t -= Time.deltaTime / speed;
            transform.position = Bezier.GetPoint(p0.position, p1.position, p2.position, p3.position, t);
        }
    }
    // void Update()
    // {
    //     if (t > 1)
    //         return;
    //     t += Time.deltaTime / 10f;
    //     transform.position = Bezier.GetPoint(p0.position, p1.position, p2.position, p3.position, t);
    //     // transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(p0.position, p1.position, p2.position, p3.position, t));
    // }
    
    private void OnDrawGizmos()
    {
        int segmentNumber = 20;
        Vector3 previosePoint = p0.position;

        for (int i = 0; i < segmentNumber + 1; i++)
        {
            float param = (float) i / segmentNumber;
            Vector3 point = Bezier.GetPoint(p0.position, p1.position, p2.position, p3.position, param);
            Gizmos.DrawLine(previosePoint, point);
            previosePoint = point;
        }
    }
}
