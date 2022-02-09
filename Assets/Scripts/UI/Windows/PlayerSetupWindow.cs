using Game.Client;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class PlayerSetupWindow : Window
    {
        [SerializeField]
        private TMP_InputField nameInputField;
		[SerializeField]
		private Button setupPlayerButton;

		[Inject]
		private IClientManager clientManager;

		public override void Show()
		{
			base.Show();
			setupPlayerButton.onClick.AddListener(SetupPlayerButton_OnClick);
		}

		public override void Hide()
		{
			base.Hide();
			setupPlayerButton.onClick.RemoveListener(SetupPlayerButton_OnClick);
		}

		private void SetupPlayerButton_OnClick()
		{
			clientManager.SendRequest(nameInputField.text);
		}
	}
}