using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    [DisallowMultipleComponent]
    public abstract class UIView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup content;
        [SerializeField]
        private UIViewType viewType = default;

        protected IUIViewsManager uiViewsManager;

        [Inject]
        private void Inject(IUIViewsManager uiViewsManager)
		{
            this.uiViewsManager = uiViewsManager;
            uiViewsManager.RegisterUIView(viewType, this);
        }

		public virtual void Show(Action onShownCallback = null)
		{
            content.gameObject.SetActive(true);
            onShownCallback?.Invoke(); 
		}

        public virtual void Hide(Action onHiddenCallback = null)
		{
            content.gameObject.SetActive(false);
            onHiddenCallback?.Invoke();
        }

        private void TweenWindowShow()
		{

		}

        private void AddFadeInAnimation(bool shouldAnimate)
        {
            //animationSequence.Insert(0.0f, content.DOFade(1.0f, shouldAnimate ? animationDuration : 0.0f).SetEase(Ease.OutQuad));
        }
    }
}