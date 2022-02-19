using DarkRift;
using UnityEngine;

namespace Game.PlayerData
{
    public class PlayerData : IDarkRiftSerializable
	{
		private const string PlayerIDPlayerPrefsKey = "PlayerID";
		private const string PlayerNamePlayerPrefsKey = "PlayerName";
		private const string PlayerPicturePlayerPrefsKey = "PlayerPicture";

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

		public int PlayerPicture
		{
			get
			{
				return PlayerPrefs.GetInt(PlayerPicturePlayerPrefsKey, 0);
			}
			set
			{
				PlayerPrefs.SetInt(PlayerPicturePlayerPrefsKey, value);
				PlayerPrefs.Save();
			}
		}

		public void Deserialize(DeserializeEvent deserializeEvent)
		{
			PlayerID = deserializeEvent.Reader.ReadInt32();
			PlayerName = deserializeEvent.Reader.ReadString();
			PlayerPicture = deserializeEvent.Reader.ReadInt32();
		}

		public void Serialize(SerializeEvent serializeEvent)
		{
			serializeEvent.Writer.Write(PlayerID);
			serializeEvent.Writer.Write(PlayerName);
			serializeEvent.Writer.Write(PlayerPicture);
		}
	}
}