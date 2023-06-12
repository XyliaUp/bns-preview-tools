using Xylia.Preview.GameUI.Controls.PanelEx;
using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	partial class SkillModifyCell
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
			this.SkillName_Txt = new System.Windows.Forms.Label();
			this.TooltipText_Txt = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.SuspendLayout();
			// 
			// SkillName_Txt
			// 
			this.SkillName_Txt.AutoSize = true;
			this.SkillName_Txt.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SkillName_Txt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(194)))), ((int)(((byte)(255)))));
			this.SkillName_Txt.Location = new System.Drawing.Point(-2, 0);
			this.SkillName_Txt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SkillName_Txt.Name = "SkillName_Txt";
			this.SkillName_Txt.Size = new System.Drawing.Size(65, 20);
			this.SkillName_Txt.TabIndex = 1;
			this.SkillName_Txt.Text = "技能标题";
			// 
			// TooltipText_Txt
			// 
			this.TooltipText_Txt.BackColor = System.Drawing.Color.Transparent;
			this.TooltipText_Txt.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TooltipText_Txt.FontName = null;
			this.TooltipText_Txt.ForeColor = System.Drawing.Color.White;
			this.TooltipText_Txt.Location = new System.Drawing.Point(0, 33);
			this.TooltipText_Txt.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
			this.TooltipText_Txt.Name = "TooltipText_Txt";
			this.TooltipText_Txt.TabIndex = 2;
			this.TooltipText_Txt.Text = "消息文本";
			// 
			// SkillModifyCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.TooltipText_Txt);
			this.Controls.Add(this.SkillName_Txt);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SkillModifyCell";
			this.Size = new System.Drawing.Size(338, 58);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label SkillName_Txt;
		private ContentPanel TooltipText_Txt;
	}
}
