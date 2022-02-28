using Game.Client;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
	public class ConnectionStatusView : UIView
	{
		[SerializeField]
		private TMP_Text statusText;

		[Inject]
		private IGameClientManager gameClientManager;

		public override void Show(Action onShownCallback = null)
		{
			base.Show(onShownCallback);
			gameClientManager.OnFirstConnectionEstablished += GameClientManager_OnFirstConnectionEstablished;
			gameClientManager.SetupConnection();
		}

		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
			gameClientManager.OnFirstConnectionEstablished -= GameClientManager_OnFirstConnectionEstablished;
		}

		private void GameClientManager_OnFirstConnectionEstablished()
		{
			uiViewsManager.ShowViewOfType(UIViewType.MainMenu);
			gameClientManager.OnConnectionStateChanged += GameClientManager_OnConnectionStateChanged;
		}

		private void GameClientManager_OnConnectionStateChanged(DarkRift.ConnectionState connectionState)
		{
			switch (connectionState)
			{
				case DarkRift.ConnectionState.Disconnected:
					SetDisconnectedClientUI();
					break;
				case DarkRift.ConnectionState.Connecting:
					SetConnectingClientUI();
					break;
				case DarkRift.ConnectionState.Connected:
					SetConnectedClientUI();
					break;
				case DarkRift.ConnectionState.Disconnecting:
					SetDisconnectedClientUI();
					break;
				case DarkRift.ConnectionState.Interrupted:
					break;
			}
		}

		private void SetDisconnectedClientUI()
		{
			statusText.SetText("Disconnected from server.");
			uiViewsManager.ShowAdditionalViewOfType(viewType);
		}

		private void SetConnectingClientUI()
		{
			statusText.SetText("Connecting to the server...");
		}

		private void SetConnectedClientUI()
		{
			uiViewsManager.HideAdditionalViewOfType(viewType);
		}
	}
}