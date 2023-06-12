namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	partial class ExchangePreview
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
			this.ProcessTitle = new System.Windows.Forms.Label();
			this.ProcessMaterialTitle = new System.Windows.Forms.Label();
			this.ProcessComparison = new System.Windows.Forms.Label();
			this.ProcessComparisonCell = new Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell.ProcessComparisonCell();
			this.ProcessMaterial = new Xylia.Preview.GameUI.Controls.ItemShowCell();
			this.Process = new Xylia.Preview.GameUI.Controls.ItemShowCell();
			this.SuspendLayout();
			// 
			// ProcessTitle
			// 
			this.ProcessTitle.AutoSize = true;
			this.ProcessTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ProcessTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(99)))));
			this.ProcessTitle.Location = new System.Drawing.Point(4, 0);
			this.ProcessTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ProcessTitle.Name = "ProcessTitle";
			this.ProcessTitle.Size = new System.Drawing.Size(58, 21);
			this.ProcessTitle.TabIndex = 1;
			this.ProcessTitle.Text = "加工品";
			this.ProcessTitle.Visible = false;
			// 
			// ProcessMaterialTitle
			// 
			this.ProcessMaterialTitle.AutoSize = true;
			this.ProcessMaterialTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ProcessMaterialTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(99)))));
			this.ProcessMaterialTitle.Location = new System.Drawing.Point(4, 91);
			this.ProcessMaterialTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ProcessMaterialTitle.Name = "ProcessMaterialTitle";
			this.ProcessMaterialTitle.Size = new System.Drawing.Size(74, 21);
			this.ProcessMaterialTitle.TabIndex = 2;
			this.ProcessMaterialTitle.Text = "加工材料";
			this.ProcessMaterialTitle.Visible = false;
			// 
			// ProcessComparison
			// 
			this.ProcessComparison.AutoSize = true;
			this.ProcessComparison.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ProcessComparison.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(99)))));
			this.ProcessComparison.Location = new System.Drawing.Point(4, 181);
			this.ProcessComparison.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ProcessComparison.Name = "ProcessComparison";
			this.ProcessComparison.Size = new System.Drawing.Size(74, 21);
			this.ProcessComparison.TabIndex = 3;
			this.ProcessComparison.Text = "加工比率";
			// 
			// ProcessComparisonCell
			// 
			this.ProcessComparisonCell.AutoSize = true;
			this.ProcessComparisonCell.BackColor = System.Drawing.Color.Transparent;
			//this.ProcessComparisonCell.CrystallRule = null;
			this.ProcessComparisonCell.ForeColor = System.Drawing.Color.Black;
			this.ProcessComparisonCell.Location = new System.Drawing.Point(8, 215);
			this.ProcessComparisonCell.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.ProcessComparisonCell.Name = "ProcessComparisonCell";
			this.ProcessComparisonCell.Size = new System.Drawing.Size(158, 44);
			this.ProcessComparisonCell.TabIndex = 6;
			this.ProcessComparisonCell.Visible = false;
			// 
			// ProcessMaterial
			// 
			this.ProcessMaterial.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ProcessMaterial.BackColor = System.Drawing.Color.Transparent;
			this.ProcessMaterial.ForeColor = System.Drawing.Color.Black;
			this.ProcessMaterial.HeightDiff = 0;
			this.ProcessMaterial.ItemData = null;
			this.ProcessMaterial.ItemGrade = ((byte)(7));
			this.ProcessMaterial.ItemIcon = null;
			this.ProcessMaterial.ItemName = "ItemName";
			this.ProcessMaterial.Location = new System.Drawing.Point(8, 120);
			this.ProcessMaterial.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.ProcessMaterial.Name = "ProcessMaterial";
			this.ProcessMaterial.ReserveIconSpace = true;
			this.ProcessMaterial.Scale = 52;
			this.ProcessMaterial.Size = new System.Drawing.Size(139, 51);
			this.ProcessMaterial.TabIndex = 5;
			this.ProcessMaterial.TagImage = null;
			this.ProcessMaterial.Visible = false;
			// 
			// Process
			// 
			this.Process.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Process.BackColor = System.Drawing.Color.Transparent;
			this.Process.ForeColor = System.Drawing.Color.Black;
			this.Process.HeightDiff = 0;
			this.Process.ItemData = null;
			this.Process.ItemGrade = ((byte)(7));
			this.Process.ItemIcon = null;
			this.Process.ItemName = "ItemName";
			this.Process.Location = new System.Drawing.Point(8, 31);
			this.Process.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Process.Name = "Process";
			this.Process.ReserveIconSpace = true;
			this.Process.Scale = 52;
			this.Process.Size = new System.Drawing.Size(139, 51);
			this.Process.TabIndex = 4;
			this.Process.TagImage = null;
			this.Process.Visible = false;
			// 
			// ExchangePreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.ProcessComparisonCell);
			this.Controls.Add(this.ProcessMaterial);
			this.Controls.Add(this.Process);
			this.Controls.Add(this.ProcessComparison);
			this.Controls.Add(this.ProcessMaterialTitle);
			this.Controls.Add(this.ProcessTitle);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ExchangePreview";
			this.Size = new System.Drawing.Size(349, 265);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label ProcessTitle;
		private System.Windows.Forms.Label ProcessMaterialTitle;
		private System.Windows.Forms.Label ProcessComparison;
		private Xylia.Preview.GameUI.Controls.ItemShowCell Process;
		private Xylia.Preview.GameUI.Controls.ItemShowCell ProcessMaterial;
		private Cell.ProcessComparisonCell ProcessComparisonCell;
	}
}
