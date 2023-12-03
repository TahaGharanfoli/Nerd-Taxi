using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Image _healthImage;
    [SerializeField] private Slider _distanceSlider;
    [SerializeField] private Transform _currentTargetQuestion;
    [SerializeField] private List<GameEvent> _gameEventList;
    [SerializeField] private Text _bestPlayerScore;
    [SerializeField] private Text _currentPlayerScore;
    [SerializeField] private GameObject _endPage;
    private int _scoreValue = 0;
    private float _healthValue = 1;
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
            _scoreValue++;
            _scoreText.text = $"{_scoreValue}";
            _healthValue += 0.1f;
            _healthImage.fillAmount = _healthValue;
            ControlGameEvent(_scoreValue);
        }
        else
        {
            _healthValue -= 0.3f;
            _healthImage.fillAmount = _healthValue;
            if (_healthValue <= 0)
            {
                ShowEndPage();
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

    private void ControlGameEvent(int score)
    {
        if (score < _gameEventList[0].Score)
        {
            // default
            return;
        }
        else if (score > _gameEventList[0].Score && score < _gameEventList[1].Score)
        {
            GameController.OnChangeGameSpeed?.Invoke(_gameEventList[0].GameSpeed);
        }
        else if (score > _gameEventList[1].Score && score < _gameEventList[2].Score)
        {
            GameController.OnChangeGameSpeed?.Invoke(_gameEventList[1].GameSpeed);
        }
        else
        {
            GameController.OnChangeGameSpeed?.Invoke(_gameEventList[3].GameSpeed);
        }
    }

    private void ShowEndPage()
    {
        _endPage.SetActive(true);
        _currentPlayerScore.text = $"{_scoreValue}";
        _bestPlayerScore.text = $"{GetPlayerBestScore()}";
        SetPlayerBestScore(_scoreValue);
    }
    private int GetPlayerBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
            return PlayerPrefs.GetInt("BestScore");
        return 0;
    }

    private void SetPlayerBestScore(int score)
    {
        if (GetPlayerBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore",score);
        }
        // set network setting 
    }
    public void OnClickReplay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
    [Serializable]
    public class GameEvent
    {
        public int Score;
        public int GameSpeed;
    }
}