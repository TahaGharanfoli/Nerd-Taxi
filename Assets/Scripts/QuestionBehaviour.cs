   using TMPro;
   using UnityEngine;
   using UnityEngine.UI;

   public class QuestionBehaviour : MonoBehaviour
   {
       [SerializeField] private TextMeshPro _questionText;
        public void Init(Operation operation, int firstNumber ,int secondNumber)
        {
            _questionText.text = $"{firstNumber}  {GetOperationSign(operation)}  {secondNumber}";
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
