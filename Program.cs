using System;
using System.Windows.Forms;

namespace RossCarlson.FlightSimulation.WoaiDownloader
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
