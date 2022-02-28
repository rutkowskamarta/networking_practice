using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class Throbber : MonoBehaviour
    {
		[SerializeField]
		private Image fill;
		[SerializeField]
		private float tweenDuration = 2f;

		private Sequence animationSequence;

		private void OnEnable()
		{
			AnimateThrobber();
		}

		private void OnDisable()
		{
			animationSequence.Kill();
		}

		private void AnimateThrobber()
		{
			var rectTransform = fill.GetComponent<RectTransform>();
			animationSequence = DOTween.Sequence().SetUpdate(true).SetLoops(-1, LoopType.Yoyo);
			animationSequence.Insert(0.0f, fill.DOFillAmount(1f, tweenDuration).SetEase(Ease.InOutExpo));
			animationSequence.Insert(0.0f, rectTransform.DORotate(new Vector3(0, 0, 360), tweenDuration, RotateMode.FastBeyond360));
		}
		
	}
}