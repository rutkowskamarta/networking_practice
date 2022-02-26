using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class PlayerVisualisationChoicesUI : MonoBehaviour
    {
        [SerializeField]
        private PlayerVisualisationContainerUI playerVisualisationContainer;
        [SerializeField]
        private PlayerVisualisationChoiceUI accessoryChoices;
        [SerializeField]
        private PlayerVisualisationChoiceUI backgroundChoices;
        [SerializeField]
        private PlayerVisualisationChoiceUI bodyChoices;
        [SerializeField]
        private PlayerVisualisationChoiceUI faceChoices;
        [SerializeField]
        private PlayerVisualisationOptions playerVisualisationOptions;

        [Inject]
        private IPlayerDataManager playerDataManager;

        public void Initialize()
		{
            var playerVisualisationData = playerDataManager.PlayerData.PlayerVisualisationData;
            playerVisualisationContainer.Initialize(playerDataManager.PlayerData);
            accessoryChoices.Initialize(playerVisualisationOptions.AccessoriesItems.Count, playerVisualisationData.PlayerVisualisationAccessories, playerVisualisationContainer.ChangeAccessoryImage);
            backgroundChoices.Initialize(playerVisualisationOptions.BackroundItems.Count, playerVisualisationData.PlayerVisualisationBackground, playerVisualisationContainer.ChangeBackgroundImage);
            bodyChoices.Initialize(playerVisualisationOptions.BodyItems.Count, playerVisualisationData.PlayerVisualisationBody, playerVisualisationContainer.ChangeBodyImage);
            faceChoices.Initialize(playerVisualisationOptions.FaceItems.Count, playerVisualisationData.PlayerVisualisationFace, playerVisualisationContainer.ChangeFaceImage);
        }

        public void Deinitialize()
		{
            accessoryChoices.Deinitialize();
            backgroundChoices.Deinitialize();
            bodyChoices.Deinitialize();
            faceChoices.Deinitialize();
        }

        public void SetupPlayerData()
		{
            var playerVisualisationData = playerDataManager.PlayerData.PlayerVisualisationData;
            playerVisualisationData.PlayerVisualisationAccessories = accessoryChoices.CurrentOption;
            playerVisualisationData.PlayerVisualisationBackground = backgroundChoices.CurrentOption;
            playerVisualisationData.PlayerVisualisationBody = bodyChoices.CurrentOption;
            playerVisualisationData.PlayerVisualisationFace = faceChoices.CurrentOption;
        }
    }
}
