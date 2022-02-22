using System;

namespace Game.Room
{
    public interface IRoomManager
    {
		string CurrentRoomId { get; }

		event Action OnRoomCreatedResponseSuccess;
		event Action OnRoomCreatedResponseFailed;
		event Action OnRoomJoinedResponseSuccess;
		event Action OnRoomJoinedResponseFail;
    }
}