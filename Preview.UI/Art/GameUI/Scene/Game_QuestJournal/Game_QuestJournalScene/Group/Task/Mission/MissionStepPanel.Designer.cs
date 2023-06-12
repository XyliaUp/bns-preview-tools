using Xylia.Preview.GameUI.Controls;


namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	partial class MissionStepPanel
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源, 为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的Functions - 不要修改
		/// 使用代码编辑器修改此Functions的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.MissionDemand = new Controls.ContentPanel();
			this.Panel_TaskDesc = new Controls.ContentPanel();
			this.Content_StepID = new Controls.ContentPanel();
			this.SuspendLayout();
			// 
			// MissionDemand
			// 
			this.MissionDemand.BackColor = System.Drawing.Color.Transparent;
			this.MissionDemand.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.MissionDemand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(102)))));
			this.MissionDemand.Location = new System.Drawing.Point(19, 0);
			this.MissionDemand.Margin = new System.Windows.Forms.Padding(2, 7, 2, 7);
			this.MissionDemand.Name = "MissionDemand";
			this.MissionDemand.TabIndex = 14;
			this.MissionDemand.Text = "课题需求信息";
			this.MissionDemand.Visible = false;
			// 
			// Panel_TaskDesc
			// 
			this.Panel_TaskDesc.BackColor = System.Drawing.Color.Transparent;
			this.Panel_TaskDesc.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Panel_TaskDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(102)))));
			this.Panel_TaskDesc.Location = new System.Drawing.Point(0, 23);
			this.Panel_TaskDesc.Margin = new System.Windows.Forms.Padding(2, 7, 2, 7);
			this.Panel_TaskDesc.Name = "Panel_TaskDesc";
			this.Panel_TaskDesc.TabIndex = 13;
			this.Panel_TaskDesc.Text = "TaskDesc";
			this.Panel_TaskDesc.Visible = false;
			// 
			// Content_StepID
			// 
			this.Content_StepID.BackColor = System.Drawing.Color.Transparent;
			this.Content_StepID.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.Content_StepID.ForeColor = System.Drawing.Color.White;
			this.Content_StepID.Location = new System.Drawing.Point(1, -1);
			this.Content_StepID.Margin = new System.Windows.Forms.Padding(2, 7, 2, 7);
			this.Content_StepID.Name = "Content_StepID";
			this.Content_StepID.TabIndex = 15;
			this.Content_StepID.Text = "●";
			// 
			// MissionStepPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Content_StepID);
			this.Controls.Add(this.MissionDemand);
			this.Controls.Add(this.Panel_TaskDesc);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MissionStepPanel";
			this.Size = new System.Drawing.Size(109, 50);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.ContentPanel Panel_TaskDesc;
		private Controls.ContentPanel MissionDemand;
		private Controls.ContentPanel Content_StepID;
	}
}
