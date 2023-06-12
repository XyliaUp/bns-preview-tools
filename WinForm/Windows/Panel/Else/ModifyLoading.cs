using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using HZH_Controls.Forms;

using Xylia.Configure;

namespace Xylia.Match.Windows.Panel
{
	[DesignTimeVisible(false)]
	public partial class ModifyLoading : UserControl
	{
		readonly bool isRun = false;

		public static string pwd = "abcde12#";

		public ModifyLoading()
		{
			InitializeComponent();
		}




		private void ucBtnFillet1_BtnClick(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new();
			fileDialog.Filter = "过图文件|loading.pkg";

			try { fileDialog.InitialDirectory = Path.GetDirectoryName(this.TextBox1.Text); }
			catch { }


			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				filePath.Text = fileDialog.FileName;
			}
		}

		private void ucBtnFillet2_BtnClick(object sender, EventArgs e)
		{
			if (isRun) return;

			if (!File.Exists(filePath.Text))
			{
				FrmTips.ShowTipsError("未选择文件");
				return;
			}
			else if (Directory.Exists(TextBox1.Text) && new DirectoryInfo(TextBox1.Text).GetFiles("*.jpg").Length != 0)
			{
				var result = MessageBox.Show("继续操作会覆盖数据, 请备份数据！如已完成, 请点击确认。", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (result != DialogResult.OK)
				{
					FrmTips.ShowTipsSuccess("用户结束操作");
					return;
				}
			}


			new Thread(o =>
			{
				if (!Directory.Exists(TextBox1.Text)) Directory.CreateDirectory(TextBox1.Text);

				//Xylia.Compress.Zip.UnCompressFile(filePath.Text, TextBox1.Text, Static.pwd);

				GC.Collect();
				this.Invoke(() => FrmTips.ShowTipsSuccess("已结束输出"));

			}).Start();
		}

		private void ucBtnFillet3_BtnClick(object sender, EventArgs e)
		{
			bool is64 = Path.GetFileName(filePath.Text).Contains("64");


			if (!File.Exists(filePath.Text))
			{
				Xylia.Tip.Message("没有选择文件");
				return;
			}

			new Thread(o =>
			{
				//Xylia.Compress.Zip.ZipDirectory(TextBox1.Text, filePath.Text, Static.pwd);
				this.Invoke(() => FrmTips.ShowTipsSuccess("封包完成"));
			}).Start();
		}

		private void ucBtnFillet4_BtnClick(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new();

			fileDialog.Filter = "过图文件|local*.dat";

			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				TextBox1.Text = fileDialog.FileName;

				//Xylia.Configure.Ini.WriteValue("Modify", "BnsFolder");
			}
		}




		public string Loading_File { get => Ini.ReadValue(this, GetType(), "SourcePath"); set => Ini.WriteValue(this, GetType(), "SourcePath", value); }

		private void ModifyLocal_Load(object sender, EventArgs e)
		{
			this.filePath.Text = this.Loading_File;
		}

		private void filePath_TextChanged(object sender, EventArgs e)
		{
			this.Loading_File = this.filePath.Text;
			TextBox1.Text = Path.GetDirectoryName(this.filePath.Text) + @"\LoadingImages";
		}
	}
}