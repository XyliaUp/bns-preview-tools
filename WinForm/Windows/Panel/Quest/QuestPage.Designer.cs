namespace Xylia.Match.Windows.Panel
{
	partial class QuestPage
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestPage));
			padding = new System.Windows.Forms.Panel();
			Btn_QusetEpic = new HZH_Controls.Controls.UCBtnFillet();
			Btn_QuestList = new HZH_Controls.Controls.UCBtnFillet();
			Button1 = new System.Windows.Forms.Button();
			Num = new HZH_Controls.Controls.UCNumTextBox();
			QuestMenu = new System.Windows.Forms.ContextMenuStrip(components);
			Output_QuestList = new System.Windows.Forms.ToolStripMenuItem();
			Output_EpicList = new System.Windows.Forms.ToolStripMenuItem();
			QuestMenu.SuspendLayout();
			SuspendLayout();
			// 
			// padding
			// 
			resources.ApplyResources(padding, "padding");
			padding.BackColor = System.Drawing.Color.Transparent;
			padding.Name = "padding";
			// 
			// Btn_QusetEpic
			// 
			resources.ApplyResources(Btn_QusetEpic, "Btn_QusetEpic");
			Btn_QusetEpic.BackColor = System.Drawing.Color.Transparent;
			Btn_QusetEpic.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			Btn_QusetEpic.BtnImage = (System.Drawing.Image)resources.GetObject("Btn_QusetEpic.BtnImage");
			Btn_QusetEpic.ConerRadius = 10;
			Btn_QusetEpic.FillColor = System.Drawing.Color.Transparent;
			Btn_QusetEpic.IsRadius = true;
			Btn_QusetEpic.IsShowRect = true;
			Btn_QusetEpic.Name = "Btn_QusetEpic";
			Btn_QusetEpic.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			Btn_QusetEpic.RectWidth = 1;
			Btn_QusetEpic.Click += Btn_QusetEpic_Click;
			// 
			// Btn_QuestList
			// 
			resources.ApplyResources(Btn_QuestList, "Btn_QuestList");
			Btn_QuestList.BackColor = System.Drawing.Color.Transparent;
			Btn_QuestList.BtnFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			Btn_QuestList.BtnImage = (System.Drawing.Image)resources.GetObject("Btn_QuestList.BtnImage");
			Btn_QuestList.ConerRadius = 10;
			Btn_QuestList.FillColor = System.Drawing.Color.Transparent;
			Btn_QuestList.IsRadius = true;
			Btn_QuestList.IsShowRect = true;
			Btn_QuestList.Name = "Btn_QuestList";
			Btn_QuestList.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
			Btn_QuestList.RectWidth = 1;
			Btn_QuestList.Click += Btn_QuestList_Click;
			// 
			// Button1
			// 
			resources.ApplyResources(Button1, "Button1");
			Button1.Name = "Button1";
			Button1.Click += Button1_Click;
			// 
			// Num
			// 
			resources.ApplyResources(Num, "Num");
			Num.Increment = new decimal(new int[] { 1, 0, 0, 0 });
			Num.InputType = HZH_Controls.TextInputType.Number;
			Num.IsNumCanInput = true;
			Num.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
			Num.MaxValue = new decimal(new int[] { 1000000, 0, 0, 0 });
			Num.MinValue = new decimal(new int[] { 0, 0, 0, 0 });
			Num.Name = "Num";
			Num.Num = new decimal(new int[] { 1, 0, 0, 0 });
			Num.NumChanged += Num_NumChanged;
			// 
			// QuestMenu
			// 
			resources.ApplyResources(QuestMenu, "QuestMenu");
			QuestMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { Output_QuestList, Output_EpicList });
			QuestMenu.Name = "Right";
			// 
			// Output_QuestList
			// 
			resources.ApplyResources(Output_QuestList, "Output_QuestList");
			Output_QuestList.Name = "Output_QuestList";
			Output_QuestList.Click += Output_QuestList_Click;
			// 
			// Output_EpicList
			// 
			resources.ApplyResources(Output_EpicList, "Output_EpicList");
			Output_EpicList.Name = "Output_EpicList";
			Output_EpicList.Click += Output_EpicList_Click;
			// 
			// QuestPage
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			ContextMenuStrip = QuestMenu;
			Controls.Add(Btn_QuestList);
			Controls.Add(padding);
			Controls.Add(Btn_QusetEpic);
			Controls.Add(Num);
			Controls.Add(Button1);
			Name = "QuestPage";
			QuestMenu.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
		public System.Windows.Forms.Panel padding;
		private HZH_Controls.Controls.UCNumTextBox Num;
		private System.Windows.Forms.ContextMenuStrip QuestMenu;
		public System.Windows.Forms.Button Button1;
		private HZH_Controls.Controls.UCBtnFillet Btn_QusetEpic;
		private HZH_Controls.Controls.UCBtnFillet Btn_QuestList;
		private System.Windows.Forms.ToolStripMenuItem Output_QuestList;
		private System.Windows.Forms.ToolStripMenuItem Output_EpicList;
	}
}
