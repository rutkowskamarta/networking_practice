using DarkRift;
using DarkRift.Client;
using Game.Client;
using Game.Room;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Game
{
	public class GameManager : MonoBehaviour, IGameManager
	{
		public const int MinimumPlayersToStartGame = 2;

		public event Action OnGameStartedSuccess;
		public event Action OnGameStartedFail;
		public event Action<string> OnGameCategoryAdded;
		public event Action<string> OnGameCategoryRemoved;

		[Inject]
		private IGameClientManager gameClientManager;
		[Inject]
		private IRoomManager roomManager;

		public List<string> GameCategories { get; private set; } = new List<string>();

		private void Start()
		{
			gameClientManager.OnMessageReceived += GameClientManager_OnMessageReceived;
		}

		private void OnDestroy()
		{
			gameClientManager.OnMessageReceived -= GameClientManager_OnMessageReceived;
		}

		public void SendStartGameRequest()
		{
			gameClientManager.SendRequest(ServerCommunicationTags.StartGameRequest, roomManager.CurrentRoomData);
		}

		public void SendAddGameCategoryRequest(string category)
		{
			if (DoesCategoryExist(category))
			{
				return;
			}
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(roomManager.CurrentRoomData.RoomId);
				writer.Write(category);

				gameClientManager.SendRequest(ServerCommunicationTags.AddCategoryRequest, writer);
			}
		}

		public void SendRemoveGameCategoryRequest(string category)
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(roomManager.CurrentRoomData.RoomId);
				writer.Write(category);

				gameClientManager.SendRequest(ServerCommunicationTags.RemoveCategoryRequest, writer);
			}
		}

		private bool DoesCategoryExist(string category)
		{
			return GameCategories.Contains(category);
		}

		private void GameClientManager_OnMessageReceived(MessageReceivedEventArgs messageEvent)
		{
			if (messageEvent.Tag == ServerCommunicationTags.GameStartedResponseSucess)
			{
				GameCategories.Clear();
				OnGameStartedSuccess?.Invoke();
			}
			else if (messageEvent.Tag == ServerCommunicationTags.GameStartedResponseFail)
			{
				OnGameStartedFail?.Invoke();
			}
			else if (messageEvent.Tag == ServerCommunicationTags.GameCategoryAddedNotification)
			{
				ProcessCategoryAddedRequest(messageEvent);
			}
			else if (messageEvent.Tag == ServerCommunicationTags.GameCategoryAddedNotification)
			{
				ProcessCategoryRemovedRequest(messageEvent);
			}
		}

		private void ProcessCategoryAddedRequest(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				var category = reader.ReadString();
				GameCategories.Add(category);
				OnGameCategoryAdded?.Invoke(category);
			}
		}

		private void ProcessCategoryRemovedRequest(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				var category = reader.ReadString();
				GameCategories.Remove(category);
				OnGameCategoryRemoved?.Invoke(category);
			}
		}

	}
}
