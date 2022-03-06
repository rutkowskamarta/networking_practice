using DarkRift;
using DarkRift.Client;
using Game.Client;
using Game.Player;
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

		public bool IsRoomAdministrator { get; private set; }
		public RoomData CurrentRoomData { get; private set; }
		
		[Inject]
		private IGameClientManager gameClientManager;

		private void Start()
		{
			gameClientManager.OnMessageReceived += GameClientManager_OnMessageReceived;
		}

		private void OnDestroy()
		{
			gameClientManager.OnMessageReceived -= GameClientManager_OnMessageReceived;
		}

		public void SendRoomJoinRequest(string roomID)
		{
			gameClientManager.SendRequest(ServerCommunicationTags.JoinRoomRequest, new RoomData(roomID));
		}

		public void SendRoomLeaveRequest()
		{
			gameClientManager.SendRequest(ServerCommunicationTags.LeaveRoomRequest, CurrentRoomData);
		}

		public void SendRoomCreationRequest()
		{
			gameClientManager.SendRequest(ServerCommunicationTags.CreateRoomRequest);
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
			else if (messageEvent.Tag == ServerCommunicationTags.UpdateRoomStateNotification)
			{
				ProcessRoomUpdateResponse(messageEvent);
			}
		}

		private void ProcessRoomCreationResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				IsRoomAdministrator = reader.ReadBoolean();
				var roomId = reader.ReadString();
				var players = reader.ReadSerializables<PlayerData>();
				CurrentRoomData = new RoomData(roomId, players);
				Debug.Log($"Created room of ID {roomId}");
			}
		}

		private void ProcessRoomUpdateResponse(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				IsRoomAdministrator = reader.ReadBoolean();
				RoomData roomData = reader.ReadSerializable<RoomData>();
				CurrentRoomData = roomData;
				OnRoomUpdatedState?.Invoke(roomData);
				Debug.Log($"Updated State room of ID {CurrentRoomData.RoomId}");
			}
		}
	}
}