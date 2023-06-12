using System;
using System.IO;
using System.Windows.Forms;

using Xylia.Preview.Data.Models.DatData.DatDetect;
using Xylia.Preview.Properties;

namespace Xylia.Match.Windows.Forms
{
	public partial class SettingsForm : Form
	{
		#region Constructor
		public SettingsForm()
		{
			InitializeComponent();
			this.TopMost = true;

			GRoot_Path.Text = CommonPath.GameFolder;
			Faster_Folder_Path.Text = CommonPath.OutputFolder;
			Preview_DataTest.Value = CommonPath.Test;
		}
		#endregion


		#region From Events 
		private void SettingsForm_Load(object sender, EventArgs e)
		{

		}

		private void SettingsForm_MouseEnter(object sender, EventArgs e)
		{
			this.TopMost = false;
		}

		private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		#endregion

		#region Folders
		private void Faster_Folder_Btn_Click(object sender, EventArgs e)
		{
			Folder.SelectedPath = Faster_Folder_Path.Text;
			if (Folder.ShowDialog() == DialogResult.OK)
				Faster_Folder_Path.Text = Path.GetFullPath(Folder.SelectedPath);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Folder.Description = "请选择游戏根目录（即包含bin、TCLS等的文件夹）";
			Folder.SelectedPath = GRoot_Path.Text;

			if (Folder.ShowDialog() == DialogResult.OK)
			{
				GRoot_Path.Text = Folder.SelectedPath;
				Folder.Description = "";
			}
		}

		private void GRoot_Path_TextChanged(object sender, EventArgs e)
		{
			var Txt = ((TextBox)sender).Text;

			var RegionInfo = Txt.GetRegion();
			if (RegionInfo != null)
			{
				lbl_Region.Text = "客户端所属区域：" + RegionInfo;
				lbl_Region.Visible = true;
			}

			if (Directory.Exists(Txt)) CommonPath.GameFolder = Txt;
		}

		private void Faster_Folder_Path_TextChanged(object sender, EventArgs e)
		{
			var Txt = ((TextBox)sender).Text;
			if (Directory.Exists(Txt)) CommonPath.OutputFolder = Txt;
		}
		#endregion

		#region Options
		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void Preview_DataTest_Scroll(object sender, EventArgs e) => CommonPath.Test = Preview_DataTest.Value;
		#endregion
	}
}