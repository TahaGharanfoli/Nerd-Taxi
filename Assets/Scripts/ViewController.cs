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
    [SerializeField] private GameObject _inputPage;
    [SerializeField] private AudioSource _correctAudio;
    [SerializeField] private AudioSource _wrongAudio;
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
            _correctAudio.Play();
            _scoreValue++;
            _scoreText.text = $"{_scoreValue}";
            if (_healthValue < 1)
            {
                _healthValue += 0.1f;
            }
            _healthImage.fillAmount = _healthValue;
             ControlGameEvent(_scoreValue);
        }
        else
        {
            _wrongAudio.Play();
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
            print("first state ");
            return;
        }
        else if (score > _gameEventList[0].Score && score < _gameEventList[1].Score)
        {
            print("second state ");
            GameController.Instance.ChangeGameSpeed(_gameEventList[0].GameSpeed);
        }
        else if (score > _gameEventList[1].Score && score < _gameEventList[2].Score)
        {
            print("third state ");
            GameController.Instance.ChangeGameSpeed(_gameEventList[1].GameSpeed);
        }
        else
        {
            print("forth state ");
            GameController.Instance.ChangeGameSpeed(_gameEventList[2].GameSpeed);
        }
    }

    private void ShowEndPage()
    {
        _inputPage.SetActive(false);
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
            StartCoroutine(NetworkManager.UpdateScore(PlayerPrefs.GetString("UserId"), score, null, null));
        }
    }
    public void OnClickReplay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickReturnHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    [Serializable]
    public class GameEvent
    {
        public int Score;
        public int GameSpeed;
    }
}