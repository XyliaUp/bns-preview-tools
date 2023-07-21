namespace Xylia.Preview.UI.Custom.Controls
{
	partial class ItemShowCell
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
			NameCell = new ItemNamePanel();
			IconCell = new ItemIconCell();
			((System.ComponentModel.ISupportInitialize)IconCell).BeginInit();
			SuspendLayout();
			// 
			// NameCell
			// 
			NameCell.BackColor = Color.Transparent;
			NameCell.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			NameCell.ItemGrade = 7;
			NameCell.Location = new Point(63, 15);
			NameCell.Margin = new Padding(6, 7, 6, 7);
			NameCell.MaximumSize = new Size(200, 50);
			NameCell.Name = "NameCell";
			NameCell.TabIndex = 1;
			NameCell.TagImage = null;
			NameCell.Text = "ItemName";
			NameCell.DoubleClick += ItemName_DoubleClick;
			// 
			// IconCell
			// 
			IconCell.BackColor = Color.Transparent;
			IconCell.ForeColor = Color.Black;
			IconCell.FrameImage = null;
			IconCell.FrameType = true;
			IconCell.Location = new Point(0, 0);
			IconCell.Margin = new Padding(4);
			IconCell.Name = "IconCell";
			IconCell.Scale = 52;
			IconCell.ShowFrameImage = true;
			IconCell.ShowStackCount = false;
			IconCell.ShowStackCountOnlyOne = true;
			IconCell.Size = new Size(52, 52);
			IconCell.SizeMode = PictureBoxSizeMode.Zoom;
			IconCell.StackCount = 1;
			IconCell.TabIndex = 2;
			IconCell.TabStop = false;
			// 
			// ItemShowCell
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSize = true;
			BackColor = Color.Transparent;
			Controls.Add(IconCell);
			Controls.Add(NameCell);
			ForeColor = Color.Black;
			Margin = new Padding(4);
			Name = "ItemShowCell";
			Size = new Size(269, 56);
			((System.ComponentModel.ISupportInitialize)IconCell).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		public ItemIconCell IconCell;
		public ItemNamePanel NameCell;
	}
}
