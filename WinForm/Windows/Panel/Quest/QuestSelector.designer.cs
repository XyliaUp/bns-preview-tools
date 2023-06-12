namespace Xylia.Match.Windows
{
	partial class QuestSelector
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestSelector));
			listBox1 = new System.Windows.Forms.ListBox();
			textBoxEx1 = new HZH_Controls.Controls.TextBoxEx();
			SuspendLayout();
			// 
			// listBox1
			// 
			listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			resources.ApplyResources(listBox1, "listBox1");
			listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			listBox1.FormattingEnabled = true;
			listBox1.Name = "listBox1";
			listBox1.DrawItem += ListBox1_DrawItem;
			listBox1.MouseDoubleClick += ListBox1_MouseDoubleClick;
			// 
			// textBoxEx1
			// 
			textBoxEx1.DecLength = 2;
			textBoxEx1.InputType = HZH_Controls.TextInputType.NotControl;
			resources.ApplyResources(textBoxEx1, "textBoxEx1");
			textBoxEx1.MaxValue = new decimal(new int[] { 1000000, 0, 0, 0 });
			textBoxEx1.MinValue = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
			textBoxEx1.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
			textBoxEx1.Name = "textBoxEx1";
			textBoxEx1.OldText = null;
			textBoxEx1.PromptColor = System.Drawing.Color.Gray;
			textBoxEx1.PromptFont = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			textBoxEx1.PromptText = "";
			textBoxEx1.RegexPattern = "";
			textBoxEx1.TextChanged += textBoxEx1_TextChanged;
			// 
			// QuestSelector
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(textBoxEx1);
			Controls.Add(listBox1);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			MinimizeBox = false;
			Name = "QuestSelector";
			TextChanged += QuestSelect_TextChanged;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private HZH_Controls.Controls.TextBoxEx textBoxEx1;
	}
}