using System.ComponentModel;
using System.Drawing.Design;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;
public partial class DescPanel : GroupBase
{
	#region Constructor
	public DescPanel()
	{
		InitializeComponent();
		this.Content.Location = new Point(ContentStartX, this.Content.Location.Y);
	}
	#endregion

	#region Fields
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Category("Data")]
	[Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
	public override string Text
	{
		get => this.Content.Text;
		set
		{
			this.Content.Text = value;
			this.Refresh();
		}
	}
	#endregion
}