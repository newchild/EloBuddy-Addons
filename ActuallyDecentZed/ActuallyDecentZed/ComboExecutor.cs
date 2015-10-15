using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;

namespace ActuallyDecentZed
{
	class ComboExecutor
	{
		public static void CastComboOnto(Obj_AI_Base target)
		{
			if (!SpellManager.Instance.isSpellReady(SpellSlot.R))
			{
				if (ComboLogic.isRActive)
				{
					SpellManager.Instance.Cast(SpellSlot.Q, Player.Instance.Position);
					
				}
			}
		}
	}
}
