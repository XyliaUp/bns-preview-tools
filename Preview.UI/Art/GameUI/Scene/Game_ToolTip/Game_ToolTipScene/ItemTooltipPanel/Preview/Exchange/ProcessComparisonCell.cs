using System.ComponentModel;

using Xylia.Preview.UI.Custom.Controls;


namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	[DesignTimeVisible(false)]
	public partial class ProcessComparisonCell : UserControl
	{
		#region Constructor
		public ProcessComparisonCell()
		{
			InitializeComponent();
		}

		public ProcessComparisonCell(List<ItemIconCell> RequiredItems, List<ItemIconCell> NormalItems) : this()
		{
			this.Controls.Remove(this.itemIconCell1);
			this.Controls.Remove(this.itemIconCell2);

			#region UI
			this.SuspendLayout();

			int LocX = 0;
			int Padding = 2;

			foreach (var c in RequiredItems)
			{
				if (!this.Controls.Contains(c)) this.Controls.Add(c);

				c.Location = new Point(LocX, 0);
				LocX += c.Scale + Padding;
			}

			this.pictureBox3.Location = new Point(LocX + 20, this.pictureBox3.Top);
			LocX += 40 + this.pictureBox3.Width;

			foreach (var c in NormalItems)
			{
				if (!this.Controls.Contains(c)) this.Controls.Add(c);

				c.Location = new Point(LocX, 0);
				LocX += c.Scale + Padding;
			}

			this.ResumeLayout();
			#endregion
		}
		#endregion
	}
}