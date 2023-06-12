using System.ComponentModel;
using System.Windows.Forms;

using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	/// <summary>
	/// 分组控件基类
	/// </summary>
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

		/// <summary>
		/// 内容起始横坐标
		/// </summary>
		public const int ContentStartX = 20;
	}
}