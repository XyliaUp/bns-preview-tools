namespace Xylia.Match.Windows.Panel
{
	partial class QuestPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestPage));
			padding = new System.Windows.Forms.Panel();
			Btn_QusetEpic = new Button();
			Btn_QuestList = new Button();
			Button1 = new Button();
			Num = new HZH_Controls.Controls.UCNumTextBox();
			QuestMenu = new ContextMenuStrip(components);
			Output_QuestList = new ToolStripMenuItem();
			Output_EpicList = new ToolStripMenuItem();
			QuestMenu.SuspendLayout();
			SuspendLayout();
			// 
			// padding
			// 
			padding.BackColor = Color.Transparent;
			resources.ApplyResources(padding, "padding");
			padding.Name = "padding";
			// 
			// Btn_QusetEpic
			// 
			Btn_QusetEpic.BackColor = Color.Transparent;
			resources.ApplyResources(Btn_QusetEpic, "Btn_QusetEpic");
			Btn_QusetEpic.Name = "Btn_QusetEpic";
			Btn_QusetEpic.UseVisualStyleBackColor = false;
			Btn_QusetEpic.Click += Btn_QusetEpic_Click;
			// 
			// Btn_QuestList
			// 
			Btn_QuestList.BackColor = Color.Transparent;
			resources.ApplyResources(Btn_QuestList, "Btn_QuestList");
			Btn_QuestList.Name = "Btn_QuestList";
			Btn_QuestList.UseVisualStyleBackColor = false;
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
			Num.Increment = new decimal(new int[] { 1, 0, 0, 0 });
			Num.InputType = HZH_Controls.TextInputType.Number;
			Num.IsNumCanInput = true;
			Num.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
			resources.ApplyResources(Num, "Num");
			Num.MaxValue = new decimal(new int[] { 1000000, 0, 0, 0 });
			Num.MinValue = new decimal(new int[] { 0, 0, 0, 0 });
			Num.Name = "Num";
			Num.Num = new decimal(new int[] { 1, 0, 0, 0 });
			Num.NumChanged += Num_NumChanged;
			// 
			// QuestMenu
			// 
			QuestMenu.Items.AddRange(new ToolStripItem[] { Output_QuestList, Output_EpicList });
			QuestMenu.Name = "Right";
			resources.ApplyResources(QuestMenu, "QuestMenu");
			// 
			// Output_QuestList
			// 
			Output_QuestList.Name = "Output_QuestList";
			resources.ApplyResources(Output_QuestList, "Output_QuestList");
			Output_QuestList.Click += Output_QuestList_Click;
			// 
			// Output_EpicList
			// 
			Output_EpicList.Name = "Output_EpicList";
			resources.ApplyResources(Output_EpicList, "Output_EpicList");
			Output_EpicList.Click += Output_EpicList_Click;
			// 
			// QuestPage
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
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
		private System.Windows.Forms.Button Btn_QusetEpic;
		private System.Windows.Forms.Button Btn_QuestList;
		private System.Windows.Forms.ToolStripMenuItem Output_QuestList;
		private System.Windows.Forms.ToolStripMenuItem Output_EpicList;
	}
}
