namespace Xylia.Preview.GameUI.Scene.Game_ChallengeToday
{
	partial class ChallengeListRewardPreview
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
			this.Btn_Prev = new System.Windows.Forms.PictureBox();
			this.Btn_Next = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.Btn_Prev)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Btn_Next)).BeginInit();
			this.SuspendLayout();
			// 
			// Btn_Prev
			// 
			this.Btn_Prev.Image = global::Xylia.Preview.Resources.Resource_Common.icPager_Prev;
			this.Btn_Prev.Location = new System.Drawing.Point(0, 9);
			this.Btn_Prev.Margin = new System.Windows.Forms.Padding(4);
			this.Btn_Prev.Name = "Btn_Prev";
			this.Btn_Prev.Size = new System.Drawing.Size(22, 38);
			this.Btn_Prev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.Btn_Prev.TabIndex = 115;
			this.Btn_Prev.TabStop = false;
			this.Btn_Prev.Click += new System.EventHandler(this.Btn_Prev_Click);
			// 
			// Btn_Next
			// 
			this.Btn_Next.Image = global::Xylia.Preview.Resources.Resource_Common.icPager_Next;
			this.Btn_Next.Location = new System.Drawing.Point(330, 9);
			this.Btn_Next.Margin = new System.Windows.Forms.Padding(4);
			this.Btn_Next.Name = "Btn_Next";
			this.Btn_Next.Size = new System.Drawing.Size(22, 38);
			this.Btn_Next.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.Btn_Next.TabIndex = 116;
			this.Btn_Next.TabStop = false;
			this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
			// 
			// ChallengeListRewardPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Btn_Next);
			this.Controls.Add(this.Btn_Prev);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "ChallengeListRewardPreview";
			this.Size = new System.Drawing.Size(356, 51);
			((System.ComponentModel.ISupportInitialize)(this.Btn_Prev)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Btn_Next)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox Btn_Prev;
		private System.Windows.Forms.PictureBox Btn_Next;
	}
}
