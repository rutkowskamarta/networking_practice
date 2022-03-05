using DarkRift.Client;
using Game.Client;
using Game.Room;
using System;
using UnityEngine;
using Zenject;

namespace Game.Game
{
	public class GameManager : MonoBehaviour, IGameManager
	{
		public const int MinimumPlayersToStartGame = 2;

		public event Action OnGameStartedSuccess;
		public event Action OnGameStartedFail;

		[Inject]
		private IGameClientManager gameClientManager;
		[Inject]
		private IRoomManager roomManager;

		private void Start()
		{
			gameClientManager.OnMessageReceived += GameClientManager_OnMessageReceived;
		}

		private void OnDestroy()
		{
			gameClientManager.OnMessageReceived -= GameClientManager_OnMessageReceived;
		}

		private void GameClientManager_OnMessageReceived(MessageReceivedEventArgs messageEvent)
		{
			if (messageEvent.Tag == ServerCommunicationTags.GameStartedResponseSucess)
			{
				OnGameStartedSuccess?.Invoke();
			}
			else if (messageEvent.Tag == ServerCommunicationTags.GameStartedResponseFail)
			{
				OnGameStartedFail?.Invoke();
			}
		}

		public void SendStartGameRequest()
		{
			gameClientManager.SendRequest(ServerCommunicationTags.StartGameRequest, roomManager.CurrentRoomData);
		}
	}
}
