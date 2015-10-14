using EloBuddy;
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
			newTargetSelector.Instance.getTarget(SpellManager.Instance.getRange(SpellSlot.Q));
		}
	}
}
