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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatSelect));
			comboBox1 = new ComboBox();
			comboBox2 = new ComboBox();
			label1 = new Label();
			label2 = new Label();
			Btn_Confirm = new Button();
			label3 = new Label();
			Btn_Cancel = new Button();
			CountDown = new System.Windows.Forms.Timer(components);
			TimeInfo = new Label();
			NoResponse = new System.Windows.Forms.Timer(components);
			Chk_HidenBpFiles = new CheckBox();
			Chk_64bit = new CheckBox();
			SuspendLayout();
			// 
			// comboBox1
			// 
			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox1.FormattingEnabled = true;
			comboBox1.ItemHeight = 17;
			comboBox1.Location = new Point(105, 52);
			comboBox1.Margin = new Padding(4);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(390, 25);
			comboBox1.TabIndex = 0;
			// 
			// comboBox2
			// 
			comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBox2.FormattingEnabled = true;
			comboBox2.ItemHeight = 17;
			comboBox2.Location = new Point(105, 98);
			comboBox2.Margin = new Padding(4);
			comboBox2.Name = "comboBox2";
			comboBox2.Size = new Size(390, 25);
			comboBox2.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(14, 58);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(56, 17);
			label1.TabIndex = 2;
			label1.Text = "数据文件";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(14, 102);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(56, 17);
			label2.TabIndex = 3;
			label2.Text = "汉化文件";
			// 
			// Btn_Confirm
			// 
			Btn_Confirm.DialogResult = DialogResult.OK;
			Btn_Confirm.Location = new Point(294, 162);
			Btn_Confirm.Margin = new Padding(4);
			Btn_Confirm.Name = "Btn_Confirm";
			Btn_Confirm.Size = new Size(91, 41);
			Btn_Confirm.TabIndex = 4;
			Btn_Confirm.Text = "确定";
			Btn_Confirm.Click += Btn_Confirm_Click;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(13, 19);
			label3.Margin = new Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new Size(368, 17);
			label3.TabIndex = 5;
			label3.Text = "由于您选择的主路径下存在多个相关文件，需要主动进行文件选择。";
			// 
			// Btn_Cancel
			// 
			Btn_Cancel.DialogResult = DialogResult.Cancel;
			Btn_Cancel.Location = new Point(405, 162);
			Btn_Cancel.Margin = new Padding(4);
			Btn_Cancel.Name = "Btn_Cancel";
			Btn_Cancel.Size = new Size(91, 41);
			Btn_Cancel.TabIndex = 6;
			Btn_Cancel.Text = "取消";
			Btn_Cancel.Click += Btn_Cancel_Click;
			// 
			// CountDown
			// 
			CountDown.Interval = 800;
			CountDown.Tick += Timer_Tick;
			// 
			// TimeInfo
			// 
			TimeInfo.AutoSize = true;
			TimeInfo.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
			TimeInfo.ForeColor = Color.OrangeRed;
			TimeInfo.Location = new Point(9, 183);
			TimeInfo.Margin = new Padding(4, 0, 4, 0);
			TimeInfo.Name = "TimeInfo";
			TimeInfo.Size = new Size(166, 21);
			TimeInfo.TabIndex = 7;
			TimeInfo.Text = "将在 99 秒后自动选择";
			TimeInfo.Visible = false;
			TimeInfo.VisibleChanged += TimeInfo_VisibleChanged;
			// 
			// NoResponse
			// 
			NoResponse.Enabled = true;
			NoResponse.Interval = 900;
			NoResponse.Tick += NoResponse_Tick;
			// 
			// Chk_HidenBpFiles
			// 
			Chk_HidenBpFiles.AutoSize = true;
			Chk_HidenBpFiles.Checked = true;
			Chk_HidenBpFiles.CheckState = CheckState.Checked;
			Chk_HidenBpFiles.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point);
			Chk_HidenBpFiles.ForeColor = Color.OrangeRed;
			Chk_HidenBpFiles.Location = new Point(14, 183);
			Chk_HidenBpFiles.Margin = new Padding(4);
			Chk_HidenBpFiles.Name = "Chk_HidenBpFiles";
			Chk_HidenBpFiles.Size = new Size(133, 24);
			Chk_HidenBpFiles.TabIndex = 8;
			Chk_HidenBpFiles.Text = "不显示备份文件";
			Chk_HidenBpFiles.UseVisualStyleBackColor = true;
			Chk_HidenBpFiles.CheckedChanged += Chk_HidenBpFiles_CheckedChanged;
			// 
			// Chk_64bit
			// 
			Chk_64bit.AutoSize = true;
			Chk_64bit.Checked = true;
			Chk_64bit.CheckState = CheckState.Checked;
			Chk_64bit.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point);
			Chk_64bit.ForeColor = Color.OrangeRed;
			Chk_64bit.Location = new Point(14, 149);
			Chk_64bit.Margin = new Padding(4);
			Chk_64bit.Name = "Chk_64bit";
			Chk_64bit.Size = new Size(61, 24);
			Chk_64bit.TabIndex = 9;
			Chk_64bit.Text = "64位";
			Chk_64bit.UseVisualStyleBackColor = true;
			Chk_64bit.CheckedChanged += Chk_64bit_CheckedChanged;
			// 
			// DatSelect
			// 
			AcceptButton = Btn_Confirm;
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			CancelButton = Btn_Cancel;
			ClientSize = new Size(515, 216);
			Controls.Add(Chk_64bit);
			Controls.Add(Chk_HidenBpFiles);
			Controls.Add(Btn_Cancel);
			Controls.Add(label3);
			Controls.Add(Btn_Confirm);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(comboBox2);
			Controls.Add(comboBox1);
			Controls.Add(TimeInfo);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4);
			MaximizeBox = false;
			Name = "DatSelect";
			StartPosition = FormStartPosition.CenterParent;
			Text = "选择文件";
			TopMost = true;
			Load += Select_Load;
			Shown += DataSelect_Shown;
			MouseClick += DataSelect_MouseEnter;
			MouseDoubleClick += DataSelect_MouseEnter;
			MouseCaptureChanged += DataSelect_MouseEnter;
			MouseDown += DataSelect_MouseEnter;
			MouseEnter += DataSelect_MouseEnter;
			MouseLeave += DataSelect_MouseEnter;
			MouseUp += DataSelect_MouseEnter;
			ResumeLayout(false);
			PerformLayout();
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