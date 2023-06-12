namespace Xylia.Preview.Data.Models.DatData
{
    partial class DatSelect
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatSelect));
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Btn_Confirm = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.Btn_Cancel = new System.Windows.Forms.Button();
			this.CountDown = new System.Windows.Forms.Timer(this.components);
			this.TimeInfo = new System.Windows.Forms.Label();
			this.NoResponse = new System.Windows.Forms.Timer(this.components);
			this.Chk_HidenBpFiles = new System.Windows.Forms.CheckBox();
			this.Chk_64bit = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.ItemHeight = 17;
			this.comboBox1.Location = new System.Drawing.Point(105, 52);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(390, 25);
			this.comboBox1.TabIndex = 0;
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.ItemHeight = 17;
			this.comboBox2.Location = new System.Drawing.Point(105, 98);
			this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(390, 25);
			this.comboBox2.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 58);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "数据文件";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 102);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "汉化文件";
			// 
			// Btn_Confirm
			// 
			this.Btn_Confirm.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Btn_Confirm.Location = new System.Drawing.Point(294, 162);
			this.Btn_Confirm.Margin = new System.Windows.Forms.Padding(4);
			this.Btn_Confirm.Name = "Btn_Confirm";
			this.Btn_Confirm.Size = new System.Drawing.Size(91, 41);
			this.Btn_Confirm.TabIndex = 4;
			this.Btn_Confirm.Text = "确定";
			this.Btn_Confirm.Click += new System.EventHandler(this.Btn_Confirm_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 19);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(368, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "由于您选择的主路径下存在多个相关文件，需要主动进行文件选择。";
			// 
			// Btn_Cancel
			// 
			this.Btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Btn_Cancel.Location = new System.Drawing.Point(405, 162);
			this.Btn_Cancel.Margin = new System.Windows.Forms.Padding(4);
			this.Btn_Cancel.Name = "Btn_Cancel";
			this.Btn_Cancel.Size = new System.Drawing.Size(91, 41);
			this.Btn_Cancel.TabIndex = 6;
			this.Btn_Cancel.Text = "取消";
			this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
			// 
			// CountDown
			// 
			this.CountDown.Interval = 800;
			this.CountDown.Tick += new System.EventHandler(this.Timer_Tick);
			// 
			// TimeInfo
			// 
			this.TimeInfo.AutoSize = true;
			this.TimeInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TimeInfo.ForeColor = System.Drawing.Color.OrangeRed;
			this.TimeInfo.Location = new System.Drawing.Point(9, 183);
			this.TimeInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TimeInfo.Name = "TimeInfo";
			this.TimeInfo.Size = new System.Drawing.Size(166, 21);
			this.TimeInfo.TabIndex = 7;
			this.TimeInfo.Text = "将在 99 秒后自动选择";
			this.TimeInfo.Visible = false;
			this.TimeInfo.VisibleChanged += new System.EventHandler(this.TimeInfo_VisibleChanged);
			// 
			// NoResponse
			// 
			this.NoResponse.Enabled = true;
			this.NoResponse.Interval = 900;
			this.NoResponse.Tick += new System.EventHandler(this.NoResponse_Tick);
			// 
			// Chk_HidenBpFiles
			// 
			this.Chk_HidenBpFiles.AutoSize = true;
			this.Chk_HidenBpFiles.Checked = true;
			this.Chk_HidenBpFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Chk_HidenBpFiles.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Chk_HidenBpFiles.ForeColor = System.Drawing.Color.OrangeRed;
			this.Chk_HidenBpFiles.Location = new System.Drawing.Point(14, 183);
			this.Chk_HidenBpFiles.Margin = new System.Windows.Forms.Padding(4);
			this.Chk_HidenBpFiles.Name = "Chk_HidenBpFiles";
			this.Chk_HidenBpFiles.Size = new System.Drawing.Size(133, 24);
			this.Chk_HidenBpFiles.TabIndex = 8;
			this.Chk_HidenBpFiles.Text = "不显示备份文件";
			this.Chk_HidenBpFiles.UseVisualStyleBackColor = true;
			this.Chk_HidenBpFiles.CheckedChanged += new System.EventHandler(this.Chk_HidenBpFiles_CheckedChanged);
			// 
			// Chk_64bit
			// 
			this.Chk_64bit.AutoSize = true;
			this.Chk_64bit.Checked = true;
			this.Chk_64bit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Chk_64bit.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Chk_64bit.ForeColor = System.Drawing.Color.OrangeRed;
			this.Chk_64bit.Location = new System.Drawing.Point(14, 149);
			this.Chk_64bit.Margin = new System.Windows.Forms.Padding(4);
			this.Chk_64bit.Name = "Chk_64bit";
			this.Chk_64bit.Size = new System.Drawing.Size(61, 24);
			this.Chk_64bit.TabIndex = 9;
			this.Chk_64bit.Text = "64位";
			this.Chk_64bit.UseVisualStyleBackColor = true;
			this.Chk_64bit.CheckedChanged += new System.EventHandler(this.Chk_64bit_CheckedChanged);
			// 
			// DataSelect
			// 
			this.AcceptButton = this.Btn_Confirm;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.Btn_Cancel;
			this.ClientSize = new System.Drawing.Size(515, 216);
			this.Controls.Add(this.Chk_64bit);
			this.Controls.Add(this.Chk_HidenBpFiles);
			this.Controls.Add(this.Btn_Cancel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.Btn_Confirm);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.TimeInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "DataSelect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "选择文件";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Select_Load);
			this.Shown += new System.EventHandler(this.DataSelect_Shown);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataSelect_MouseEnter);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataSelect_MouseEnter);
			this.MouseCaptureChanged += new System.EventHandler(this.DataSelect_MouseEnter);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataSelect_MouseEnter);
			this.MouseEnter += new System.EventHandler(this.DataSelect_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.DataSelect_MouseEnter);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DataSelect_MouseEnter);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_Confirm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Btn_Cancel;
		private System.Windows.Forms.Timer CountDown;
		private System.Windows.Forms.Label TimeInfo;
		private System.Windows.Forms.Timer NoResponse;
		private System.Windows.Forms.CheckBox Chk_HidenBpFiles;
		public System.Windows.Forms.CheckBox Chk_64bit;
	}
}