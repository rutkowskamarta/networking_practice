using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameCategoryUI : MonoBehaviour
    {
        private event Action<GameCategoryUI> RemoveCategoryAction;

        [SerializeField]
        private TMP_Text categoryNameText;
        [SerializeField]
        private Button removeButton;

        public string CategoryName { get; private set; }

        public void Initialize(string categoryName, Action<GameCategoryUI> removeCategoryAction)
		{
            CategoryName = categoryName;
            categoryNameText.SetText(categoryName);
            RemoveCategoryAction = removeCategoryAction;
            removeButton.onClick.AddListener(DeleteButton_OnClick);
        }

		public void Destroy()
		{
            Destroy(gameObject);
		}

		private void DeleteButton_OnClick()
		{
            RemoveCategoryAction?.Invoke(this);
        }
    }
}