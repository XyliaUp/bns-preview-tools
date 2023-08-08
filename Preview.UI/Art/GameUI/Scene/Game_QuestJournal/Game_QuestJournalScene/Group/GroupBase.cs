using System.ComponentModel;

using Xylia.Preview.UI.Interface;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;

[DesignTimeVisible(false)]
public partial class GroupBase : UserControl , IPreview
{
	#region Constructor
	public GroupBase() => InitializeComponent();
	#endregion


	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public string Title { get => this.GroupName.Text; set => this.GroupName.Text = value; }

	public const int ContentStartX = 20;
}