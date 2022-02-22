using Game.Room;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
	public class WaitingForRoomConnectionView : UIView
	{
		[SerializeField]
		private TMP_Text statusMessage;

		[Inject]
		private IRoomManager roomManager;

		public override void Show(Action onShownCallback = null)
		{
			base.Show(onShownCallback);
			InitializeStatusMessage();
			roomManager.OnRoomCreatedResponseSuccess += RoomManager_OnRoomCreatedResponseSuccess;
			roomManager.OnRoomCreatedResponseFailed += RoomManager_OnRoomCreatedResponseFailed;
		}
		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
			roomManager.OnRoomCreatedResponseSuccess -= RoomManager_OnRoomCreatedResponseSuccess;
			roomManager.OnRoomCreatedResponseFailed -= RoomManager_OnRoomCreatedResponseFailed;
		}

		private void InitializeStatusMessage()
		{
			SetStatusMessage("Waiting for server response...");
		}

		private void SetStatusMessage(string text)
		{
			statusMessage.SetText(text);
		}

		private void RoomManager_OnRoomCreatedResponseSuccess()
		{
			SetStatusMessage("Creation succeeded");
			uiViewsManager.ShowViewOfType(UIViewType.LobbyRoom);
		}

		private void RoomManager_OnRoomCreatedResponseFailed()
		{
			SetStatusMessage("Failed to create game lobby.");
		}
	}
}