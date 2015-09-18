using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using SharpDX;
using System.Text;
using System.Threading.Tasks;

namespace EvadeBuddy
{
	class Evade
	{
		private enum Mode{
			blocking,
			evading
		}


		private float _Distance(Vector3 VecOne, Vector3 VecTwo)
		{
			return (VecTwo - VecOne).Length() > 0.0f ? (VecTwo - VecOne).Length() : (VecOne - VecTwo).Length();
		}

		private float _Dot(Vector3 VectorOne, Vector3 VectorTwo)
		{
			return VectorOne.X * VectorTwo.X + VectorOne.Y * VectorTwo.Y + VectorOne.Z * VectorTwo.Z;
		}

		private bool _PosInSpell(Obj_AI_Base target, GameObjectProcessSpellCastEventArgs spell)
		{
			if (_Dot(spell.End - spell.Start, target.Position - spell.Start) < 0)
				return false;
			if (_Dot(spell.End - spell.Start, target.Position - spell.Start) > _Distance(spell.Start, spell.End) * _Distance(spell.Start, spell.End))
				return false;
			return true;
		}

		private void _Evade_OnSpellProc(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
		{
			EvadableSpell temp = new EvadableSpell(args.SData, EvadableSpell.Type.Line);
			Chat.Print(_PosInSpell(sender, args).ToString() + " <- IsInSpell ", temp.getWidth().ToString() + " <- Width");
		}


		private Dictionary<Obj_AI_Base, Mode> _Objects = new Dictionary<Obj_AI_Base, Mode>();
		public Evade(Obj_AI_Base Evading)
		{
			if (Evading.IsMe || Evading == ObjectManager.Player.Pet)
			{
				_Objects.Add(Evading, Mode.evading);
				Obj_AI_Base.OnProcessSpellCast += _Evade_OnSpellProc;
			}	
				
		}

		
	}
}
