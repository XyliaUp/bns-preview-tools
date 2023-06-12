using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class SlateScrollTooltip
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
			this.Guide = new ContentPanel();
			this.SuspendLayout();
			// 
			// Guide
			// 
			this.Guide.BackColor = System.Drawing.Color.Transparent;
			this.Guide.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
			this.Guide.ForeColor = System.Drawing.Color.White;
			this.Guide.Location = new System.Drawing.Point(7, 303);
			this.Guide.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.Guide.Name = "Guide";
			this.Guide.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Guide.Size = new System.Drawing.Size(208, 19);
			this.Guide.TabIndex = 7;
			this.Guide.Text = "刻印的能力值受到潜力数值影响。";
			// 
			// SlateScrollTooltip
			// 
	
			this.BackColor = System.Drawing.Color.Transparent;
		
			this.Controls.Add(this.Guide);
			this.Name = "SlateScrollTooltip";
	
			this.Size = new System.Drawing.Size(410, 325);
			this.Title = "代表能力值及基于潜力的最大数值";
			this.SizeChanged += new System.EventHandler(this.SlateScrollTooltip_SizeChanged);
			this.Controls.SetChildIndex(this.Guide, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ContentPanel Guide;
	}
}
