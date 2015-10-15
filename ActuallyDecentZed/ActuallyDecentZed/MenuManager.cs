using EloBuddy.SDK.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActuallyDecentZed
{
	class MenuManager
	{
		private static MenuManager _Instance; //yey yes another singleton, fuck me right?

		private Menu _RootMenu;
		private Menu _ComboMenu;

		public dynamic getValue(string Identifier)
		{
			return null;
		}

		private MenuManager()
		{
			newTargetSelector.getInstance();//Set up TS first :^)
			_RootMenu = MainMenu.AddMenu("Decent Zed", "newchild::DecentZed");
			_RootMenu.AddGroupLabel("Decent Zed");
			_RootMenu.AddLabel("This my first \"real\" Addon, please no hate Kappa");
			_ComboMenu = _RootMenu.AddSubMenu("Combo", "newchild::DecentZed::Combo");
		}

		public static MenuManager getInstance()
		{
			if (_Instance == null)
				_Instance = new MenuManager();
			return _Instance;
		}

		public static MenuManager  Instance
		{
			get
			{
				if(_Instance == null)
					_Instance = new MenuManager();
				return _Instance;
			}
		}
	}
}
