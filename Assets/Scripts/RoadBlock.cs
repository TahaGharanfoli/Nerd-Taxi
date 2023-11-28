using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{ 
    [SerializeField] private Material _roadMaterial;
   [SerializeField] private float _moveSpeed;
   [SerializeField] public float _zScale;
   

   private void OnEnable()
   {
       _moveSpeed = GameController.Instance.GameSpeed;
       // GameController.OnChangeGameSpeed -= OnChangeSpeed;
       GameController.OnChangeGameSpeed += OnChangeSpeed;
   }

   private void OnDisable()
   {
       GameController.OnChangeGameSpeed -= OnChangeSpeed;
   }

   public void Init()
   {
       // GameController.OnChangeGameSpeed -= OnChangeSpeed;
       // GameController.OnChangeGameSpeed += OnChangeSpeed;
   }
   private void OnChangeSpeed(float speed)
   {
       print("Road Block changed speed");
       _moveSpeed = speed;
   }
   private void Update()
   {
       transform.Translate(0,0,-(_moveSpeed*Time.deltaTime));
   }
}
