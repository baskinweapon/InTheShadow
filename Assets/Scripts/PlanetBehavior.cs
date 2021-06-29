using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetBehavior : MonoBehaviour
{
   [SerializeField] private int scene_id;
   
   private void OnMouseDown()
   {
      transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
      if (coroutine != null) StopCoroutine(coroutine);
   }

   private void OnMouseUp()
   {
      coroutine = StartCoroutine(ReScale());
   }
   
   private Coroutine coroutine;
   private IEnumerator ReScale()
   {
      while (transform.localScale.x < 0.95f)
      {
         transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime);
         yield return null;
      }

      SceneController.instance.LoadLevel(scene_id);
   }
}
