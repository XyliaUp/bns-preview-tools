namespace Xylia.Match.Windows.Panel.TextInfo
{
	partial class TextPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextPage));
			Label1 = new System.Windows.Forms.Label();
			Path_Local1 = new System.Windows.Forms.TextBox();
			Label2 = new System.Windows.Forms.Label();
			Path_Local2 = new System.Windows.Forms.TextBox();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			Open = new System.Windows.Forms.OpenFileDialog();
			Save = new System.Windows.Forms.SaveFileDialog();
			Btn_StartWithEnd = new HZH_Controls.Controls.UCBtnExt();
			Step1 = new HZH_Controls.Controls.UCStep();
			ucBtnFillet2 = new HZH_Controls.Controls.UCBtnFillet();
			ucBtnFillet1 = new HZH_Controls.Controls.UCBtnFillet();
			ucBtnFillet3 = new HZH_Controls.Controls.UCBtnFillet();
			ucBtnFillet4 = new HZH_Controls.Controls.UCBtnFillet();
			tabControl1 = new System.Windows.Forms.TabControl();
			tabPage1 = new System.Windows.Forms.TabPage();
			tabPage2 = new System.Windows.Forms.TabPage();
			ucCheckBox2 = new HZH_Controls.Controls.UCCheckBox();
			TextBox2 = new System.Windows.Forms.TextBox();
			ucBtnFillet5 = new HZH_Controls.Controls.UCBtnFillet();
			ucBtnFillet7 = new HZH_Controls.Controls.UCBtnFillet();
			label3 = new System.Windows.Forms.Label();
			ucCheckBox1 = new HZH_Controls.Controls.UCCheckBox();
			SaveAsBin = new HZH_Controls.Controls.UCCheckBox();
			TextBox1 = new System.Windows.Forms.TextBox();
			ucBtnFillet6 = new HZH_Controls.Controls.UCBtnFillet();
			ucBtnFillet8 = new HZH_Controls.Controls.UCBtnFillet();
			Note_GRoot = new System.Windows.Forms.Label();
			filePath = new System.Windows.Forms.TextBox();
			ucBtnFillet9 = new HZH_Controls.Controls.UCBtnFillet();
			ucBtnFillet10 = new HZH_Controls.Controls.UCBtnFillet();
			label4 = new System.Windows.Forms.Label();
			ucBtnFillet11 = new HZH_Controls.Controls.UCBtnFillet();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			tabPage2.SuspendLayout();
			SuspendLayout();
			// 
			// Label1
			// 
			resources.ApplyResources(Label1, "Label1");
			Label1.Name = "Label1";
			// 
			// Path_Local1
			// 
			resources.ApplyResources(Path_Local1, "Path_Local1");
			Path_Local1.Name = "Path_Local1";
			Path_Local1.TextChanged += DataPath_TextChanged;
			// 
			// Label2
			// 
			resources.ApplyResources(Label2, "Label2");
			Label2.Name = "Label2";
			// 
			// Path_Local2
			// 
			resources.ApplyResources(Path_Local2, "Path_Local2");
			Path_Local2.Name = "Path_Local2";
			Path_Local2.TextChanged += DataPath_TextChanged;
			// 
			// pictureBox1
			// 
			pictureBox1.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(pictureBox1, "pictureBox1");
			pictureBox1.Name = "pictureBox1";
			pictureBox1.TabStop = false;
			pictureBox1.Click += pictureBox1_Click;
			// 
			// Btn_StartWithEnd
			// 
			Btn_StartWithEnd.BtnBackColor = System.Drawing.Color.Empty;
			Btn_StartWithEnd.BtnFont = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			Btn_StartWithEnd.BtnForeColor = System.Drawing.Color.FromArgb(192, 192, 255);
			Btn_StartWithEnd.Text = "开始";
			Btn_StartWithEnd.ConerRadius = 8;
			Btn_StartWithEnd.Cursor = System.Windows.Forms.Cursors.Hand;
			Btn_StartWithEnd.DialogResult = System.Windows.Forms.DialogResult.None;
			Btn_StartWithEnd.EnabledMouseEffect = false;
			Btn_StartWithEnd.FillColor = System.Drawing.Color.White;
			resources.ApplyResources(Btn_StartWithEnd, "Btn_StartWithEnd");
			Btn_StartWithEnd.IsRadius = true;
			Btn_StartWithEnd.IsShowRect = true;
			Btn_StartWithEnd.IsShowTips = false;
			Btn_StartWithEnd.Name = "Btn_StartWithEnd";
			Btn_StartWithEnd.RectColor = System.Drawing.Color.FromArgb(192, 192, 255);
			Btn_StartWithEnd.RectWidth = 1;
			Btn_StartWithEnd.TabStop = false;
			Btn_StartWithEnd.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
			Btn_StartWithEnd.TipsText = "";
			Btn_StartWithEnd.Click += Btn_End_BtnClick;
			// 
			// Step1
			// 
			Step1.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(Step1, "Step1");
			Step1.ImgCompleted = (System.Drawing.Image)resources.GetObject("Step1.ImgCompleted");
			Step1.LineWidth = 2;
			Step1.Name = "Step1";
			Step1.ReadOnly = false;
			Step1.StepBackColor = System.Drawing.Color.FromArgb(189, 189, 189);
			Step1.StepFontColor = System.Drawing.Color.White;
			Step1.StepForeColor = System.Drawing.Color.Pink;
			Step1.StepIndex = 0;
			Step1.Steps = (new string[] { "准备", "解析", "结束" });
			Step1.StepWidth = 32;
			// 
			// ucBtnFillet2
			// 
			ucBtnFillet2.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet2.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet2.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet2.BtnImage");
			ucBtnFillet2.Text = "输出";
			ucBtnFillet2.ConerRadius = 10;
			ucBtnFillet2.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet2, "ucBtnFillet2");
			ucBtnFillet2.IsRadius = true;
			ucBtnFillet2.IsShowRect = true;
			ucBtnFillet2.Name = "ucBtnFillet2";
			ucBtnFillet2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet2.RectWidth = 1;
			ucBtnFillet2.Click += Btn_OutLocal_1_Click;
			// 
			// ucBtnFillet1
			// 
			ucBtnFillet1.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet1.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet1.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet1.BtnImage");
			ucBtnFillet1.Text = "选择";
			ucBtnFillet1.ConerRadius = 10;
			ucBtnFillet1.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet1, "ucBtnFillet1");
			ucBtnFillet1.IsRadius = true;
			ucBtnFillet1.IsShowRect = true;
			ucBtnFillet1.Name = "ucBtnFillet1";
			ucBtnFillet1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet1.RectWidth = 1;
			ucBtnFillet1.Click += Button2_Click;
			// 
			// ucBtnFillet3
			// 
			ucBtnFillet3.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet3.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet3.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet3.BtnImage");
			ucBtnFillet3.Text = "输出";
			ucBtnFillet3.ConerRadius = 10;
			ucBtnFillet3.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet3, "ucBtnFillet3");
			ucBtnFillet3.IsRadius = true;
			ucBtnFillet3.IsShowRect = true;
			ucBtnFillet3.Name = "ucBtnFillet3";
			ucBtnFillet3.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet3.RectWidth = 1;
			ucBtnFillet3.Click += Btn_OutLocal_2_Click;
			// 
			// ucBtnFillet4
			// 
			ucBtnFillet4.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet4.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet4.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet4.BtnImage");
			ucBtnFillet4.Text = "选择";
			ucBtnFillet4.ConerRadius = 10;
			ucBtnFillet4.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet4, "ucBtnFillet4");
			ucBtnFillet4.IsRadius = true;
			ucBtnFillet4.IsShowRect = true;
			ucBtnFillet4.Name = "ucBtnFillet4";
			ucBtnFillet4.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet4.RectWidth = 1;
			ucBtnFillet4.Click += Button1_Click;
			// 
			// tabControl1
			// 
			tabControl1.Controls.Add(tabPage1);
			tabControl1.Controls.Add(tabPage2);
			resources.ApplyResources(tabControl1, "tabControl1");
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			// 
			// tabPage1
			// 
			tabPage1.BackColor = System.Drawing.Color.White;
			tabPage1.Controls.Add(ucBtnFillet3);
			tabPage1.Controls.Add(ucBtnFillet4);
			tabPage1.Controls.Add(ucBtnFillet2);
			tabPage1.Controls.Add(ucBtnFillet1);
			tabPage1.Controls.Add(Step1);
			tabPage1.Controls.Add(Btn_StartWithEnd);
			tabPage1.Controls.Add(Label1);
			tabPage1.Controls.Add(Path_Local1);
			tabPage1.Controls.Add(Label2);
			tabPage1.Controls.Add(Path_Local2);
			tabPage1.Controls.Add(pictureBox1);
			resources.ApplyResources(tabPage1, "tabPage1");
			tabPage1.Name = "tabPage1";
			// 
			// tabPage2
			// 
			tabPage2.BackColor = System.Drawing.Color.White;
			tabPage2.Controls.Add(ucCheckBox2);
			tabPage2.Controls.Add(TextBox2);
			tabPage2.Controls.Add(ucBtnFillet5);
			tabPage2.Controls.Add(ucBtnFillet7);
			tabPage2.Controls.Add(label3);
			tabPage2.Controls.Add(ucCheckBox1);
			tabPage2.Controls.Add(SaveAsBin);
			tabPage2.Controls.Add(TextBox1);
			tabPage2.Controls.Add(ucBtnFillet6);
			tabPage2.Controls.Add(ucBtnFillet8);
			tabPage2.Controls.Add(Note_GRoot);
			tabPage2.Controls.Add(filePath);
			tabPage2.Controls.Add(ucBtnFillet9);
			tabPage2.Controls.Add(ucBtnFillet10);
			tabPage2.Controls.Add(label4);
			tabPage2.Controls.Add(ucBtnFillet11);
			resources.ApplyResources(tabPage2, "tabPage2");
			tabPage2.Name = "tabPage2";
			// 
			// ucCheckBox2
			// 
			ucCheckBox2.BackColor = System.Drawing.Color.Transparent;
			ucCheckBox2.Checked = false;
			resources.ApplyResources(ucCheckBox2, "ucCheckBox2");
			ucCheckBox2.Name = "ucCheckBox2";
			ucCheckBox2.TextValue = "只显示韩文";
			// 
			// TextBox2
			// 
			resources.ApplyResources(TextBox2, "TextBox2");
			TextBox2.Name = "TextBox2";
			TextBox2.TextChanged += TextBox2_TextChanged;
			TextBox2.DoubleClick += DoubleClickPath;
			// 
			// ucBtnFillet5
			// 
			ucBtnFillet5.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet5.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet5.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet5.BtnImage");
			ucBtnFillet5.Text = "选择";
			ucBtnFillet5.ConerRadius = 10;
			ucBtnFillet5.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet5, "ucBtnFillet5");
			ucBtnFillet5.IsRadius = true;
			ucBtnFillet5.IsShowRect = true;
			ucBtnFillet5.Name = "ucBtnFillet5";
			ucBtnFillet5.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet5.RectWidth = 1;
			ucBtnFillet5.Click += ucBtnFillet5_BtnClick;
			// 
			// ucBtnFillet7
			// 
			ucBtnFillet7.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet7.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet7.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet7.BtnImage");
			ucBtnFillet7.Text = "选择";
			ucBtnFillet7.ConerRadius = 10;
			ucBtnFillet7.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet7, "ucBtnFillet7");
			ucBtnFillet7.IsRadius = true;
			ucBtnFillet7.IsShowRect = true;
			ucBtnFillet7.Name = "ucBtnFillet7";
			ucBtnFillet7.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet7.RectWidth = 1;
			// 
			// label3
			// 
			resources.ApplyResources(label3, "label3");
			label3.Name = "label3";
			// 
			// ucCheckBox1
			// 
			ucCheckBox1.BackColor = System.Drawing.Color.Transparent;
			ucCheckBox1.Checked = false;
			resources.ApplyResources(ucCheckBox1, "ucCheckBox1");
			ucCheckBox1.Name = "ucCheckBox1";
			ucCheckBox1.TextValue = "替换模式";
			ucCheckBox1.CheckedChangeEvent += ucCheckBox1_CheckedChangeEvent;
			// 
			// SaveAsBin
			// 
			SaveAsBin.BackColor = System.Drawing.Color.Transparent;
			SaveAsBin.Checked = true;
			resources.ApplyResources(SaveAsBin, "SaveAsBin");
			SaveAsBin.Name = "SaveAsBin";
			SaveAsBin.TextValue = ".bin文件";
			SaveAsBin.CheckedChangeEvent += ucCheckBox2_CheckedChangeEvent;
			// 
			// TextBox1
			// 
			resources.ApplyResources(TextBox1, "TextBox1");
			TextBox1.Name = "TextBox1";
			TextBox1.TextChanged += TextBox1_TextChanged;
			TextBox1.DoubleClick += DoubleClickPath;
			// 
			// ucBtnFillet6
			// 
			ucBtnFillet6.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet6.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet6.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet6.BtnImage");
			ucBtnFillet6.Text = "输出";
			ucBtnFillet6.ConerRadius = 10;
			ucBtnFillet6.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet6, "ucBtnFillet6");
			ucBtnFillet6.IsRadius = true;
			ucBtnFillet6.IsShowRect = true;
			ucBtnFillet6.Name = "ucBtnFillet6";
			ucBtnFillet6.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet6.RectWidth = 1;
			ucBtnFillet6.Click += ucBtnFillet6_BtnClick;
			// 
			// ucBtnFillet8
			// 
			ucBtnFillet8.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet8.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet8.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet8.BtnImage");
			ucBtnFillet8.Text = "选择";
			ucBtnFillet8.ConerRadius = 10;
			ucBtnFillet8.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet8, "ucBtnFillet8");
			ucBtnFillet8.IsRadius = true;
			ucBtnFillet8.IsShowRect = true;
			ucBtnFillet8.Name = "ucBtnFillet8";
			ucBtnFillet8.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet8.RectWidth = 1;
			ucBtnFillet8.Click += ucBtnFillet8_BtnClick;
			// 
			// Note_GRoot
			// 
			resources.ApplyResources(Note_GRoot, "Note_GRoot");
			Note_GRoot.Name = "Note_GRoot";
			// 
			// filePath
			// 
			resources.ApplyResources(filePath, "filePath");
			filePath.Name = "filePath";
			filePath.TextChanged += filePath_TextChanged;
			filePath.DoubleClick += DoubleClickPath;
			// 
			// ucBtnFillet9
			// 
			ucBtnFillet9.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet9.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet9.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet9.BtnImage");
			ucBtnFillet9.Text = "选择";
			ucBtnFillet9.ConerRadius = 10;
			ucBtnFillet9.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet9, "ucBtnFillet9");
			ucBtnFillet9.IsRadius = true;
			ucBtnFillet9.IsShowRect = true;
			ucBtnFillet9.Name = "ucBtnFillet9";
			ucBtnFillet9.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet9.RectWidth = 1;
			ucBtnFillet9.Click += ucBtnFillet9_BtnClick;
			// 
			// ucBtnFillet10
			// 
			ucBtnFillet10.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet10.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet10.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet10.BtnImage");
			ucBtnFillet10.Text = "选择";
			ucBtnFillet10.ConerRadius = 10;
			ucBtnFillet10.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet10, "ucBtnFillet10");
			ucBtnFillet10.IsRadius = true;
			ucBtnFillet10.IsShowRect = true;
			ucBtnFillet10.Name = "ucBtnFillet10";
			ucBtnFillet10.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet10.RectWidth = 1;
			// 
			// label4
			// 
			resources.ApplyResources(label4, "label4");
			label4.Name = "label4";
			// 
			// ucBtnFillet11
			// 
			ucBtnFillet11.BackColor = System.Drawing.Color.Transparent;
			ucBtnFillet11.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			ucBtnFillet11.BtnImage = (System.Drawing.Image)resources.GetObject("ucBtnFillet11.BtnImage");
			ucBtnFillet11.Text = "封包";
			ucBtnFillet11.ConerRadius = 10;
			ucBtnFillet11.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(ucBtnFillet11, "ucBtnFillet11");
			ucBtnFillet11.IsRadius = true;
			ucBtnFillet11.IsShowRect = true;
			ucBtnFillet11.Name = "ucBtnFillet11";
			ucBtnFillet11.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			ucBtnFillet11.RectWidth = 1;
			ucBtnFillet11.Click += ucBtnFillet11_BtnClick;
			// 
			// TextPage
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			Controls.Add(tabControl1);
			Name = "TextPage";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tabPage1.PerformLayout();
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Label Label1;
		private System.Windows.Forms.TextBox Path_Local1;
		private System.Windows.Forms.Label Label2;
		private System.Windows.Forms.TextBox Path_Local2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.OpenFileDialog Open;
		private System.Windows.Forms.SaveFileDialog Save;
		private HZH_Controls.Controls.UCBtnExt Btn_StartWithEnd;
		private HZH_Controls.Controls.UCStep Step1;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet2;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet1;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet3;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet4;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private HZH_Controls.Controls.UCCheckBox ucCheckBox2;
		private System.Windows.Forms.TextBox TextBox2;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet5;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet7;
		private System.Windows.Forms.Label label3;
		private HZH_Controls.Controls.UCCheckBox ucCheckBox1;
		private HZH_Controls.Controls.UCCheckBox SaveAsBin;
		private System.Windows.Forms.TextBox TextBox1;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet6;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet8;
		private System.Windows.Forms.Label Note_GRoot;
		private System.Windows.Forms.TextBox filePath;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet9;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet10;
		private System.Windows.Forms.Label label4;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet11;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	}
}
