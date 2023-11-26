
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Operation
{
    Sum=0,
    Subtraction=1,
    Multiply=2,
    Division=3
}
public class ChallengeController : MonoBehaviour
{
    [SerializeField] private float _currentDelay=5f;
    [SerializeField] private GameObject _answerPrefab;
    private GameObject _currentQuestion;
    [SerializeField] private QuestionBehaviour _questionBehaviour;
    private AnswerBlock _currentAnswerBlock;
    private Challenge _currentChallenge;
    private Coroutine _makeChallengeCoroutine;
    private List<Challenge> _challengeQueue = new List<Challenge>();
    private void Start()
    {
        ProvideBaseChallenges();
        GameController.OnFindAnswer += OnFindAnswer;
        MakeFirstQuestion(); 
        _makeChallengeCoroutine = StartCoroutine(MakeChallenge());
    }

    private void OnFindAnswer(bool isCorrect)
    {
        print("on find answer ");
        if (isCorrect)
        {
            GameController.Instance.ChangeGameSpeed(0.3f);
            _currentDelay -= 0.1f;
        }
        else
        {
            _currentDelay -= 0.2f;
            GameController.Instance.ChangeGameSpeed(2);
        }
        ShowNextQuestion();
    }

    private IEnumerator MakeChallenge()
    {
        yield return new WaitForSecondsRealtime(_currentDelay);
        SetChallenge();
        if(_makeChallengeCoroutine!=null) StopCoroutine(_makeChallengeCoroutine);
        _makeChallengeCoroutine=StartCoroutine(MakeChallenge());
    }
    private void ShowNextQuestion()
    {
        _questionBehaviour.Set();
    }
    private void MakeFirstQuestion()
    {
        _currentChallenge = _challengeQueue.First();
        CreateAnswer(_currentChallenge);
        _questionBehaviour.Set(_currentChallenge);
    }

    private void ProvideBaseChallenges()
    {
        for (int i = 0; i < 10; i++)
        {
               _currentChallenge = new Challenge();
               _challengeQueue.Add(_currentChallenge);
        }
    }
    private void SetChallenge()
    {
        if (_challengeQueue.Count < 10)
        {
            _currentChallenge = new Challenge();
            _challengeQueue.Add(_currentChallenge);
        }

        _challengeQueue.Remove(_challengeQueue.First());
        CreateAnswer();
    }

    private void CreateAnswer(Challenge challenge)
    {
        var answerRoadBlock = RoadController.Instance.GetRoadBlockByIndex(7);
        Vector3 centerBlockPosition = answerRoadBlock.transform.position;
        centerBlockPosition.z+=(answerRoadBlock._zScale / 2);
        _currentAnswerBlock=Instantiate(_answerPrefab, centerBlockPosition, Quaternion.identity).GetComponent<AnswerBlock>();
        _currentAnswerBlock.Init(challenge);
        _currentAnswerBlock.transform.SetParent(answerRoadBlock.transform);
    }

    //[Serializable]
    public class Challenge
    {
        public Operation Operation;
        public int FirstNumber,SecondNumber;
        public int CorrectAnswer;
        public int WrongAnswer;

        public Challenge ()
        {
            Operation=(Operation)Random.Range(0, 4);
            FirstNumber = Random.Range(0, 101);
            SecondNumber = Random.Range(0, 101);
            CorrectAnswer = GenerateRightAnswer(Operation, FirstNumber, SecondNumber);
            WrongAnswer =
                GenerateWrongAnswer((Operation)Random.Range(0, 4), Random.Range(0, 101), Random.Range(0, 101));
        }

        private int GenerateRightAnswer(Operation operation, int firstNumber, int secondNumber)
        {
            switch (operation)
            {
                case Operation.Sum:
                    return firstNumber + secondNumber;
                break;
                case Operation.Subtraction:
                    return firstNumber - secondNumber;
                break;
                case Operation.Multiply:
                    return firstNumber * secondNumber;
                break;
                case Operation.Division:
                    return firstNumber / secondNumber;
                break;
                default:
                    return 0;
                break;
            }
        }
        private int GenerateWrongAnswer(Operation operation, int firstNumber, int secondNumber)
        {
            switch (operation)
            {
                case Operation.Sum:
                    return firstNumber + secondNumber;
                    break;
                case Operation.Subtraction:
                    return firstNumber - secondNumber;
                    break;
                case Operation.Multiply:
                    return firstNumber * secondNumber;
                    break;
                case Operation.Division:
                    return firstNumber / secondNumber;
                    break;
                default:
                    return 0;
                    break;
            }
        }
    
    }
}

