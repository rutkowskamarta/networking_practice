using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup content;
        [SerializeField]
        private bool hideAtStart;

		private void Start()
		{
            Initialize();
        }

		public virtual void Show()
		{
            gameObject.SetActive(true);
		}

        public virtual void Hide()
		{
            gameObject.SetActive(false);
		}

        private void Initialize()
		{
            if (hideAtStart)
            {
                Hide();
            }
			else
			{
                Show();
			}
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