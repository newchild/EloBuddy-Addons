using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellInfo
{
		public class SpellInfo
		{
			private SpellData _Data;
			private SpellSlot _Slot;
			private GameObjectProcessSpellCastEventArgs _Event;

			public SpellInfo(GameObjectProcessSpellCastEventArgs SpellEvent)
			{
				_Data = SpellEvent.SData;
				_Event = SpellEvent;
			}

			public SpellInfo(SpellSlot slot)
			{
				_Slot = slot;
				_Data = Player.Instance.Spellbook.GetSpell(slot).SData;
				_Event = null;
			}

			public bool isTargeted()
			{
				return (_Data.TargettingType == SpellDataTargetType.Self || _Data.TargettingType == SpellDataTargetType.Unit || _Data.TargettingType == SpellDataTargetType.SelfAndUnit || _Data.TargettingType == SpellDataTargetType.SelfAoe);
			}
			/// <summary>
			/// Generates a Spell from the given SpellSlot 
			/// Only works for skillshots and targeted Spells
			/// Spells might need manual adjusting
			/// </summary>
			/// <returns></returns>
			public Spell.SpellBase generateSpell(int rangeOffset = 0, int castDelayOffset = 0, int missileSpeedOffset = 0, int widthOffset = 0)
			{
				
				if (isTargeted())
				{
					return new Spell.Targeted(_Slot, (uint) getRange());
				}
				return new Spell.Skillshot(_Slot, (uint) (getRange() + rangeOffset), getSkillShotType(), (int)( getCastDelay() * 1000 + castDelayOffset), (int)(getMissileSpeed() * 1000 + missileSpeedOffset), (int)(getWidth() * 1000 + widthOffset));
			}

			

			public bool isChannel()
			{
				return _Data.ChannelDuration >= 0.0f;
				
			}

			public bool isToggle()
			{
				return _Data.IsToggleSpell;
			}
		

			public SkillShotType getSkillShotType()
			{
				switch (_Data.TargettingType)
				{
					case SpellDataTargetType.Cone:
						return SkillShotType.Cone;
					case SpellDataTargetType.LocationAoe:
						return SkillShotType.Circular;
				}
				return SkillShotType.Linear;
			}

			public string getName()
			{
				return _Data.Name;
			}

			public float getMissileSpeed()
			{
				return _Data.MissileSpeed;
			}

			

			public float getCastDelay()
			{
				return _Data.CastTime;
			}

			public float getRange()
			{
				if (_Data.TargettingType == SpellDataTargetType.Cone)
					return _Data.CastConeDistance;
				return _Data.CastRange;
			}

			public float getCastedSpellLength()
			{
				if (_Event == null)
					throw new Exception("Can't get Length of a non casted Spell, use getRange() instead");
				if (_Data.LineMissileEndsAtTargetPoint)
					return _Event.End.Distance(_Event.Start);
				if (_Data.TargettingType == SpellDataTargetType.Cone)
					return _Data.CastConeDistance;
				return _Data.CastRange;
			}

			public float getWidth()
			{
				return _Data.LineWidth;
			}

			
		
	}
}
