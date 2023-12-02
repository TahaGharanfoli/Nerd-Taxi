   using TMPro;
   using UnityEngine;
   using UnityEngine.UI;

   public class QuestionBehaviour : MonoBehaviour
   {
       [SerializeField] private TextMeshPro _questionText;
        public void Set(ChallengeController.Challenge challenge)
        {
            _questionText.text = $"{challenge.FirstNumber}  {GetOperationSign(challenge.Operation)}  {challenge.SecondNumber} = {challenge.Answer} ";
        }

        private string GetOperationSign(Operation operation)
        {
            switch (operation)
            {
                case Operation.Sum:
                    return "+";
                    break;
                case Operation.Subtraction:
                    return "-";
                    break;
                case Operation.Multiply:
                    return "X";
                    break;
                case Operation.Division:
                    return "/";
                    break;
                default:
                    return "";
                    break;
            }
        }
    }
