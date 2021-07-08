using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors: MonoBehaviour
{
   public static Colors _instance;
   
   [ColorUsage(true, true)]
   public Color[] colorGradients;
   
   private void Awake()
   {
      if (_instance == null)
      {
         _instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
         DestroyImmediate(this);
   }
}
