using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace ActuallyDecentZed
{
	public class newTargetSelector
	{
		private float _LastTick = 0.0f;
		private float _Delay = 0.0f;
		public enum HumanMode
		{
			Slow,
			Human,
			Fast,
			Instant
		}

		public delegate void onModeChangedDelegate(HumanMode mode);
		public event onModeChangedDelegate onModeChanged;

		public enum SelectionMode
		{
			NearMouse,
			LeastHealth,
			LeastArmorKill,
			LeastMRKill,
			Closest,
			Orbwalker
		}

		private SelectionMode _ActiveSelectionMode;

		private HumanMode _ActiveModeContainer;
		private HumanMode _ActiveMode
		{
			get
			{
				return _ActiveModeContainer;
			}

			set
			{
				_onModeChangedLocal(value);
				_ActiveModeContainer = value;
			}
		}

		private void _onModeChangedLocal(HumanMode value)
		{
			switch (value)
			{
				case HumanMode.Human:
					_Delay = 500.0f;
					break;
				case HumanMode.Fast:
					_Delay = 250.0f;
					break;
				case HumanMode.Slow:
					_Delay = 1000.0f;
					break;
				case HumanMode.Instant:
					_Delay = 0.0f;
					break;
			}
			onModeChanged(value);
		}

		public AIHeroClient getTarget(float range)
		{
			if (_LastTick < Environment.TickCount + _Delay)
				return null;
			var possibleHeroes = EntityManager.Heroes.Enemies.Where(hero => hero.Distance(Player.Instance) <= range);
			switch (_ActiveSelectionMode)
			{
				case SelectionMode.Closest:
					return ClosestSelect(possibleHeroes);

				case SelectionMode.LeastHealth:
					return LeastHealthSelect(possibleHeroes);

				case SelectionMode.LeastMRKill:
					return LeastSpellsSelect(possibleHeroes);

				case SelectionMode.LeastArmorKill:
					return LeastAttackSelect(possibleHeroes);

				case SelectionMode.NearMouse:
					return NearMouseSelect(possibleHeroes);

				case SelectionMode.Orbwalker:
					return OrbwalkerSelect(possibleHeroes);
			}
			return null;
		}

		private AIHeroClient LeastHealthSelect(IEnumerable<AIHeroClient> possibleHeroes)
		{
			var target = possibleHeroes.OrderBy(x => x.Health).FirstOrDefault();
			if (target != null)
				_LastTick = Environment.TickCount;
			return target;
		}

		private AIHeroClient OrbwalkerSelect(IEnumerable<AIHeroClient> possibleHeroes)
		{
			if (Orbwalker.ForcedTarget.Type == GameObjectType.AIHeroClient)
			{
				var hero = Orbwalker.ForcedTarget as AIHeroClient;
				if (possibleHeroes.Contains(hero))
				{
					_LastTick = Environment.TickCount;
					return hero;
				}
				return null;
			}
			return null;


		}

		private AIHeroClient NearMouseSelect(IEnumerable<AIHeroClient> possibleHeroes)
		{
			var target = possibleHeroes.OrderBy(hero => hero.Distance(Game.CursorPos)).FirstOrDefault();
			if (target != null)
				_LastTick = Environment.TickCount;
			return target;
		}

		private AIHeroClient LeastSpellsSelect(IEnumerable<AIHeroClient> possibleHeroes)
		{
			var target = possibleHeroes.OrderBy(hero => hero.PercentMagicReduction * hero.Health).FirstOrDefault();
			if (target != null)
				_LastTick = Environment.TickCount;
			return target;
		}

		private AIHeroClient LeastAttackSelect(IEnumerable<AIHeroClient> possibleHeroes)
		{
			var target = possibleHeroes.OrderBy(hero => hero.PercentArmorMod * hero.Health).FirstOrDefault();
			if (target != null)
				_LastTick = Environment.TickCount;
			return target;
		}

		private AIHeroClient ClosestSelect(IEnumerable<AIHeroClient> possibleHeroes)
		{
			var target = possibleHeroes.OrderBy(hero => hero.Distance(Player.Instance)).FirstOrDefault();
			if (target != null)
				_LastTick = Environment.TickCount;
			return target;
		}


		public void setMode(HumanMode mode)
		{
			_ActiveMode = mode;
		}

		public HumanMode getMode()
		{
			return _ActiveMode;
		}

		private static newTargetSelector _Instance;

		private Menu _Menu;

		private Slider _modeSlide;
		private Slider _humanizerSlider;
		private void _SetupMenu()
		{
			_Menu = MainMenu.AddMenu("newTarget Selector", "newchild.TS.Menu");
			_modeSlide = _Menu.Add("newchild.TS.currentSelectionMode", new Slider("Current Selectionmode: NearMouse", 0, 0, 5));
			_modeSlide.OnValueChange += _modeSlide_OnValueChange;
			_humanizerSlider = _Menu.Add("newchild.TS.currentHumanizerMode", new Slider("Current Humanizermode: Human", 1, 0, 3));
			_humanizerSlider.OnValueChange += _humanizerSlider_OnValueChange;

		}

		private void _humanizerSlider_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
		{
			_ActiveMode = (HumanMode)args.NewValue;
			_modeSlide.DisplayName = "Current Humanizermode: " + _ActiveMode.ToString();
		}

		void _modeSlide_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
		{
			_ActiveSelectionMode = (SelectionMode)args.NewValue;
			_modeSlide.DisplayName = "Current Selectionmode: " + _ActiveSelectionMode.ToString();
		}

		private newTargetSelector(HumanMode mode, SelectionMode selectMode)
		{
			_ActiveMode = mode;
			_LastTick = Environment.TickCount;
			_ActiveSelectionMode = selectMode;
		}

		public static newTargetSelector Instance
		{
			get
			{
				if (_Instance == null)
					_Instance = new newTargetSelector(HumanMode.Human, SelectionMode.NearMouse);
				return _Instance;
			}
		}
	}
}

