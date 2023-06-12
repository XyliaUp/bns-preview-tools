
namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class GroupBase
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
			this.GroupName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// GroupName
			// 
			this.GroupName.AutoSize = true;
			this.GroupName.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.GroupName.ForeColor = System.Drawing.Color.White;
			this.GroupName.Location = new System.Drawing.Point(4, 0);
			this.GroupName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.GroupName.Name = "GroupName";
			this.GroupName.Size = new System.Drawing.Size(88, 26);
			this.GroupName.TabIndex = 7;
			this.GroupName.Text = "分组名称";
			// 
			// Base
			// 

			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.GroupName);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Base";
			this.Size = new System.Drawing.Size(96, 26);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.Label GroupName;
	}
}
