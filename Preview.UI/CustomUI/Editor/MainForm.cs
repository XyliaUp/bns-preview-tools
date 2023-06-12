using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

using Xylia.Windows.Forms.Editor;

namespace Crypto_Notepad
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			richOut.DragDrop += new DragEventHandler(CustomRTB_DragDrop);
			richOut.AllowDrop = true;
			DefaultName = this.Text + " -";

			ps = Settings.Default;
		}


		Settings ps;

		string filePath, argsPath = "";

		int caretPos = 0;
		bool shiftPresed;
		bool noExit = false;


		public string DefaultName, FileName;

		public string Content
		{
			set => this.richOut.Text = value;
			get => this.richOut.Text;
		}


		/*Functions*/
		private void DecryptAES()
		{

			if (!PublicVar.okPressed)
			{
				PublicVar.openFileName = Path.GetFileName(filePath);
				return;
			}
			if (SearchPanel.Visible)
			{
				FindToolStripMenuItem_Click(this, new EventArgs());
			}
			try
			{
				string opnfile = File.ReadAllText(OpenFile.FileName);
				string NameWithotPath = Path.GetFileName(OpenFile.FileName);
				string de;

				if (ps.TheSalt != null)
				{
					de = AES.Decrypt(opnfile, TypedPassword.Value, ps.TheSalt, ps.HashAlgorithm, ps.PasswordIterations, ps.KeySize);
				}
				else
				{
					de = AES.Decrypt(opnfile, TypedPassword.Value, null, ps.HashAlgorithm, ps.PasswordIterations, ps.KeySize);
				}

				richOut.Text = de;

				Text = PublicVar.appName + " – " + NameWithotPath;
				filePath = OpenFile.FileName;
				string cc2 = richOut.Text.Length.ToString(CultureInfo.InvariantCulture);
				richOut.Select(Convert.ToInt32(cc2), 0);
				PublicVar.openFileName = Path.GetFileName(OpenFile.FileName);
				PublicVar.encryptionKey.Set(TypedPassword.Value);
				TypedPassword.Value = null;
			}
			catch (CryptographicException)
			{
				using (new CenterWinDialog(this))
				{
					TypedPassword.Value = null;
					DialogResult dialogResult = MessageBox.Show("Invalid key!", PublicVar.appName, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
					if (dialogResult == DialogResult.Retry)
					{
						DecryptAES();
					}
					if (dialogResult == DialogResult.Cancel)
					{
						PublicVar.openFileName = Path.GetFileName(filePath);
						return;
					}
				}
			}
		}


		private void SendTo()
		{
			string fileExtension = Path.GetExtension(argsPath);
			string opnfile = File.ReadAllText(argsPath);
			string NameWithotPath = Path.GetFileName(argsPath);
			richOut.Text = opnfile;
			Text = PublicVar.appName + " – " + NameWithotPath;
			string cc2 = richOut.Text.Length.ToString(CultureInfo.InvariantCulture);
			richOut.Select(Convert.ToInt32(cc2), 0);
		}

		//private void SendToShortcut()
		//{
		//    string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\SendTo";
		//    string shortcutName = PublicVar.appName + ".lnk";
		//    string shortcutLocation = Path.Combine(shortcutPath, shortcutName);
		//    string LvFileLocation = Assembly.GetEntryAssembly().Location;

		//    WshShell shell = new WshShell();
		//    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
		//    shortcut.Description = PublicVar.appName;
		//    shortcut.IconLocation = LvFileLocation;
		//    shortcut.TargetPath = LvFileLocation;
		//    shortcut.Arguments = "/s";
		//    shortcut.Save();
		//}

		private void DeleteUpdateFiles()
		{
			string exePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\";
			string UpdaterExe = exePath + "Updater.exe";
			string UpdateZip = exePath + "Crypto-Notepad-Update.zip";
			string ZipDll = exePath + "Ionic.Zip.dll";

			if (File.Exists(UpdaterExe))
			{
				File.Delete(UpdaterExe);
			}

			if (File.Exists(UpdateZip))
			{
				File.Delete(UpdateZip);
			}

			if (File.Exists(ZipDll))
			{
				File.Delete(ZipDll);
			}
		}

		private void SaveConfirm(bool exit)
		{
			if (!richOut.Modified)
			{
				if (exit)
				{
					//Environment.Exit(0);

				}
			}
			else
			{
				if (PublicVar.openFileName == null)
				{
					PublicVar.openFileName = "Unnamed.cnp";
				}

				if (richOut.Text != "")
				{
					string messageBoxText = "";

					if (!PublicVar.keyChanged)
					{
						messageBoxText = "Save file: " + "\"" + PublicVar.openFileName + "\"" + " ? ";
					}
					else
					{
						messageBoxText = "Save file: " + "\"" + PublicVar.openFileName + "\"" + " with a new key? ";
					}

					using (new CenterWinDialog(this))
					{
						DialogResult res = MessageBox.Show(messageBoxText, PublicVar.appName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
						if (res == DialogResult.Yes)
						{
							SaveToolStripMenuItem_Click(this, new EventArgs());
							if (exit)
							{
								//Environment.Exit(0);
							}

						}

						if (res == DialogResult.No)
						{
							if (exit)
							{
								//Environment.Exit(0);
							}
						}

						if (res == DialogResult.Cancel)
						{
							noExit = true;
							return;
						}
					}
				}
			}
		}

		private void AutoLock(bool minimize)
		{

			if (!PublicVar.okPressed)
			{
				PublicVar.encryptionKey.Set(null);
				richOut.Clear();
				Text = PublicVar.appName;
				PublicVar.openFileName = null;
				filePath = "";
				Show();
				return;
			}
			PublicVar.okPressed = false;

			try
			{
				richOut.Clear();
				string opnfile = File.ReadAllText(filePath);
				string de = AES.Decrypt(opnfile, TypedPassword.Value, null, ps.HashAlgorithm, ps.PasswordIterations, ps.KeySize);
				richOut.Text = de;
				Text = PublicVar.appName + " – " + PublicVar.openFileName;
				string cc2 = richOut.Text.Length.ToString(CultureInfo.InvariantCulture);
				richOut.Select(Convert.ToInt32(cc2), 0);
				richOut.SelectionStart = caretPos;
				PublicVar.encryptionKey.Set(TypedPassword.Value);
				TypedPassword.Value = null;
				Show();
			}
			catch (Exception ex)
			{
				if (ex is CryptographicException)
				{
					TypedPassword.Value = null;
					DialogResult dialogResult = MessageBox.Show("Invalid key!", PublicVar.appName, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
					if (dialogResult == DialogResult.Retry)
					{
						AutoLock(false);
					}
					if (dialogResult == DialogResult.Cancel)
					{
						PublicVar.encryptionKey.Set(null);
						richOut.Clear();
						Text = PublicVar.appName;
						filePath = "";
						PublicVar.openFileName = null;
						Show();
						return;
					}
				}
			}
		}

		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const int SC_MINIMIZE = 0xF020;

			if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_MINIMIZE && ps.AutoLock && PublicVar.encryptionKey.Get() != null)
			{
				if (ps.AutoSave)
				{
					SaveToolStripMenuItem_Click(this, new EventArgs());
				}
				else
				{
					SaveConfirm(false);
				}
				AutoLock(true);
				return;
			}
			base.WndProc(ref m);
		}


		/*Form Events*/
		private void MainWindow_Activated(object sender, EventArgs e)
		{
			if (PublicVar.settingsChanged)
			{
				PublicVar.settingsChanged = false;
				richOut.Font = new Font(ps.RichTextFont, ps.RichTextSize);
				richOut.ForeColor = ps.RichForeColor;
				richOut.BackColor = ps.RichBackColor;
				BackColor = ps.RichBackColor;


				#region  workaround, unhighlight URLs fix
				richOut.DetectUrls = false;
				richOut.DetectUrls = true;
				#endregion

				if (!ps.ShowToolbar && ToolbarPanel.Visible)
				{
					ToolbarPanel.Visible = true;
					richOut.Height += 23;
					richOut.Location = new Point(0, 24);
				}
				if (ps.ShowToolbar && !ToolbarPanel.Visible)
				{
					ToolbarPanel.Visible = true;
					richOut.Height -= 23;
					richOut.Location = new Point(0, 47);
				}

				if (ps.ColoredToolbar)
				{
					ToolbarPanel.BackColor = ps.RichBackColor;
					ToolbarPanel.BorderStyle = BorderStyle.FixedSingle;
				}
				else
				{
					ToolbarPanel.BackColor = SystemColors.ButtonFace;
					ToolbarPanel.BorderStyle = BorderStyle.None;
				}

			}

			if (PublicVar.keyChanged)
			{
				richOut.Modified = true;
			}


		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{

			if (WindowState == FormWindowState.Normal)
			{
				ps.WindowSize = Size;
				ps.WindowLocation = Location;
				ps.WindowState = WindowState;
			}

			if (WindowState == FormWindowState.Maximized)
			{
				ps.WindowState = WindowState;
			}

			ps.Save();
			//SaveConfirm(true);

			this.Hide();
			if (noExit)
			{
				e.Cancel = true;
			}
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			this.Text = DefaultName + FileName;
			this.TopMost = true;

			try
			{
				richOut.Font = new Font(ps.RichTextFont, ps.RichTextSize);
				richOut.ForeColor = ps.RichForeColor;
				richOut.BackColor = ps.RichBackColor;
				//BackColor = ps.RichBackColor;
				WordWrapToolStripMenuItem.Checked = ps.MenuWrap;

				if (ps.ColoredToolbar)
				{
					ToolbarPanel.BackColor = ps.RichBackColor;
					ToolbarPanel.BorderStyle = BorderStyle.FixedSingle;
				}

				Size = ps.WindowSize;
				WindowState = ps.WindowState;
			}
			catch (Exception ee)
			{
				Debug.WriteLine(ee);
			}

			richOut.WordWrap = !WordWrapToolStripMenuItem.Checked;

			ToolbarPanel.Visible = true;



			this.TopMost = true;

			DeleteUpdateFiles();

		}
		/*Form Events*/


		/*CustomRTB Events*/
		private void CustomRTB_SelectionChanged(object sender, EventArgs e)
		{
			#region 获得行列号
			//获取行号
			int LineCursor = richOut.GetLineFromCharIndex(richOut.SelectionStart) + 1;
			int LineTotal = richOut.GetLineFromCharIndex(richOut.TextLength) + 1;

			//获取列号
			int ColumnCursor = richOut.SelectionStart - richOut.GetFirstCharIndexFromLine(LineCursor - 1) + 1;
			int ColumnTotal = 0;

			if (LineCursor == LineTotal)
				//末尾行用总索引减去上一行末尾索引
				ColumnTotal = richOut.TextLength + 1 - richOut.GetFirstCharIndexFromLine(LineCursor - 1);
			else
				//其他行用当前行末尾索引减去上一行末尾索引
				ColumnTotal = richOut.GetFirstCharIndexFromLine(LineCursor) - richOut.GetFirstCharIndexFromLine(LineCursor - 1);

			Column.Text = String.Format(@"列: {0}\{1}", ColumnCursor, ColumnTotal);
			lines.Text = String.Format(@"行: {0}\{1}", LineCursor, LineTotal);
			#endregion


			if (richOut.SelectionLength != 0)
			{
				CutToolbarButton.Enabled = true;
				CopyToolbarButton.Enabled = true;
			}
			else
			{
				CutToolbarButton.Enabled = false;
				CopyToolbarButton.Enabled = false;
			}
		}

		private void CustomRTB_Click(object sender, EventArgs e)
		{
			caretPos = richOut.SelectionStart;
		}

		private void CustomRTB_KeyDown(object sender, KeyEventArgs e)
		{
			caretPos = richOut.SelectionStart;
			if (e.KeyCode == Keys.ShiftKey)
			{
				shiftPresed = true;
			}
			else if (e.Modifiers == Keys.Control)
			{

				//if (e.KeyCode == Keys.Down)
				//    //this.Text = list[1].ToString();
				//    TrunRowsId(list[1]);
			}
		}

		private void TrunRowsId(int iCodeRowsID)
		{
			richOut.SelectionStart = richOut.GetFirstCharIndexFromLine(iCodeRowsID);
			richOut.Focus();
			richOut.ScrollToCaret();
		}

		private void CustomRTB_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			if (shiftPresed)
			{
				shiftPresed = false;
				Process.Start(e.LinkText);
			}
		}

		private void CustomRTB_DragDrop(object sender, DragEventArgs e)
		{
			SaveConfirm(false);

			string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			foreach (string file in FileList) OpenFile.FileName = file;
			object fname = e.Data.GetData("FileDrop");
			PublicVar.openFileName = Path.GetFileName(OpenFile.FileName);
			if (fname != null)
			{
				var list = fname as string[];
				if (list != null && !string.IsNullOrWhiteSpace(list[0]))
				{
					if (!OpenFile.FileName.Contains(".cnp"))
					{
						string opnfile = File.ReadAllText(OpenFile.FileName);
						string NameWithotPath = Path.GetFileName(OpenFile.FileName);
						richOut.Text = opnfile;
						Text = PublicVar.appName + " – " + NameWithotPath;
						string cc2 = richOut.Text.Length.ToString(CultureInfo.InvariantCulture);
						richOut.Select(Convert.ToInt32(cc2), 0);
						filePath = OpenFile.FileName;
						return;
					}
					DecryptAES();
					if (PublicVar.okPressed)
					{
						PublicVar.okPressed = false;
					}
				}
			}
		}

		private void CustomRTB_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ShiftKey)
			{
				shiftPresed = false;
			}
		}

		private void CustomRTB_KeyPress(object sender, KeyPressEventArgs e)
		{

		}

		/* Main Menu */

		/*File*/


		private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFile.FileName = "";
			SaveConfirm(false);

			if (OpenFile.ShowDialog() != DialogResult.OK) return;
			{
				PublicVar.openFileName = Path.GetFileName(OpenFile.FileName);
				string opnfile = File.ReadAllText(OpenFile.FileName);
				string NameWithotPath = Path.GetFileName(OpenFile.FileName);
				richOut.Text = opnfile;
				Text = PublicVar.appName + " – " + NameWithotPath;
				string cc2 = richOut.Text.Length.ToString(CultureInfo.InvariantCulture);
				richOut.Select(Convert.ToInt32(cc2), 0);
				return;
			}
		}

		public TreeView treeView = null;
		public Label Label = null;



		private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (SaveFile.FileName.Contains(".txt"))
			{
				SaveFile.FilterIndex = 1;
			}
			else if (SaveFile.FileName.Contains(".xml"))
			{
				SaveFile.FilterIndex = 2;
			}
			else
			{
				SaveFile.FilterIndex = 3;
			}

			if (SaveFile.ShowDialog() == DialogResult.OK)
			{
				try
				{
					using (StreamWriter Output = new StreamWriter(SaveFile.FileName))
					{
						Output.WriteLine(richOut.Text);
						Output.Close();
					}

					Xylia.Tip.Message("提示", "另存为完成！");
				}
				catch
				{
					Xylia.Tip.Warning("错误提示", "另存为失败！");
				}

			}
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}


		/*Edit*/
		private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richOut.Undo();
		}

		public void RedoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richOut.Redo();
		}

		private void CutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richOut.Cut();
		}

		private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richOut.Copy();
		}

		private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (richOut.Focused)
			{
				richOut.Paste(DataFormats.GetFormat(DataFormats.Text));
			}
			if (SearchTextBox.Focused)
			{
				SearchTextBox.Paste();
			}
		}

		private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richOut.SelectedText = "";
		}

		private void FindToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (SearchPanel.Visible)
			{
				SearchTextBox.Text = "";
				SearchPanel.Visible = false;
				richOut.Height += 27;
				richOut.Focus();
				richOut.DeselectAll();
				richOut.SelectionStart = caretPos;
			}
			else
			{
				SearchPanel.Visible = true;
				SearchTextBox.Focus();
				richOut.Height -= 27;
			}
		}

		private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (richOut.Focused)
			{
				richOut.SelectAll();
			}
			if (SearchTextBox.Focused)
			{
				SearchTextBox.SelectAll();
			}
		}

		private void WordWrapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!WordWrapToolStripMenuItem.Checked)
			{
				richOut.WordWrap = true;
			}
			else
			{
				richOut.WordWrap = false;
			}
			ps.MenuWrap = WordWrapToolStripMenuItem.Checked;
			ps.RichWrap = richOut.WordWrap;
			ps.Save();
		}

		private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richOut.Clear();
		}

		private void EditToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
		{
			if (richOut.SelectionLength != 0)
			{
				CutToolStripMenuItem.Enabled = true;
				CopyToolStripMenuItem.Enabled = true;
				DeleteToolStripMenuItem.Enabled = true;
			}
			else
			{
				CutToolStripMenuItem.Enabled = false;
				CopyToolStripMenuItem.Enabled = false;
				DeleteToolStripMenuItem.Enabled = false;
			}
		}
		/*Edit*/

		/*Tools*/




		private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Xylia.Windows.Forms.PublicSet sf = new Xylia.Windows.Forms.PublicSet();
			sf.ShowDialog();
		}
		/*Tools*/


		/* Main Menu */
		/* Editor Menu */
		private void UndoEditorMenuStrip_Click(object sender, EventArgs e)
		{
			UndoToolStripMenuItem_Click(this, new EventArgs());
		}

		private void RedoEditorMenuStrip_Click(object sender, EventArgs e)
		{
			RedoToolStripMenuItem_Click(this, new EventArgs());
		}

		private void CutEditorMenuStrip_Click(object sender, EventArgs e)
		{
			CutToolStripMenuItem_Click(this, new EventArgs());
		}

		private void CopyEditorMenuStrip_Click(object sender, EventArgs e)
		{
			CopyToolStripMenuItem_Click(this, new EventArgs());
		}

		private void PasteEditorMenuStrip_Click(object sender, EventArgs e)
		{
			PasteToolStripMenuItem_Click(this, new EventArgs());
		}

		private void DeleteEditorMenuStrip_Click(object sender, EventArgs e)
		{
			DeleteToolStripMenuItem_Click(this, new EventArgs());
		}

		private void SelectAllEditorMenuStrip_Click(object sender, EventArgs e)
		{
			SelectAllToolStripMenuItem_Click(this, new EventArgs());
		}

		private void RightToLeftEditorMenuStrip_Click(object sender, EventArgs e)
		{
			if (RightToLeftEditorMenuStrip.Checked)
			{
				if (!WordWrapToolStripMenuItem.Checked)
				{
					string rtbTxt = richOut.Text;
					richOut.Clear();
					richOut.RightToLeft = RightToLeft.Yes;
					Application.DoEvents();
					richOut.Text = rtbTxt;
				}
				else
				{
					richOut.RightToLeft = RightToLeft.Yes;
				}
			}
			else
			{
				richOut.RightToLeft = RightToLeft.No;
			}
		}

		private void ClearEditorMenuStrip_Click(object sender, EventArgs e)
		{
			ClearToolStripMenuItem_Click(this, new EventArgs());
		}

		private void EditorMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			if (richOut.SelectionLength != 0)
			{
				CutEditorMenuStrip.Enabled = true;
				CopyEditorMenuStrip.Enabled = true;
				DeleteEditorMenuStrip.Enabled = true;
			}
			else
			{
				CutEditorMenuStrip.Enabled = false;
				CopyEditorMenuStrip.Enabled = false;
				DeleteEditorMenuStrip.Enabled = false;
			}
		}
		/* Editor Menu */


		/*Toolbar*/


		private void OpenToolbarButton_Click(object sender, EventArgs e)
		{
			OpenToolStripMenuItem_Click(this, new EventArgs());
		}

		private void SaveToolbarButton_Click(object sender, EventArgs e)
		{
			SaveToolStripMenuItem_Click(this, new EventArgs());
		}



		private void CutToolbarButton_Click(object sender, EventArgs e)
		{
			CutToolStripMenuItem_Click(this, new EventArgs());
		}

		private void CopyToolbarButton_Click(object sender, EventArgs e)
		{
			CopyToolStripMenuItem_Click(this, new EventArgs());
		}

		private void PasteToolbarButton_Click(object sender, EventArgs e)
		{
			PasteToolStripMenuItem_Click(this, new EventArgs());
		}


		private void SettingsToolbarButton_Click(object sender, EventArgs e)
		{
			this.TopMost = false;
			SettingsToolStripMenuItem_Click(this, new EventArgs());
		}



		private void CloseToolbar_Click(object sender, EventArgs e)
		{
			ToolbarPanel.Visible = false;
			richOut.Height += 23;
			richOut.Location = new Point(0, 24);

			ToolBar.Checked = ps.ShowToolbar = false;

			ps.Save();
		}

		private void CloseToolbar_MouseEnter(object sender, EventArgs e)
		{
			//CloseToolbar.Image = 剑灵引导程序.Properties.Resources.close_b;
		}

		private void CloseToolbar_MouseLeave(object sender, EventArgs e)
		{
			//CloseToolbar.Image = 剑灵引导程序.Properties.Resources.close_g;
		}
		/*Toolbar*/



		List<int> list = new List<int>();

		/*Search Panel*/
		private void SearchTextBox_TextChanged(object sender, EventArgs e)
		{
			DateTime Orginial = DateTime.Now;
			var Wait = new System.Windows.Forms.Timer();
			Wait.Interval = 100;
			Wait.Tick += delegate
			{
				if ((DateTime.Now - Orginial).Seconds >= 2)
				{
					Wait.Stop();

					list = richOut.Highlight(SearchTextBox.Text, ps.HighlightsColor, chkMatchCase.Checked, chkMatchWholeWord.Checked);
				}
			};
			Wait.Start();
		}

		private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
		{

			if (e.KeyCode == Keys.Escape)
			{
				SearchTextBox.Text = "";
				SearchPanel.Visible = false;
				richOut.Height += 27;


				richOut.Focus();

				richOut.DeselectAll();
				richOut.SelectionStart = caretPos;
				e.Handled = e.SuppressKeyPress = true;
			}
		}

		private void CloseSearchPanel_Click(object sender, EventArgs e)
		{
			FindToolStripMenuItem_Click(this, new EventArgs());
		}

		private void CloseSearchPanel_MouseHover(object sender, EventArgs e)
		{
			// CloseSearchPanel.Image = 剑灵引导程序.Properties.Resources.close_b;
		}

		private void CloseSearchPanel_MouseLeave(object sender, EventArgs e)
		{
			//CloseSearchPanel.Image = 剑灵引导程序.Properties.Resources.close_g;
		}

		private void ChkMatchCase_CheckedChanged(object sender, EventArgs e)
		{
			/*bool isexist =*/
			richOut.Highlight(SearchTextBox.Text, ps.HighlightsColor, chkMatchCase.Checked, chkMatchWholeWord.Checked);
		}

		private void SearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{

		}


		private void RichOut_TextChanged(object sender, EventArgs e)
		{

		}

		private void RichOut_VScroll(object sender, EventArgs e)
		{

		}

		private void ChkMatchWholeWord_CheckedChanged(object sender, EventArgs e)
		{
			/*bool isexist = */
			richOut.Highlight(SearchTextBox.Text, ps.HighlightsColor, chkMatchCase.Checked, chkMatchWholeWord.Checked);
		}
		/*Search Panel*/




		private void MainForm_Shown(object sender, EventArgs e)
		{
			this.TopMost = false;
		}

		private void ToolBaToolBar_Click(object sender, EventArgs e)
		{
			if (ToolBar.Checked)
			{
				ToolbarPanel.Visible = true;
				richOut.Height -= 23;
				richOut.Location = new Point(0, 49);
			}
			else
			{
				ToolbarPanel.Visible = false;
				richOut.Height += 23;
				richOut.Location = new Point(0, 24);
			}
		}

		/*Debug Menu*/



		#region 保存Event
		//定义委托
		public delegate void SaveHandle(object sender, EventArgs e);
		//定义Event
		public event SaveHandle SaveContent;

		private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (SaveContent != null) SaveContent(sender, new EventArgs());//把按钮自身作为参数传递
		}
		#endregion




	}
}