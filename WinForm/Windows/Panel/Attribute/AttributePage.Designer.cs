namespace Xylia.Match.Windows.Attribute
{
	partial class AttributePage
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttributePage));
			Btn_Start = new System.Windows.Forms.Button();
			AttritubeValue = new System.Windows.Forms.TextBox();
			numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			Label2 = new System.Windows.Forms.Label();
			Label3 = new System.Windows.Forms.Label();
			AttritubeValue_Extra = new System.Windows.Forms.TextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			NewItem = new System.Windows.Forms.ToolStripMenuItem();
			label1 = new System.Windows.Forms.Label();
			UseCompare = new System.Windows.Forms.CheckBox();
			newTreeView1 = new Xylia.Windows.Controls.TreeView();
			TextBox1 = new System.Windows.Forms.TextBox();
			MyAttributeVal = new System.Windows.Forms.TextBox();
			MyAttritube = new System.Windows.Forms.TextBox();
			TextBox2 = new System.Windows.Forms.TextBox();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			Level = new System.Windows.Forms.NumericUpDown();
			label4 = new System.Windows.Forms.Label();
			ucWaveChart1 = new HZH_Controls.Controls.UCWaveChart();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)Level).BeginInit();
			SuspendLayout();
			// 
			// Btn_Start
			// 
			resources.ApplyResources(Btn_Start, "Btn_Start");
			Btn_Start.Name = "Btn_Start";
			Btn_Start.Click += Btn_Start_Click;
			// 
			// AttritubeValue
			// 
			resources.ApplyResources(AttritubeValue, "AttritubeValue");
			AttritubeValue.Name = "AttritubeValue";
			AttritubeValue.TextChanged += TextBox1_TextChanged;
			// 
			// numericUpDown1
			// 
			numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			numericUpDown1.DecimalPlaces = 3;
			resources.ApplyResources(numericUpDown1, "numericUpDown1");
			numericUpDown1.Name = "numericUpDown1";
			// 
			// Label2
			// 
			resources.ApplyResources(Label2, "Label2");
			Label2.Name = "Label2";
			// 
			// Label3
			// 
			resources.ApplyResources(Label3, "Label3");
			Label3.Name = "Label3";
			// 
			// AttritubeValue_Extra
			// 
			resources.ApplyResources(AttritubeValue_Extra, "AttritubeValue_Extra");
			AttritubeValue_Extra.Name = "AttritubeValue_Extra";
			AttritubeValue_Extra.TextChanged += TextBox2_TextChanged;
			// 
			// contextMenuStrip1
			// 
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { NewItem });
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			resources.ApplyResources(contextMenuStrip1, "contextMenuStrip1");
			// 
			// NewItem
			// 
			NewItem.Name = "NewItem";
			resources.ApplyResources(NewItem, "NewItem");
			NewItem.Click += NewItem_Click;
			// 
			// label1
			// 
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			// 
			// UseCompare
			// 
			resources.ApplyResources(UseCompare, "UseCompare");
			UseCompare.Name = "UseCompare";
			UseCompare.CheckedChanged += CheckBox1_CheckedChanged;
			// 
			// newTreeView1
			// 
			newTreeView1.BackColor = System.Drawing.Color.White;
			newTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			newTreeView1.ContextMenuStrip = contextMenuStrip1;
			resources.ApplyResources(newTreeView1, "newTreeView1");
			newTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
			newTreeView1.HotTracking = true;
			newTreeView1.ItemHeight = 30;
			newTreeView1.Name = "newTreeView1";
			newTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { (System.Windows.Forms.TreeNode)resources.GetObject("newTreeView1.Nodes") });
			newTreeView1.ShowLines = false;
			newTreeView1.AfterSelect += newTreeView1_AfterSelect;
			// 
			// TextBox1
			// 
			resources.ApplyResources(TextBox1, "TextBox1");
			TextBox1.Name = "TextBox1";
			TextBox1.TextChanged += TextBox1_TextChanged;
			// 
			// MyAttributeVal
			// 
			resources.ApplyResources(MyAttributeVal, "MyAttributeVal");
			MyAttributeVal.Name = "MyAttributeVal";
			MyAttributeVal.TextChanged += TextBox1_TextChanged;
			// 
			// MyAttritube
			// 
			resources.ApplyResources(MyAttritube, "MyAttritube");
			MyAttritube.Name = "MyAttritube";
			MyAttritube.TextChanged += TextBox1_TextChanged;
			// 
			// TextBox2
			// 
			resources.ApplyResources(TextBox2, "TextBox2");
			TextBox2.Name = "TextBox2";
			TextBox2.TextChanged += TextBox2_TextChanged;
			// 
			// pictureBox1
			// 
			resources.ApplyResources(pictureBox1, "pictureBox1");
			pictureBox1.Name = "pictureBox1";
			pictureBox1.TabStop = false;
			// 
			// Level
			// 
			resources.ApplyResources(Level, "Level");
			Level.Name = "Level";
			Level.Value = new decimal(new int[] { 60, 0, 0, 0 });
			// 
			// label4
			// 
			resources.ApplyResources(label4, "label4");
			label4.Name = "label4";
			// 
			// ucWaveChart1
			// 
			resources.ApplyResources(ucWaveChart1, "ucWaveChart1");
			ucWaveChart1.ConerRadius = 10;
			ucWaveChart1.FillColor = System.Drawing.Color.FromArgb(50, 255, 77, 59);
			ucWaveChart1.GridLineColor = System.Drawing.Color.FromArgb(50, 255, 77, 59);
			ucWaveChart1.GridLineTextColor = System.Drawing.Color.FromArgb(150, 255, 77, 59);
			ucWaveChart1.IsRadius = true;
			ucWaveChart1.IsShowRect = false;
			ucWaveChart1.LineColor = System.Drawing.Color.FromArgb(150, 255, 77, 59);
			ucWaveChart1.LineTension = 0.5F;
			ucWaveChart1.Name = "ucWaveChart1";
			ucWaveChart1.RectColor = System.Drawing.Color.FromArgb(232, 232, 232);
			ucWaveChart1.RectWidth = 1;
			ucWaveChart1.SleepTime = 500;
			ucWaveChart1.WaveWidth = 50;
			// 
			// AttributePage
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(ucWaveChart1);
			Controls.Add(label4);
			Controls.Add(Level);
			Controls.Add(pictureBox1);
			Controls.Add(UseCompare);
			Controls.Add(label1);
			Controls.Add(newTreeView1);
			Controls.Add(AttritubeValue_Extra);
			Controls.Add(Label3);
			Controls.Add(numericUpDown1);
			Controls.Add(AttritubeValue);
			Controls.Add(Btn_Start);
			Controls.Add(Label2);
			Name = "AttributePage";
			Load += Form1_Load;
			((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)Level).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private System.Windows.Forms.Button Btn_Start;
		private System.Windows.Forms.TextBox AttritubeValue;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label Label2;
		private System.Windows.Forms.Label Label3;
		private System.Windows.Forms.TextBox AttritubeValue_Extra;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		protected Xylia.Windows.Controls.TreeView newTreeView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox UseCompare;
		private System.Windows.Forms.ToolStripMenuItem NewItem;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox TextBox1;
		private System.Windows.Forms.TextBox MyAttributeVal;
		private System.Windows.Forms.TextBox MyAttritube;
		private System.Windows.Forms.TextBox TextBox2;
		private System.Windows.Forms.NumericUpDown Level;
		private System.Windows.Forms.Label label4;
		private HZH_Controls.Controls.UCWaveChart ucWaveChart1;
	}
}