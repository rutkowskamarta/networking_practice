using System;

namespace Game.Room
{
    public interface IRoomManager
    {
		RoomData CurrentRoomData { get; }
		bool IsRoomAdministrator { get; }

		event Action OnRoomCreatedResponseSuccess;
		event Action OnRoomCreatedResponseFailed;
		event Action OnRoomJoinedResponseSuccess;
		event Action OnRoomJoinedResponseFail;
		event Action<RoomData> OnRoomUpdatedState;

		void SendRoomJoinRequest(string roomID);
		void SendRoomLeaveRequest();
		void SendRoomCreationRequest();
	}
}