using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Image _healthImage;
    [SerializeField] private Slider _distanceSlider;
    private int ScoreValue = 0;
    private float _healthValue = 1;
    [SerializeField] private Transform _currentTargetQuestion;
    private float _mainDistance;
    private Queue<Transform> _questionTransformQueue=new Queue<Transform>();
    
    private void OnEnable()
    {
        GameController.OnFindAnswer += OnFindAnswer;
    }
    private void OnDisable()
    {
        GameController.OnFindAnswer -= OnFindAnswer;
    }

    public void InitFirst()
    {
        _currentTargetQuestion = _questionTransformQueue.Dequeue();
        _mainDistance = _currentTargetQuestion.position.z;
    }

    private void OnFindAnswer(bool isCorrect)
    {
        // if (isCorrect)
        // {
        //     ScoreValue += 100;
        // }
        // else
        // {
        //     if (ScoreValue - 70 < 0) ScoreValue = 0;
        //     else
        //     {
        //         ScoreValue -= 70;
        //     }
        // }
        _currentTargetQuestion = _questionTransformQueue.Dequeue();
        _mainDistance = _currentTargetQuestion.position.z;
        if (isCorrect)
        {
            ScoreValue++;
            _scoreText.text = $"{ScoreValue}";
            _healthValue += 0.1f;
            _healthImage.fillAmount = _healthValue;
        }
        else
        {
            _healthValue -= 0.3f;
            _healthImage.fillAmount = _healthValue;
            if (_healthValue <= 0)
            {
                Time.timeScale = 0;
            }
            
        }
    }

    private void Update()
    {
        if (_currentTargetQuestion!=null)
        {
            _distanceSlider.value = ((_currentTargetQuestion.position.z)/_mainDistance);
        }   
    }

    public void AddAnswerQueue(Transform transform)
    {
        _questionTransformQueue.Enqueue(transform);
    }
}
