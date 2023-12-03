using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardRow : MonoBehaviour
{
    [SerializeField] private Text _userNameText;
    [SerializeField] private Text _indexText;
    [SerializeField] private Text _scoreText;

    public void Init(string userName, int index, int score)
    {
        _userNameText.text = userName;
        _indexText.text = $"{index}";
        _scoreText.text = $"{score}";
    }
}
