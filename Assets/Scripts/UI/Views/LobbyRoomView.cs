using Game.Game;
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
        private IGameManager gameManager;

        public override void Show(Action onShownCallback = null)
        {
            base.Show(onShownCallback);
            InitializeRoomInfo();
            leaveRoomButton.onClick.AddListener(LeaveRoomButton_OnClick);
            startGameButton.onClick.AddListener(StartGameButton_OnClick);
			roomManager.OnRoomUpdatedState += RoomManager_OnRoomUpdatedState;
			gameManager.OnGameStartedSuccess += GameManager_OnGameStartedSuccess;
			gameManager.OnGameStartedFail += GameManager_OnGameStartedFail;
        }

		public override void Hide(Action onHiddenCallback = null)
        {
            base.Hide(onHiddenCallback);
            lobbyPlayerHolder.ClearPlayers();
            leaveRoomButton.onClick.RemoveListener(LeaveRoomButton_OnClick);
            startGameButton.onClick.RemoveListener(StartGameButton_OnClick);
            roomManager.OnRoomUpdatedState -= RoomManager_OnRoomUpdatedState;
            gameManager.OnGameStartedSuccess -= GameManager_OnGameStartedSuccess;
            gameManager.OnGameStartedFail -= GameManager_OnGameStartedFail;
        }

        private void InitializeRoomInfo()
        {
            roomID.SetText(roomManager.CurrentRoomData.RoomId);
            lobbyPlayerHolder.UpdatePlayers(roomManager.CurrentRoomData.Players);
            startGameButton.interactable = roomManager.IsRoomHost;
            leaveRoomButton.interactable = true;
        }

        private void LeaveRoomButton_OnClick()
        {
            uiViewsManager.ShowViewOfType(UIViewType.MainMenu);
            roomManager.SendRoomLeaveRequest();
        }

        private void StartGameButton_OnClick()
        {
            gameManager.SendStartGameRequest();
            startGameButton.interactable = false;
            leaveRoomButton.interactable = false;
        }

        private void RoomManager_OnRoomUpdatedState(RoomData roomData)
        {
            lobbyPlayerHolder.UpdatePlayers(roomData.Players);
        }

        private void GameManager_OnGameStartedSuccess()
        {
            uiViewsManager.ShowViewOfType(UIViewType.GamePreparationView);
        }

        private void GameManager_OnGameStartedFail()
        {
            //TODO: OR SOME DIFFERENT ERROR HANDLING
            InitializeRoomInfo();
        }
    }
}