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

namespace Freaky_twitch
{
	class Program
	{
		private static Menu _Menu;
		private static Spell.Skillshot _W;
		private static int[] _EDamage = new int[] { 20, 35, 50, 65, 80};
		private static Spell.Active _E;
		static void Main(string[] args)
		{
			Loading.OnLoadingComplete+=Loading_OnLoadingComplete;
		}

		private static void Loading_OnLoadingComplete(EventArgs args)
		{
			if (ObjectManager.Player.BaseSkinName != "Twitch")
				return;
			_Menu = MainMenu.AddMenu("Freaky Twitch", "Twitch.MainMenu");
			_Menu.Add("Twitch.UseE", new Slider("Cast E at x Stacks", 1, 5, 5));
			_Menu.Add("Twitch.UseW", new CheckBox("Use W in Comco"));
			_Menu.Add("Twitch.KS", new CheckBox("Use E to KS"));
			_W = new Spell.Skillshot(SpellSlot.W, 900, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 250, 1400, 275);
			_E = new Spell.Active(SpellSlot.E, 1200);
			Game.OnTick += Game_OnTick;
		}

		static void Game_OnTick(EventArgs args)
		{
			switch (Orbwalker.ActiveModesFlags)
			{
				case Orbwalker.ActiveModes.Combo:
					_Combo();
					break;
			}
			_KS();
		}

		private static void _KS()
		{
			foreach (var Hero in ObjectManager.Get<AIHeroClient>().Where(x => x.Position.Distance(ObjectManager.Player.Position) < 1200))
			{
				if (_ECanKill(Hero, _E) && _Menu["Twitch.KS"].Cast<CheckBox>().CurrentValue)
					_E.Cast();
			}
		}

		

		private static void _Combo()
		{
			var WTarget = TargetSelector.GetTarget(_W.Range, DamageType.True);
			if (_Menu["Twitch.UseW"].Cast<CheckBox>().CurrentValue && !_W.IsOnCooldown)
				_W.Cast(WTarget);
			foreach (var Hero in ObjectManager.Get<AIHeroClient>().Where(x => x.Position.Distance(ObjectManager.Player.Position) < 1200))
			{
				if (Hero.GetBuffCount("twitchdeadlyvenom") >= _Menu["Twitch.UseE"].Cast<Slider>().CurrentValue)
				{
					_E.Cast();
				}
			}

		}

		private static bool _ECanKill(AIHeroClient Hero, Spell.Active _E)
		{
			float EDamage = Convert.ToSingle(Hero.GetBuffCount("twitchdeadlyvenom") *( _EDamage[_E.Level] + ObjectManager.Player.TotalAttackDamage * 0.25 + ObjectManager.Player.TotalMagicalDamage * 0.2));
			if (Damage.CalculateDamageOnUnit(ObjectManager.Player, Hero, DamageType.Physical, EDamage) > Hero.Health)
				return true;
			return false;

		}
	}
}
