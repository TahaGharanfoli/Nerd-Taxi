using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshPro _text;
    private bool _isCorrectAnswer;
    public void Init(Sprite sprite,bool isCorrectAnswer)
    {
        _spriteRenderer.sprite = sprite;
        _isCorrectAnswer = isCorrectAnswer;
        _text.text = "" + isCorrectAnswer;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameController.OnFindAnswer?.Invoke(_isCorrectAnswer);
            Destroy(transform.parent.gameObject);
    }
}
