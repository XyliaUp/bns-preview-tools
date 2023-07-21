using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class PieceTransformPreview
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
			ProcessTitle = new Label();
			ProcessMaterialTitle = new Label();
			ProcessComparison = new Label();
			ProcessComparisonCell = new Cell.ProcessComparisonCell();
			ProcessMaterial = new ItemShowCell();
			Process = new ItemShowCell();
			SuspendLayout();
			// 
			// ProcessTitle
			// 
			ProcessTitle.AutoSize = true;
			ProcessTitle.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ProcessTitle.ForeColor = Color.FromArgb(255, 235, 99);
			ProcessTitle.Location = new Point(4, 0);
			ProcessTitle.Margin = new Padding(4, 0, 4, 0);
			ProcessTitle.Name = "ProcessTitle";
			ProcessTitle.Size = new Size(58, 21);
			ProcessTitle.TabIndex = 1;
			ProcessTitle.Text = "加工品";
			ProcessTitle.Visible = false;
			// 
			// ProcessMaterialTitle
			// 
			ProcessMaterialTitle.AutoSize = true;
			ProcessMaterialTitle.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ProcessMaterialTitle.ForeColor = Color.FromArgb(255, 235, 99);
			ProcessMaterialTitle.Location = new Point(4, 91);
			ProcessMaterialTitle.Margin = new Padding(4, 0, 4, 0);
			ProcessMaterialTitle.Name = "ProcessMaterialTitle";
			ProcessMaterialTitle.Size = new Size(74, 21);
			ProcessMaterialTitle.TabIndex = 2;
			ProcessMaterialTitle.Text = "加工材料";
			ProcessMaterialTitle.Visible = false;
			// 
			// ProcessComparison
			// 
			ProcessComparison.AutoSize = true;
			ProcessComparison.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ProcessComparison.ForeColor = Color.FromArgb(255, 235, 99);
			ProcessComparison.Location = new Point(4, 181);
			ProcessComparison.Margin = new Padding(4, 0, 4, 0);
			ProcessComparison.Name = "ProcessComparison";
			ProcessComparison.Size = new Size(302, 21);
			ProcessComparison.TabIndex = 3;
			ProcessComparison.Text = "UI.PieceTransform.ProcessComparison";
			// 
			// ProcessComparisonCell
			// 
			ProcessComparisonCell.AutoSize = true;
			ProcessComparisonCell.BackColor = Color.Transparent;
			ProcessComparisonCell.ForeColor = Color.Black;
			ProcessComparisonCell.Location = new Point(8, 215);
			ProcessComparisonCell.Margin = new Padding(5, 6, 5, 6);
			ProcessComparisonCell.Name = "ProcessComparisonCell";
			ProcessComparisonCell.Size = new Size(168, 56);
			ProcessComparisonCell.TabIndex = 6;
			ProcessComparisonCell.Visible = false;
			// 
			// ProcessMaterial
			// 
			ProcessMaterial.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			ProcessMaterial.BackColor = Color.Transparent;
			ProcessMaterial.ForeColor = Color.Black;
			ProcessMaterial.HeightDiff = 0;
			ProcessMaterial.ItemData = null;
			ProcessMaterial.ItemGrade = 7;
			ProcessMaterial.ItemIcon = null;
			ProcessMaterial.ItemName = "ItemName";
			ProcessMaterial.Location = new Point(8, 120);
			ProcessMaterial.Margin = new Padding(5, 6, 5, 6);
			ProcessMaterial.Name = "ProcessMaterial";
			ProcessMaterial.ReserveIconSpace = true;
			ProcessMaterial.Scale = 52;
			ProcessMaterial.Size = new Size(139, 51);
			ProcessMaterial.TabIndex = 5;
			ProcessMaterial.TagImage = null;
			ProcessMaterial.Visible = false;
			// 
			// Process
			// 
			Process.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			Process.BackColor = Color.Transparent;
			Process.ForeColor = Color.Black;
			Process.HeightDiff = 0;
			Process.ItemData = null;
			Process.ItemGrade = 7;
			Process.ItemIcon = null;
			Process.ItemName = "ItemName";
			Process.Location = new Point(8, 31);
			Process.Margin = new Padding(5, 6, 5, 6);
			Process.Name = "Process";
			Process.ReserveIconSpace = true;
			Process.Scale = 52;
			Process.Size = new Size(139, 51);
			Process.TabIndex = 4;
			Process.TagImage = null;
			Process.Visible = false;
			// 
			// ExchangePreview
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSize = true;
			BackColor = Color.Transparent;
			Controls.Add(ProcessComparisonCell);
			Controls.Add(ProcessMaterial);
			Controls.Add(Process);
			Controls.Add(ProcessComparison);
			Controls.Add(ProcessMaterialTitle);
			Controls.Add(ProcessTitle);
			Margin = new Padding(4);
			Name = "ExchangePreview";
			Size = new Size(349, 277);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label ProcessTitle;
		private System.Windows.Forms.Label ProcessMaterialTitle;
		private System.Windows.Forms.Label ProcessComparison;
		private Xylia.Preview.UI.Custom.Controls.ItemShowCell Process;
		private Xylia.Preview.UI.Custom.Controls.ItemShowCell ProcessMaterial;
		private Cell.ProcessComparisonCell ProcessComparisonCell;
	}
}
