
namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class TaskPanel
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源, 为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的Functions - 不要修改
		/// 使用代码编辑器修改此Functions的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// TaskInfo
			// 
			this.Title = "任务";
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "TaskInfo";
			this.Size = new System.Drawing.Size(58, 26);
			
			this.SizeChanged += new System.EventHandler(this.ContentInfo_SizeChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
	}
}
