using DarkRift;

namespace Game.Client
{
	interface IGameClientManager
	{
		bool IsClientConnected { get; }
		void SendRequest(ushort tag, IDarkRiftSerializable data);
	}
}