using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EloBuddy;
using EloBuddy.SDK.Menu;
using System.Threading.Tasks;

namespace EvadeBuddy
{
	class Program
	{
		private static Evade _EvadeInst;
		static void Main(string[] args)
		{
			Game.OnLoad += Game_OnLoad;
		}

		static void Game_OnLoad(EventArgs args)
		{
			_EvadeInst = new Evade(ObjectManager.Player);
			Chat.Print("loaded");
		}
	}
}
