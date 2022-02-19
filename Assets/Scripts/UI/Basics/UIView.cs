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
        private Sequence animationSequence;

        [Inject]
        private void Inject(IUIViewsManager uiViewsManager)
		{
            this.uiViewsManager = uiViewsManager;
            uiViewsManager.RegisterUIView(viewType, this);
        }

		public virtual void Show(Action onShownCallback = null)
		{
            TweenWindowShow(onShownCallback);
        }

        public virtual void Hide(Action onHiddenCallback = null)
		{
            TweenWindowHide(onHiddenCallback);
        }

        private void TweenWindowShow(Action onShownCallback = null)
		{
            animationSequence?.Complete(true);
            content.gameObject.SetActive(true);
            animationSequence = DOTween.Sequence().SetUpdate(true);
            animationSequence.Insert(0.0f, content.DOFade(1.0f, 1.0f).SetEase(Ease.OutQuad));
            animationSequence.AppendCallback(OnWindowShown);
            animationSequence.AppendCallback(() => onShownCallback?.Invoke());
		}

        private void TweenWindowHide(Action onShownCallback = null)
        {
            animationSequence?.Complete(true);
            animationSequence = DOTween.Sequence().SetUpdate(true);
            animationSequence.Insert(0.0f, content.DOFade(0.0f, 1.0f).SetEase(Ease.OutQuad));
            animationSequence.AppendCallback(OnWindowHide);
            animationSequence.AppendCallback(() => onShownCallback?.Invoke());
        }

        private void OnWindowShown()
        {
            content.blocksRaycasts = true;
        }

        private void OnWindowHide()
        {
            content.blocksRaycasts = false;
            content.gameObject.SetActive(false);
        }
    }
}