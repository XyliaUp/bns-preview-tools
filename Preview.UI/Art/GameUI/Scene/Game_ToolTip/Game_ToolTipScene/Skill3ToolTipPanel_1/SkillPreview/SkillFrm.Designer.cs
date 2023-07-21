using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Skill
{
	partial class SkillFrm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillFrm));
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// textBox1
			// 
			resources.ApplyResources(textBox1, "textBox1");
			textBox1.Name = "textBox1";
			textBox1.TextChanged += textBox1_TextChanged;
			// 
			// SkillFrm
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Black;
			Controls.Add(textBox1);
			DoubleBuffered = true;
			ForeColor = Color.DimGray;
			FormBorderStyle = FormBorderStyle.SizableToolWindow;
			Name = "SkillFrm";
			Shown += SkillFrm_Shown;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ItemNamePanel itemNameCell1;
		private System.Windows.Forms.TextBox textBox1;
	}
}

