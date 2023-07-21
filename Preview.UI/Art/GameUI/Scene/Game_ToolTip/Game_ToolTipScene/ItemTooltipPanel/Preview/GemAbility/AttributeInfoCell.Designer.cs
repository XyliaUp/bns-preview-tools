using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	partial class AttributeInfoCell
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lbl_MainInfo = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
			this.panelContent1 = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
			this.SuspendLayout();
			// 
			// lbl_MainInfo
			// 
			this.lbl_MainInfo.BackColor = System.Drawing.Color.Transparent;
			this.lbl_MainInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lbl_MainInfo.ForeColor = System.Drawing.Color.White;
			this.lbl_MainInfo.Location = new System.Drawing.Point(0, 0);
			this.lbl_MainInfo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.lbl_MainInfo.MinimumSize = new System.Drawing.Size(26, 18);
			this.lbl_MainInfo.Name = "lbl_MainInfo";
			this.lbl_MainInfo.TabIndex = 21;
			this.lbl_MainInfo.Text = "生命6000";
			// 
			// panelContent1
			// 
			this.panelContent1.BackColor = System.Drawing.Color.Transparent;
			this.panelContent1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.panelContent1.ForeColor = System.Drawing.Color.Yellow;
			this.panelContent1.Location = new System.Drawing.Point(136, 0);
			this.panelContent1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.panelContent1.Name = "panelContent1";
			this.panelContent1.TabIndex = 20;
			this.panelContent1.Text = "最高50000";
			// 
			// AttributeInfoCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.panelContent1);
			this.Controls.Add(this.lbl_MainInfo);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ForeColor = System.Drawing.Color.Black;
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.Name = "AttributeInfoCell";
			this.Size = new System.Drawing.Size(212, 20);
			this.Resize += new System.EventHandler(this.AttributeInfoCell_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ContentPanel lbl_MainInfo;
		private ContentPanel panelContent1;
	}
}
