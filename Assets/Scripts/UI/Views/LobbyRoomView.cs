
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            roomID.SetText("11112222");
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