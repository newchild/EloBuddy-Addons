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
			private UltShadowManager _Instance;

			private List<UltShadow> _Shadows;

			public List<UltShadow> getUltimateShadows()
			{
				foreach (var shadow in _Shadows)
				{
					if (!shadow.isValid())
						_Shadows.Remove(shadow);
				}
				return _Shadows;
			}

			private UltShadowManager()
			{
				_Shadows = new List<UltShadow>();
				AIHeroClient.OnProcessSpellCast+=AIHeroClient_OnProcessSpellCast;
			}

			private void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
			{
				if (sender.IsMe && args.Slot == SpellSlot.R && args.SData.Name.Contains("1"))
					_Shadows.Add(new UltShadow(sender.ServerPosition));
				if (sender.IsMe && args.Slot == SpellSlot.R && args.SData.Name.Contains("2"))
					_Shadows.FirstOrDefault().updatePosition(sender.ServerPosition);

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
			private WShadowManager _Instance;

			private List<WShadow> _Shadows;

			public List<WShadow> getWShadows()
			{
				foreach (var shadow in _Shadows)
				{
					if (!shadow.isValid())
						_Shadows.Remove(shadow);
				}
				return _Shadows;
			}

			private WShadowManager()
			{
				_Shadows = new List<WShadow>();
				AIHeroClient.OnProcessSpellCast+=AIHeroClient_OnProcessSpellCast;
			}

			private void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
			{
				if (sender == Player.Instance && args.Slot == SpellSlot.W && args.SData.Name.Contains("1"))
					_Shadows.Add(new WShadow(calculateShadowPos(Player.Instance.Position, Player.Instance.Direction)));
				if (sender == Player.Instance && args.Slot == SpellSlot.W && args.SData.Name.Contains("2"))
					_Shadows.FirstOrDefault().updatePosition(sender.ServerPosition);

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

