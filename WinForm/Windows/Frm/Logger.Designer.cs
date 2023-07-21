namespace Xylia.Match.Windows.Forms
{
    partial class LoggerFrm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggerFrm));
			this.ucDataGridView2 = new HZH_Controls.Controls.UCDataGridView();
			this.PublicTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// ucDataGridView2
			// 
			resources.ApplyResources(this.ucDataGridView2, "ucDataGridView2");
			this.ucDataGridView2.BackColor = System.Drawing.Color.White;
			this.ucDataGridView2.Columns = null;
			this.ucDataGridView2.DataSource = null;
			this.ucDataGridView2.HeadFont = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ucDataGridView2.HeadHeight = 40;
			this.ucDataGridView2.HeadPadingLeft = 0;
			this.ucDataGridView2.HeadTextColor = System.Drawing.Color.Black;
			this.ucDataGridView2.IsShowCheckBox = false;
			this.ucDataGridView2.IsShowHead = true;
			this.ucDataGridView2.Name = "ucDataGridView2";
			this.ucDataGridView2.RowHeight = 40;
			this.ucDataGridView2.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
			// 
			// LoggerFrm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ucDataGridView2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "LoggerFrm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Logger_FormClosing);
			this.ResumeLayout(false);

        }

        #endregion

        private HZH_Controls.Controls.UCDataGridView ucDataGridView2;
        private System.Windows.Forms.ToolTip PublicTip;

    }
}
