using Game.PlayerData;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerVisualisationContainer : MonoBehaviour
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