
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
    [SerializeField] private float _currentDelay=7f;
    [SerializeField] private GameObject _answerPrefab;
    private GameObject _currentQuestion;
    [SerializeField] private QuestionBehaviour _questionBehaviour;
    [SerializeField] private float _maxAnswerDistance;
    private AnswerBlock _currentAnswerBlock;
    private Challenge _currentChallenge;
    private Coroutine _makeChallengeCoroutine;
    private Queue<Challenge> _answerQueue = new Queue<Challenge>();
    private Queue<Challenge> _questionQueue = new Queue<Challenge>();
    private ViewController _viewController;
    private Queue<AnswerBlock> _answerBlockQueue = new Queue<AnswerBlock>();
    private int _lastAnswerRoadBlockIndex;
    private void Start()
    {
        _viewController=GetComponent<ViewController>();
        GameController.OnFindAnswer += OnFindAnswer;
        ProvideBaseChallenges();
        CreateBaseAnswers();
        _viewController.InitFirst();
    }

    private void OnFindAnswer(bool isCorrect)
    {
        // print("on find answer ");
        ShowNextQuestion();
        SetChallenge();
    }

    // private IEnumerator MakeChallenge()
    // {
    //     yield return new WaitForSecondsRealtime(_currentDelay);
    //     SetChallenge();
    //     print("Makeeeeeeeeeeeeeeee challenge ");
    //     if(_makeChallengeCoroutine!=null) StopCoroutine(_makeChallengeCoroutine);
    //     _makeChallengeCoroutine=StartCoroutine(MakeChallenge());
    // }
    private void ShowNextQuestion()
    {
        if (_questionQueue.Count > 0)
        {
            _questionBehaviour.Set(_questionQueue.Dequeue());
        }
    }

    private void CreateBaseAnswers()
    {
        int index = 3;
        for (int i = 0; i < 3; i++)
        {
            _currentChallenge = _answerQueue.Dequeue();
             CreateAnswer(_currentChallenge,index);
             index += 3;
             _questionQueue.Enqueue(_currentChallenge);
        }
        _questionBehaviour.Set(_questionQueue.Dequeue());
    }

    private void ProvideBaseChallenges()
    {
        for (int i = 0; i < 10; i++)
        {
               _currentChallenge = new Challenge();
               _answerQueue.Enqueue(_currentChallenge);
        }
    }
    private void SetChallenge()
    {
        _answerBlockQueue.Dequeue();
        if (_answerQueue.Count < 10)
        {
            _currentChallenge = new Challenge();
            _answerQueue.Enqueue(_currentChallenge);
        }
        
        var challenge = _answerQueue.Dequeue();
        if (_answerBlockQueue.Count<3)
        {
         CreateAnswer(challenge,9);   
        }
        _questionQueue.Enqueue(challenge);
         // CreateAnswer(challenge);
    }
    private void CreateAnswer(Challenge challenge,int roadBlockIndex)
    {
        var answerRoadBlock = RoadController.Instance.GetRoadBlockByIndex(roadBlockIndex);
        Vector3 centerBlockPosition = answerRoadBlock.transform.position;
        centerBlockPosition.z+=(answerRoadBlock._zScale / 2);
        _currentAnswerBlock=Instantiate(_answerPrefab, centerBlockPosition, Quaternion.identity).GetComponent<AnswerBlock>();
        _answerBlockQueue.Enqueue(_currentAnswerBlock);
        _currentAnswerBlock.Init(challenge);
        _currentAnswerBlock.transform.SetParent(answerRoadBlock.transform);
       _viewController.AddAnswerQueue(_currentAnswerBlock.transform);
       _lastAnswerRoadBlockIndex = roadBlockIndex;
    }
    
    //[Serializable]
    public class Challenge
    {
        public Operation Operation;
        public int FirstNumber,SecondNumber;
        public int Answer;
        public bool IsCorrectAnswer;

        public Challenge ()
        {
            Operation=(Operation)Random.Range(0, 3);
            FirstNumber = Random.Range(0, 101);
            SecondNumber = Random.Range(0, 101);
            if (Random.Range(0, 2) == 0)
            {
                Answer = GenerateCorrectAnswer(Operation, FirstNumber, SecondNumber);
                IsCorrectAnswer = true;
            }
            else
            {
                Answer=GenerateWrongAnswer((Operation)Random.Range(0, 3), Random.Range(0, 101), Random.Range(0, 101));
                IsCorrectAnswer = false;
            }
        }

        private int GenerateCorrectAnswer(Operation operation, int firstNumber, int secondNumber)
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
                {
                    if (firstNumber > secondNumber)
                    {
                        return firstNumber / secondNumber;
                    }

                    return firstNumber + secondNumber;
                }
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

