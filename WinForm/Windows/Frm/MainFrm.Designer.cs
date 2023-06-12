﻿namespace Xylia.Match.Windows
{
	partial class MainFrm
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
			tvMenu = new HZH_Controls.Controls.TreeViewEx();
			Memory = new System.Windows.Forms.Label();
			Panel = new System.Windows.Forms.Panel();
			MainMenu = new System.Windows.Forms.ContextMenuStrip(components);
			OpenFolder = new System.Windows.Forms.ToolStripMenuItem();
			OpenRes = new System.Windows.Forms.ToolStripMenuItem();
			Footer = new System.Windows.Forms.Label();
			Btn_log = new System.Windows.Forms.PictureBox();
			Btn_Set = new System.Windows.Forms.PictureBox();
			Btn_AboutUs = new System.Windows.Forms.PictureBox();
			tvMenu.SuspendLayout();
			MainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)Btn_log).BeginInit();
			((System.ComponentModel.ISupportInitialize)Btn_Set).BeginInit();
			((System.ComponentModel.ISupportInitialize)Btn_AboutUs).BeginInit();
			SuspendLayout();
			// 
			// tvMenu
			// 
			resources.ApplyResources(tvMenu, "tvMenu");
			tvMenu.BackColor = System.Drawing.Color.FromArgb(40, 43, 51);
			tvMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
			tvMenu.Controls.Add(Memory);
			tvMenu.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
			tvMenu.FullRowSelect = true;
			tvMenu.HideSelection = false;
			tvMenu.IsShowByCustomModel = true;
			tvMenu.IsShowTip = false;
			tvMenu.ItemHeight = 50;
			tvMenu.LstTips = null;
			tvMenu.Name = "tvMenu";
			tvMenu.NodeBackgroundColor = System.Drawing.Color.FromArgb(40, 43, 51);
			tvMenu.NodeForeColor = System.Drawing.Color.White;
			tvMenu.NodeHeight = 50;
			tvMenu.NodeIsShowSplitLine = true;
			tvMenu.NodeSelectedColor = System.Drawing.Color.FromArgb(57, 61, 73);
			tvMenu.NodeSelectedForeColor = System.Drawing.Color.White;
			tvMenu.NodeSplitLineColor = System.Drawing.Color.FromArgb(57, 61, 73);
			tvMenu.ParentNodeCanSelect = true;
			tvMenu.ShowLines = false;
			tvMenu.ShowPlusMinus = false;
			tvMenu.ShowRootLines = false;
			tvMenu.TipFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			// 
			// Memory
			// 
			resources.ApplyResources(Memory, "Memory");
			Memory.BackColor = System.Drawing.Color.Transparent;
			Memory.ForeColor = System.Drawing.Color.MediumAquamarine;
			Memory.Name = "Memory";
			// 
			// Panel
			// 
			resources.ApplyResources(Panel, "Panel");
			Panel.BackColor = System.Drawing.Color.Transparent;
			Panel.ContextMenuStrip = MainMenu;
			Panel.Name = "Panel";
			// 
			// MainMenu
			// 
			resources.ApplyResources(MainMenu, "MainMenu");
			MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { OpenFolder });
			MainMenu.Name = "Right";
			// 
			// OpenFolder
			// 
			resources.ApplyResources(OpenFolder, "OpenFolder");
			OpenFolder.Name = "OpenFolder";
			OpenFolder.Click += OpenFolder_Click;
			// 
			// OpenRes
			// 
			resources.ApplyResources(OpenRes, "OpenRes");
			OpenRes.Name = "OpenRes";
			// 
			// Footer
			// 
			resources.ApplyResources(Footer, "Footer");
			Footer.BackColor = System.Drawing.Color.Transparent;
			Footer.ForeColor = System.Drawing.Color.FromArgb(153, 204, 255);
			Footer.Name = "Footer";
			// 
			// Btn_log
			// 
			resources.ApplyResources(Btn_log, "Btn_log");
			Btn_log.BackColor = System.Drawing.Color.Azure;
			Btn_log.Name = "Btn_log";
			Btn_log.TabStop = false;
			Btn_log.Click += pictureBox1_Click;
			// 
			// Btn_Set
			// 
			resources.ApplyResources(Btn_Set, "Btn_Set");
			Btn_Set.BackColor = System.Drawing.Color.Azure;
			Btn_Set.Name = "Btn_Set";
			Btn_Set.TabStop = false;
			Btn_Set.Click += Set_Click;
			// 
			// Btn_AboutUs
			// 
			resources.ApplyResources(Btn_AboutUs, "Btn_AboutUs");
			Btn_AboutUs.BackColor = System.Drawing.Color.Azure;
			Btn_AboutUs.Name = "Btn_AboutUs";
			Btn_AboutUs.TabStop = false;
			Btn_AboutUs.Click += Btn_AboutUs_Click;
			// 
			// MainFrm
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ContextMenuStrip = MainMenu;
			Controls.Add(Btn_AboutUs);
			Controls.Add(Footer);
			Controls.Add(Btn_Set);
			Controls.Add(Btn_log);
			Controls.Add(Panel);
			Controls.Add(tvMenu);
			IsShowCloseBtn = true;
			KeyPreview = true;
			Name = "MainFrm";
			ShowIcon = true;
			Title = "Title";
			FormClosing += MainForm_FormClosing;
			Shown += MainForm_Shown;
			SizeChanged += MainForm_SizeChanged;
			Controls.SetChildIndex(tvMenu, 0);
			Controls.SetChildIndex(Panel, 0);
			Controls.SetChildIndex(Btn_log, 0);
			Controls.SetChildIndex(Btn_Set, 0);
			Controls.SetChildIndex(Footer, 0);
			Controls.SetChildIndex(Btn_AboutUs, 0);
			tvMenu.ResumeLayout(false);
			tvMenu.PerformLayout();
			MainMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)Btn_log).EndInit();
			((System.ComponentModel.ISupportInitialize)Btn_Set).EndInit();
			((System.ComponentModel.ISupportInitialize)Btn_AboutUs).EndInit();
			ResumeLayout(false);
		}
		#endregion

		private HZH_Controls.Controls.TreeViewEx tvMenu;
		private System.Windows.Forms.Panel Panel;
		private System.Windows.Forms.PictureBox Btn_log;
		private System.Windows.Forms.ToolStripMenuItem Faction;
		private System.Windows.Forms.ToolStripMenuItem OpenRes;
		private System.Windows.Forms.ToolStripMenuItem OpenFolder;
		private System.Windows.Forms.Label Footer;
		private System.Windows.Forms.Label Memory;
		private System.Windows.Forms.PictureBox Btn_Set;
		private System.Windows.Forms.PictureBox Btn_AboutUs;
		private System.Windows.Forms.ContextMenuStrip MainMenu;
	}
}