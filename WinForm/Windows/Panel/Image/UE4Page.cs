using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Xylia.Configure;
using CUE4Parse.BNS;


namespace Xylia.Match.Windows.Panel
{
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
		private void Btn_Output_BtnClick(object sender, EventArgs e)
		{
			var tempPath = this.Selector.TextValue;
			if (string.IsNullOrWhiteSpace(tempPath)) return;

			new Thread(t =>
			{
				this.Btn_Output.Visible = false;

				using PakData PakData = new();
				PakData.Initialize();

				foreach (var gamefile in PakData._provider.Files
					.Where(o => o.Value.Extension == "uasset" && o.Value.Path.Contains(tempPath))
					.Select(o => o.Value))
				{
					string dir = this.Path_OutDir.Text + "\\" + Path.GetDirectoryName(gamefile.Path);
					if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

					var exports = PakData._provider.LoadPackage(gamefile).GetExports();
					if (exports is null || !exports.Any()) continue;

					var o = exports.First();
					var type = checkBox1.Checked ? "." + o.ExportType : null;
					var ext = ".png";
					o.GetImage()?.Save(dir + $"\\{o.Name}{type}{ext}");
				}

				GC.Collect();
				this.Btn_Output.Visible = true;

			}).Start();
		}


		private void ucBtnFillet1_BtnClick(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new()
			{

			};

			if (dialog.ShowDialog() == DialogResult.OK)
				Path_OutDir.Text = dialog.SelectedPath;
		}

		private void Path_OutDir_TextChanged(object sender, EventArgs e) => Ini.WriteValue(this.GetType(), nameof(this.Path_OutDir), this.Path_OutDir.Text);
		#endregion
	}
}