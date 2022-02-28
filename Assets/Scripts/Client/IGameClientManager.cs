using DarkRift;
using DarkRift.Client;
using System;

namespace Game.Client
{
	interface IGameClientManager
	{
		bool IsClientConnected { get; }
		event Action<ConnectionState> OnConnectionStateChanged; 
		event Action<MessageReceivedEventArgs> OnMessageReceived;
		event Action OnFirstConnectionEstablished;

		void SetupConnection();
		void SendRequest(ushort tag, IDarkRiftSerializable data = null);
	}
}