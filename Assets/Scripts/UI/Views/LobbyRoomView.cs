
using Game.Client;
using Game.Room;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class LobbyRoomView : UIView
    {
        [SerializeField]
        private TMP_Text roomID;
        [SerializeField]
        private Button leaveRoomButton;
        [SerializeField]
        private Button startGameButton;
        [SerializeField]
        private LobbyPlayerHolder lobbyPlayerHolder;

        [Inject]
        private IRoomManager roomManager;
        [Inject]
        private IGameClientManager gameClientManager;

        public override void Show(Action onShownCallback = null)
        {
            base.Show(onShownCallback);
            InitializeRoomInfo();
            leaveRoomButton.onClick.AddListener(LeaveRoomButton_OnClick);
            startGameButton.onClick.AddListener(StartGameButton_OnClick);
			roomManager.OnRoomUpdatedState += RoomManager_OnRoomUpdatedState;
        }

		public override void Hide(Action onHiddenCallback = null)
        {
            base.Hide(onHiddenCallback);
            lobbyPlayerHolder.ClearPlayers();
            leaveRoomButton.onClick.RemoveListener(LeaveRoomButton_OnClick);
            startGameButton.onClick.RemoveListener(StartGameButton_OnClick);
            roomManager.OnRoomUpdatedState -= RoomManager_OnRoomUpdatedState;
        }

        private void InitializeRoomInfo()
        {
            roomID.SetText(roomManager.CurrentRoomData.RoomId);
            lobbyPlayerHolder.UpdatePlayers(roomManager.CurrentRoomData.Players);
        }

        private void LeaveRoomButton_OnClick()
        {
            gameClientManager.SendRequest(ServerCommunicationTags.LeaveRoomRequest, roomManager.CurrentRoomData);
            uiViewsManager.ShowViewOfType(UIViewType.MainMenu);
        }

        private void StartGameButton_OnClick()
        {
        }

        private void RoomManager_OnRoomUpdatedState(RoomData roomData)
        {
            lobbyPlayerHolder.UpdatePlayers(roomData.Players);
        }
    }
}