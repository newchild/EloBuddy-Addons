using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Enumerations;
using EloBuddy;
using EloBuddy.SDK;

namespace AutoAim
{
	class Spells
	{
		public static Dictionary<Champion, SpellInstance> SpellInstances = new Dictionary<Champion, SpellInstance>()
		{
			#region Aatrox
			{ Champion.Aatrox, new SpellInstance()
				{
					Q = new Spell.Skillshot(SpellSlot.Q, 650, SkillShotType.Circular, 250, 450, 650 ),
					E = new Spell.Skillshot(SpellSlot.E, 1075, SkillShotType.Linear, 250, 1200, 100 ),
					QIsSkillShot = true,
					WIsSkillShot = false,
					EIsSkillShot = true,
					RIsSkillShot = false
				}
			},
			#endregion
			#region Ahri
			{ Champion.Aatrox, new SpellInstance()
				{
					Q = new Spell.Skillshot(SpellSlot.Q, 925, SkillShotType.Circular, 250, 1750, 100 ),
					E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Linear, 250, 1550, 60 ),
					QIsSkillShot = true,
					WIsSkillShot = false,
					EIsSkillShot = true,
					RIsSkillShot = false
				}
			}
			#endregion
			
		};
	}

	class SpellInstance
	{
		public Spell.SpellBase Q;
		public Spell.SpellBase W;
		public Spell.SpellBase E;
		public Spell.SpellBase R;
		public bool QIsSkillShot;
		public bool WIsSkillShot;
		public bool EIsSkillShot;
		public bool RIsSkillShot;

	}
}
