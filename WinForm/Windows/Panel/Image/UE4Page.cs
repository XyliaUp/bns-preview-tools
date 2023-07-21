using System.ComponentModel;
using System.IO;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets.Exports.SkeletalMesh;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Assets.Exports.StaticMesh;
using CUE4Parse.UE4.Assets.Exports.Texture;

using CUE4Parse_Conversion;
using CUE4Parse_Conversion.Sounds;

using Xylia.Configure;

namespace Xylia.Match.Windows.Panel;

[DesignTimeVisible(false)]
public partial class UE4Page : UserControl
{
	#region Constructor
	public UE4Page()
	{
		InitializeComponent();

		this.Path_OutDir.Text = Ini.ReadValue(this.GetType(), nameof(this.Path_OutDir));
	}
	#endregion


	#region Functions
	private void ucBtnFillet1_BtnClick(object sender, EventArgs e)
	{
		FolderBrowserDialog dialog = new()
		{

		};

		if (dialog.ShowDialog() == DialogResult.OK)
			Path_OutDir.Text = dialog.SelectedPath;
	}

	private void Path_OutDir_TextChanged(object sender, EventArgs e) => Ini.WriteValue(this.GetType(), nameof(this.Path_OutDir), this.Path_OutDir.Text);

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

			// get path
			string dir = this.Path_OutDir.Text + "\\" + Path.GetDirectoryName(gamefile.Path);
			Directory.CreateDirectory(dir);

			var name = obj.Name + (checkBox1.Checked ? "." + obj.ExportType : null);


			// output
			switch (obj)
			{
				case UTexture2D:
				case UImageSet:
				{
					obj.GetImage()?.Save(dir + $"\\{name}.png");
				}
				break;

				case USoundWave:
				{
					obj.Decode(true, out var audioFormat, out var data);
				}
				break;


				case UStaticMesh:
				case USkeletalMesh:
				//case UAnimSequence:
				{
					var toSave = new Exporter(obj, new ExporterOptions()
					{

					});
					var success = toSave.TryWriteToDir(new DirectoryInfo(this.Path_OutDir.Text), out var label, out var savedFilePath);
				}
				break;
			}
		}
	});
	#endregion
}