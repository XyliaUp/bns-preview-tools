
namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
	partial class ResultWeaponPreview
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultWeaponPreview));
			this.Btn_Next = new System.Windows.Forms.PictureBox();
			this.Btn_Prev = new System.Windows.Forms.PictureBox();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.Btn_Next)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Btn_Prev)).BeginInit();
			this.SuspendLayout();
			// 
			// Btn_Next
			// 
			this.Btn_Next.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Next.Image")));
			this.Btn_Next.Location = new System.Drawing.Point(443, 9);
			this.Btn_Next.Margin = new System.Windows.Forms.Padding(4);
			this.Btn_Next.Name = "Btn_Next";
			this.Btn_Next.Size = new System.Drawing.Size(37, 85);
			this.Btn_Next.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.Btn_Next.TabIndex = 15;
			this.Btn_Next.TabStop = false;
			this.Btn_Next.Visible = false;
			this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
			// 
			// Btn_Prev
			// 
			this.Btn_Prev.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Prev.Image")));
			this.Btn_Prev.Location = new System.Drawing.Point(0, 9);
			this.Btn_Prev.Margin = new System.Windows.Forms.Padding(4);
			this.Btn_Prev.Name = "Btn_Prev";
			this.Btn_Prev.Size = new System.Drawing.Size(37, 85);
			this.Btn_Prev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.Btn_Prev.TabIndex = 16;
			this.Btn_Prev.TabStop = false;
			this.Btn_Prev.Visible = false;
			this.Btn_Prev.Click += new System.EventHandler(this.Btn_Prev_Click);
			// 
			// ToolTip
			// 
			this.ToolTip.AutoPopDelay = 5000;
			this.ToolTip.InitialDelay = 500;
			this.ToolTip.IsBalloon = true;
			this.ToolTip.ReshowDelay = 0;
			// 
			// ResultWeaponPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Btn_Prev);
			this.Controls.Add(this.Btn_Next);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ResultWeaponPreview";
			this.Size = new System.Drawing.Size(484, 131);
			((System.ComponentModel.ISupportInitialize)(this.Btn_Next)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Btn_Prev)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox Btn_Next;
		private System.Windows.Forms.PictureBox Btn_Prev;
		private System.Windows.Forms.ToolTip ToolTip;
	}
}
