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
		private static Slider _DelayVal;
		private static float _LastTick;
		private static Menu _RootMenu;
		private static float _Delay
		{
			get
			{
				return Convert.ToSingle(_DelayVal.CurrentValue * 0.001);
			}
			set
			{
				_DelayVal.CurrentValue = Convert.ToInt32(value * 1000);
			}
		}
		static void Main(string[] args)
		{
			Loading.OnLoadingComplete +=Loading_OnLoadingComplete;
		}

		private static void Loading_OnLoadingComplete(EventArgs args)
		{
			_RootMenu = MainMenu.AddMenu("HumanizerBuddy", "HumanizerBuddy");
			_DelayVal = _RootMenu.Add("Delay", new Slider("Delay between actions", 0, 0, 1500));
			_LastTick = Game.Time;
			Player.OnProcessSpellCast += Player_OnProcessSpellCast;
			Player.OnIssueOrder += Player_OnIssueOrder;
		}

		static void Player_OnIssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
		{
			
			if (Game.Time < (_LastTick + _Delay) && args.Order == GameObjectOrder.MoveTo)
			{
				args.Process = false;
			}
			else
			{
				_LastTick = Game.Time;
			}
		}

		static void Player_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
		{
			if (Game.Time < (_LastTick + _Delay))
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
