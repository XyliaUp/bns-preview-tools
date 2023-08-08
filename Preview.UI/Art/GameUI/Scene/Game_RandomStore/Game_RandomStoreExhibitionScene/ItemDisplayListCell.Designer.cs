
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_RandomStore
{
	partial class ItemDisplayListCell
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemDisplayListCell));
			this.ItemShow = new Xylia.Preview.UI.Custom.Controls.ItemShowCell();
			this.SuspendLayout();
			// 
			// ItemShow
			// 
			this.ItemShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ItemShow.BackColor = System.Drawing.Color.Transparent;
			this.ItemShow.ForeColor = System.Drawing.Color.Black;
			this.ItemShow.HeightDiff = 0;
			this.ItemShow.ItemData = null;
			this.ItemShow.ItemGrade = ((sbyte)(7));
			this.ItemShow.ItemIcon = ((System.Drawing.Bitmap)(resources.GetObject("ItemShow.ItemIcon")));
			this.ItemShow.ItemName = "ItemName";
			this.ItemShow.Location = new System.Drawing.Point(4, 0);
			this.ItemShow.Margin = new System.Windows.Forms.Padding(4);
			this.ItemShow.Name = "ItemShow";
			this.ItemShow.ReserveIconSpace = true;
			this.ItemShow.Scale = 52;
			this.ItemShow.Size = new System.Drawing.Size(146, 57);
			this.ItemShow.TabIndex = 27;
			this.ItemShow.TagImage = null;
			// 
			// ItemDisplayListCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoSize = true;
			this.Controls.Add(this.ItemShow);
			this.Name = "ItemDisplayListCell";
			this.Size = new System.Drawing.Size(385, 61);
			this.Controls.SetChildIndex(this.ItemShow, 0);
			this.ResumeLayout(false);

		}
		#endregion

		public ItemShowCell ItemShow;
	}
}