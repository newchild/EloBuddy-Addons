using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EloBuddy;
using System.Threading.Tasks;

namespace EvadeBuddy
{
	class EvadableSpell
	{
		private SpellData _Data;
		private Type _Type;
		public enum Type{
			Circle,
			Line
		}
		public EvadableSpell(SpellData data, Type PredictionType)
		{
			_Data = data;
			_Type = PredictionType;
		}

		public float getDelay(){
			return _Data.SpellCastTime;
		}

		public float getWidth()
		{
			if (_Type == Type.Line)
				return _Data.LineWidth;
			if (_Type == Type.Circle)
				return _Data.CastRadius * 2;
			return 0.0f;
		}
	}
}
