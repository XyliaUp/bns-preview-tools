using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
    public partial class WarningPreview : UserControl
	{
		#region Constructor
		public WarningPreview() => InitializeComponent();
		#endregion

		#region Fields
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
		public override string Text
		{
			get => this.panelContent1.Text;
			set
			{
				this.panelContent1.Text = value;
				this.Visible = value is not null;

				if (this.Visible) this.OnTextChanged(new());
			}
		}

		public ContentParams Params => this.panelContent1.Params;
		#endregion
	}
}