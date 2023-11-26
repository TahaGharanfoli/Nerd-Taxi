using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float GameSpeed;
    public static GameController Instance;
    public static Action<float> OnChangeGameSpeed;
    public static Action<bool> OnFindAnswer;
     private void Awake()
     {
        Instance = this;
     }
     public void ChangeGameSpeed(float delta)
     {
         GameSpeed += delta;
         OnChangeGameSpeed?.Invoke(GameSpeed);
     }

}
