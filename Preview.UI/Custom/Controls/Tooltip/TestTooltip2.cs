using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Xylia.Preview.UI.Custom.Controls
{
	public partial class TestTooltip2 : Form
	{
		#region Constructor
		private readonly Dictionary<Control, string> _tools = new();

		private readonly Size defaultSize;

		public TestTooltip2()
		{
			InitializeComponent();
			defaultSize = this.Size;
		}
		#endregion


		#region Functions
		public override string Text { get => contentPanel?.Text; set => contentPanel.Text = value; }

		private void Frm_VisibleChanged(object sender, EventArgs e)
		{
			if (!this.Visible) return;

			this.TopMost = true;
			this.Height = contentPanel.Height;
			this.Width = contentPanel.Width + 5;

			var p = new Point(Cursor.Position.X + 5, Cursor.Position.Y);
			this.Location = p;
		}


		public void SetToolTip(Control control, string info)
		{
			ArgumentNullException.ThrowIfNull(control);

			bool exists = _tools.ContainsKey(control);
			bool empty = info is null || string.IsNullOrEmpty(info);
			if (exists && empty)
			{
				_tools.Remove(control);
			}
			else if (!empty)
			{
				_tools[control] = info;
			}

			if (!empty && !exists)
			{
				control.MouseEnter += mouseEnter;
				control.MouseLeave += mouseLeave;
			}
		}

		private void mouseEnter(object sender, EventArgs eventargs)
		{
			Control control = (Control)sender;
			if (!_tools.TryGetValue(control, out var info))
				return;

			this.Size = defaultSize;
			this.Text = info;
			this.Visible = true;

			control.Focus();
		}

		private void mouseLeave(object sender, EventArgs eventargs)
		{
			Control control = (Control)sender;
			if (!_tools.TryGetValue(control, out _))
				return;

			this.Visible = false;
		}
		#endregion


		#region Static Functions
		public static void SetTooltip(Control control, string info) => new TestTooltip2().SetToolTip(control, info);
		#endregion
	}
}