using System.ComponentModel;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;

using Xylia.Preview.Properties;

namespace Xylia.Match.Windows.Panel;

[DesignTimeVisible(false)]
public partial class UE4Page : UserControl
{
	#region Constructor
	public UE4Page()
	{
		InitializeComponent();
		this.Path_OutDir.Text = CommonPath.OutputFolder_Resource;
	}
	#endregion


	#region Functions
	private void Path_OutDir_TextChanged(object sender, EventArgs e) => CommonPath.OutputFolder_Resource = this.Path_OutDir.Text;

	private void ucBtnFillet1_BtnClick(object sender, EventArgs e)
	{
		FolderBrowserDialog dialog = new()
		{

		};

		if (dialog.ShowDialog() == DialogResult.OK)
			Path_OutDir.Text = dialog.SelectedPath;
	}

	private void Selector_DrawItem(object sender, DrawItemEventArgs e)
	{
		if (e.Index < 0) return;

		e.DrawBackground();
		e.DrawFocusRectangle();
		e.Graphics.DrawString(Selector.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 3);
	}


	private async void Btn_Output_BtnClick(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(this.Selector.Text))
			return;

		this.Btn_Output.Visible = false;
		await UeExporter(this.Selector.Text);

		this.Btn_Output.Visible = true;
	}

	private async Task UeExporter(string FilterText) => await Task.Run(() =>
	{
		using GameFileProvider Provider = new();
		var filter = Provider.FixPath(FilterText, false) ?? FilterText;

		Parallel.ForEach(Provider.Files.Values, gamefile =>
		{
			if (gamefile.Extension != "uasset" || !gamefile.Path.Contains(filter, StringComparison.OrdinalIgnoreCase))
				return;

			try { Common.Output(Provider.LoadPackage(gamefile), this.checkBox1.Checked); }
			catch { }
		});
	});
	#endregion
}