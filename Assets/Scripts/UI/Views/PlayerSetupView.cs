using Game.PlayerData;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class PlayerSetupView : UIView
    {
        [SerializeField]
        private TMP_InputField nameInputField;
		[SerializeField]
		private Button setupPlayerButton;
		[SerializeField]
		private Button backToMenuButton;
		[SerializeField]
		private PlayerVisualisationChoicesUI playerVisualisationChoicesUI;

		[Inject]
		private IPlayerDataManager playerDataManager;

		public override void Show(Action onShownCallback = null)
		{
			base.Show(onShownCallback);
			InitializePlayerData();
			playerVisualisationChoicesUI.Initialize();
			setupPlayerButton.onClick.AddListener(SetupPlayerButton_OnClick);
			backToMenuButton.onClick.AddListener(BackToMenuButton_OnClick);
		}

		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
			playerVisualisationChoicesUI.Deinitialize();
			setupPlayerButton.onClick.RemoveListener(SetupPlayerButton_OnClick);
			backToMenuButton.onClick.RemoveListener(BackToMenuButton_OnClick);  
		}

		private void InitializePlayerData()
		{
			var playerData = playerDataManager.PlayerData;
			nameInputField.SetTextWithoutNotify(playerData.PlayerName);
		}

		private void SetupPlayerButton_OnClick()
		{
			playerDataManager.PlayerData.PlayerName = nameInputField.text;
			playerDataManager.PlayerData.PlayerPicture = 0;
			playerVisualisationChoicesUI.SetupPlayerData();
			playerDataManager.SendPlayerDataUpdate();
		}

		private void BackToMenuButton_OnClick()
		{
			uiViewsManager.ShowViewOfType(UIViewType.MainMenu);
		}
	}
}