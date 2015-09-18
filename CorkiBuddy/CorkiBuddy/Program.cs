using System;
using System.Collections;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Utils;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace CorkiBuddy
{
	class Program
	{

		private static Menu _MainMenu;
		private static Slider _ManaWC;
		private static Slider _RocketCount;
		private static CheckBox _shouldSheen;
		private static Spell.Active _E = new Spell.Active(SpellSlot.E, 600);
		private static Dictionary<SpellSlot, CheckBox> _ComboSpellStatus = new Dictionary<SpellSlot, CheckBox>();
		private static Dictionary<SpellSlot, CheckBox> _WaveClearSpellStatus = new Dictionary<SpellSlot, CheckBox>();
		private static Dictionary<SpellSlot, Spell.SpellBase> _Spells = new Dictionary<SpellSlot, Spell.SpellBase>();
		private static int _UltSaveCount = 3;
		static void Main(string[] args)
		{
			Loading.OnLoadingComplete += onLoadComplete;
		}

		private static bool _ShouldCastSheen()
		{
			bool hasSheen = false;
			foreach (var item in Player.Instance.InventoryItems)
			{
				if (item.DisplayName == "Sheen" || item.DisplayName == "Trinity Force" && !Player.Instance.HasBuff("sheen") && item.CanUseItem())
					hasSheen = true;
			}
			return hasSheen;
		
		}

		private static void onLoadComplete(EventArgs args)
		{
			if (Player.Instance.ChampionName != "Corki")
				return;
			Drawing.OnDraw += Drawing_OnDraw;
			Orbwalker.OnPreAttack += Orbwalker_OnPreAttack;
			_MainMenu = MainMenu.AddMenu("CorkiBuddy", "CorkiBuddyMenu");
			_MainMenu.AddGroupLabel("Info");
			_MainMenu.AddLabel("This is a simple Corki Addon");
			_MainMenu.AddGroupLabel("Combo");
			_SetUpSpellStati();
			_ComboSpellStatus[SpellSlot.Q] = _MainMenu.Add("comboUseQ", new CheckBox("Use Q"));
			_ComboSpellStatus[SpellSlot.E] = _MainMenu.Add("comboUseE", new CheckBox("Use E"));
			_ComboSpellStatus[SpellSlot.R] = _MainMenu.Add("comboUseR", new CheckBox("Use R"));
			_RocketCount = _MainMenu.Add("comboSaveR", new Slider("Save this amount of R stacks", 2, 0, 6));
			//_shouldSheen = _MainMenu.Add("EnforeSheen", new CheckBox("Try to cast a spell before an autoattack, if your sheen is not on Cooldown"));
			_MainMenu.AddGroupLabel("Farming");
			/*_WaveClearSpellStatus[SpellSlot.Q] = _MainMenu.Add("wcUseQ", new CheckBox("Use Q", false));
			_WaveClearSpellStatus[SpellSlot.E] = _MainMenu.Add("wcUseE", new CheckBox("Use E"));
			_ManaWC = _MainMenu.Add("wcSaveMana", new Slider("Dont use Spells when under X Mana", 60, 0, 100));
			 */
			_MainMenu.AddLabel("Currently WiP");
			_Spells.Add(SpellSlot.Q, new Spell.Skillshot(SpellSlot.Q, 825, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 300, 1000, 250));
			_Spells.Add(SpellSlot.R, new Spell.Skillshot(SpellSlot.R, 1300, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 200, 2000, 40)); //todo: add R2
			Game.OnTick += Game_OnTick;
		}

		static void Game_OnTick(EventArgs args)
		{
			switch (Orbwalker.ActiveModesFlags)
			{
				case Orbwalker.ActiveModes.Combo:
					_Combo();
					break;
				case Orbwalker.ActiveModes.LaneClear:
					_WaveClear();
					break;
			}
		}

		private static void _WaveClear()
		{
			if (_WaveClearSpellStatus[SpellSlot.Q].CurrentValue && _Spells[SpellSlot.Q].IsReady() && _ManaWC.CurrentValue > Player.Instance.ManaPercent)
				_Spells[SpellSlot.Q].Cast(EntityManager.GetLaneMinions(radius: 850)[0]); //logic sucks
			if (_WaveClearSpellStatus[SpellSlot.E].CurrentValue && _E.IsReady() && _ManaWC.CurrentValue > Player.Instance.ManaPercent)
				_E.Cast();

		}

		private static void _Combo()
		{
			var RTarget = TargetSelector.GetTarget(1300.0f, DamageType.Magical);
			var ETarget = TargetSelector.GetTarget(600.0f, DamageType.Physical);
			if (_ComboSpellStatus[SpellSlot.Q].CurrentValue && _Spells[SpellSlot.Q].IsReady())
				_Spells[SpellSlot.Q].Cast(TargetSelector.GetTarget(825.0f, DamageType.Magical));
			if (_ComboSpellStatus[SpellSlot.E].CurrentValue && _E.IsReady() && ETarget != null)
				_E.Cast();
			if (_ComboSpellStatus[SpellSlot.R].CurrentValue && _Spells[SpellSlot.R].IsReady() && _Spells[SpellSlot.R].Handle.Ammo > _RocketCount.CurrentValue)
				_Spells[SpellSlot.R].Cast(RTarget);

		}

		private static void _SetUpSpellStati()
		{
			_ComboSpellStatus.Add(SpellSlot.Q, null);
			_ComboSpellStatus.Add(SpellSlot.E, null);
			_ComboSpellStatus.Add(SpellSlot.R, null);
			_WaveClearSpellStatus.Add(SpellSlot.Q, null);
			_WaveClearSpellStatus.Add(SpellSlot.E, null);

		}

		private static void Orbwalker_OnPreAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
		{
			/*if (!target.IsEnemy || target.Name.Contains("minion") || target == null)
				return;
			if (_shouldSheen.CurrentValue && _ShouldCastSheen())
			{
				var targetLoc = TargetSelector.GetTarget(825.0f, DamageType.Magical);
				if (targetLoc != null && _Spells[SpellSlot.Q].IsReady() && _Spells[SpellSlot.Q].Cast(targetLoc))
				{
					args.Process = false;
					Orbwalker.ResetAutoAttack();
					return;
				}
				if (_Spells[SpellSlot.E].IsReady() && _E.Cast())
				{
					args.Process = false;
					Orbwalker.ResetAutoAttack();
					return;
				}
				if (targetLoc != null && _Spells[SpellSlot.R].IsReady() && _Spells[SpellSlot.R].Cast(targetLoc))
				{
					args.Process = false;
					Orbwalker.ResetAutoAttack();
					return;
				}
			}*/

			
		}

		private static void Drawing_OnDraw(EventArgs args)
		{
			
		}

	}
}
