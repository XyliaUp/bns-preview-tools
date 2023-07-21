using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.UI.Custom.Controls.List
{
	partial class ListItemCell
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
		protected void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListItemCell));
			ItemShow = new ItemShowCell();
			SuspendLayout();

			// 
			// ItemShow
			// 
			ItemShow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			ItemShow.BackColor = Color.Transparent;
			ItemShow.ForeColor = Color.Black;
			ItemShow.HeightDiff = 0;
			ItemShow.ItemData = null;
			ItemShow.ItemGrade = 7;
			ItemShow.ItemIcon = (Bitmap)resources.GetObject("ItemShow.ItemIcon");
			ItemShow.ItemName = "ItemName";
			ItemShow.Location = new Point(0, 0);
			ItemShow.Margin = new Padding(0);
			ItemShow.Name = "ItemShow";
			ItemShow.ReserveIconSpace = true;
			ItemShow.Scale = 48;
			ItemShow.Size = new Size(135, 47);
			ItemShow.TabIndex = 26;
			ItemShow.TagImage = null;
			// 
			// ItemListCell
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSize = true;
			BackColor = Color.Transparent;
			Controls.Add(ItemShow);
			ForeColor = Color.White;
			Margin = new Padding(5, 6, 5, 6);
			Name = "ItemListCell";
			Size = new Size(318, 47);
			Controls.SetChildIndex(ItemShow, 0);
			ResumeLayout(false);
		}
		#endregion

		public ItemShowCell ItemShow;
	}
}