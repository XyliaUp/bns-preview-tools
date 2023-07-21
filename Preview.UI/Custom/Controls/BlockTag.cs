using System.ComponentModel;

namespace Xylia.Windows.Controls;
public partial class BlockTag : UserControl
{
	#region Constructor
	public BlockTag()
	{
		InitializeComponent();
	}

	public BlockTag(string Text) : this()
	{
		this.Text = Text;
	}
	#endregion

	#region Event
	public event HandledEventHandler CancelClicked;
	#endregion

	#region Fields
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public override string Text
	{
		get => this.label1.Text;
		set
		{
			this.label1.Text = value;
			this.Refresh();
		}
	}
	#endregion

	#region Functions
	public override void Refresh()
	{
		base.Refresh();

		this.pictureBox1.Location = new Point(this.label1.Right + 1, this.pictureBox1.Top);
	}

	private void pictureBox1_Click(object sender, EventArgs e)
	{
		CancelClicked?.Invoke(null, null);
	}
	#endregion
}