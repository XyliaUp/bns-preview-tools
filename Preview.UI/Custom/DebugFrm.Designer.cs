using System.Windows.Forms.Integration;

using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Custom.Controls.Currency;

namespace Xylia.Preview.UI
{
	partial class DebugFrm
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
			textBox1 = new TextBox();
			elementHost1 = new ElementHost();
			listPreview1 = new ListPreview();
			contentPanel1 = new ContentPanel();
			priceCell1 = new PriceCell();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.Location = new Point(12, 12);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(237, 23);
			textBox1.TabIndex = 0;
			// 
			// elementHost1
			// 
			elementHost1.Location = new Point(0, 100);
			elementHost1.Name = "elementHost1";
			elementHost1.Size = new Size(500, 200);
			elementHost1.TabIndex = 0;
			// 
			// listPreview1
			// 
			listPreview1.AutoScroll = true;
			listPreview1.BackColor = Color.Transparent;
			listPreview1.ForeColor = Color.Blue;
			listPreview1.Location = new Point(301, 12);
			listPreview1.Name = "listPreview1";
			listPreview1.Size = new Size(284, 320);
			listPreview1.TabIndex = 4;
			// 
			// contentPanel1
			// 
			contentPanel1.BackColor = Color.Transparent;
			contentPanel1.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
			contentPanel1.ForeColor = Color.White;
			contentPanel1.Location = new Point(17, 127);
			contentPanel1.Margin = new Padding(2, 5, 2, 5);
			contentPanel1.Name = "contentPanel1";
			contentPanel1.TabIndex = 7;
			contentPanel1.Text = "<arg p=\"2:integer:money-default\">";
			// 
			// priceCell1
			// 
			priceCell1.BackColor = Color.Transparent;
			priceCell1.CurrencyCount = 1;
			priceCell1.CurrencyType = CurrencyType.Money;
			priceCell1.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
			priceCell1.FontStyle = FontStyle.Regular;
			priceCell1.ForeColor = Color.White;
			priceCell1.Location = new Point(17, 68);
			priceCell1.Margin = new Padding(5);
			priceCell1.Name = "priceCell1";
			priceCell1.Size = new Size(105, 23);
			priceCell1.TabIndex = 8;
			priceCell1.Tooltip = "出售价格为";
			// 
			// DebugFrm
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoScroll = true;
			AutoSize = true;
			BackColor = Color.Black;
			BackgroundImageLayout = ImageLayout.Stretch;
			ClientSize = new Size(617, 358);
			Controls.Add(priceCell1);
			Controls.Add(contentPanel1);
			Controls.Add(listPreview1);
			Controls.Add(elementHost1);
			Controls.Add(textBox1);
			DoubleBuffered = true;
			ForeColor = Color.DimGray;
			FormBorderStyle = FormBorderStyle.SizableToolWindow;
			Margin = new Padding(4);
			Name = "DebugFrm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Debug";
			Load += DebugFrm_Load;
			ResumeLayout(false);
			PerformLayout();
		}


		#endregion

		private System.Windows.Forms.TextBox textBox1;

		private ElementHost elementHost1;
		private ListPreview listPreview1;
		private ContentPanel contentPanel1;
		private PriceCell priceCell1;
	}
}