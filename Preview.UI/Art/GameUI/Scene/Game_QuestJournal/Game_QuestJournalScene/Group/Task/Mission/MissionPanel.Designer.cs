using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class MissionPanel
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
			this.components = new System.ComponentModel.Container();
			this.MissionText = new Xylia.Preview.UI.Custom.Controls.ContentPanel();
			this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// MissionText
			// 
			this.MissionText.BackColor = System.Drawing.Color.Transparent;
			this.MissionText.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.MissionText.ForeColor = System.Drawing.Color.White;
			this.MissionText.Location = new System.Drawing.Point(2, 0);
			this.MissionText.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
			this.MissionText.Name = "MissionText";
			this.MissionText.TabIndex = 11;
			this.MissionText.Text = "课题信息描述";
			// 
			// ToolTip
			// 
			this.ToolTip.AutoPopDelay = 5000;
			this.ToolTip.InitialDelay = 500;
			this.ToolTip.IsBalloon = true;
			this.ToolTip.ReshowDelay = 0;
			// 
			// MissionPanel
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.MissionText);
			this.Name = "MissionPanel";
			this.Size = new System.Drawing.Size(92, 23);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private ContentPanel MissionText;
		private System.Windows.Forms.ToolTip ToolTip;
	}
}
