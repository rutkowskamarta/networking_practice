using DarkRift;
using Game.Player;

namespace Game.Room
{
    public class RoomData : IDarkRiftSerializable
    {
        public string RoomId { get; private set; }
		public PlayerData[] Players { get; private set; }

		public RoomData()
		{
		}

        public RoomData(string roomId, PlayerData[] players)
        {
			RoomId = roomId;
            Players = players;
        }

		public RoomData(string roomId)
		{
			RoomId = roomId;
			Players = new PlayerData[0];
		}

		public void Serialize(SerializeEvent serializeEvent)
		{
			serializeEvent.Writer.Write(RoomId);
			serializeEvent.Writer.Write(Players);
		}

		public void Deserialize(DeserializeEvent deserializeEvent)
		{
			RoomId = deserializeEvent.Reader.ReadString();
			Players = deserializeEvent.Reader.ReadSerializables<PlayerData>();
		}
	}
}