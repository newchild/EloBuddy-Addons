using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;
using System.Threading.Tasks;

namespace NewchildUtils
{
	class Program
	{
		private static Dictionary<AIHeroClient, Slider> _SkinVals = new Dictionary<AIHeroClient, Slider>();

		enum Keys
		{
			LMB = 0x01,
			Numpad0 = 0x60,
			Numpad1,
			Numpad2,
			Numpad3,
			Numpad4,
			Numpad5,
			Numpad6,
			Numpad7,
			Numpad8,
			Numpad9
		}

		private static Menu _Menu;

		static void Main(string[] args)
		{
			Loading.OnLoadingComplete += Loading_OnLoadingComplete;
			
			
			
		}

		private static void Loading_OnLoadingComplete(EventArgs args)
		{
			Game.OnWndProc += Game_OnWndProc;
			Chat.Print("Creating Menu");
			_Menu = MainMenu.AddMenu("SkinHack", "skinhackMenu");
			foreach (var hero in HeroManager.AllHeroes)
			{
				_SkinVals.Add(hero, _Menu.Add(hero.BaseSkinName, new Slider("Skin ID", 0, 0, 9)));
				_SkinVals[hero].OnValueChange+=Program_OnValueChange;
			}

		}

		private static void Program_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
		{
			var hero = ObjectManager.Get<AIHeroClient>().Where(x => x.BaseSkinName == sender.DisplayName).FirstOrDefault();
			if (hero == null)
				return;
			hero.SetSkinId(args.NewValue);
		}

		static void Game_OnWndProc(WndEventArgs args)
		{
			if (args.Msg == 0x0100)
			{
				KeyDownCallBack((Keys)args.WParam);
			}
		}

		private static void KeyDownCallBack(Keys keys)
		{
			switch (keys)
			{
				case Keys.Numpad8:
					Camera.Angle += 3.0f;
					break;
				case Keys.Numpad2:
					Camera.Angle -= 3.0f;
					break;
				case Keys.Numpad4:
					Camera.RotationX += 3.0f;
					break;
				case Keys.Numpad6:
					Camera.RotationX -= 3.0f;
					break;
				case Keys.Numpad7:
					Camera.RotationY += 3.0f;
					break;
				case Keys.Numpad9:
					Camera.RotationY -= 3.0f;
					break;
			}
		}
	}
}
