using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class GameAnswerHolderUI : MonoBehaviour
    {
        [SerializeField]
        private GameAnswerUI gameAnswerPrefab;
        [SerializeField]
        private Transform gameAnswersParent;

        private List<GameAnswerUI> gameAnswersInstances = new List<GameAnswerUI>();

        public void Initialize(List<string> categories)
        {
            foreach (var item in gameAnswersInstances)
            {
                Destroy(item.gameObject);
            }
            gameAnswersInstances.Clear();
            foreach (var item in categories)
            {
                var instance = Instantiate(gameAnswerPrefab, gameAnswersParent);
                instance.Initialize(item);
                gameAnswersInstances.Add(instance);
            }
        }
    }
}