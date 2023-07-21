using System.ComponentModel;

namespace Xylia.Preview.UI.Custom.Controls.Forms
{
	public class PreviewFrm : Form
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text { get => base.Text; set => base.Text = value; }

		[SettingsBindable(true)]
		public string Title
		{
			get => this.Text;
			set => this.Text = value;
		}

		protected override Point ScrollToControl(Control activeControl) => this.AutoScrollPosition;
	}
}