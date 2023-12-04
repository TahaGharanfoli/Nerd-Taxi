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
   [SerializeField] private List<Transform> _targetPosition;
   private void OnEnable()
   {
       if (GameController.Instance!=null)
       {
           _moveSpeed = GameController.Instance.GameSpeed;
           GameController.OnChangeGameSpeed += OnChangeSpeed;
       }
   }
       private void Start()
   {
       _moveSpeed = GameController.Instance.GameSpeed;
   }

   private void OnDisable()
   {
       GameController.OnChangeGameSpeed -= OnChangeSpeed;
   }

   public void Init(List<GameObject> items)
   {
       // GameController.OnChangeGameSpeed -= OnChangeSpeed;
       // GameController.OnChangeGameSpeed += OnChangeSpeed;
       int index = 0;
       foreach (var target in _targetPosition)
       {
           Instantiate(items[0], target);
       }
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
