using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;

namespace AutoAim
{
	class Program
	{
		private static Menu _AutoAimMenu;
		private static Dictionary<SpellSlot, bool> isAddonCasted = new Dictionary<SpellSlot, bool>() { { SpellSlot.Q, false }, { SpellSlot.W, false }, { SpellSlot.E, false }, { SpellSlot.R, false } };
        static void Main(string[] args)
		{
			
			if (!Spells.SpellInstances.Keys.Contains(Player.Instance.Hero))
				return;
			Loading.OnLoadingComplete += Loading_OnLoadingComplete;
		}

		private static void Loading_OnLoadingComplete(EventArgs args)
		{
			Player.OnSpellCast += Player_OnSpellCast;
			_AutoAimMenu = MainMenu.AddMenu("AutoAIM", "AutoAim");
			_AutoAimMenu.AddLabel("Addon created by newchild. Supported champs: Aatrox, Ahri, Amumumu, Annie");
			_AutoAimMenu.AddLabel("Quality right now is meh, but it works. Casting logic needs a rework");
			_AutoAimMenu.AddLabel("Basically you just have to cast Skillshots and it will try to aim for you or block the spell. RiP brain");
			_AutoAimMenu.AddLabel("PLEASE LEAVE FEEDBACK, SINCE MY CASTING LOGIC NEEDS IMPROVEMENT");
			_AutoAimMenu.AddLabel("BUT I NEED EMPIRICAL DATA");
        }

		private static AIHeroClient NearMouseSelect()
		{ 
 			var target = HeroManager.Enemies.OrderBy(hero => hero.Distance(Game.CursorPos)).FirstOrDefault(); 
 			return target; 
 		}


	private static void Player_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
		{
			Console.WriteLine(args.Slot.ToString());
			if ((int)args.Slot > 3)
				return;
			if (isAddonCasted[args.Slot])
			{
				isAddonCasted[args.Slot] = false;
				return;
			}
			switch (args.Slot)
			{
				case SpellSlot.Q:
					if (Spells.SpellInstances[Player.Instance.Hero].QIsSkillShot)
					{
						args.Process = false;
						isAddonCasted[args.Slot] = true;
						Spells.SpellInstances[Player.Instance.Hero].Q.Cast(NearMouseSelect());
					}
					break;
				case SpellSlot.E:
					if (Spells.SpellInstances[Player.Instance.Hero].WIsSkillShot)
					{
						args.Process = false;
						isAddonCasted[args.Slot] = true;
						Spells.SpellInstances[Player.Instance.Hero].W.Cast(NearMouseSelect());
					}
					break;
				case SpellSlot.W:
					if (Spells.SpellInstances[Player.Instance.Hero].EIsSkillShot)
					{
						args.Process = false;
						isAddonCasted[args.Slot] = true;
						Spells.SpellInstances[Player.Instance.Hero].E.Cast(NearMouseSelect());
					}
					break;
				case SpellSlot.R:
					if (Spells.SpellInstances[Player.Instance.Hero].RIsSkillShot)
					{
						args.Process = false;
						isAddonCasted[args.Slot] = true;
						Spells.SpellInstances[Player.Instance.Hero].R.Cast(NearMouseSelect());
					}
					break;
				default:
					return;
			}
		}
	}
}
