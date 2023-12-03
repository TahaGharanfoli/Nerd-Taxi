using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class AnswerBlock : MonoBehaviour
{
    [SerializeField] private TextMeshPro _question;
    [SerializeField] private Sprite CorrectSprite;
    [SerializeField] private Sprite WrongSprite;
    [SerializeField] private AnswerBehaviour[] _answers;
    public void Init(ChallengeController.Challenge challenge)
    {
         _question.text = $"{challenge.FirstNumber} {challenge.Operation}  {challenge.SecondNumber}";
        if (challenge.IsCorrectAnswer)
        {
            if (Random.Range(0,2)==0)
            {
                _answers[0].Init(CorrectSprite,true);
                _answers[1].Init(WrongSprite,false);
            }
            else
            {
                _answers[0].Init(WrongSprite,false);
                _answers[1].Init(CorrectSprite,true);
            }
        }
        else
        {
            if (Random.Range(0,2)==0)
            {
                _answers[0].Init(CorrectSprite,false);
                _answers[1].Init(WrongSprite,true);
            }
            else
            {
                _answers[0].Init(WrongSprite,true);
                _answers[1].Init(CorrectSprite,false);
            }
        }
    }
}
