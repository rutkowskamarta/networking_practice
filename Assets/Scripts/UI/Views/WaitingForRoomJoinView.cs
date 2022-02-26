using Game.Room;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class WaitingForRoomJoinView : UIView
    {
		[SerializeField]
		private TMP_Text statusMessage;

		[Inject]
		private IRoomManager roomManager;

		public override void Show(Action onShownCallback = null)
		{
			base.Show(onShownCallback);
			InitializeStatusMessage();
			roomManager.OnRoomJoinedResponseSuccess += RoomManager_OnRoomJoinResponseSuccess;
			roomManager.OnRoomJoinedResponseFail += RoomManager_OnRoomJoinResponseFailed;
		}
		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
			roomManager.OnRoomJoinedResponseSuccess -= RoomManager_OnRoomJoinResponseSuccess;
			roomManager.OnRoomJoinedResponseFail -= RoomManager_OnRoomJoinResponseFailed;
		}

		private void InitializeStatusMessage()
		{
			SetStatusMessage("Waiting for server response...");
		}

		private void SetStatusMessage(string text)
		{
			statusMessage.SetText(text);
		}

		private void RoomManager_OnRoomJoinResponseSuccess()
		{
			SetStatusMessage("Join succeeded");
			uiViewsManager.ShowViewOfType(UIViewType.LobbyRoom);
		}

		private void RoomManager_OnRoomJoinResponseFailed()
		{
			SetStatusMessage("Failed to join game lobby.");
			uiViewsManager.ShowViewOfType(UIViewType.JoinRoom);
		}
	}
}