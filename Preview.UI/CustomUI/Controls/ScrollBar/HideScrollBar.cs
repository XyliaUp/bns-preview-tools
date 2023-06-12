using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Xylia.Preview.GameUI.Controls.PanelEx.ScrollBar
{
	/// <summary>
	/// 隐藏自身系统滚动条, 以支持自定义滚动条
	/// </summary>
	[DesignTimeVisible(false)]
	public class HideScrollBarPanel : Panel
	{
		[DllImport("user32.dll")]
		private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

		protected override void WndProc(ref Message m)
		{
			//0:horizontal,1:vertical,3:both
			ShowScrollBar(this.Handle, 3, false);
			base.WndProc(ref m);
		}

		protected override Point ScrollToControl(Control activeControl) => this.AutoScrollPosition;
	}
}