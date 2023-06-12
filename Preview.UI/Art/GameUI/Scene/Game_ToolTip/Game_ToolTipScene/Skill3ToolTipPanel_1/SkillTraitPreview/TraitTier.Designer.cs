﻿namespace Xylia.Preview.GameUI.Scene.Skill
{
	partial class TraitTier
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
			this.traitTierCell1 = new Xylia.Preview.GameUI.Scene.Skill.TraitTierCell();
			this.traitTierCell2 = new Xylia.Preview.GameUI.Scene.Skill.TraitTierCell();
			this.traitTierCell3 = new Xylia.Preview.GameUI.Scene.Skill.TraitTierCell();
			this.SuspendLayout();
			// 
			// traitTierCell1
			// 
			this.traitTierCell1.AutoSize = true;
			this.traitTierCell1.BackColor = System.Drawing.Color.Transparent;
			this.traitTierCell1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.traitTierCell1.Location = new System.Drawing.Point(3, 0);
			this.traitTierCell1.Name = "traitTierCell1";
			this.traitTierCell1.Size = new System.Drawing.Size(200, 71);
			this.traitTierCell1.SkillTrait = null;
			this.traitTierCell1.TabIndex = 0;
			this.traitTierCell1.Click += new System.EventHandler(this.TierCell_Click);
			// 
			// traitTierCell2
			// 
			this.traitTierCell2.AutoSize = true;
			this.traitTierCell2.BackColor = System.Drawing.Color.Transparent;
			this.traitTierCell2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.traitTierCell2.Location = new System.Drawing.Point(213, 0);
			this.traitTierCell2.Name = "traitTierCell2";
			this.traitTierCell2.Size = new System.Drawing.Size(200, 71);
			this.traitTierCell2.SkillTrait = null;
			this.traitTierCell2.TabIndex = 1;
			this.traitTierCell2.Click += new System.EventHandler(this.TierCell_Click);
			// 
			// traitTierCell3
			// 
			this.traitTierCell3.AutoSize = true;
			this.traitTierCell3.BackColor = System.Drawing.Color.Transparent;
			this.traitTierCell3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.traitTierCell3.Location = new System.Drawing.Point(423, 0);
			this.traitTierCell3.Name = "traitTierCell3";
			this.traitTierCell3.Size = new System.Drawing.Size(200, 71);
			this.traitTierCell3.SkillTrait = null;
			this.traitTierCell3.TabIndex = 2;
			this.traitTierCell3.Click += new System.EventHandler(this.TierCell_Click);
			// 
			// TraitTier
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.traitTierCell3);
			this.Controls.Add(this.traitTierCell2);
			this.Controls.Add(this.traitTierCell1);
			this.Name = "TraitTier";
			this.Size = new System.Drawing.Size(626, 74);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TraitTierCell traitTierCell1;
		private TraitTierCell traitTierCell2;
		private TraitTierCell traitTierCell3;
	}
}
