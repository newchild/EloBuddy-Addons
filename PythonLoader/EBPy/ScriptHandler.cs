using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBPy
{
	public class ScriptHandler
	{
		public List<Script> loadedScripts = new List<Script>();
		
		public void LoadScript(string Name, string content)
		{
			loadedScripts.Add(new Script(content) { name = Name, enabled = true });
		}

		public void RunAllEnabledScripts()
		{
			foreach(var script in loadedScripts)
			{
				if (script.enabled)
				{
					script.run();
				}
			}
		}
	}

	public class Script
	{
		public bool enabled;
		public string name;
		private Core ExecuteCore = new Core();
		private CompiledCode code;
		public void run()
		{
			code.Execute(ExecuteCore.GetScope());
		}
		public Script(string Value)
		{
			code = ExecuteCore.CompileScript(Value);
		}
		
	}
}
