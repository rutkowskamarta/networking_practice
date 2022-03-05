using Game.Client;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Game.Room;

namespace Game.UI
{
    public class JoinRoomView : UIView
    {
        [SerializeField]
        private TMP_InputField inputField;
		[SerializeField]
		private TMP_Text validationText;
        [SerializeField]
        private Button joinRoom;
        [SerializeField]
        private Button backToMenu;

		[Inject]
		private IRoomManager roomManager;

		public override void Show(Action onShownCallback = null)
		{
			base.Show(onShownCallback);
			ResetLayout();
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
			string roomID = inputField.text.ToUpperInvariant();
			if (string.IsNullOrEmpty(roomID))
			{
				validationText.SetText("Please provide room ID");
			}
			else
			{
				ResetLayout();
				uiViewsManager.ShowViewOfType(UIViewType.WaitingForRoomJoin, () => roomManager.SendRoomJoinRequest(roomID));
			}
		}

		private void ResetLayout()
		{
			validationText.SetText(string.Empty);
		}

		private void BackToMenuButton_OnClick()
		{
			uiViewsManager.ShowViewOfType(UIViewType.MainMenu);
		}
	}
}