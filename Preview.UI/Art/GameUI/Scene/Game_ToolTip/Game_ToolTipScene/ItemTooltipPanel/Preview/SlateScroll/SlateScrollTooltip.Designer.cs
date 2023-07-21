using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class SlateScrollTooltip
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
			Guide = new ContentPanel();
			SuspendLayout();
			// 
			// Guide
			// 
			Guide.BackColor = Color.Transparent;
			Guide.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			Guide.ForeColor = Color.White;
			Guide.Location = new Point(7, 303);
			Guide.Margin = new Padding(2, 5, 2, 5);
			Guide.Name = "Guide";
			Guide.RightToLeft = RightToLeft.Yes;
			Guide.TabIndex = 7;
			Guide.Text = "UI.SlateScrollTooltip.Guide";
			// 
			// SlateScrollTooltip
			// 
			BackColor = Color.Transparent;
			Controls.Add(Guide);
			Name = "SlateScrollTooltip";
			Size = new Size(410, 325);
			Title = "UI.SlateScrollTooltip.AbilityTitle";
			SizeChanged += SlateScrollTooltip_SizeChanged;
			Controls.SetChildIndex(Guide, 0);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ContentPanel Guide;
	}
}
