namespace Xylia.Preview.GameUI.Scene.Searcher
{
	partial class SearcherDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearcherDialog));
			textBox1 = new TextBox();
			Confirm = new HZH_Controls.Controls.UCBtnExt();
			label1 = new Label();
			SuspendLayout();
			// 
			// textBox1
			// 
			resources.ApplyResources(textBox1, "textBox1");
			textBox1.Name = "textBox1";
			// 
			// Confirm
			// 
			Confirm.BtnBackColor = Color.Empty;
			Confirm.BtnFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			Confirm.BtnForeColor = Color.FromArgb(192, 192, 255);
			Confirm.ConerRadius = 8;
			Confirm.Cursor = Cursors.Hand;
			Confirm.DialogResult = DialogResult.None;
			Confirm.EnabledMouseEffect = false;
			Confirm.FillColor = Color.White;
			resources.ApplyResources(Confirm, "Confirm");
			Confirm.IsRadius = true;
			Confirm.IsShowRect = true;
			Confirm.IsShowTips = false;
			Confirm.Name = "Confirm";
			Confirm.RectColor = Color.FromArgb(192, 192, 255);
			Confirm.RectWidth = 1;
			Confirm.TabStop = false;
			Confirm.TipsColor = Color.FromArgb(232, 30, 99);
			Confirm.TipsText = "";
			Confirm.Click += Confirm_BtnClick;
			// 
			// label1
			// 
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			// 
			// SearcherDialog
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			Controls.Add(label1);
			Controls.Add(Confirm);
			Controls.Add(textBox1);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			KeyPreview = true;
			Name = "SearcherDialog";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private HZH_Controls.Controls.UCBtnExt Confirm;
		public System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
	}
}