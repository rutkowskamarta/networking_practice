using DarkRift;
using DarkRift.Client;
using System;

namespace Game.Client
{
	interface IGameClientManager
	{
		bool IsClientConnected { get; }

		event Action<MessageReceivedEventArgs> OnMessageReceived;
		void SendRequest(ushort tag, IDarkRiftSerializable data);
	}
}