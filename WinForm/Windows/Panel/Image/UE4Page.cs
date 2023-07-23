using System.ComponentModel;
using System.IO;

using CUE4Parse.BNS;

using Xylia.Configure;
using Xylia.Preview.CUE4Parse_BNS.Conversion;
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
		using PakData PakData = new();
		var filter = PakData.FixPath(FilterText, false) ?? FilterText;

		foreach (var gamefile in PakData.Provider.Files.Values.Where(o => o.Extension == "uasset" &&
			o.Path.Contains(filter, StringComparison.OrdinalIgnoreCase)))
		{
			// get object
			var obj = PakData.Provider.LoadPackage(gamefile).GetExports().FirstOrDefault();
			if (obj is null) continue;

			// output
			Common.Output(obj, checkBox1.Checked);
		}
	});
	#endregion
}