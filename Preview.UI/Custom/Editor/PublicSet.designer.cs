namespace Xylia.Preview.UI.Custom.Editor
{
	partial class PublicSet
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublicSet));
			colorDialog1 = new ColorDialog();
			toolTip1 = new ToolTip(components);
			Folder = new FolderBrowserDialog();
			SettingsTabControl = new TabControl();
			EditorTabPage = new TabPage();
			SaveSettingsButton = new Button();
			HighlightsColorPanel = new Panel();
			ResetSettingsButton = new Button();
			HighlightsColorLabel = new Label();
			FontSizeComboBox = new ComboBox();
			BackgroundColorLabel = new Label();
			FontSizeLabel = new Label();
			BackgroundColorPanel = new Panel();
			FontColorPanel = new Panel();
			FontNameComboBox = new ComboBox();
			FontNameLabel = new Label();
			FontColorLabel = new Label();
			SettingsTabControl.SuspendLayout();
			EditorTabPage.SuspendLayout();
			SuspendLayout();
			// 
			// colorDialog1
			// 
			colorDialog1.AnyColor = true;
			colorDialog1.FullOpen = true;
			// 
			// SettingsTabControl
			// 
			SettingsTabControl.Controls.Add(EditorTabPage);
			SettingsTabControl.Dock = DockStyle.Fill;
			SettingsTabControl.Location = new Point(0, 0);
			SettingsTabControl.Margin = new Padding(4, 4, 4, 4);
			SettingsTabControl.Multiline = true;
			SettingsTabControl.Name = "SettingsTabControl";
			SettingsTabControl.SelectedIndex = 0;
			SettingsTabControl.Size = new Size(450, 336);
			SettingsTabControl.TabIndex = 4;
			SettingsTabControl.SelectedIndexChanged += SettingsTabControl_SelectedIndexChanged;
			// 
			// EditorTabPage
			// 
			EditorTabPage.BackColor = SystemColors.Window;
			EditorTabPage.Controls.Add(SaveSettingsButton);
			EditorTabPage.Controls.Add(HighlightsColorPanel);
			EditorTabPage.Controls.Add(ResetSettingsButton);
			EditorTabPage.Controls.Add(HighlightsColorLabel);
			EditorTabPage.Controls.Add(FontSizeComboBox);
			EditorTabPage.Controls.Add(BackgroundColorLabel);
			EditorTabPage.Controls.Add(FontSizeLabel);
			EditorTabPage.Controls.Add(BackgroundColorPanel);
			EditorTabPage.Controls.Add(FontColorPanel);
			EditorTabPage.Controls.Add(FontNameComboBox);
			EditorTabPage.Controls.Add(FontNameLabel);
			EditorTabPage.Controls.Add(FontColorLabel);
			EditorTabPage.Location = new Point(4, 26);
			EditorTabPage.Margin = new Padding(4, 4, 4, 4);
			EditorTabPage.Name = "EditorTabPage";
			EditorTabPage.Padding = new Padding(4, 4, 4, 4);
			EditorTabPage.Size = new Size(442, 306);
			EditorTabPage.TabIndex = 0;
			EditorTabPage.Text = "编辑器";
			// 
			// SaveSettingsButton
			// 
			SaveSettingsButton.Location = new Point(329, 245);
			SaveSettingsButton.Margin = new Padding(4, 4, 4, 4);
			SaveSettingsButton.Name = "SaveSettingsButton";
			SaveSettingsButton.Size = new Size(77, 31);
			SaveSettingsButton.TabIndex = 4;
			SaveSettingsButton.TabStop = false;
			SaveSettingsButton.Text = "保存";
			SaveSettingsButton.Click += SaveSettingsButton_Click;
			// 
			// HighlightsColorPanel
			// 
			HighlightsColorPanel.BackColor = Color.White;
			HighlightsColorPanel.BorderStyle = BorderStyle.FixedSingle;
			HighlightsColorPanel.Cursor = Cursors.Hand;
			HighlightsColorPanel.Location = new Point(159, 191);
			HighlightsColorPanel.Margin = new Padding(4, 4, 4, 4);
			HighlightsColorPanel.Name = "HighlightsColorPanel";
			HighlightsColorPanel.Size = new Size(116, 28);
			HighlightsColorPanel.TabIndex = 12;
			HighlightsColorPanel.Click += HighlightsColorPanel_Click;
			// 
			// ResetSettingsButton
			// 
			ResetSettingsButton.Location = new Point(246, 245);
			ResetSettingsButton.Margin = new Padding(4, 4, 4, 4);
			ResetSettingsButton.Name = "ResetSettingsButton";
			ResetSettingsButton.Size = new Size(78, 31);
			ResetSettingsButton.TabIndex = 5;
			ResetSettingsButton.Text = "重置";
			ResetSettingsButton.Click += ResetSettingsButton_Click;
			// 
			// HighlightsColorLabel
			// 
			HighlightsColorLabel.AutoSize = true;
			HighlightsColorLabel.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			HighlightsColorLabel.Location = new Point(8, 194);
			HighlightsColorLabel.Margin = new Padding(4, 0, 4, 0);
			HighlightsColorLabel.Name = "HighlightsColorLabel";
			HighlightsColorLabel.Size = new Size(80, 17);
			HighlightsColorLabel.TabIndex = 11;
			HighlightsColorLabel.Text = "高亮字体颜色";
			// 
			// FontSizeComboBox
			// 
			FontSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			FontSizeComboBox.FormattingEnabled = true;
			FontSizeComboBox.Items.AddRange(new object[] { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72", "mkm" });
			FontSizeComboBox.Location = new Point(159, 58);
			FontSizeComboBox.Margin = new Padding(4, 4, 4, 4);
			FontSizeComboBox.Name = "FontSizeComboBox";
			FontSizeComboBox.Size = new Size(116, 25);
			FontSizeComboBox.TabIndex = 6;
			// 
			// BackgroundColorLabel
			// 
			BackgroundColorLabel.AutoSize = true;
			BackgroundColorLabel.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			BackgroundColorLabel.Location = new Point(8, 149);
			BackgroundColorLabel.Margin = new Padding(4, 0, 4, 0);
			BackgroundColorLabel.Name = "BackgroundColorLabel";
			BackgroundColorLabel.Size = new Size(56, 17);
			BackgroundColorLabel.TabIndex = 10;
			BackgroundColorLabel.Text = "背景颜色";
			// 
			// FontSizeLabel
			// 
			FontSizeLabel.AutoSize = true;
			FontSizeLabel.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			FontSizeLabel.Location = new Point(8, 60);
			FontSizeLabel.Margin = new Padding(4, 0, 4, 0);
			FontSizeLabel.Name = "FontSizeLabel";
			FontSizeLabel.Size = new Size(56, 17);
			FontSizeLabel.TabIndex = 9;
			FontSizeLabel.Text = "字体大小";
			// 
			// BackgroundColorPanel
			// 
			BackgroundColorPanel.BorderStyle = BorderStyle.FixedSingle;
			BackgroundColorPanel.Cursor = Cursors.Hand;
			BackgroundColorPanel.Location = new Point(159, 146);
			BackgroundColorPanel.Margin = new Padding(4, 4, 4, 4);
			BackgroundColorPanel.Name = "BackgroundColorPanel";
			BackgroundColorPanel.Size = new Size(116, 28);
			BackgroundColorPanel.TabIndex = 8;
			BackgroundColorPanel.Click += BackgroundColorPanel_Click;
			BackgroundColorPanel.Paint += BackgroundColorPanel_Paint;
			// 
			// FontColorPanel
			// 
			FontColorPanel.BorderStyle = BorderStyle.FixedSingle;
			FontColorPanel.Cursor = Cursors.Hand;
			FontColorPanel.Location = new Point(159, 102);
			FontColorPanel.Margin = new Padding(4, 4, 4, 4);
			FontColorPanel.Name = "FontColorPanel";
			FontColorPanel.Size = new Size(116, 28);
			FontColorPanel.TabIndex = 7;
			FontColorPanel.Click += FontColorPanel_Click_1;
			// 
			// FontNameComboBox
			// 
			FontNameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			FontNameComboBox.FormattingEnabled = true;
			FontNameComboBox.Location = new Point(159, 13);
			FontNameComboBox.Margin = new Padding(4, 4, 4, 4);
			FontNameComboBox.Name = "FontNameComboBox";
			FontNameComboBox.Size = new Size(116, 25);
			FontNameComboBox.TabIndex = 5;
			// 
			// FontNameLabel
			// 
			FontNameLabel.AutoSize = true;
			FontNameLabel.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			FontNameLabel.Location = new Point(8, 16);
			FontNameLabel.Margin = new Padding(4, 0, 4, 0);
			FontNameLabel.Name = "FontNameLabel";
			FontNameLabel.Size = new Size(56, 17);
			FontNameLabel.TabIndex = 1;
			FontNameLabel.Text = "字体名称";
			// 
			// FontColorLabel
			// 
			FontColorLabel.AutoSize = true;
			FontColorLabel.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point);
			FontColorLabel.Location = new Point(8, 105);
			FontColorLabel.Margin = new Padding(4, 0, 4, 0);
			FontColorLabel.Name = "FontColorLabel";
			FontColorLabel.Size = new Size(56, 17);
			FontColorLabel.TabIndex = 0;
			FontColorLabel.Text = "字体颜色";
			// 
			// PublicSet
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(450, 336);
			Controls.Add(SettingsTabControl);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4, 4, 4, 4);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "PublicSet";
			Text = "设置";
			FormClosed += PublicSet_FormClosed;
			Load += SettingsForm_Load;
			MouseEnter += SettingsForm_MouseEnter;
			SettingsTabControl.ResumeLayout(false);
			EditorTabPage.ResumeLayout(false);
			EditorTabPage.PerformLayout();
			ResumeLayout(false);
		}

		#endregion
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.FolderBrowserDialog Folder;
		private System.Windows.Forms.TabControl SettingsTabControl;
		private System.Windows.Forms.TabPage EditorTabPage;
		private System.Windows.Forms.Button SaveSettingsButton;
		private System.Windows.Forms.Panel HighlightsColorPanel;
		private System.Windows.Forms.Button ResetSettingsButton;
		private System.Windows.Forms.Label HighlightsColorLabel;
		private System.Windows.Forms.ComboBox FontSizeComboBox;
		private System.Windows.Forms.Label BackgroundColorLabel;
		private System.Windows.Forms.Label FontSizeLabel;
		private System.Windows.Forms.Panel BackgroundColorPanel;
		private System.Windows.Forms.Panel FontColorPanel;
		private System.Windows.Forms.ComboBox FontNameComboBox;
		private System.Windows.Forms.Label FontNameLabel;
		private System.Windows.Forms.Label FontColorLabel;
	}
}