using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameCategory : MonoBehaviour
    {
        private event Action DeleteCategoryAction;

        [SerializeField]
        private TMP_Text categoryNameText;
        [SerializeField]
        private Button deleteButton;

        public void Initialize(string categoryName, Action deleteCategoryAction)
		{
            categoryNameText.SetText(categoryName);
            DeleteCategoryAction = deleteCategoryAction;
            deleteButton.onClick.AddListener(DeleteButton_OnClick);
        }

        private void DeleteButton_OnClick()
		{
            DeleteCategoryAction?.Invoke();
        }
    }
}