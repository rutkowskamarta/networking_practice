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
		public event Action OnRoomCreatedResponseFailed;
		public event Action OnRoomJoinedResponseSuccess;
		public event Action OnRoomJoinedResponseFail;
		public event Action<RoomData> OnRoomUpdatedState;

		[Inject]
		private IGameClientManager gameClientManager;

		public RoomData CurrentRoomData { get; protected set; }

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
			else if(messageEvent.Tag == ServerCommunicationTags.JoinRoomResponseSucess)
			{
				OnRoomJoinedResponseSuccess?.Invoke();
			}
			else if (messageEvent.Tag == ServerCommunicationTags.JoinRoomResponseFail)
			{
				OnRoomJoinedResponseFail?.Invoke();
			}
			else if (messageEvent.Tag == ServerCommunicationTags.UpdateRoomState)
			{
				ProcessRoomUpdateResponse(messageEvent);
			}
		}

		private void ProcessRoomCreationResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				var roomId = reader.ReadString();
				CurrentRoomData = new RoomData(roomId);
				Debug.Log($"Created room of ID {roomId}");
			}
		}

		private void ProcessRoomUpdateResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				RoomData roomData = reader.ReadSerializable<RoomData>();
				CurrentRoomData = roomData;
				OnRoomUpdatedState?.Invoke(roomData);
				Debug.Log($"Updated State room of ID {CurrentRoomData.RoomId}");
			}
		}
	}
}