using DarkRift;
using DarkRift.Client;
using Game.Client;
using System;
using UnityEngine;
using Zenject;

namespace Game.Room
{
    public class RoomManager : MonoBehaviour, IRoomManager
    {
		public event Action OnRoomCreatedResponseSuccess;
		public event Action OnRoomJoinedResponseSuccess;
		public event Action OnRoomCreatedResponseFailed;
		public event Action OnRoomJoinedResponseFail;

		[Inject]
		private IGameClientManager gameClientManager;

		public string CurrentRoomId { get; protected set; }

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
			if(messageEvent.Tag == ServerCommunicationTags.CreateRoomResponseSucess)
			{
				ProcessRoomCreationResponse(messageEvent);
				OnRoomCreatedResponseSuccess?.Invoke();
			}
			else if (messageEvent.Tag == ServerCommunicationTags.CreateRoomResponseFail)
			{
				OnRoomCreatedResponseFailed?.Invoke();
			}
		}


		private void ProcessRoomCreationResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				CurrentRoomId = reader.ReadString();
				Debug.Log($"Created room of ID {CurrentRoomId}");
			}
		}

	}
}