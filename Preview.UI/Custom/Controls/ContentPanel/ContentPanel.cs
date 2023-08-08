using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

using Xylia.Configure;
using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Helper;

using BNSTag = Xylia.Preview.Common.Tag;

namespace Xylia.Preview.UI.Custom.Controls;

[Designer(typeof(ContentPanelDesigner))]
public partial class ContentPanel : Panel
{
	#region Constructor
	public ContentPanel()
	{
		InitializeComponent();
		CheckForIllegalCrossThreadCalls = false;

		this.BackColor = Color.Transparent;
		this.DoubleBuffered = true;
		this.ResizeRedraw = false;
		this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
		this.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);


		// some tag is special in real html
		// those are non-closed, so must to re-set in bHtml
		HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("link");
	}

	public ContentPanel(string Text) : this() => base.Text = Text;

	public ContentPanel(string Text, params object[] args) : this() => base.Text = new ContentParams(args).Handle(Text);
	#endregion

	#region Designer
	public class ContentPanelDesigner : ControlDesigner
	{
		public override SelectionRules SelectionRules
		{
			get
			{
				if (this.Control.AutoSize) return SelectionRules.Visible | SelectionRules.Moveable;
				else return SelectionRules.Visible | SelectionRules.Moveable |
						SelectionRules.TopSizeable | SelectionRules.BottomSizeable |
						SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
			}
		}
	}
	#endregion


	#region Override Fields
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
	public override string Text
	{
		get => base.Text;
		set
		{
			base.Text = value;
			this.Invalidate();
		}
	}


	[DefaultValue(true)]
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = value; } 

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public new Size Size { get => base.Size; set => base.Size = value; }
	#endregion

	#region Fields
	public ContentParams Params = new();

	public Dictionary<int, BNSTag.Timer> Timers = new();


	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string FontName { get; set; }


	public bool _useMaxWidth = false;
	#endregion


	#region OnClick
	protected override void OnClick(EventArgs e)
	{
		base.OnClick(e);


		if (Tags is null) return;

		var Position = this.PointToClient(MousePosition);
		var tags = Tags.Where(tag => tag.Check(Position));

		//Debug.WriteLine(tags.Count());
		foreach (var tag in tags)
		{
			tag.ClickEvent?.Invoke(this, e);
		}
	}
	#endregion

	#region OnDoubleClick
	public static int CopyMode
	{
		get => Ini.ReadValue("Preview", "copy-mode").ToInt32();
		set => Ini.WriteValue("Preview", "copy-mode", value);
	}

	protected override void OnDoubleClick(EventArgs e)
	{
		base.OnDoubleClick(e);

		var CopyTxt = CopyMode switch
		{
			0 => this.Params.Handle(this.Text).CutText(),
			1 => this.Params.Handle(this.Text),
			2 => this.Text,

			_ => throw new NotSupportedException(),
		};

		if (string.IsNullOrWhiteSpace(CopyTxt)) return;
		Register.Dispatcher.Invoke(() => Clipboard.SetText(CopyTxt));
	}
	#endregion
}