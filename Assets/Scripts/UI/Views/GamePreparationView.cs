using Game.Game;
using Game.Room;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class GamePreparationView : UIView
    {
        [SerializeField]
        private TMP_InputField roundsInputField;
        [SerializeField]
        private GameCategoryHolderUI gameCategoryHolderUI;

        [SerializeField]
        private TMP_InputField newCategoryInputField;
        [SerializeField]
        private Button addCategoryButton;
        [SerializeField]
        private Button modifyRoundsButton;
        [SerializeField]
        private Button readyButton;

        [SerializeField]
        private TMP_Text readyPlayersText;

        [Inject]
        private IGameManager gameManager;
        [Inject]
        private IRoomManager roomManager;

        private bool isReadyClicked;

        public override void Show(Action onShownCallback = null)
        {
            base.Show(onShownCallback);
            Initialize();

			gameManager.OnGameCategoryAdded += GameManager_OnGameCategoryAdded;
			gameManager.OnGameCategoryRemoved += GameManager_OnGameCategoryRemoved;
            gameManager.OnRoundsModified += GameManager_OnRoundsModified;
			gameManager.OnPlayersReadyModified += GameManager_OnPlayersReadyModified;
			gameManager.OnEveryoneReady += GameManager_OnEveryoneReady;

            addCategoryButton.onClick.AddListener(AddCategoryButton_OnClick);
            readyButton.onClick.AddListener(ReadyCategoryButton_OnClick);
            modifyRoundsButton.onClick.AddListener(SetRoundsButton_OnClick);
        }

		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);

            gameManager.OnGameCategoryAdded -= GameManager_OnGameCategoryAdded;
            gameManager.OnGameCategoryRemoved -= GameManager_OnGameCategoryRemoved;
            gameManager.OnRoundsModified -= GameManager_OnRoundsModified;
            gameManager.OnPlayersReadyModified -= GameManager_OnPlayersReadyModified;
            gameManager.OnEveryoneReady -= GameManager_OnEveryoneReady;

            addCategoryButton.onClick.RemoveListener(AddCategoryButton_OnClick);
            readyButton.onClick.RemoveListener(ReadyCategoryButton_OnClick);
            modifyRoundsButton.onClick.RemoveListener(SetRoundsButton_OnClick);
        }

		private void Initialize()
		{
            isReadyClicked = false;
            roundsInputField.SetTextWithoutNotify(gameManager.Rounds.ToString());
            SetReadyText(0);

            var isRoomHost = roomManager.IsRoomHost;

            //if is room host, then only this player can modify sonme things, probably rounds 
            //manage room host change on disconnection

        }

		private void GameManager_OnGameCategoryAdded(string category)
        {
            gameCategoryHolderUI.AddCategoryOnStateUpdate(category);
        }

        private void GameManager_OnGameCategoryRemoved(string category)
        {
            gameCategoryHolderUI.RemoveCategoryOnStateUpdate(category);
        }

        private void GameManager_OnRoundsModified(int rounds)
        {
            roundsInputField.SetTextWithoutNotify(rounds.ToString());
        }

        private void AddCategoryButton_OnClick()
        {
            var categoryText = newCategoryInputField.text;
            if (!string.IsNullOrEmpty(categoryText))
			{
                gameCategoryHolderUI.AddCategory(categoryText);
            }
        }

        private void ReadyCategoryButton_OnClick()
        {
			if (isReadyClicked)
			{
                gameManager.SendPlayerUnreadyRequest();
            }
			else
			{
                gameManager.SendPlayerReadyRequest();
			}
            isReadyClicked = !isReadyClicked;
        }

        private void GameManager_OnPlayersReadyModified(int playersReady)
        {
            SetReadyText(playersReady);
        }

        private void SetReadyText(int playersReady)
		{
            readyPlayersText.SetText($"{playersReady}/{gameManager.PlayersParticipating}");
        }

        private void SetRoundsButton_OnClick()
        {
            var parseSuccess = int.TryParse(roundsInputField.text, out int rounds);
			if (parseSuccess)
			{
                gameManager.SendRoundsModifiedRequest(rounds);
			}
        }

        private void GameManager_OnEveryoneReady()
        {
            uiViewsManager.ShowViewOfType(UIViewType.GameLetterSelectionView);
        }
    }
}