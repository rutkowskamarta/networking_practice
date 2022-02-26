using Game.Client;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class CreateRoomView : UIView
    {
        [SerializeField]
        private Button createRoom;
        [SerializeField]
        private Button backToMenu;

		[Inject]
		private IGameClientManager gameClientManager;

		public override void Show(Action onShownCallback = null)
		{
			base.Show(onShownCallback);
			createRoom.onClick.AddListener(CreateRoomButton_OnClick);
			backToMenu.onClick.AddListener(BackToMenuButton_OnClick);
		}

		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
			createRoom.onClick.RemoveListener(CreateRoomButton_OnClick);
			backToMenu.onClick.RemoveListener(BackToMenuButton_OnClick);
		}

		private void CreateRoomButton_OnClick()
		{
			uiViewsManager.ShowViewOfType(UIViewType.WaitingForRoomCreation, SendRoomConnectionRequest);
		}

		private void SendRoomConnectionRequest()
		{
			gameClientManager.SendRequest(ServerCommunicationTags.CreateRoomRequest, null);
		}

		private void BackToMenuButton_OnClick()
		{
			uiViewsManager.ShowViewOfType(UIViewType.MainMenu);
		}

	}
}