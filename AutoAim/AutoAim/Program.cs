using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace AutoAim
{
	class Program
	{
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
		}

		private static AIHeroClient NearMouseSelect()
		{ 
 			var target = HeroManager.Enemies.OrderBy(hero => hero.Distance(Game.CursorPos)).FirstOrDefault(); 
 			return target; 
 		}


	private static void Player_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
		{
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
			}
		}
	}
}
