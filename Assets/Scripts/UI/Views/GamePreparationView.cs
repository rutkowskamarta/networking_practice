using Game.Game;
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
        private Button readyButton;

        [Inject]
        private IGameManager gameManager;

        public override void Show(Action onShownCallback = null)
        {
            base.Show(onShownCallback);
			gameManager.OnGameCategoryAdded += GameManager_OnGameCategoryAdded;
			gameManager.OnGameCategoryRemoved += GameManager_OnGameCategoryRemoved;
            addCategoryButton.onClick.AddListener(AddCategoryButton_OnClick);
            readyButton.onClick.AddListener(ReadyCategoryButton_OnClick);
            roundsInputField.onValueChanged.AddListener(RoundsInputField_OnValueChanged);
        }
	
		public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
            gameManager.OnGameCategoryAdded -= GameManager_OnGameCategoryAdded;
            gameManager.OnGameCategoryRemoved -= GameManager_OnGameCategoryRemoved;
            addCategoryButton.onClick.RemoveListener(AddCategoryButton_OnClick);
            readyButton.onClick.RemoveListener(ReadyCategoryButton_OnClick);
            roundsInputField.onValueChanged.RemoveListener(RoundsInputField_OnValueChanged);
        }

        private void GameManager_OnGameCategoryAdded(string category)
        {
            gameCategoryHolderUI.AddCategoryOnStateUpdate(category);
        }

        private void GameManager_OnGameCategoryRemoved(string category)
        {
            gameCategoryHolderUI.RemoveCategoryOnStateUpdate(category);
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
        }

        private void RoundsInputField_OnValueChanged(string value)
        {
        }
    }
}