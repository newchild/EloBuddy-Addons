using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActuallyDecentZed
{
	class Program
	{
		static void Main(string[] args)
		{
			if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
				ComboExecutor.CastLineComboOnto(TargetSelector.GetTarget(450, DamageType.Mixed));
		}
	}
}
