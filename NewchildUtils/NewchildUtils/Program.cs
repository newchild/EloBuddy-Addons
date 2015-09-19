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
		private static CheckBox _Mirror;
		private static bool _isMirrored = false;
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

		private static Menu _SkinMenu;
		private static Menu _CameraMenu;

		static void Main(string[] args)
		{
			Loading.OnLoadingComplete += Loading_OnLoadingComplete;
		
			
			
		}

		private static void Loading_OnLoadingComplete(EventArgs args)
		{
			Game.OnWndProc += Game_OnWndProc;
			_SkinMenu = MainMenu.AddMenu("Camera", "cameraMenu");
			_SkinMenu.AddLabel("You shouldn't use it right now :P");
			_Mirror = _SkinMenu.Add("camera.Mirror", new CheckBox("Mirror"));
			_Mirror.CurrentValue = false;
			_Mirror.DisplayName = "Mirror: " + _Mirror.CurrentValue.ToString();
			_Mirror.OnValueChange += _Mirror_OnValueChange;
			_SkinMenu = MainMenu.AddMenu("SkinHack", "skinhackMenu");
			foreach (var hero in ObjectManager.Get<AIHeroClient>())
			{
				if (ObjectManager.Player != hero)
				{
					var Slider = _SkinMenu.Add(hero.BaseSkinName, new Slider("Skin ID " + hero.BaseSkinName, 0, 0, 9));
					_SkinVals.Add(hero, Slider);
					_SkinVals[hero].OnValueChange += Program_OnValueChange;
				}
				
			}
			var slid = _SkinMenu.Add(ObjectManager.Player.BaseSkinName, new Slider("Skin ID " + ObjectManager.Player.BaseSkinName, 0, 0, 9));
			_SkinVals.Add(ObjectManager.Player, slid);
			_SkinVals[ObjectManager.Player].OnValueChange += Program_OnValueChange;

		}


		static void _Mirror_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
		{
			Camera.RotationX += 180;
		}

		

		private static void Program_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
		{
			var hero = ObjectManager.Get<AIHeroClient>().Where(x => x.BaseSkinName == sender.DisplayName.Replace("Skin ID ", "")).FirstOrDefault();
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
