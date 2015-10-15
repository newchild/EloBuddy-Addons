using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace ActuallyDecentZed
{
	class ComboExecutor
	{
		public static void CastLineComboOnto(Obj_AI_Base target)
		{
			if (!SpellManager.Instance.isSpellReady(SpellSlot.R))
			{
				if (ComboLogic.isRActive)
				{
					if (!SpellManager.Instance.isSpellReady(SpellSlot.W))
					{
						if (ComboLogic.isWActive)
						{
							ExecuteLineCombo(target);
						}
						else
						{
							return;
						}
					}	
				}
				else
				{
					return;
				}
			}
			else
			{
				ExecuteLineCombo(target);
			}
		}


		private static void ExecuteLineCombo(Obj_AI_Base target)
		{
			SpellManager.Instance.Cast(SpellSlot.R, target);
			Core.DelayAction(() => {LineComboStage2(target);}, 500);
		}

		private static void LineComboStage2(Obj_AI_Base target)
		{
			Vector3 WPos = ShadowManager.UltShadowManager.Instance.getUltimateShadows().FirstOrDefault().getPosition()
				.Extend(Player.Instance, Player.Instance.Distance(ShadowManager.UltShadowManager.Instance.getUltimateShadows().FirstOrDefault().getPosition()) + 550).To3D();
			SpellManager.Instance.Cast(SpellSlot.W, WPos);
			Core.DelayAction(() => { LineComboStage3(target); }, 250);
		}

		private static void LineComboStage3(Obj_AI_Base target)
		{
			SpellManager.Instance.Cast(SpellSlot.Q, target);
			SpellManager.Instance.Cast(SpellSlot.E, null);
			
		}
	}
}
