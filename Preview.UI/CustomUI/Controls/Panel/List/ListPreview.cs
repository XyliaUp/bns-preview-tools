using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Preview.GameUI.Controls.List;

namespace Xylia.Preview.GameUI.Controls
{
	[DesignTimeVisible(false)]
	public partial class ListPreview : Panel
	{
		#region Constructor
		public EventHandler ItemCellDoubleClick;

		public ListPreview()
		{
			InitializeComponent();
		}
		#endregion


		#region Fields
		/// <summary>
		/// 最大单页控件数量
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MaxCellNum { get; set; } = 100;


		private IEnumerable<ListData> _cells;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<ListData> Cells
		{
			get => _cells;
			set
			{
				_cells = value;
				PageIndex = 1;

				if (value != null && MaxCellNum != 0)
					PageCount = (int)Math.Ceiling((float)_cells.Count() / MaxCellNum);


				if (this.IsHandleCreated) this.Invoke(() => this.Refresh());
				else this.Refresh();
			}
		}


		private int PageIndex;

		private int PageCount;
		#endregion


		#region Functions
		protected override Point ScrollToControl(Control activeControl) => this.AutoScrollPosition;

		public override void Refresh()
		{
			this.Controls.Clear();
			this.VerticalScroll.Value = 0;

			if (Cells is null) return;
			this.SuspendLayout();


			int LocY = 0;

			var data = Cells.Skip(MaxCellNum * (PageIndex - 1));
			if (MaxCellNum > 0) data = data.Take(MaxCellNum);

			foreach (var o in data)
			{
				var c = o.GetCell();

				this.Controls.Add(c);
				c.Location = new Point(0, LocY);
				c.Width = this.Width - 20;
				LocY = c.Bottom + this.Padding.Bottom;

				if (ItemCellDoubleClick != null)
					c.DoubleClick += ItemCellDoubleClick;
			}

			if (this.pageSelector.Visible = PageCount > 1)
			{
				LocY += 10;

				pageSelector.Text = $"{PageIndex} / {PageCount}";
				pageSelector.Location = new Point((this.Width - pageSelector.Width) / 2, LocY);
				this.Controls.Add(pageSelector);
			}

			this.ResumeLayout(true);
		}

		private void pageSelector_PrevSeleted(object sender, EventArgs e)
		{
			if (PageIndex - 1 <= 0) return;

			PageIndex--;
			this.Refresh();
		}

		private void pageSelector_NextSeleted(object sender, EventArgs e)
		{
			if (PageIndex + 1 > PageCount) return;

			PageIndex++;
			this.Refresh();
		}


		/// <summary>
		/// 另存为图片
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveAsImage_Click(object sender, EventArgs e) => this.DrawMeToBitmap().SaveDialog(this.Name);
		#endregion
	}
}