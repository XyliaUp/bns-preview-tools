using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Xylia.Preview.GameUI.Controls
{
	partial class ItemIconCell
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
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// ItemIconCell
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.ForeColor = System.Drawing.Color.Black;
			this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.DoubleClick += new System.EventHandler(this.ItemIconCell_DoubleClick);
			this.Resize += new System.EventHandler(this.ItemIconCell_Resize);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
	}
}
