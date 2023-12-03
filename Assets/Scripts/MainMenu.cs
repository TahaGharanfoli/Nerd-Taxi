using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private Transform _target;
   [SerializeField] private float _speed;

   private void Update()
   {
      _target.Rotate(0,10*_speed*Time.deltaTime,0);
   }
}
