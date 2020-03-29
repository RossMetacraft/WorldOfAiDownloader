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
		private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, ref Point lParam);

		public bool IsAtMaxScroll()
		{
			GetScrollRange(Handle, SB_VERT, out int _, out int maxScroll);
			Point rtfPoint = Point.Empty;
			SendMessage(Handle, EM_GETSCROLLPOS, 0, ref rtfPoint);

			return (rtfPoint.Y + ClientSize.Height >= maxScroll);
		}
	}
}
