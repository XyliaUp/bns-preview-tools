namespace Xylia.Windows.Forms
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublicSet));
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.Folder = new System.Windows.Forms.FolderBrowserDialog();
			this.SettingsTabControl = new System.Windows.Forms.TabControl();
			this.EditorTabPage = new System.Windows.Forms.TabPage();
			this.SaveSettingsButton = new System.Windows.Forms.Button();
			this.HighlightsColorPanel = new System.Windows.Forms.Panel();
			this.ResetSettingsButton = new System.Windows.Forms.Button();
			this.HighlightsColorLabel = new System.Windows.Forms.Label();
			this.FontSizeComboBox = new System.Windows.Forms.ComboBox();
			this.BackgroundColorLabel = new System.Windows.Forms.Label();
			this.FontSizeLabel = new System.Windows.Forms.Label();
			this.BackgroundColorPanel = new System.Windows.Forms.Panel();
			this.FontColorPanel = new System.Windows.Forms.Panel();
			this.FontNameComboBox = new System.Windows.Forms.ComboBox();
			this.FontNameLabel = new System.Windows.Forms.Label();
			this.FontColorLabel = new System.Windows.Forms.Label();
			this.SettingsTabControl.SuspendLayout();
			this.EditorTabPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// colorDialog1
			// 
			this.colorDialog1.AnyColor = true;
			this.colorDialog1.FullOpen = true;
			// 
			// SettingsTabControl
			// 
			this.SettingsTabControl.Controls.Add(this.EditorTabPage);
			this.SettingsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SettingsTabControl.Location = new System.Drawing.Point(20, 60);
			this.SettingsTabControl.Multiline = true;
			this.SettingsTabControl.Name = "SettingsTabControl";
			this.SettingsTabControl.SelectedIndex = 0;
			this.SettingsTabControl.Size = new System.Drawing.Size(400, 250);
			this.SettingsTabControl.TabIndex = 4;
			this.SettingsTabControl.SelectedIndexChanged += new System.EventHandler(this.SettingsTabControl_SelectedIndexChanged);
			// 
			// EditorTabPage
			// 
			this.EditorTabPage.BackColor = System.Drawing.SystemColors.Window;
			this.EditorTabPage.Controls.Add(this.SaveSettingsButton);
			this.EditorTabPage.Controls.Add(this.HighlightsColorPanel);
			this.EditorTabPage.Controls.Add(this.ResetSettingsButton);
			this.EditorTabPage.Controls.Add(this.HighlightsColorLabel);
			this.EditorTabPage.Controls.Add(this.FontSizeComboBox);
			this.EditorTabPage.Controls.Add(this.BackgroundColorLabel);
			this.EditorTabPage.Controls.Add(this.FontSizeLabel);
			this.EditorTabPage.Controls.Add(this.BackgroundColorPanel);
			this.EditorTabPage.Controls.Add(this.FontColorPanel);
			this.EditorTabPage.Controls.Add(this.FontNameComboBox);
			this.EditorTabPage.Controls.Add(this.FontNameLabel);
			this.EditorTabPage.Controls.Add(this.FontColorLabel);
			this.EditorTabPage.Location = new System.Drawing.Point(4, 36);
			this.EditorTabPage.Name = "EditorTabPage";
			this.EditorTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.EditorTabPage.Size = new System.Drawing.Size(392, 210);
			this.EditorTabPage.TabIndex = 0;
			this.EditorTabPage.Text = "编辑器";
			// 
			// SaveSettingsButton
			// 
			this.SaveSettingsButton.Location = new System.Drawing.Point(282, 173);
			this.SaveSettingsButton.Name = "SaveSettingsButton";
			this.SaveSettingsButton.Size = new System.Drawing.Size(66, 22);
			this.SaveSettingsButton.TabIndex = 4;
			this.SaveSettingsButton.TabStop = false;
			this.SaveSettingsButton.Text = "保存";
			this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
			// 
			// HighlightsColorPanel
			// 
			this.HighlightsColorPanel.BackColor = System.Drawing.Color.White;
			this.HighlightsColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.HighlightsColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.HighlightsColorPanel.Location = new System.Drawing.Point(136, 135);
			this.HighlightsColorPanel.Name = "HighlightsColorPanel";
			this.HighlightsColorPanel.Size = new System.Drawing.Size(100, 20);
			this.HighlightsColorPanel.TabIndex = 12;
			this.HighlightsColorPanel.Click += new System.EventHandler(this.HighlightsColorPanel_Click);
			// 
			// ResetSettingsButton
			// 
			this.ResetSettingsButton.Location = new System.Drawing.Point(211, 173);
			this.ResetSettingsButton.Name = "ResetSettingsButton";
			this.ResetSettingsButton.Size = new System.Drawing.Size(67, 22);
			this.ResetSettingsButton.TabIndex = 5;
			this.ResetSettingsButton.Text = "重置";
			this.ResetSettingsButton.Click += new System.EventHandler(this.ResetSettingsButton_Click);
			// 
			// HighlightsColorLabel
			// 
			this.HighlightsColorLabel.AutoSize = true;
			this.HighlightsColorLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.HighlightsColorLabel.Location = new System.Drawing.Point(7, 137);
			this.HighlightsColorLabel.Name = "HighlightsColorLabel";
			this.HighlightsColorLabel.Size = new System.Drawing.Size(80, 17);
			this.HighlightsColorLabel.TabIndex = 11;
			this.HighlightsColorLabel.Text = "高亮字体颜色";
			// 
			// FontSizeComboBox
			// 
			this.FontSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FontSizeComboBox.FormattingEnabled = true;
			this.FontSizeComboBox.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72",
            "mkm"});
			this.FontSizeComboBox.Location = new System.Drawing.Point(136, 41);
			this.FontSizeComboBox.Name = "FontSizeComboBox";
			this.FontSizeComboBox.Size = new System.Drawing.Size(100, 20);
			this.FontSizeComboBox.TabIndex = 6;
			// 
			// BackgroundColorLabel
			// 
			this.BackgroundColorLabel.AutoSize = true;
			this.BackgroundColorLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BackgroundColorLabel.Location = new System.Drawing.Point(7, 105);
			this.BackgroundColorLabel.Name = "BackgroundColorLabel";
			this.BackgroundColorLabel.Size = new System.Drawing.Size(56, 17);
			this.BackgroundColorLabel.TabIndex = 10;
			this.BackgroundColorLabel.Text = "背景颜色";
			// 
			// FontSizeLabel
			// 
			this.FontSizeLabel.AutoSize = true;
			this.FontSizeLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FontSizeLabel.Location = new System.Drawing.Point(7, 42);
			this.FontSizeLabel.Name = "FontSizeLabel";
			this.FontSizeLabel.Size = new System.Drawing.Size(56, 17);
			this.FontSizeLabel.TabIndex = 9;
			this.FontSizeLabel.Text = "字体大小";
			// 
			// BackgroundColorPanel
			// 
			this.BackgroundColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BackgroundColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BackgroundColorPanel.Location = new System.Drawing.Point(136, 103);
			this.BackgroundColorPanel.Name = "BackgroundColorPanel";
			this.BackgroundColorPanel.Size = new System.Drawing.Size(100, 20);
			this.BackgroundColorPanel.TabIndex = 8;
			this.BackgroundColorPanel.Click += new System.EventHandler(this.BackgroundColorPanel_Click);
			this.BackgroundColorPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.BackgroundColorPanel_Paint);
			// 
			// FontColorPanel
			// 
			this.FontColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FontColorPanel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.FontColorPanel.Location = new System.Drawing.Point(136, 72);
			this.FontColorPanel.Name = "FontColorPanel";
			this.FontColorPanel.Size = new System.Drawing.Size(100, 20);
			this.FontColorPanel.TabIndex = 7;
			this.FontColorPanel.Click += new System.EventHandler(this.FontColorPanel_Click_1);
			// 
			// FontNameComboBox
			// 
			this.FontNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FontNameComboBox.FormattingEnabled = true;
			this.FontNameComboBox.Location = new System.Drawing.Point(136, 9);
			this.FontNameComboBox.Name = "FontNameComboBox";
			this.FontNameComboBox.Size = new System.Drawing.Size(100, 20);
			this.FontNameComboBox.TabIndex = 5;
			// 
			// FontNameLabel
			// 
			this.FontNameLabel.AutoSize = true;
			this.FontNameLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FontNameLabel.Location = new System.Drawing.Point(7, 11);
			this.FontNameLabel.Name = "FontNameLabel";
			this.FontNameLabel.Size = new System.Drawing.Size(56, 17);
			this.FontNameLabel.TabIndex = 1;
			this.FontNameLabel.Text = "字体名称";
			// 
			// FontColorLabel
			// 
			this.FontColorLabel.AutoSize = true;
			this.FontColorLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FontColorLabel.Location = new System.Drawing.Point(7, 74);
			this.FontColorLabel.Name = "FontColorLabel";
			this.FontColorLabel.Size = new System.Drawing.Size(56, 17);
			this.FontColorLabel.TabIndex = 0;
			this.FontColorLabel.Text = "字体颜色";
			// 
			// PublicSet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(440, 330);
			this.Controls.Add(this.SettingsTabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PublicSet";
			this.Text = "设置";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PublicSet_FormClosed);
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.MouseEnter += new System.EventHandler(this.SettingsForm_MouseEnter);
			this.SettingsTabControl.ResumeLayout(false);
			this.EditorTabPage.ResumeLayout(false);
			this.EditorTabPage.PerformLayout();
			this.ResumeLayout(false);

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