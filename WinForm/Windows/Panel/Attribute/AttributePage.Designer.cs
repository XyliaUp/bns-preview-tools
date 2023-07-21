namespace Xylia.Match.Windows.Attribute
{
	partial class AttributePage
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

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttributePage));
			Btn_Start = new Button();
			AttritubeValue = new TextBox();
			numericUpDown1 = new NumericUpDown();
			Label2 = new Label();
			Label3 = new Label();
			AttritubeValue_Extra = new TextBox();
			contextMenuStrip1 = new ContextMenuStrip(components);
			NewItem = new ToolStripMenuItem();
			label1 = new Label();
			UseCompare = new CheckBox();
			newTreeView1 = new Xylia.Windows.Controls.TreeView();
			TextBox1 = new TextBox();
			MyAttributeVal = new TextBox();
			MyAttritube = new TextBox();
			TextBox2 = new TextBox();
			pictureBox1 = new PictureBox();
			Level = new NumericUpDown();
			label4 = new Label();
			CartesianChart_Panel = new System.Windows.Forms.Panel();
			CartesianChart = new LiveCharts.WinForms.CartesianChart();
			label5 = new Label();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)Level).BeginInit();
			CartesianChart_Panel.SuspendLayout();
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
			resources.ApplyResources(numericUpDown1, "numericUpDown1");
			numericUpDown1.BorderStyle = BorderStyle.FixedSingle;
			numericUpDown1.DecimalPlaces = 3;
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
			resources.ApplyResources(contextMenuStrip1, "contextMenuStrip1");
			contextMenuStrip1.Items.AddRange(new ToolStripItem[] { NewItem });
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.RenderMode = ToolStripRenderMode.Professional;
			// 
			// NewItem
			// 
			resources.ApplyResources(NewItem, "NewItem");
			NewItem.Name = "NewItem";
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
			resources.ApplyResources(newTreeView1, "newTreeView1");
			newTreeView1.BackColor = Color.White;
			newTreeView1.BorderStyle = BorderStyle.None;
			newTreeView1.ContextMenuStrip = contextMenuStrip1;
			newTreeView1.DrawMode = TreeViewDrawMode.OwnerDrawAll;
			newTreeView1.HotTracking = true;
			newTreeView1.ItemHeight = 30;
			newTreeView1.Name = "newTreeView1";
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
			// CartesianChart_Panel
			// 
			resources.ApplyResources(CartesianChart_Panel, "CartesianChart_Panel");
			CartesianChart_Panel.Controls.Add(CartesianChart);
			CartesianChart_Panel.Name = "CartesianChart_Panel";
			// 
			// CartesianChart
			// 
			resources.ApplyResources(CartesianChart, "CartesianChart");
			CartesianChart.Name = "CartesianChart";
			// 
			// label5
			// 
			resources.ApplyResources(label5, "label5");
			label5.Name = "label5";
			// 
			// AttributePage
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(label5);
			Controls.Add(CartesianChart_Panel);
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
			CartesianChart_Panel.ResumeLayout(false);
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
		private System.Windows.Forms.Panel CartesianChart_Panel;
		private LiveCharts.WinForms.CartesianChart CartesianChart;
		private Label label5;
	}
}