using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActuallyDecentZed
{
	public class SpellManager
	{
		private Spell.Skillshot _Q, _W;
		private Spell.Active _E;
		private Spell.Targeted _R;
		private static SpellManager _Instance;

		private SpellManager()
		{
			_Q = new Spell.Skillshot(SpellSlot.Q, 915, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 250, 1700, 50);
			_W = new Spell.Skillshot(SpellSlot.W, 550, EloBuddy.SDK.Enumerations.SkillShotType.Linear);
			_E = new Spell.Active(SpellSlot.E, 280);
			_R = new Spell.Targeted(SpellSlot.R, 625);

		}

		public static SpellManager Instance
		{
			get
			{
				if(_Instance == null)
					_Instance = new SpellManager();
				return _Instance;
			}
		}

		public static SpellManager getInstance()
		{
			return Instance;
		}

		public float getRange(SpellSlot slot)
		{
			switch (slot)
			{
				case SpellSlot.Q:
					return _Q.Range;
				case SpellSlot.W:
					return _W.Range;
				case SpellSlot.E:
					return _E.Range;
				case SpellSlot.R:
					return _R.Range;
			}
			return 0;
		}

		public bool isSpellReady(SpellSlot slot)
		{
			switch (slot)
			{
				case SpellSlot.Q:
					return _Q.IsReady();
				case SpellSlot.W:
					return _W.IsReady();
				case SpellSlot.E:
					return _E.IsReady();
				case SpellSlot.R:
					return _R.IsReady();
			}
			return false;
		}
		public bool Cast(SpellSlot slot, Obj_AI_Base target)
		{
			if (target == null || target.IsDead || target.IsZombie)
				return false;
			switch (slot)
			{
				case SpellSlot.Q:
					return _Q.Cast(target);
				case SpellSlot.W:
					return _W.Cast(target);
				case SpellSlot.E:
					return _E.Cast();
				case SpellSlot.R:
					return _R.Cast(target);
			}
			return false;
		}

		public bool Cast(SpellSlot slot, Vector3 targetPos)
		{
			switch (slot)
			{
				case SpellSlot.Q:
					return _Q.Cast(targetPos);
				case SpellSlot.W:
					return _W.Cast(targetPos);
				case SpellSlot.E:
					return _E.Cast(targetPos);
				case SpellSlot.R:
					return _R.Cast(targetPos);
			}
			return false;
		}
	}
}
