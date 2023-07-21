using System.ComponentModel;
using System.Drawing.Design;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell;

[DesignTimeVisible(false)]
public partial class SkillModifyCell : UserControl
{
	#region Constructor
	public SkillModifyCell()
	{
		InitializeComponent();

		this.BackColor = Color.Transparent;
	}
	#endregion

	#region Fields
	/// <summary>
	/// 技能名称
	/// </summary>
	[Browsable(true)]
	public string SkillName 
	{ 
		set => this.SkillName_Txt.Text = value; 
		get => this.SkillName_Txt.Text;
	}

	/// <summary>
	/// 提示文本
	/// </summary>
	[Browsable(true)]
	[Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(UITypeEditor))]
	public string TooltipText 
	{
		get => this.TooltipText_Txt.Text;
		set
		{
			this.TooltipText_Txt.Text = value;
			this.Refresh();
		}
	}
	#endregion


	#region Functions (Override)
	public override void Refresh()
	{
		this.Height = SkillName_Txt.Bottom + this.TooltipText_Txt.Height;
	}
	#endregion
}
