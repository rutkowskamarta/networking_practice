using Game.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class PlayerVisualisationContainerUI : MonoBehaviour
    {
        [SerializeField]
        private Image backgroundImage; 
        [SerializeField]
        private Image bodyImage; 
        [SerializeField]
        private Image faceImage; 
        [SerializeField]
        private Image accessoryImage;
        [SerializeField]
        private PlayerVisualisationOptions playerVisualisationOptions;

        [Inject]
        private IPlayerDataManager playerDataManager;

        public void Initialize(PlayerData playerData)
		{
            var playerVisualisationData = playerData.PlayerVisualisationData;
            ChangeBackgroundImage(playerVisualisationData.PlayerVisualisationBackground);
            ChangeBodyImage(playerVisualisationData.PlayerVisualisationBody);
            ChangeFaceImage(playerVisualisationData.PlayerVisualisationFace);
            ChangeAccessoryImage(playerVisualisationData.PlayerVisualisationAccessories);
        }

        public void ChangeBackgroundImage(int backgroundIndex)
		{
            backgroundImage.sprite = playerVisualisationOptions.BackroundItems[backgroundIndex];
        }

        public void ChangeBodyImage(int bodyIndex)
        {
            bodyImage.sprite = playerVisualisationOptions.BodyItems[bodyIndex];
        }

        public void ChangeFaceImage(int faceIndex)
        {
            faceImage.sprite = playerVisualisationOptions.FaceItems[faceIndex];
        }

        public void ChangeAccessoryImage(int accessoryIndex)
        {
            accessoryImage.sprite = playerVisualisationOptions.AccessoriesItems[accessoryIndex];
        }
    }
}