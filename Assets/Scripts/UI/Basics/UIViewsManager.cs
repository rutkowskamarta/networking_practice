using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
	public class UIViewsManager : MonoBehaviour, IUIViewsManager
	{
		[SerializeField]
		private UIViewType firstView;

		private readonly Dictionary<UIViewType, UIView> views = new Dictionary<UIViewType, UIView>();

		public UIView CurrentView { get; private set; }
		public UIView AdditionalView { get; private set; }

		private void Start()
		{
			Initialize();
		}

		public void RegisterUIView(UIViewType viewType, UIView view)
		{
			views.Add(viewType, view);
		}

		public void ShowViewOfType(UIViewType viewToShow, Action onShowCallback = null, Action onHiddenCallback = null)
		{
			if (views.ContainsKey(viewToShow))
			{
				if (CurrentView != null)
				{
					Action hideCallback = () => views[viewToShow].Show(onShowCallback);
					hideCallback += onHiddenCallback; 
					CurrentView.Hide(hideCallback);
				}
				else
				{
					views[viewToShow].Show(onShowCallback);
				}
				CurrentView = views[viewToShow];
			}
		}

		public void ShowAdditionalViewOfType(UIViewType viewToShow, Action onShowCallback = null, Action onHiddenCallback = null)
		{
			if (views.ContainsKey(viewToShow))
			{
				AdditionalView = views[viewToShow];
				views[viewToShow].Show(onShowCallback);
			}
		}

		public void HideAdditionalViewOfType(UIViewType viewToHide, Action onHiddenCallback = null)
		{
			if (views.ContainsKey(viewToHide))
			{
				if (AdditionalView == null)
				{
					CurrentView.Hide(onHiddenCallback);
				}
				else if(AdditionalView.ViewType == viewToHide)
				{
					AdditionalView.Hide(onHiddenCallback);
				}
			}
		}

		public UIView GetView(UIViewType viewEnum)
		{
			return views[viewEnum];
		}

		private void Initialize()
		{
			foreach (var view in views)
			{
				view.Value.HideInstant();
			}
			ShowViewOfType(firstView);
		}
	}
}