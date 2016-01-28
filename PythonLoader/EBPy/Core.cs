using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBPy
{
    public class Core
    {
		private ScriptEngine _engine = null;
		private ScriptRuntime _runtime = null;
		private ScriptScope _scope = null;

		public Core()
		{
			_engine = Python.CreateEngine();
			_scope = _engine.CreateScope();
			_scope.SetVariable("printChat", new Action<string>(Chat.Print));
			_scope.SetVariable("PlayerInstance", Player.Instance);
			_scope.SetVariable("drawCircle", new Action<string, float, AIHeroClient>(pyDrawCircle));
			Game.OnTick += Game_OnTick;
			Drawing.OnDraw += Drawing_OnDraw;
		}

		private void pyDrawCircle(string arg1, float arg2, AIHeroClient arg3)
		{
			Color test = (Color)Enum.Parse(typeof(Color), arg1);
			
			if (test != null)
			{
				Circle.Draw(ColorBGRA.FromBgra(test.ToBgra()), arg2, new GameObject[] { arg3 });
			}
		}

		private void Drawing_OnDraw(EventArgs args)
		{
			Action onDrawPy;
			if (_scope.TryGetVariable("onDraw", out onDrawPy))
			{
				onDrawPy();
			}
		}

		private void Game_OnTick(EventArgs args)
		{
			Action onTickPy;
			if(_scope.TryGetVariable("onTick", out onTickPy))
			{
				onTickPy();
			}
		}

		public CompiledCode CompileScript(string value)
		{
			ScriptSource _source = _engine.CreateScriptSourceFromString(value);
			return _source.Compile();
		}

		public ScriptScope GetScope()
		{
			return _scope;
		}
    }
}
