using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Collider))]
public class Highligher : MonoBehaviour
{
   private MeshRenderer _meshRenderer;

   [SerializeField] 
   private Material originalMat;

   [SerializeField]
   private Material highlightedMat;

   private void Start()
   {
      _meshRenderer = GetComponent<MeshRenderer>();
      
      
   }

   public void EnableHighlight(bool state)
   {
      if (_meshRenderer != null && originalMat != null && highlightedMat != null)
      {
         _meshRenderer.material = state ? highlightedMat : originalMat;
      }
   }

   private void OnMouseOver()
   {
      EnableHighlight(true);
   }

   private void OnMouseExit()
   {
      EnableHighlight(false);
   }
}
