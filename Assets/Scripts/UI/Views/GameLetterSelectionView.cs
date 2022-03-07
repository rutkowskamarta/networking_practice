using Game.Game;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class GameLetterSelectionView : UIView
    {
        [SerializeField]
        private GameLetterRandomGeneratorUI randomLetterGenerator;
		[SerializeField]
		private Button continueButton;

		[Inject]
		private IGameManager gameManager;

		public override void Show(Action onShownCallback = null)
		{
			base.Show(onShownCallback);
			Initialize();
			randomLetterGenerator.StartLetterGeneration(SendGenerateLetterRequest, GameManager.SupportedLetters);
			gameManager.OnLetterGeneratedResponse += GameManager_OnLetterGeneratedResponse;
			continueButton.onClick.AddListener(ContinueButton_OnClick);
		}

		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
			gameManager.OnLetterGeneratedResponse -= GameManager_OnLetterGeneratedResponse;
			continueButton.onClick.RemoveListener(ContinueButton_OnClick);
		}

		private void Initialize()
		{
			continueButton.interactable = false;
		}

		private void SendGenerateLetterRequest()
		{
			gameManager.SendLetterGenerationRequest();
		}

		private void GameManager_OnLetterGeneratedResponse(char letter)
		{
			randomLetterGenerator.StopLetterGeneration(letter);
			continueButton.interactable = true;
		}

		private void ContinueButton_OnClick()
		{
			uiViewsManager.ShowViewOfType(UIViewType.GameView);
		}
	}
}