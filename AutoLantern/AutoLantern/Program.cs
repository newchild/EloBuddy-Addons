using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AutoLantern
{
	class Program
	{
		private static Menu _SubMenu;

		private static Vector3 LanternPos;

		private static GameObject Lantern;

		private static bool LanternActive = false;

		static void Main(string[] args)
		{
			Loading.OnLoadingComplete += Loading_OnLoadingComplete;
		}

		private static void Loading_OnLoadingComplete(EventArgs args)
		{
			GameObject.OnCreate += GameObject_OnCreate;
			GameObject.OnDelete += GameObject_OnDelete;
			Game.OnTick += Game_OnTick;
			_SubMenu = MainMenu.AddMenu("Auto Lantern", "nal");
			_SubMenu.AddLabel("Thresh Auto Lantern Grabber");
			_SubMenu.Add("Distance", new Slider("Maximum distance to use", 150, 0, 700));
		}

		private static void Game_OnTick(EventArgs args)
		{
			if (LanternActive)
			{
				if (Player.Instance.Distance(LanternPos) <= _SubMenu["Distance"].Cast<Slider>().CurrentValue)
				{
					Player.CastSpell((SpellSlot)62, Lantern);
				}
			}
		}

		private static void GameObject_OnDelete(GameObject sender, EventArgs args)
		{
			if (sender.Name == "ThreshLantern")
			{
				LanternActive = false;
			}
		}

		private static void GameObject_OnCreate(GameObject sender, EventArgs args)
		{
			
			if(sender.Name == "ThreshLantern")
			{
				if(Player.Instance.Distance(sender) <= _SubMenu["Distance"].Cast<Slider>().CurrentValue)
				{
					Player.CastSpell((SpellSlot)62, sender);
				}
				LanternPos = sender.Position;
				LanternActive = true;
				Lantern = sender;
			}

			
		}
	}
}
