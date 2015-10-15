using System;
using EloBuddy;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using SharpDX;



namespace ActuallyDecentZed
{
	#region UglyAsFuckNeedsRework
	class ShadowManager
	{
		public class UltShadowManager
		{
			private static UltShadowManager _Instance;

			private UltShadow _Shadow;

			public UltShadow getUltimateShadow()
			{
				if(!_Shadow.isValid())
					_Shadow = null;
				return _Shadow;
			}

			private UltShadowManager()
			{
				AIHeroClient.OnProcessSpellCast+=AIHeroClient_OnProcessSpellCast;
			}

			private void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
			{
				if (sender.IsMe && args.Slot == SpellSlot.R && args.SData.Name.Contains("1")) //Placeholder
					_Shadow = new UltShadow(sender.ServerPosition);
				if (sender.IsMe && args.Slot == SpellSlot.R && args.SData.Name.Contains("2"))
					_Shadow.updatePosition(sender.ServerPosition);

			}

			public static UltShadowManager Instance
			{
				get
				{
					if(_Instance == null)
						_Instance = new UltShadowManager();
					return _Instance;
				}
			}

		}

		public class UltShadow
		{
			private Vector3 _Position;

			private int expiration;

			public void updatePosition(Vector3 newPos)
			{
				_Position = newPos;
			}

			public UltShadow(Vector3 position)
			{

				expiration = Environment.TickCount + 6000; //Six seconds remaining
			}

			public bool isValid()
			{
				return Environment.TickCount < expiration;
			}

			public Vector3 getPosition()
			{
				return _Position;
			}

			public int getRemainingTicks()
			{
				return expiration - Environment.TickCount;
			}
		}

		public class WShadow
		{
			private Vector3 _Position;

			public void updatePosition(Vector3 newPos)
			{
				_Position = newPos;
			}

			private int expiration;

			public WShadow(Vector3 position)
			{

				expiration = Environment.TickCount + 4000; //Four seconds remaining
			}

			public bool isValid()
			{
				return Environment.TickCount < expiration;
			}

			public Vector3 getPosition()
			{
				return _Position;
			}

			
		}

		public class WShadowManager
		{
			private static WShadowManager _Instance;

			private WShadow _Shadow;

			public WShadow getWShadow()
			{
				if (!_Shadow.isValid())
					_Shadow = null;
				return _Shadow;
			}

			private WShadowManager()
			{
				AIHeroClient.OnProcessSpellCast+=AIHeroClient_OnProcessSpellCast;
			}

			private void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
			{
				if (sender == Player.Instance && args.Slot == SpellSlot.W && args.SData.Name.Contains("1"))
					_Shadow = new WShadow(calculateShadowPos(Player.Instance.Position, Player.Instance.Direction));
				if (sender == Player.Instance && args.Slot == SpellSlot.W && args.SData.Name.Contains("2"))
					_Shadow.updatePosition(sender.ServerPosition);

			}

			private Vector3 calculateShadowPos(Vector3 position, Vector3 direction)
			{
				return position + (direction * 550);
			}

			public static WShadowManager Instance
			{
				get
				{
					if (_Instance == null)
						_Instance = new WShadowManager();
					return _Instance;
				}
			}
		}
	}
#endregion
}

