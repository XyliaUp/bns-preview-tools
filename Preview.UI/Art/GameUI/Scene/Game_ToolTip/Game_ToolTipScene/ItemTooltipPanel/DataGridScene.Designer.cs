namespace Xylia.Preview.Art.GameUI.Scene.Game_ToolTip.Game_ToolTipScene.ItemTooltipPanel;

partial class DataGridScene
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridScene));
		dataGridView = new HZH_Controls.Controls.UCDataGridView();
		Filter = new HZH_Controls.Controls.UCTextBoxEx();
		SuspendLayout();
		// 
		// dataGridView
		// 
		resources.ApplyResources(dataGridView, "dataGridView");
		dataGridView.BackColor = Color.Transparent;
		dataGridView.Columns = null;
		dataGridView.DataSource = null;
		dataGridView.HeadFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
		dataGridView.HeadHeight = 40;
		dataGridView.HeadPadingLeft = 0;
		dataGridView.HeadTextColor = Color.Black;
		dataGridView.IsShowCheckBox = false;
		dataGridView.IsShowHead = true;
		dataGridView.Name = "dataGridView";
		dataGridView.RowHeight = 40;
		dataGridView.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		// 
		// Filter
		// 
		resources.ApplyResources(Filter, "Filter");
		Filter.BackColor = Color.Transparent;
		Filter.ConerRadius = 5;
		Filter.DecLength = 2;
		Filter.FillColor = Color.Empty;
		Filter.FocusBorderColor = Color.FromArgb(255, 77, 59);
		Filter.InputText = "";
		Filter.InputType = HZH_Controls.TextInputType.NotControl;
		Filter.IsFocusColor = true;
		Filter.IsRadius = true;
		Filter.IsShowClearBtn = true;
		Filter.IsShowKeyboard = false;
		Filter.IsShowRect = true;
		Filter.IsShowSearchBtn = false;
		Filter.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderAll_EN;
		Filter.MaxValue = new decimal(new int[] { 1000000, 0, 0, 0 });
		Filter.MinValue = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
		Filter.Name = "Filter";
		Filter.PromptColor = Color.Gray;
		Filter.PromptFont = new Font("微软雅黑", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
		Filter.PromptText = "";
		Filter.RectColor = Color.FromArgb(220, 220, 220);
		Filter.RectWidth = 1;
		Filter.RegexPattern = "";
		Filter.TextChanged += Filter_SearchClick;
		// 
		// DataGridScene
		// 
		resources.ApplyResources(this, "$this");
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = Color.White;
		Controls.Add(Filter);
		Controls.Add(dataGridView);
		FormBorderStyle = FormBorderStyle.SizableToolWindow;
		Name = "DataGridScene";
		SizeChanged += DataGridScene_SizeChanged;
		ResumeLayout(false);
	}

	#endregion

	private HZH_Controls.Controls.UCDataGridView dataGridView;
	private HZH_Controls.Controls.UCTextBoxEx Filter;
}