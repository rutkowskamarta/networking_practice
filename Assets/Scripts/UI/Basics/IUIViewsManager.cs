using System;

namespace Game.UI
{
	public interface IUIViewsManager
	{
		void RegisterUIView(UIViewType viewType, UIView view);
		void ShowViewOfType(UIViewType viewToShow, Action onShowCallback = null, Action onHiddenCallback = null);
		void HideViewOfType(UIViewType viewToHide, Action onHiddenCallback = null);
		UIView GetView(UIViewType viewEnum);
	}

}