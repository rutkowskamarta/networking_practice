using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerVisualisationChoiceUI : MonoBehaviour
    {
        [SerializeField]
        private Button nextButton;
        [SerializeField]
        private Button preivousButton;

        public int CurrentOption { get; private set; }

        private int optionsAmount;
        private Action<int> OnOptionChanged;

		public void Initialize(int optionsAmount, int currentOption, Action<int> OnOptionChanged)
		{
            this.optionsAmount = optionsAmount;
            this.CurrentOption = currentOption;

            this.OnOptionChanged = OnOptionChanged;

            nextButton.onClick.AddListener(NextButton_OnClick);
            preivousButton.onClick.AddListener(PreviousButton_OnClick);
        }

        public void Deinitialize()
		{
            nextButton.onClick.RemoveListener(NextButton_OnClick);
            preivousButton.onClick.RemoveListener(PreviousButton_OnClick);
        }

        private void NextButton_OnClick()
		{
            CurrentOption = CurrentOption + 1 >= optionsAmount ? optionsAmount - 1 : CurrentOption + 1;
            OnOptionChanged?.Invoke(CurrentOption);
        }

        private void PreviousButton_OnClick()
		{
            CurrentOption = CurrentOption - 1 <= 0 ? 0 : CurrentOption - 1;
            OnOptionChanged?.Invoke(CurrentOption);
		}
	}
}