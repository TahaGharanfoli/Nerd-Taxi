using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshPro _answerText;
    private bool _isCorrectAnswer;
    public void Init(int value,bool isCorrectAnswer)
    {
        _answerText.text = $"{value}";
        _isCorrectAnswer = isCorrectAnswer;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameController.OnFindAnswer?.Invoke(_isCorrectAnswer);
            Destroy(transform.parent.gameObject);
    }
}
