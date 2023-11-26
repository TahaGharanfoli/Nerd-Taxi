using UnityEngine;

public class AnswerBlock : MonoBehaviour
{
    [SerializeField] private AnswerBehaviour[] _answers;
    public void Init(ChallengeController.Challenge challenge)
    {
        if (Random.Range(0,2)==0)
        {
            _answers[0].Init(challenge.CorrectAnswer,true);
            _answers[1].Init(challenge.WrongAnswer,false);
        }
        else
        {
            _answers[0].Init(challenge.WrongAnswer,false);
            _answers[1].Init(challenge.CorrectAnswer,true);
        }
    }
}
