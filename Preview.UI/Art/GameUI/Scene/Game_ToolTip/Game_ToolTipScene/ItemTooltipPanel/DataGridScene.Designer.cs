using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
    partial class DataGridScene
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的Functions - 不要修改
        /// 使用代码编辑器修改此Functions的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.dataGridView = new HZH_Controls.Controls.UCDataGridView();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.BackColor = System.Drawing.Color.White;
			this.dataGridView.Columns = null;
			this.dataGridView.DataSource = null;
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.HeadFont = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.dataGridView.HeadHeight = 40;
			this.dataGridView.HeadPadingLeft = 0;
			this.dataGridView.HeadTextColor = System.Drawing.Color.Black;
			this.dataGridView.IsShowCheckBox = false;
			this.dataGridView.IsShowHead = true;
			this.dataGridView.Location = new System.Drawing.Point(0, 0);
			this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.Padding = new System.Windows.Forms.Padding(0, 57, 0, 0);
			this.dataGridView.RowHeight = 40;
			this.dataGridView.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
			this.dataGridView.Size = new System.Drawing.Size(863, 598);
			this.dataGridView.TabIndex = 0;
			// 
			// DataGridScene
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(863, 598);
			this.Controls.Add(this.dataGridView);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.DimGray;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "DataGridScene";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "查看Fields";
			this.ResumeLayout(false);

        }

		#endregion

		private ItemNameCell itemNameCell1;
		private HZH_Controls.Controls.UCDataGridView dataGridView;
	}
}

