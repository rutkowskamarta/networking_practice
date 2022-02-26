using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(fileName = "PlayerVisualisationOptions", menuName = "Player Visualisation Options", order = 0)]
    public class PlayerVisualisationOptions : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> backroundItems;
        public List<Sprite> BackroundItems => backroundItems;

        [SerializeField]
        private List<Sprite> bodyItems;
        public List<Sprite> BodyItems => bodyItems;

        [SerializeField]
        private List<Sprite> faceItems;
        public List<Sprite> FaceItems => faceItems;

        [SerializeField]
        private List<Sprite> accessoriesItems;
        public List<Sprite> AccessoriesItems => accessoriesItems;

    }
}