using DarkRift;
using UnityEngine;

namespace Game.PlayerData
{
    public class PlayerVisualisationData : IDarkRiftSerializable
    {
		private const string PlayerVisualisationAccessoriesPlayerPrefsKey = "PlayerVisualisationAccessories";
		private const string PlayerVisualisationBackgroundPlayerPrefsKey = "PlayerVisualisationBackground";
		private const string PlayerVisualisationBodyPlayerPrefsKey = "PlayerVisualisationBody";
		private const string PlayerVisualisationFacePlayerPrefsKey = "PlayerVisualisationFace";

		public int PlayerVisualisationAccessories
		{
			get
			{
				return PlayerPrefs.GetInt(PlayerVisualisationAccessoriesPlayerPrefsKey, 0);
			}
			set
			{
				PlayerPrefs.SetInt(PlayerVisualisationAccessoriesPlayerPrefsKey, value);
				PlayerPrefs.Save();
			}
		}

		public int PlayerVisualisationBackground
		{
			get
			{
				return PlayerPrefs.GetInt(PlayerVisualisationBackgroundPlayerPrefsKey, 0);
			}
			set
			{
				PlayerPrefs.SetInt(PlayerVisualisationBackgroundPlayerPrefsKey, value);
				PlayerPrefs.Save();
			}
		}

		public int PlayerVisualisationBody
		{
			get
			{
				return PlayerPrefs.GetInt(PlayerVisualisationBodyPlayerPrefsKey, 0);
			}
			set
			{
				PlayerPrefs.SetInt(PlayerVisualisationBodyPlayerPrefsKey, value);
				PlayerPrefs.Save();
			}
		}

		public int PlayerVisualisationFace
		{
			get
			{
				return PlayerPrefs.GetInt(PlayerVisualisationFacePlayerPrefsKey, 0);
			}
			set
			{
				PlayerPrefs.SetInt(PlayerVisualisationFacePlayerPrefsKey, value);
				PlayerPrefs.Save();
			}
		}

		public void Deserialize(DeserializeEvent deserializeEvent)
		{
			PlayerVisualisationAccessories = deserializeEvent.Reader.ReadInt32();
			PlayerVisualisationBackground = deserializeEvent.Reader.ReadInt32();
			PlayerVisualisationBody = deserializeEvent.Reader.ReadInt32();
			PlayerVisualisationFace = deserializeEvent.Reader.ReadInt32();
		}

		public void Serialize(SerializeEvent serializeEvent)
		{
			serializeEvent.Writer.Write(PlayerVisualisationAccessories);
			serializeEvent.Writer.Write(PlayerVisualisationBackground);
			serializeEvent.Writer.Write(PlayerVisualisationBody);
			serializeEvent.Writer.Write(PlayerVisualisationFace);
		}
	}
}