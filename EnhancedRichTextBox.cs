using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Metacraft.FlightSimulation.WoaiDownloader
{
	public class EnhancedRichTextBox : RichTextBox
	{
		private const int WM_USER = 0x400;
		private const int SB_VERT = 1;
		private const int EM_GETSCROLLPOS = WM_USER + 221;

		[DllImport("user32.dll")]
		private static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, ref Point lParam);

		public bool IsAtMaxScroll()
		{
			int minScroll;
			int maxScroll;
			GetScrollRange(this.Handle, SB_VERT, out minScroll, out maxScroll);
			Point rtfPoint = Point.Empty;
			SendMessage(this.Handle, EM_GETSCROLLPOS, 0, ref rtfPoint);
			return (rtfPoint.Y + this.ClientSize.Height >= maxScroll);
		}
	}
}
