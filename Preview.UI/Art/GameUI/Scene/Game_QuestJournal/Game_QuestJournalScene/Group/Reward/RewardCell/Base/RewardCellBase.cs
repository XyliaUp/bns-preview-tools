using System.ComponentModel;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal.RewardCell;

[DesignTimeVisible(false)]
public partial class RewardCellBase : Panel
{
	#region Constructor
	public RewardCellBase()
	{
		InitializeComponent();

		this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		this.AutoSize = true;
	}
	#endregion

	#region Fields
	/// <summary>
	/// 标题
	/// </summary>
	public string Title { get => this.RewardTitle.Text; set => this.RewardTitle.Text = value; }

	/// <summary>
	/// 信息文本
	/// </summary>
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public override string Text { get => this.panelContent1.Text; set => this.panelContent1.Text = value; }

	/// <summary>
	/// 使用基础信息板
	/// </summary>
	public virtual bool UseBasicPanel { get => this.panelContent1.Visible; set => this.panelContent1.Visible = value; }
	#endregion
}