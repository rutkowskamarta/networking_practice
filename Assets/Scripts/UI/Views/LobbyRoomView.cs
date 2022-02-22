
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

        [Inject]
        private IRoomManager roomManager;

        public override void Show(Action onShownCallback = null)
        {
            base.Show(onShownCallback);
            SetRoomID();
            leaveRoomButton.onClick.AddListener(LeaveRoomButton_OnClick);
            startGameButton.onClick.AddListener(StartGameButton_OnClick);
        }

        public override void Hide(Action onHiddenCallback = null)
        {
            base.Hide(onHiddenCallback);
            leaveRoomButton.onClick.RemoveListener(LeaveRoomButton_OnClick);
            startGameButton.onClick.RemoveListener(StartGameButton_OnClick);
        }

        private void SetRoomID()
        {
            roomID.SetText(roomManager.CurrentRoomId);
        }

        private void LeaveRoomButton_OnClick()
        {
            uiViewsManager.ShowViewOfType(UIViewType.MainMenu);
        }

        private void StartGameButton_OnClick()
        {

        }
    }
}