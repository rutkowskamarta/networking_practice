using Game.Client;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Player
{
	public class PlayerDataManager : MonoBehaviour, IPlayerDataManager
	{
		[Inject]
		private IGameClientManager gameClientManager;

		private PlayerData playerData;
		public PlayerData PlayerData => playerData;

		private void Start()
		{
			Initialize();
		}

		public void SendPlayerDataUpdate()
		{
			gameClientManager.SendRequest(ServerCommunicationTags.PlayerDataRequest, playerData);
		}

		private void Initialize()
		{
			playerData = new PlayerData();
			StartCoroutine(SendDataOnInitialization());
		}

		private IEnumerator SendDataOnInitialization()
		{
			if (!gameClientManager.IsClientConnected)
			{
				yield return new WaitForEndOfFrame();
			}
			SendPlayerDataUpdate();
		}
	}
}