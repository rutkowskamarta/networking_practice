using Game.Client;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.PlayerData
{
	public class PlayerDataManager : MonoBehaviour, IPlayerDataManager
	{
		[Inject]
		private IGameClientManager clientManager;

		private PlayerData playerData;
		public PlayerData PlayerData => playerData;

		private void Start()
		{
			Initialize();
		}

		public void SendPlayerDataUpdate()
		{
			clientManager.SendRequest(ServerCommunicationTags.PlayerDataRequest, playerData);
		}

		private void Initialize()
		{
			playerData = new PlayerData();
			StartCoroutine(SendDataOnInitialization());
		}

		private IEnumerator SendDataOnInitialization()
		{
			if (!clientManager.IsClientConnected)
			{
				yield return new WaitForEndOfFrame();
			}
			SendPlayerDataUpdate();
		}
	}
}