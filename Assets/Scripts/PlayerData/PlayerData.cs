using DarkRift;
using UnityEngine;

namespace Game.Player
{
    public class PlayerData : IDarkRiftSerializable
	{
		private const string PlayerIDPlayerPrefsKey = "PlayerID";
		private const string PlayerNamePlayerPrefsKey = "PlayerName";

		public int PlayerID
		{
			get
			{
				return PlayerPrefs.GetInt(PlayerIDPlayerPrefsKey, 0);
			}
			set
			{
				PlayerPrefs.SetInt(PlayerIDPlayerPrefsKey, value);
				PlayerPrefs.Save();
			}
		}

		public string PlayerName
		{
			get
			{
				return PlayerPrefs.GetString(PlayerNamePlayerPrefsKey, "Name");
			}
			set
			{
				PlayerPrefs.SetString(PlayerNamePlayerPrefsKey, value);
				PlayerPrefs.Save();
			}
		}

		public PlayerVisualisationData PlayerVisualisationData { get; private set; }

		public PlayerData()
		{
			PlayerVisualisationData = new PlayerVisualisationData();
		}

		public void Deserialize(DeserializeEvent deserializeEvent)
		{
			PlayerID = deserializeEvent.Reader.ReadInt32();
			PlayerName = deserializeEvent.Reader.ReadString();
			PlayerVisualisationData = deserializeEvent.Reader.ReadSerializable<PlayerVisualisationData>();
		}

		public void Serialize(SerializeEvent serializeEvent)
		{
			serializeEvent.Writer.Write(PlayerID);
			serializeEvent.Writer.Write(PlayerName);
			serializeEvent.Writer.Write(PlayerVisualisationData);
		}
	}
}