using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;

namespace HumanizerBuddy
{
	class Program
	{
		private static Slider _SpellDelayVal;
		private static float _LastTick;
		private static Menu _RootMenu;
		private static float _SpellDelay
		{
			get
			{
				return Convert.ToSingle(_SpellDelayVal.CurrentValue * 0.001);
			}
			set
			{
				_SpellDelayVal.CurrentValue = Convert.ToInt32(value * 1000);
			}
		}
		static void Main(string[] args)
		{
			Loading.OnLoadingComplete +=Loading_OnLoadingComplete;
		}

		private static void Loading_OnLoadingComplete(EventArgs args)
		{
			_RootMenu = MainMenu.AddMenu("HumanizerBuddy", "HumanizerBuddy");
			_RootMenu.AddLabel("I am crediting Trees here, because reasons :^)");
			_SpellDelayVal = _RootMenu.Add("SDelay", new Slider("Delay between SpellCommands (Beware of prediction issues)", 0, 0, 100));
			_LastTick = Game.Time;
			Player.OnProcessSpellCast += Player_OnProcessSpellCast;
		}

		static void Player_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
		{
			if (Game.Time < (_LastTick + _SpellDelay))
			{
				args.Process = false;
			}
			else
			{
				_LastTick = Game.Time;
			}
		}
	}
}
