using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private int ScoreValue = 0;
    private void Start()
    {
        GameController.OnFindAnswer += OnFindAnswer;
    }

    private void OnFindAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            ScoreValue += 100;
        }
        else
        {
            if (ScoreValue - 70 < 0) ScoreValue = 0;
            else
            {
                ScoreValue -= 70;
            }
        }

        _scoreText.text = $"Score : {ScoreValue}";
    }
}
