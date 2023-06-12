using System;
using System.Windows.Forms;

namespace Xylia.Preview.GameUI.Controls
{
	//[DesignTimeVisible(false)]
	public partial class PageSelector : UserControl
	{
		public PageSelector()
		{
			InitializeComponent();
		}



		public event EventHandler PrevSeleted;

		public event EventHandler NextSeleted;

		private void page_Prev_Click(object sender, EventArgs e) =>  this.PrevSeleted?.Invoke(this , e);

		private void page_Next_Click(object sender, EventArgs e) => this.NextSeleted?.Invoke(this, e);


		public override string Text { get => pageinfo.Text; set => pageinfo.Text = value; }
	}
}
