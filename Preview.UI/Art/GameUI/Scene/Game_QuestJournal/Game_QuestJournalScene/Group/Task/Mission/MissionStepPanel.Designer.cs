using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class MissionStepPanel
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
			MissionDemand = new ContentPanel();
			Panel_TaskDesc = new ContentPanel();
			Content_StepID = new ContentPanel();
			SuspendLayout();
			// 
			// MissionDemand
			// 
			MissionDemand.BackColor = Color.Transparent;
			MissionDemand.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
			MissionDemand.ForeColor = Color.FromArgb(255, 255, 102);
			MissionDemand.Location = new Point(19, 0);
			MissionDemand.Margin = new Padding(2, 7, 2, 7);
			MissionDemand.Name = "MissionDemand";
			MissionDemand.TabIndex = 14;
			MissionDemand.Text = "课题需求信息";
			MissionDemand.Visible = false;
			// 
			// Panel_TaskDesc
			// 
			Panel_TaskDesc.BackColor = Color.Transparent;
			Panel_TaskDesc.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
			Panel_TaskDesc.ForeColor = Color.FromArgb(255, 255, 102);
			Panel_TaskDesc.Location = new Point(0, 23);
			Panel_TaskDesc.Margin = new Padding(2, 7, 2, 7);
			Panel_TaskDesc.Name = "Panel_TaskDesc";
			Panel_TaskDesc.TabIndex = 13;
			Panel_TaskDesc.Text = "TaskDesc";
			Panel_TaskDesc.Visible = false;
			// 
			// Content_StepID
			// 
			Content_StepID.BackColor = Color.Transparent;
			Content_StepID.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
			Content_StepID.ForeColor = Color.White;
			Content_StepID.Location = new Point(1, -1);
			Content_StepID.Margin = new Padding(2, 7, 2, 7);
			Content_StepID.Name = "Content_StepID";
			Content_StepID.TabIndex = 15;
			Content_StepID.Text = "●";
			// 
			// MissionStepPanel
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSize = true;
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			BackColor = Color.Transparent;
			Controls.Add(Content_StepID);
			Controls.Add(MissionDemand);
			Controls.Add(Panel_TaskDesc);
			Margin = new Padding(4);
			Name = "MissionStepPanel";
			Size = new Size(109, 48);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ContentPanel Panel_TaskDesc;
		private ContentPanel MissionDemand;
		private ContentPanel Content_StepID;
	}
}
