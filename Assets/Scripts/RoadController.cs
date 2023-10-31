using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
   [SerializeField] private Material _roadMaterial;
   [SerializeField] private float _moveSpeed;
   [SerializeField] private float _zScale;
   private void Start()
   {
      _roadMaterial = GetComponent<Renderer>().material;
   }

   private void Update()
   {
      float offsetX = Time.time * _moveSpeed; 
      // float offsetY = Time.time * 0.2f;
      _roadMaterial.mainTextureOffset = new Vector2(offsetX,0);
   }
}
