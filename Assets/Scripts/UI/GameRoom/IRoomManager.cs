using System;

namespace Game.Room
{
    public interface IRoomManager
    {
		RoomData CurrentRoomData { get; }
		bool IsRoomHost { get; }

		event Action OnRoomCreatedResponseSuccess;
		event Action OnRoomCreatedResponseFailed;
		event Action OnRoomJoinedResponseSuccess;
		event Action OnRoomJoinedResponseFail;
		event Action<RoomData> OnRoomUpdatedState;
		event Action<bool> OnRoomHostChanged;

		void SendRoomJoinRequest(string roomID);
		void SendRoomLeaveRequest();
		void SendRoomCreationRequest();
	}
}