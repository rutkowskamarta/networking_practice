using System;

namespace Game.Room
{
    public interface IRoomManager
    {
		RoomData CurrentRoomData { get; }

		event Action OnRoomCreatedResponseSuccess;
		event Action OnRoomCreatedResponseFailed;
		event Action OnRoomJoinedResponseSuccess;
		event Action OnRoomJoinedResponseFail;
		event Action<RoomData> OnRoomUpdatedState;
	}
}