using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{ 
    [SerializeField] private Material _roadMaterial;
   [SerializeField] private float _moveSpeed;
   [SerializeField] public float _zScale;

   public void Init(Action<float> onChangeSpeed)
   {
       onChangeSpeed += OnChangeSpeed;
   }
   private void OnRemove(Action<float> onChangeSpeed)
   {
       onChangeSpeed -= OnChangeSpeed;
   }

   private void OnChangeSpeed(float speed)
   {
       _moveSpeed = speed;
   }
   private void Update()
   {
       transform.Translate(0,0,-(_moveSpeed*Time.deltaTime));
   }
}
