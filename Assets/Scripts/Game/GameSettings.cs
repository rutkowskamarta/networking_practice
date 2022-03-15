using UnityEngine;

namespace Game.Game
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public const string SupportedLetters = "abcdefghijklmnoprstuwz";

        [SerializeField]
        private int minimumPlayersToStartGame = 2;
        public int MinimumPlayersToStartGame => minimumPlayersToStartGame;

        [SerializeField]
        private float timeAfterStop = 20;
        public float TimeAfterStop => timeAfterStop;

    }
}