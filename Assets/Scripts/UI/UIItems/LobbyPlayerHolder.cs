using Game.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class LobbyPlayerHolder : MonoBehaviour
    {
        [SerializeField]
        private LobbyPlayer lobbyPlayerPrefab;
        [SerializeField]
        private Transform lobbyPlayersTransform;

        private List<LobbyPlayer> lobbyPlayers = new List<LobbyPlayer>();

        public void UpdatePlayers(PlayerData[] playerDatas)
		{
            ClearPlayers();
			foreach (var playerData in playerDatas)
			{
                AddPlayer(playerData);
            }
        }

        private void ClearPlayers()
		{
			foreach (var item in lobbyPlayers)
			{
                Destroy(item);
			}
            lobbyPlayers.Clear();
		}

        private void AddPlayer(PlayerData playerData)
		{
            LobbyPlayer lobbyPlayer = Instantiate(lobbyPlayerPrefab, lobbyPlayersTransform);
            lobbyPlayer.Initialize(playerData);
            lobbyPlayers.Add(lobbyPlayer);
        }
    }
}