namespace Xylia.Preview.GameUI.Scene.Skill
{
	partial class TraitTierCell
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
			this.TraitName = new Xylia.Preview.GameUI.Controls.ContentPanel();
			this.TraitIcon = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.TraitIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// TraitName
			// 
			this.TraitName.BackColor = System.Drawing.Color.Transparent;
			this.TraitName.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TraitName.ForeColor = System.Drawing.Color.White;
			this.TraitName.Location = new System.Drawing.Point(76, 24);
			this.TraitName.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.TraitName.Name = "TraitName";
			this.TraitName.TabIndex = 3;
			this.TraitName.Text = "特性名称";
			// 
			// TraitIcon
			// 
			this.TraitIcon.Image = global::Xylia.Preview.Resources.Resource_Common.ItemIcon;
			this.TraitIcon.Location = new System.Drawing.Point(2, 2);
			this.TraitIcon.Name = "TraitIcon";
			this.TraitIcon.Size = new System.Drawing.Size(64, 64);
			this.TraitIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.TraitIcon.TabIndex = 2;
			this.TraitIcon.TabStop = false;
			// 
			// TraitTierCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.TraitName);
			this.Controls.Add(this.TraitIcon);
			this.Name = "TraitTierCell";
			this.Size = new System.Drawing.Size(200, 69);
			((System.ComponentModel.ISupportInitialize)(this.TraitIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.ContentPanel TraitName;
		private System.Windows.Forms.PictureBox TraitIcon;
	}
}
