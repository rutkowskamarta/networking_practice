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
		public const string SupportedLetters = "abcdefghijklmnoprstuwz";
		public const int MinimumPlayersToStartGame = 2;

		public event Action OnGameStartedSuccess;
		public event Action OnGameStartedFail;
		public event Action<string> OnGameCategoryAdded;
		public event Action<string> OnGameCategoryRemoved;
		public event Action<int> OnRoundsModified;
		public event Action<int> OnPlayersReadyModified;
		public event Action OnEveryoneReady;
		public event Action<char> OnLetterGeneratedResponse;

		[Inject]
		private IGameClientManager gameClientManager;
		[Inject]
		private IRoomManager roomManager;

		public List<string> GameCategories { get; private set; } = new List<string>();
		public int Rounds { get; private set; }
		public int PlayersParticipating => roomManager.CurrentRoomData.PlayersCount;

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

		public void SendRoundsModifiedRequest(int rounds)
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(roomManager.CurrentRoomData.RoomId);
				writer.Write(rounds);

				gameClientManager.SendRequest(ServerCommunicationTags.SetRoundsNumberRequest, writer);
			}
		}

		public void SendPlayerReadyRequest()
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(roomManager.CurrentRoomData.RoomId);

				gameClientManager.SendRequest(ServerCommunicationTags.PlayerReadyRequest, writer);
			}
		}

		public void SendPlayerUnreadyRequest()
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(roomManager.CurrentRoomData.RoomId);

				gameClientManager.SendRequest(ServerCommunicationTags.PlayerUnreadyRequest, writer);
			}
		}

		public void SendLetterGenerationRequest()
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(roomManager.CurrentRoomData.RoomId);

				gameClientManager.SendRequest(ServerCommunicationTags.GenerateLetterRequest, writer);
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
				ProcessGameStartedResponse(messageEvent);
			}
			else if (messageEvent.Tag == ServerCommunicationTags.GameStartedResponseFail)
			{
				OnGameStartedFail?.Invoke();
			}
			else if (messageEvent.Tag == ServerCommunicationTags.GameCategoryAddedNotification)
			{
				ProcessCategoryAddedResponse(messageEvent);
			}
			else if (messageEvent.Tag == ServerCommunicationTags.GameCategoryAddedNotification)
			{
				ProcessCategoryRemovedResponse(messageEvent);
			}
			else if (messageEvent.Tag == ServerCommunicationTags.RoundsModifiedResponse)
			{
				ProcessRoundsModifiedResponse(messageEvent);
			}
			else if (messageEvent.Tag == ServerCommunicationTags.ReadyStateChangedResponse)
			{
				ProcessReadyStateChangedResponse(messageEvent);
			}
			else if (messageEvent.Tag == ServerCommunicationTags.EveryoneReadyNotification)
			{
				OnEveryoneReady?.Invoke();
			}
			else if (messageEvent.Tag == ServerCommunicationTags.LetterGeneratedResponse)
			{
				ProcessLetterGenerationResponse(messageEvent);
			}
		}

		private void ProcessGameStartedResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				Rounds = reader.ReadInt32();
				GameCategories.Clear();
				OnGameStartedSuccess?.Invoke();
			}
		}

		private void ProcessCategoryAddedResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				var category = reader.ReadString();
				GameCategories.Add(category);
				OnGameCategoryAdded?.Invoke(category);
			}
		}

		private void ProcessCategoryRemovedResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				var category = reader.ReadString();
				GameCategories.Remove(category);
				OnGameCategoryRemoved?.Invoke(category);
			}
		}

		private void ProcessRoundsModifiedResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				var rounds = reader.ReadInt32();
				OnRoundsModified?.Invoke(rounds);
			}
		}

		private void ProcessReadyStateChangedResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				var playersReady = reader.ReadInt32();
				OnPlayersReadyModified?.Invoke(playersReady);
			}
		}

		private void ProcessLetterGenerationResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				var generatedLetter = reader.ReadChar();
				OnLetterGeneratedResponse?.Invoke(generatedLetter);
			}
		}
	}
}
