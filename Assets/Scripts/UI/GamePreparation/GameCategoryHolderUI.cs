using Game.Game;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class GameCategoryHolderUI : MonoBehaviour
    {
        [SerializeField]
        private Transform categoriesParent;
        [SerializeField]
        private GameCategoryUI gameCategoryPrefab;

        private Dictionary<string, GameCategoryUI> gameCategories = new Dictionary<string, GameCategoryUI>();

        [Inject]
        private IGameManager gameManager;

		public void Initialize()
		{
			foreach (var category in gameCategories)
			{
                category.Value.Destroy();
			}
            gameCategories.Clear();
        }

        public void AddCategory(string categoryName)
		{
            gameManager.SendAddGameCategoryRequest(categoryName);
        }

        public void AddCategoryOnStateUpdate(string categoryName)
        {
            var category = Instantiate(gameCategoryPrefab, categoriesParent);
            category.Initialize(categoryName, RemoveCategoryFromButton);
        }

        public void RemoveCategoryFromButton(GameCategoryUI gameCategoryUI)
		{
            gameCategories.Remove(gameCategoryUI.CategoryName);
            gameManager.SendRemoveGameCategoryRequest(gameCategoryUI.CategoryName);
            gameCategoryUI.Destroy();
        }

        public void RemoveCategoryOnStateUpdate(string categoryName)
        {
            var category = gameCategories[categoryName];
            gameCategories.Remove(category.CategoryName);
            category.Destroy();
        }
    }
}