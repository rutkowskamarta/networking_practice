using Game.Game;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class GameView : UIView
    {
        [SerializeField]
        private GameAnswerHolderUI gameAnswerHolderUI;
        [SerializeField]
        private Button stopButton;

        [Inject]
        private IGameManager gameManager;

        public override void Show(Action onShownCallback = null)
        {
            base.Show(onShownCallback);
            gameAnswerHolderUI.Initialize(gameManager.GameCategories);
            stopButton.onClick.AddListener(StopButton_OnClick);
        }

        public override void Hide(Action onHiddenCallback = null)
        {
            base.Hide(onHiddenCallback);
            stopButton.onClick.RemoveListener(StopButton_OnClick);
        }
      
        private void StopButton_OnClick()
        {

        }
    }
}