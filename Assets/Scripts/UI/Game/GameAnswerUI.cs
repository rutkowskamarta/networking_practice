using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class GameAnswerUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text categoryName;
        [SerializeField]
        private TMP_InputField answerInputField;

        public void Initialize(string categoryNameString)
        {
            categoryName.SetText(categoryNameString);
        }

        public string GetAnswer()
        {
            return answerInputField.text;
        }
    }
}