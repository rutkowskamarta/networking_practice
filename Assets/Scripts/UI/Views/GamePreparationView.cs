using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GamePreparationView : UIView
    {
        [SerializeField]
        private TMP_InputField roundsInputField;

        [SerializeField]
        private Transform categoriesParent;
        [SerializeField]
        private GameCategory gameCategoryPrefab;

        [SerializeField]
        private TMP_InputField newCategoryInputField;
        [SerializeField]
        private Button addCategoryButton;
        [SerializeField]
        private Button readyButton;

        public override void Show(Action onShownCallback = null)
        {
            base.Show(onShownCallback);
        }

        public override void Hide(Action onHiddenCallback = null)
		{
			base.Hide(onHiddenCallback);
		}
	}
}