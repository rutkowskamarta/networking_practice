using Game.Player;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class LobbyPlayer : MonoBehaviour
    {
        [SerializeField]
        private PlayerVisualisationContainerUI playerVisualisation;
        [SerializeField]
        private TMP_Text playerName;

        public void Initialize(PlayerData playerData)
		{
            playerName.SetText(playerData.PlayerName);
            playerVisualisation.Initialize(playerData);
		}
    }
}
