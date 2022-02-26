
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainMenuView : UIView
    {
        [SerializeField]
        private Button setupAccountButton;
        [SerializeField]
        private Button createRoomButton;
        [SerializeField] 
        private Button joinRoomButton;
		[SerializeField]
		private Button exitButton;

		public override void Show(Action onShownCallback = null)
		{
			base.Show(onShownCallback);
			setupAccountButton.onClick.AddListener(SetupAccountButton_OnClick);
			createRoomButton.onClick.AddListener(CreateRoomButton_OnClick);
			joinRoomButton.onClick.AddListener(JoinRoomButton_OnClick);
			exitButton.onClick.AddListener(ExitButton_OnClick);
		}

		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
			setupAccountButton.onClick.RemoveListener(SetupAccountButton_OnClick);
			createRoomButton.onClick.RemoveListener(CreateRoomButton_OnClick);
			joinRoomButton.onClick.RemoveListener(JoinRoomButton_OnClick);
			exitButton.onClick.RemoveListener(ExitButton_OnClick);
		}

		private void SetupAccountButton_OnClick()
		{
			uiViewsManager.ShowViewOfType(UIViewType.PlayerSetup);
		}

		private void CreateRoomButton_OnClick()
		{
			uiViewsManager.ShowViewOfType(UIViewType.CreateRoom);
		}

		private void JoinRoomButton_OnClick()
		{
			uiViewsManager.ShowViewOfType(UIViewType.JoinRoom);
		}

		private void ExitButton_OnClick()
		{
			Application.Quit();
		}
	}
}