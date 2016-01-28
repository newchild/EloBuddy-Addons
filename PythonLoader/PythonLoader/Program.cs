using System;
using EloBuddy;
using EloBuddy.SDK;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EloBuddy.SDK.Events;

namespace PythonLoader
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Loading.OnLoadingComplete += Loading_OnLoadingComplete;
			
		}

		private static void Loading_OnLoadingComplete(EventArgs args)
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Application.EnableVisualStyles();
			Form1 x = new Form1();
			x.Show();
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Console.WriteLine(e.ExceptionObject.ToString());
		}
	}
}
