using Game.Client;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class JoinRoomView : UIView
    {
        [SerializeField]
        private TMP_InputField inputField;
        [SerializeField]
        private Button joinRoom;
        [SerializeField]
        private Button backToMenu;

		[Inject]
		private IGameClientManager gameClientManager;

		public override void Show(Action onShownCallback = null)
		{
			base.Show(onShownCallback);
			joinRoom.onClick.AddListener(JoinRoomButton_OnClick);
			backToMenu.onClick.AddListener(BackToMenuButton_OnClick);
		}

		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
			joinRoom.onClick.RemoveListener(JoinRoomButton_OnClick);
			backToMenu.onClick.RemoveListener(BackToMenuButton_OnClick);
		}

		private void JoinRoomButton_OnClick()
		{
			string roomID = inputField.text;
			//gameClientManager.SendRequest(ServerCommunicationTags.JoinRoom, );
		}

		private void BackToMenuButton_OnClick()
		{
			uiViewsManager.ShowViewOfType(UIViewType.MainMenu);
		}

	}
}