namespace Xylia.Match.Windows.Panel
{
	partial class ItemPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemPage));
			TabControl = new TabControl();
			MainPage = new TabPage();
			TimeInfo = new Label();
			Step1 = new HZH_Controls.Controls.UCStep();
			Chv_Path = new TextBox();
			Note_GRoot = new Label();
			Btn_StartMatch = new HZH_Controls.Controls.UCBtnExt();
			GRoot_Path = new TextBox();
			Note_Chv = new Label();
			Chk_OnlyNew = new HZH_Controls.Controls.UCSwitch();
			Switch_64Bit = new HZH_Controls.Controls.UCSwitch();
			File_Searcher = new HZH_Controls.Controls.UCBtnFillet();
			ucBtnFillet1 = new HZH_Controls.Controls.UCBtnFillet();
			PreviewPage_Item = new TabPage();
			ItemPreview_Search = new HZH_Controls.Controls.UCTextBoxEx();
			ucBtnExt5 = new HZH_Controls.Controls.UCBtnExt();
			label1 = new Label();
			PreviewPage_Else = new TabPage();
			btn_SetOutput = new HZH_Controls.Controls.UCBtnExt();
			groupBox2 = new GroupBox();
			ucBtnExt11 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt1 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt20 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt9 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt8 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt13 = new HZH_Controls.Controls.UCBtnExt();
			groupBox1 = new GroupBox();
			ucBtnExt3 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt2 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt10 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt18 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt12 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt14 = new HZH_Controls.Controls.UCBtnExt();
			ucBtnExt16 = new HZH_Controls.Controls.UCBtnExt();
			Timer = new System.Windows.Forms.Timer(components);
			Open = new OpenFileDialog();
			ucBtnExt4 = new HZH_Controls.Controls.UCBtnExt();
			TabControl.SuspendLayout();
			MainPage.SuspendLayout();
			PreviewPage_Item.SuspendLayout();
			PreviewPage_Else.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// TabControl
			// 
			TabControl.Controls.Add(MainPage);
			TabControl.Controls.Add(PreviewPage_Item);
			TabControl.Controls.Add(PreviewPage_Else);
			resources.ApplyResources(TabControl, "TabControl");
			TabControl.Name = "TabControl";
			TabControl.SelectedIndex = 0;
			TabControl.KeyDown += TabControl_KeyDown;
			// 
			// MainPage
			// 
			MainPage.BackColor = Color.White;
			MainPage.Controls.Add(TimeInfo);
			MainPage.Controls.Add(Step1);
			MainPage.Controls.Add(Chv_Path);
			MainPage.Controls.Add(Note_GRoot);
			MainPage.Controls.Add(Btn_StartMatch);
			MainPage.Controls.Add(GRoot_Path);
			MainPage.Controls.Add(Note_Chv);
			MainPage.Controls.Add(Chk_OnlyNew);
			MainPage.Controls.Add(Switch_64Bit);
			MainPage.Controls.Add(File_Searcher);
			MainPage.Controls.Add(ucBtnFillet1);
			resources.ApplyResources(MainPage, "MainPage");
			MainPage.Name = "MainPage";
			// 
			// TimeInfo
			// 
			resources.ApplyResources(TimeInfo, "TimeInfo");
			TimeInfo.BackColor = SystemColors.Window;
			TimeInfo.Name = "TimeInfo";
			TimeInfo.TextChanged += TimeInfo_TextChanged;
			// 
			// Step1
			// 
			Step1.BackColor = Color.Transparent;
			resources.ApplyResources(Step1, "Step1");
			Step1.ImgCompleted = (Image)resources.GetObject("Step1.ImgCompleted");
			Step1.LineWidth = 2;
			Step1.Name = "Step1";
			Step1.ReadOnly = false;
			Step1.StepBackColor = Color.FromArgb(189, 189, 189);
			Step1.StepFontColor = Color.White;
			Step1.StepForeColor = Color.Pink;
			Step1.StepIndex = 0;
			Step1.Steps = (new string[] { "准备开始", "解析资源", "执行输出", "结束操作" });
			Step1.StepWidth = 32;
			// 
			// Chv_Path
			// 
			resources.ApplyResources(Chv_Path, "Chv_Path");
			Chv_Path.Name = "Chv_Path";
			// 
			// Note_GRoot
			// 
			resources.ApplyResources(Note_GRoot, "Note_GRoot");
			Note_GRoot.BackColor = Color.Transparent;
			Note_GRoot.Name = "Note_GRoot";
			// 
			// Btn_StartMatch
			// 
			Btn_StartMatch.BtnBackColor = Color.Empty;
			Btn_StartMatch.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			Btn_StartMatch.BtnForeColor = Color.FromArgb(192, 192, 255);
			Btn_StartMatch.ConerRadius = 8;
			Btn_StartMatch.Cursor = Cursors.Hand;
			Btn_StartMatch.DialogResult = DialogResult.None;
			Btn_StartMatch.EnabledMouseEffect = false;
			Btn_StartMatch.FillColor = Color.White;
			resources.ApplyResources(Btn_StartMatch, "Btn_StartMatch");
			Btn_StartMatch.IsRadius = true;
			Btn_StartMatch.IsShowRect = true;
			Btn_StartMatch.IsShowTips = false;
			Btn_StartMatch.Name = "Btn_StartMatch";
			Btn_StartMatch.RectColor = Color.FromArgb(192, 192, 255);
			Btn_StartMatch.RectWidth = 1;
			Btn_StartMatch.TabStop = false;
			Btn_StartMatch.TipsColor = Color.FromArgb(232, 30, 99);
			Btn_StartMatch.TipsText = "";
			Btn_StartMatch.Click += Btn_StartMatch_BtnClick;
			// 
			// GRoot_Path
			// 
			resources.ApplyResources(GRoot_Path, "GRoot_Path");
			GRoot_Path.Name = "GRoot_Path";
			// 
			// Note_Chv
			// 
			resources.ApplyResources(Note_Chv, "Note_Chv");
			Note_Chv.BackColor = Color.Transparent;
			Note_Chv.Name = "Note_Chv";
			// 
			// Chk_OnlyNew
			// 
			Chk_OnlyNew.BackColor = Color.Transparent;
			Chk_OnlyNew.Checked = true;
			Chk_OnlyNew.FalseColor = Color.FromArgb(189, 189, 189);
			Chk_OnlyNew.FalseTextColr = Color.White;
			resources.ApplyResources(Chk_OnlyNew, "Chk_OnlyNew");
			Chk_OnlyNew.Name = "Chk_OnlyNew";
			Chk_OnlyNew.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
			Chk_OnlyNew.Texts = (new string[] { "仅更新 ", "全部   " });
			Chk_OnlyNew.TrueColor = Color.FromArgb(255, 192, 192);
			Chk_OnlyNew.TrueTextColr = Color.Black;
			Chk_OnlyNew.CheckedChanged += Chk_OnlyNew_CheckedChanged;
			// 
			// Switch_64Bit
			// 
			Switch_64Bit.BackColor = Color.Transparent;
			Switch_64Bit.Checked = false;
			Switch_64Bit.FalseColor = Color.FromArgb(189, 189, 189);
			Switch_64Bit.FalseTextColr = Color.White;
			resources.ApplyResources(Switch_64Bit, "Switch_64Bit");
			Switch_64Bit.Name = "Switch_64Bit";
			Switch_64Bit.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
			Switch_64Bit.Texts = (new string[] { "64位   ", "32位   " });
			Switch_64Bit.TrueColor = Color.FromArgb(255, 192, 192);
			Switch_64Bit.TrueTextColr = Color.Black;
			// 
			// File_Searcher
			// 
			File_Searcher.BackColor = Color.Transparent;
			File_Searcher.BtnFont = new Font("微软雅黑", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
			File_Searcher.BtnImage = (Image)resources.GetObject("File_Searcher.BtnImage");
			File_Searcher.ConerRadius = 10;
			File_Searcher.FillColor = Color.Transparent;
			resources.ApplyResources(File_Searcher, "File_Searcher");
			File_Searcher.IsRadius = true;
			File_Searcher.IsShowRect = true;
			File_Searcher.Name = "File_Searcher";
			File_Searcher.RectColor = Color.FromArgb(220, 220, 220);
			File_Searcher.RectWidth = 1;
			File_Searcher.Click += File_Searcher_BtnClick;
			File_Searcher.MouseEnter += File_Searcher_MouseEnter;
			// 
			// ucBtnFillet1
			// 
			ucBtnFillet1.BackColor = Color.Transparent;
			ucBtnFillet1.BtnFont = new Font("微软雅黑", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
			ucBtnFillet1.BtnImage = (Image)resources.GetObject("ucBtnFillet1.BtnImage");
			ucBtnFillet1.ConerRadius = 10;
			ucBtnFillet1.FillColor = Color.Transparent;
			resources.ApplyResources(ucBtnFillet1, "ucBtnFillet1");
			ucBtnFillet1.IsRadius = true;
			ucBtnFillet1.IsShowRect = true;
			ucBtnFillet1.Name = "ucBtnFillet1";
			ucBtnFillet1.RectColor = Color.FromArgb(220, 220, 220);
			ucBtnFillet1.RectWidth = 1;
			ucBtnFillet1.Click += ucBtnFillet1_BtnClick;
			// 
			// PreviewPage_Item
			// 
			PreviewPage_Item.BackColor = Color.White;
			PreviewPage_Item.Controls.Add(ItemPreview_Search);
			PreviewPage_Item.Controls.Add(ucBtnExt5);
			PreviewPage_Item.Controls.Add(label1);
			resources.ApplyResources(PreviewPage_Item, "PreviewPage_Item");
			PreviewPage_Item.Name = "PreviewPage_Item";
			// 
			// ItemPreview_Search
			// 
			ItemPreview_Search.BackColor = Color.Transparent;
			ItemPreview_Search.ConerRadius = 5;
			ItemPreview_Search.Cursor = Cursors.IBeam;
			ItemPreview_Search.DecLength = 2;
			ItemPreview_Search.FillColor = Color.Empty;
			ItemPreview_Search.FocusBorderColor = Color.FromArgb(255, 77, 59);
			resources.ApplyResources(ItemPreview_Search, "ItemPreview_Search");
			ItemPreview_Search.InputText = "";
			ItemPreview_Search.InputType = HZH_Controls.TextInputType.NotControl;
			ItemPreview_Search.IsFocusColor = true;
			ItemPreview_Search.IsRadius = true;
			ItemPreview_Search.IsShowClearBtn = true;
			ItemPreview_Search.IsShowKeyboard = false;
			ItemPreview_Search.IsShowRect = true;
			ItemPreview_Search.IsShowSearchBtn = true;
			ItemPreview_Search.KeyBoardType = HZH_Controls.Controls.KeyBoardType.Null;
			ItemPreview_Search.MaxValue = new decimal(new int[] { 1000000, 0, 0, 0 });
			ItemPreview_Search.MinValue = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
			ItemPreview_Search.Name = "ItemPreview_Search";
			ItemPreview_Search.PromptColor = Color.Gray;
			ItemPreview_Search.PromptFont = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
			ItemPreview_Search.PromptText = "请输入物品信息  (ID/别名/名称)";
			ItemPreview_Search.RectColor = Color.FromArgb(220, 220, 220);
			ItemPreview_Search.RectWidth = 1;
			ItemPreview_Search.RegexPattern = "";
			ItemPreview_Search.SearchClick += ItemPreview_Search_SearchClick;
			ItemPreview_Search.TextChanged += ItemPreview_Search_TextChanged;
			// 
			// ucBtnExt5
			// 
			ucBtnExt5.BtnBackColor = Color.Empty;
			ucBtnExt5.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt5.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt5.ConerRadius = 8;
			ucBtnExt5.Cursor = Cursors.Hand;
			ucBtnExt5.DialogResult = DialogResult.None;
			ucBtnExt5.EnabledMouseEffect = false;
			ucBtnExt5.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt5, "ucBtnExt5");
			ucBtnExt5.IsRadius = true;
			ucBtnExt5.IsShowRect = true;
			ucBtnExt5.IsShowTips = false;
			ucBtnExt5.Name = "ucBtnExt5";
			ucBtnExt5.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt5.RectWidth = 1;
			ucBtnExt5.TabStop = false;
			ucBtnExt5.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt5.TipsText = "";
			ucBtnExt5.Click += ucBtnExt5_BtnClick;
			ucBtnExt5.MouseEnter += ucBtnExt5_MouseEnter;
			// 
			// label1
			// 
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			// 
			// PreviewPage_Else
			// 
			PreviewPage_Else.BackColor = Color.White;
			PreviewPage_Else.Controls.Add(btn_SetOutput);
			PreviewPage_Else.Controls.Add(groupBox2);
			PreviewPage_Else.Controls.Add(groupBox1);
			resources.ApplyResources(PreviewPage_Else, "PreviewPage_Else");
			PreviewPage_Else.Name = "PreviewPage_Else";
			// 
			// btn_SetOutput
			// 
			btn_SetOutput.BtnBackColor = Color.Empty;
			btn_SetOutput.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			btn_SetOutput.BtnForeColor = Color.FromArgb(192, 192, 255);
			btn_SetOutput.ConerRadius = 8;
			btn_SetOutput.Cursor = Cursors.Hand;
			btn_SetOutput.DialogResult = DialogResult.None;
			btn_SetOutput.EnabledMouseEffect = false;
			btn_SetOutput.FillColor = Color.White;
			resources.ApplyResources(btn_SetOutput, "btn_SetOutput");
			btn_SetOutput.IsRadius = true;
			btn_SetOutput.IsShowRect = true;
			btn_SetOutput.IsShowTips = false;
			btn_SetOutput.Name = "btn_SetOutput";
			btn_SetOutput.RectColor = Color.FromArgb(192, 192, 255);
			btn_SetOutput.RectWidth = 1;
			btn_SetOutput.TabStop = false;
			btn_SetOutput.TipsColor = Color.FromArgb(232, 30, 99);
			btn_SetOutput.TipsText = "";
			btn_SetOutput.Click += btn_SetOutput_Click;
			// 
			// groupBox2
			// 
			groupBox2.BackColor = Color.Transparent;
			groupBox2.Controls.Add(ucBtnExt4);
			groupBox2.Controls.Add(ucBtnExt11);
			groupBox2.Controls.Add(ucBtnExt1);
			groupBox2.Controls.Add(ucBtnExt20);
			groupBox2.Controls.Add(ucBtnExt9);
			groupBox2.Controls.Add(ucBtnExt8);
			groupBox2.Controls.Add(ucBtnExt13);
			resources.ApplyResources(groupBox2, "groupBox2");
			groupBox2.Name = "groupBox2";
			groupBox2.TabStop = false;
			// 
			// ucBtnExt11
			// 
			ucBtnExt11.BtnBackColor = Color.Empty;
			ucBtnExt11.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt11.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt11.ConerRadius = 8;
			ucBtnExt11.Cursor = Cursors.Hand;
			ucBtnExt11.DialogResult = DialogResult.None;
			ucBtnExt11.EnabledMouseEffect = false;
			ucBtnExt11.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt11, "ucBtnExt11");
			ucBtnExt11.IsRadius = true;
			ucBtnExt11.IsShowRect = true;
			ucBtnExt11.IsShowTips = false;
			ucBtnExt11.Name = "ucBtnExt11";
			ucBtnExt11.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt11.RectWidth = 1;
			ucBtnExt11.TabStop = false;
			ucBtnExt11.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt11.TipsText = "";
			ucBtnExt11.Click += ucBtnExt11_BtnClick;
			// 
			// ucBtnExt1
			// 
			ucBtnExt1.BtnBackColor = Color.Empty;
			ucBtnExt1.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt1.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt1.ConerRadius = 8;
			ucBtnExt1.Cursor = Cursors.Hand;
			ucBtnExt1.DialogResult = DialogResult.None;
			ucBtnExt1.EnabledMouseEffect = false;
			ucBtnExt1.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt1, "ucBtnExt1");
			ucBtnExt1.IsRadius = true;
			ucBtnExt1.IsShowRect = true;
			ucBtnExt1.IsShowTips = false;
			ucBtnExt1.Name = "ucBtnExt1";
			ucBtnExt1.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt1.RectWidth = 1;
			ucBtnExt1.TabStop = false;
			ucBtnExt1.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt1.TipsText = "";
			ucBtnExt1.Click += ucBtnExt1_BtnClick;
			// 
			// ucBtnExt20
			// 
			ucBtnExt20.BtnBackColor = Color.Empty;
			ucBtnExt20.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt20.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt20.ConerRadius = 8;
			ucBtnExt20.Cursor = Cursors.Hand;
			ucBtnExt20.DialogResult = DialogResult.None;
			ucBtnExt20.EnabledMouseEffect = false;
			ucBtnExt20.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt20, "ucBtnExt20");
			ucBtnExt20.IsRadius = true;
			ucBtnExt20.IsShowRect = true;
			ucBtnExt20.IsShowTips = false;
			ucBtnExt20.Name = "ucBtnExt20";
			ucBtnExt20.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt20.RectWidth = 1;
			ucBtnExt20.TabStop = false;
			ucBtnExt20.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt20.TipsText = "";
			ucBtnExt20.Click += ucBtnExt20_BtnClick;
			// 
			// ucBtnExt9
			// 
			ucBtnExt9.BtnBackColor = Color.Empty;
			ucBtnExt9.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt9.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt9.ConerRadius = 8;
			ucBtnExt9.Cursor = Cursors.Hand;
			ucBtnExt9.DialogResult = DialogResult.None;
			ucBtnExt9.EnabledMouseEffect = false;
			ucBtnExt9.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt9, "ucBtnExt9");
			ucBtnExt9.IsRadius = true;
			ucBtnExt9.IsShowRect = true;
			ucBtnExt9.IsShowTips = false;
			ucBtnExt9.Name = "ucBtnExt9";
			ucBtnExt9.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt9.RectWidth = 1;
			ucBtnExt9.TabStop = false;
			ucBtnExt9.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt9.TipsText = "";
			ucBtnExt9.Click += ucBtnExt9_BtnClick;
			// 
			// ucBtnExt8
			// 
			ucBtnExt8.BtnBackColor = Color.Empty;
			ucBtnExt8.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt8.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt8.ConerRadius = 8;
			ucBtnExt8.Cursor = Cursors.Hand;
			ucBtnExt8.DialogResult = DialogResult.None;
			ucBtnExt8.EnabledMouseEffect = false;
			ucBtnExt8.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt8, "ucBtnExt8");
			ucBtnExt8.IsRadius = true;
			ucBtnExt8.IsShowRect = true;
			ucBtnExt8.IsShowTips = false;
			ucBtnExt8.Name = "ucBtnExt8";
			ucBtnExt8.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt8.RectWidth = 1;
			ucBtnExt8.TabStop = false;
			ucBtnExt8.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt8.TipsText = "";
			ucBtnExt8.Click += ucBtnExt8_BtnClick;
			// 
			// ucBtnExt13
			// 
			ucBtnExt13.BtnBackColor = Color.Empty;
			ucBtnExt13.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt13.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt13.ConerRadius = 8;
			ucBtnExt13.Cursor = Cursors.Hand;
			ucBtnExt13.DialogResult = DialogResult.None;
			ucBtnExt13.EnabledMouseEffect = false;
			ucBtnExt13.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt13, "ucBtnExt13");
			ucBtnExt13.IsRadius = true;
			ucBtnExt13.IsShowRect = true;
			ucBtnExt13.IsShowTips = false;
			ucBtnExt13.Name = "ucBtnExt13";
			ucBtnExt13.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt13.RectWidth = 1;
			ucBtnExt13.TabStop = false;
			ucBtnExt13.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt13.TipsText = "";
			ucBtnExt13.Click += ucBtnExt13_BtnClick;
			// 
			// groupBox1
			// 
			groupBox1.BackColor = Color.Transparent;
			groupBox1.Controls.Add(ucBtnExt3);
			groupBox1.Controls.Add(ucBtnExt2);
			groupBox1.Controls.Add(ucBtnExt10);
			groupBox1.Controls.Add(ucBtnExt18);
			groupBox1.Controls.Add(ucBtnExt12);
			groupBox1.Controls.Add(ucBtnExt14);
			groupBox1.Controls.Add(ucBtnExt16);
			resources.ApplyResources(groupBox1, "groupBox1");
			groupBox1.Name = "groupBox1";
			groupBox1.TabStop = false;
			// 
			// ucBtnExt3
			// 
			ucBtnExt3.BtnBackColor = Color.Empty;
			ucBtnExt3.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt3.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt3.ConerRadius = 8;
			ucBtnExt3.Cursor = Cursors.Hand;
			ucBtnExt3.DialogResult = DialogResult.None;
			ucBtnExt3.EnabledMouseEffect = false;
			ucBtnExt3.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt3, "ucBtnExt3");
			ucBtnExt3.IsRadius = true;
			ucBtnExt3.IsShowRect = true;
			ucBtnExt3.IsShowTips = false;
			ucBtnExt3.Name = "ucBtnExt3";
			ucBtnExt3.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt3.RectWidth = 1;
			ucBtnExt3.TabStop = false;
			ucBtnExt3.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt3.TipsText = "";
			ucBtnExt3.Click += ucBtnExt3_BtnClick;
			// 
			// ucBtnExt2
			// 
			ucBtnExt2.BtnBackColor = Color.Empty;
			ucBtnExt2.BtnFont = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt2.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt2.ConerRadius = 8;
			ucBtnExt2.Cursor = Cursors.Hand;
			ucBtnExt2.DialogResult = DialogResult.None;
			ucBtnExt2.EnabledMouseEffect = false;
			ucBtnExt2.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt2, "ucBtnExt2");
			ucBtnExt2.IsRadius = true;
			ucBtnExt2.IsShowRect = true;
			ucBtnExt2.IsShowTips = false;
			ucBtnExt2.Name = "ucBtnExt2";
			ucBtnExt2.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt2.RectWidth = 1;
			ucBtnExt2.TabStop = false;
			ucBtnExt2.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt2.TipsText = "";
			ucBtnExt2.Click += ucBtnExt2_BtnClick;
			// 
			// ucBtnExt10
			// 
			ucBtnExt10.BtnBackColor = Color.Empty;
			ucBtnExt10.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt10.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt10.ConerRadius = 8;
			ucBtnExt10.Cursor = Cursors.Hand;
			ucBtnExt10.DialogResult = DialogResult.None;
			ucBtnExt10.EnabledMouseEffect = false;
			ucBtnExt10.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt10, "ucBtnExt10");
			ucBtnExt10.IsRadius = true;
			ucBtnExt10.IsShowRect = true;
			ucBtnExt10.IsShowTips = false;
			ucBtnExt10.Name = "ucBtnExt10";
			ucBtnExt10.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt10.RectWidth = 1;
			ucBtnExt10.TabStop = false;
			ucBtnExt10.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt10.TipsText = "";
			ucBtnExt10.Click += ucBtnExt10_BtnClick;
			// 
			// ucBtnExt18
			// 
			ucBtnExt18.BtnBackColor = Color.Empty;
			ucBtnExt18.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt18.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt18.ConerRadius = 8;
			ucBtnExt18.Cursor = Cursors.Hand;
			ucBtnExt18.DialogResult = DialogResult.None;
			ucBtnExt18.EnabledMouseEffect = false;
			ucBtnExt18.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt18, "ucBtnExt18");
			ucBtnExt18.IsRadius = true;
			ucBtnExt18.IsShowRect = true;
			ucBtnExt18.IsShowTips = false;
			ucBtnExt18.Name = "ucBtnExt18";
			ucBtnExt18.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt18.RectWidth = 1;
			ucBtnExt18.TabStop = false;
			ucBtnExt18.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt18.TipsText = "";
			ucBtnExt18.Click += ucBtnExt18_BtnClick;
			// 
			// ucBtnExt12
			// 
			ucBtnExt12.BtnBackColor = Color.Empty;
			ucBtnExt12.BtnFont = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt12.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt12.ConerRadius = 8;
			ucBtnExt12.Cursor = Cursors.Hand;
			ucBtnExt12.DialogResult = DialogResult.None;
			ucBtnExt12.EnabledMouseEffect = false;
			ucBtnExt12.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt12, "ucBtnExt12");
			ucBtnExt12.IsRadius = true;
			ucBtnExt12.IsShowRect = true;
			ucBtnExt12.IsShowTips = false;
			ucBtnExt12.Name = "ucBtnExt12";
			ucBtnExt12.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt12.RectWidth = 1;
			ucBtnExt12.TabStop = false;
			ucBtnExt12.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt12.TipsText = "";
			ucBtnExt12.Click += ucBtnExt12_BtnClick;
			// 
			// ucBtnExt14
			// 
			ucBtnExt14.BtnBackColor = Color.Empty;
			ucBtnExt14.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt14.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt14.ConerRadius = 8;
			ucBtnExt14.Cursor = Cursors.Hand;
			ucBtnExt14.DialogResult = DialogResult.None;
			ucBtnExt14.EnabledMouseEffect = false;
			ucBtnExt14.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt14, "ucBtnExt14");
			ucBtnExt14.IsRadius = true;
			ucBtnExt14.IsShowRect = true;
			ucBtnExt14.IsShowTips = false;
			ucBtnExt14.Name = "ucBtnExt14";
			ucBtnExt14.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt14.RectWidth = 1;
			ucBtnExt14.TabStop = false;
			ucBtnExt14.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt14.TipsText = "";
			ucBtnExt14.Click += ucBtnExt14_BtnClick;
			// 
			// ucBtnExt16
			// 
			ucBtnExt16.BtnBackColor = Color.Empty;
			ucBtnExt16.BtnFont = null;
			ucBtnExt16.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt16.ConerRadius = 8;
			ucBtnExt16.Cursor = Cursors.Hand;
			ucBtnExt16.DialogResult = DialogResult.None;
			ucBtnExt16.EnabledMouseEffect = false;
			ucBtnExt16.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt16, "ucBtnExt16");
			ucBtnExt16.IsRadius = true;
			ucBtnExt16.IsShowRect = true;
			ucBtnExt16.IsShowTips = false;
			ucBtnExt16.Name = "ucBtnExt16";
			ucBtnExt16.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt16.RectWidth = 1;
			ucBtnExt16.TabStop = false;
			ucBtnExt16.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt16.TipsText = "";
			ucBtnExt16.Click += ucBtnExt16_BtnClick;
			// 
			// Timer
			// 
			Timer.Interval = 500;
			// 
			// ucBtnExt4
			// 
			ucBtnExt4.BtnBackColor = Color.Empty;
			ucBtnExt4.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			ucBtnExt4.BtnForeColor = Color.FromArgb(192, 192, 255);
			ucBtnExt4.ConerRadius = 8;
			ucBtnExt4.Cursor = Cursors.Hand;
			ucBtnExt4.DialogResult = DialogResult.None;
			ucBtnExt4.EnabledMouseEffect = false;
			ucBtnExt4.FillColor = Color.White;
			resources.ApplyResources(ucBtnExt4, "ucBtnExt4");
			ucBtnExt4.IsRadius = true;
			ucBtnExt4.IsShowRect = true;
			ucBtnExt4.IsShowTips = false;
			ucBtnExt4.Name = "ucBtnExt4";
			ucBtnExt4.RectColor = Color.FromArgb(192, 192, 255);
			ucBtnExt4.RectWidth = 1;
			ucBtnExt4.TabStop = false;
			ucBtnExt4.TipsColor = Color.FromArgb(232, 30, 99);
			ucBtnExt4.TipsText = "";
			ucBtnExt4.Click += ucBtnExt4_Click;
			// 
			// ItemPage
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Transparent;
			Controls.Add(TabControl);
			Name = "ItemPage";
			KeyDown += TabControl_KeyDown;
			TabControl.ResumeLayout(false);
			MainPage.ResumeLayout(false);
			MainPage.PerformLayout();
			PreviewPage_Item.ResumeLayout(false);
			PreviewPage_Item.PerformLayout();
			PreviewPage_Else.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private HZH_Controls.Controls.UCSwitch Chk_OnlyNew;
		private HZH_Controls.Controls.UCSwitch Switch_64Bit;
		private HZH_Controls.Controls.UCStep Step1;
		private HZH_Controls.Controls.UCBtnFillet ucBtnFillet1;
		private HZH_Controls.Controls.UCBtnFillet File_Searcher;
		private System.Windows.Forms.Label Note_Chv;
		private System.Windows.Forms.Label Note_GRoot;
		private HZH_Controls.Controls.UCBtnExt Btn_StartMatch;
		private System.Windows.Forms.TextBox Chv_Path;
		private System.Windows.Forms.Label TimeInfo;
		public System.Windows.Forms.TextBox GRoot_Path;
		private System.Windows.Forms.TabControl TabControl;
		private System.Windows.Forms.TabPage MainPage;
		private System.Windows.Forms.TabPage PreviewPage_Item;
		private System.Windows.Forms.Label label1;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt5;
		private System.Windows.Forms.TabPage PreviewPage_Else;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt11;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt1;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt8;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt13;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt14;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt16;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt9;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt12;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt18;
		private System.Windows.Forms.Timer Timer;
		private System.Windows.Forms.OpenFileDialog Open;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt10;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt20;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt2;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt3;
		private HZH_Controls.Controls.UCBtnExt btn_SetOutput;
		private HZH_Controls.Controls.UCTextBoxEx ItemPreview_Search;
		private HZH_Controls.Controls.UCBtnExt ucBtnExt4;
	}
}
