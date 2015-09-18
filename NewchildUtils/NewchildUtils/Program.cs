using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using System.Threading.Tasks;

namespace NewchildUtils
{
	class Program
	{
		enum Keys
		{
			LMB = 0x01,
			Numpad0 = 0x60,
			Numpad1,
			Numpad2,
			Numpad3,
			Numpad4,
			Numpad5,
			Numpad6,
			Numpad7,
			Numpad8,
			Numpad9
		}
		static void Main(string[] args)
		{
			Game.OnWndProc += Game_OnWndProc;
		}

		static void Game_OnWndProc(WndEventArgs args)
		{
			if (args.Msg == 0x0100)
			{
				KeyDownCallBack((Keys)args.WParam);
			}
		}

		private static void KeyDownCallBack(Keys keys)
		{
			Chat.Print(keys.ToString());
			switch (keys)
			{
				case Keys.Numpad8:
					Camera.Angle += 3.0f;
					break;
				case Keys.Numpad2:
					Camera.Angle -= 3.0f;
					break;
				case Keys.Numpad4:
					Camera.RotationX += 3.0f;
					break;
				case Keys.Numpad6:
					Camera.RotationX -= 3.0f;
					break;
				case Keys.Numpad7:
					Camera.RotationY += 3.0f;
					break;
				case Keys.Numpad9:
					Camera.RotationY -= 3.0f;
					break;
			}
		}
	}
}
