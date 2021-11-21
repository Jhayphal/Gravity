using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace StarConnections
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (var singleton = new Mutex(true, "StarConnectionsProgram"))
			{
				if (singleton.WaitOne(0, false))
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new MainForm());
				}
			}
		}
	}
}
