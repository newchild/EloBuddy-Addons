using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActuallyDecentZed
{
	class ShadowManager
	{
		public class UltShadowManager
		{
			private UltShadowManager _Instance;

			public UltShadowManager Instance
			{
				get
				{
					if(_Instance == null)
						_Instance = new UltShadowManager();
					return _Instance;
				}
			}

		}

		private class UltShadow
		{

		}

		private class WShadow
		{

		}

		public class WShadowManager
		{
			private WShadowManager _Instance;

			public WShadowManager Instance
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
}
