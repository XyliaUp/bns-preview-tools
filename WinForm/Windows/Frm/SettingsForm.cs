﻿using System.ComponentModel;
using System.IO;

using Xylia.Extension;
using Xylia.Preview.Data.Models.DatData.DatDetect;
using Xylia.Preview.Properties;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Match.Windows.Forms;
public partial class SettingsForm : Form
{
	#region Constructor
	public SettingsForm()
	{
		InitializeComponent();
		this.TopMost = true;

		GRoot_Path.Text = CommonPath.GameFolder;
		Faster_Folder_Path.Text = CommonPath.OutputFolder;
		textBox1.Text = CommonPath.OutputFolder_Resource;

		cmb_ClipboardMode.SelectedIndex = ContentPanel.CopyMode;
		cmb_DataTestMode.SelectedIndex = (int)Settings.TestMode;
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
		Folder.SelectedPath = GRoot_Path.Text;
		if (Folder.ShowDialog() == DialogResult.OK)
		{
			GRoot_Path.Text = Folder.SelectedPath;
			Folder.Description = "";
		}
	}

	private void GRoot_Path_TextChanged(object sender, EventArgs e)
	{
		CommonPath.GameFolder = GRoot_Path.Text;

		var Locale = new Locale(new DirectoryInfo(GRoot_Path.Text));
		if (Locale._language != null)
		{
			lbl_Region.Visible = true;
			lbl_Region.Text = new ComponentResourceManager(typeof(SettingsForm)).GetString("lbl_Region.Text") + 
				(Locale.Language == Language.None ? Locale._language : Locale.Language.GetDescription());
		}
	}

	private void Faster_Folder_Path_TextChanged(object sender, EventArgs e)
	{
		CommonPath.OutputFolder = Faster_Folder_Path.Text;
	}
	#endregion

	#region Options
	private void cmb_ClipboardMode_SelectedChangedEvent(object sender, EventArgs e) => ContentPanel.CopyMode = cmb_ClipboardMode.SelectedIndex;

	private void cmb_DataTestMode_SelectedChangedEvent(object sender, EventArgs e) => Settings.TestMode = (DumpMode)cmb_DataTestMode.SelectedIndex;
	#endregion




	private void ucBtnFillet3_Click(object sender, EventArgs e)
	{
		if (Folder.ShowDialog() == DialogResult.OK)
			textBox1.Text = Path.GetFullPath(Folder.SelectedPath);
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
		CommonPath.OutputFolder_Resource = textBox1.Text;
	}
}