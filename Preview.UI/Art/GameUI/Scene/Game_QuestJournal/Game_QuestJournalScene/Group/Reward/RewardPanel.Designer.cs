
namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class RewardPanel
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
			this.RewardSelect = new HZH_Controls.Controls.UCCombox();
			this.SuspendLayout();
			// 
			// RewardSelect
			// 
			this.RewardSelect.BackColor = System.Drawing.Color.Transparent;
			this.RewardSelect.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RewardSelect.ConerRadius = 10;
			this.RewardSelect.DropPanelHeight = -1;
			this.RewardSelect.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.RewardSelect.ForeColor = System.Drawing.Color.White;
			this.RewardSelect.IsRadius = true;
			this.RewardSelect.IsShowRect = true;
			this.RewardSelect.ItemWidth = 40;
			this.RewardSelect.Location = new System.Drawing.Point(426, 20);
			this.RewardSelect.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
			this.RewardSelect.Name = "RewardSelect";
			this.RewardSelect.RectColor = System.Drawing.Color.DimGray;
			this.RewardSelect.RectWidth = 1;
			this.RewardSelect.SelectedIndex = -1;
			this.RewardSelect.Size = new System.Drawing.Size(109, 45);
			this.RewardSelect.TabIndex = 109;
			this.RewardSelect.TextValue = "奖励组";
			this.RewardSelect.TriangleColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.RewardSelect.Visible = false;
			this.RewardSelect.SelectedChangedEvent += new System.EventHandler(this.RewardSelect_SelectedChangedEvent);
			// 
			// RewardPanel
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.RewardSelect);
			this.ForeColor = System.Drawing.Color.White;
			this.Title = "奖励";
			this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.Name = "RewardPanel";
			this.Size = new System.Drawing.Size(540, 72);
			this.Controls.SetChildIndex(this.RewardSelect, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private HZH_Controls.Controls.UCCombox RewardSelect;
	}
}
