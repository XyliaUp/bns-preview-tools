using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using HZH_Controls.Forms;

using Xylia.Configure;
using Xylia.Match.Util.Paks;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell;
using Xylia.Preview.Properties;

namespace Xylia.Match.Windows.Panel
{
	[DesignTimeVisible(false)]
	public partial class IconPage : UserControl
	{
		#region Constructor
		public IconPage()
		{
			this.DoubleBuffered = true;
			CheckForIllegalCrossThreadCalls = false;

			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
			SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

			IsInitialization = true;
			InitializeComponent();

			#region Initialize道具Property选择框
			ComboBox1.Source = new();
			ComboBox2.Source = new();
			ComboBox3.Source = new();

			foreach (var Item in CombineOption.Grades) ComboBox1.Source.Add(Item.Name);
			foreach (var Item in CombineOption.BLImage) ComboBox2.Source.Add(Item.Name);
			foreach (var Item in CombineOption.TRImage) ComboBox3.Source.Add(Item.Name);

			ImageCompose_Reset_Click(null, null);
			#endregion


			this.tabControl1.SelectedIndex = 0;
		}

		private void IconOperator_Load(object sender, EventArgs e)
		{
			//Initialize文本框路径
			//this.ReadConfig(this.GetType().Name);

			Path_ResultPath.Text = Ini.ReadValue(this.GetType(), "OutFolder");
			Path_GameFolder.Text = CommonPath.GameFolder;
			TextBox1.Text = Ini.ReadValue(this.GetType(), "CacheList");


			#region Load 输出格式设置
			this.FormatSelect.Source = new();
			this.FormatSelect.Source.Add("[id]");
			this.FormatSelect.Source.Add("[id]_[name]");
			this.FormatSelect.Source.Add("[id]_[alias]");


			string FormatSel = Ini.ReadValue(this.GetType(), "FormatSel");

			if (string.IsNullOrWhiteSpace(FormatSel) || !FormatSel.Contains('[')) FormatSelect.TextValue = FormatSelect.Source?.First().ToString();
			else FormatSelect.TextValue = FormatSel;
			#endregion

			if (bool.TryParse(Ini.ReadValue(this.GetType(), "Mode"), out bool Result)) Switch_Mode.Checked = Result;
			if (bool.TryParse(Ini.ReadValue(this.GetType(), "HasBG"), out Result)) checkBox1.Checked = Result;
		}
		#endregion



		#region 输出道具图标
		private void FormatSelect_MouseEnter(object sender, EventArgs e)
		{
			string Msg = "*可自定义输出格式  特殊规则为 [id]、[name/名称]、[alias/别名]\n（建议使用英文, 英文不区分大小写)";
			frmAnchor = FrmAnchorTips.ShowTips(FormatSelect, Msg, AnchorTipsLocation.BOTTOM, Color.MediumOrchid, Color.FloralWhite, null, 12, 0, false);
		}

		private void FormatSelect_TextChanged(object sender, EventArgs e) => Ini.WriteValue(this.GetType(), "FormatSel", FormatSelect.TextValue);

		private void Path_ResultPath_TextChanged(object sender, EventArgs e) => Ini.WriteValue(this.GetType(), "OutFolder", Path_ResultPath.Text);

		private void TextBox1_TextChanged(object sender, EventArgs e) => Ini.WriteValue(this.GetType(), "CacheList", TextBox1.Text);

		private void Path_GameFolder_TextChanged(object sender, EventArgs e) => CommonPath.GameFolder = ((Control)sender).Text;




		private void Btn_Search_1_Click(object sender, EventArgs e)
		{
			if (Folder.ShowDialog() == DialogResult.OK) Path_GameFolder.Text = Folder.SelectedPath;
		}

		private void Btn_Search_2_Click(object sender, EventArgs e)
		{
			if (Folder.ShowDialog() == DialogResult.OK) Path_ResultPath.Text = Folder.SelectedPath;
		}

		private void Btn_Search_3_Click(object sender, EventArgs e)
		{
			Open.Filter = "数据配置文件|*.chv|所有文件|*.*";
			if (int.TryParse(Ini.ReadValue(this.GetType(), "CacheListFilter"), out int Result)) Open.FilterIndex = Result;

			if (Open.ShowDialog() == DialogResult.OK)
			{
				TextBox1.Text = Open.FileName;
				Ini.WriteValue(this.GetType(), "CacheListFilter", Open.FilterIndex);
			}
		}


		FrmAnchorTips frmAnchor;

		private void Switch_HasBG_MouseEnter(object sender, EventArgs e)
		{
			string Msg = checkBox1.Checked ? "生成道具图标时\n会附带道具品级" : "只生成出道具的\n透明背景图标";
			frmAnchor = FrmAnchorTips.ShowTips(checkBox1, Msg, AnchorTipsLocation.BOTTOM, Color.MediumOrchid, Color.FloralWhite, null, 12, 3500);
		}

		private void Switch_Mode_MouseEnter(object sender, EventArgs e)
		{
			string Msg = Switch_Mode.Checked ? "跳过配置文件中\n所记录的道具" : "导出配置文件中\n所记录的道具";
			frmAnchor = FrmAnchorTips.ShowTips(Switch_Mode, Msg, AnchorTipsLocation.BOTTOM, Color.MediumOrchid, Color.FloralWhite, null, 12, 3500);
		}

		private void Button1_MouseEnter(object sender, EventArgs e)
		{
			string Msg = "按照已生成的图标生成配置列表, 便于版本更新\n\n注意：如果修改了输出文件夹的名字（即\"生成\"文件夹）\n\n输出栏 直接选择新文件夹, 点击按钮即可";
			frmAnchor = FrmAnchorTips.ShowTips(Button1, Msg, AnchorTipsLocation.BOTTOM, Color.MediumOrchid, Color.FloralWhite, null, 12, 3500);
		}


		private void Switch_HasBG_MouseLeave(object sender, EventArgs e)
		{
			if (frmAnchor == null) return;

			try { frmAnchor.Hide(); }
			catch { }
		}


		private void Switch_Mode_CheckedChanged(object sender, EventArgs e)
		{
			if (IsInitialization) return;

			Ini.WriteValue(this.GetType(), "Mode", Switch_Mode.Checked);
			//FrmAnchorTips.CloseLastTip();
			Switch_Mode_MouseEnter(null, null);
		}


		private void Button1_Click(object sender, EventArgs e)
		{
			SaveFileDialog.FileName = "配置文件";
			SaveFileDialog.Filter = "Xylia Value 配置文件|*.chv";

			if (SaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Thread thread = new((ThreadStart)delegate
				{
					int Count = 1;

					using (StreamWriter sw = new(SaveFileDialog.FileName))
					{
						string FolderPath = this.Path_ResultPath.Text + @"\物品";
						if (!Directory.Exists(FolderPath))
						{
							FolderPath = Path_ResultPath.Text;
							this.Invoke(new Action(() => FrmTips.ShowTips("由于不存在目标子文件夹, 已变更为扫描所选的<输出目录>")));
						}

						var Files = new DirectoryInfo(FolderPath).GetFiles();
						foreach (FileInfo fileInfo in Files)
						{
							this.Invoke(new Action(() => Footer.Text = $"正在生成配置文件  {100 * Count++ / Files.Length}%"));

							if (fileInfo.Name.Contains('_'))
							{
								string[] Temp = fileInfo.Name.Split('_');

								foreach (var T in Temp) if (int.TryParse(T.Replace(".png", null), out int Result)) sw.WriteLine(Result);
							}
							else if (int.TryParse(fileInfo.Name.Replace(".png", null), out int ItemID))
							{
								sw.WriteLine(ItemID);
							}
						}
					}

					this.Invoke(new Action(() =>
					{
						Footer.Text = $"输出配置文件已完成";
						FrmTips.ShowTips("输出配置文件已完成！");
					}));
				});

				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (IsInitialization) return;

			Ini.WriteValue(this.GetType(), "HasBG", checkBox1.Checked);

			//FrmAnchorTips.CloseLastTip();
			Switch_HasBG_MouseEnter(null, null);
		}



		private Thread Thread_ItemIcon;

		private void Button2_Click(object sender, EventArgs e)
		{
			// 结束任务 
			if (Thread_ItemIcon != null)
			{
				var result = MessageBox.Show("是否确认强制结束？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
				if (result != DialogResult.OK) return;

				Thread_ItemIcon.Interrupt();
				Thread_ItemIcon = null;

				Footer.Text = $"正在结束任务...";
				return;
			}



			#region Initialize
			string ChvPath = TextBox1.Text;
			if (!string.IsNullOrWhiteSpace(ChvPath) && !File.Exists(ChvPath))
			{
				Xylia.Tip.Message("Chv文件路径错误或不存在, 请重新确认！");
				return;
			}

			if (!this.Switch_Mode.Checked && !File.Exists(TextBox1.Text))
			{
				Xylia.Tip.Message("选择白名单模式时, 必须选择配置文件!");
				return;
			}
			#endregion

			#region 执行
			var IconTextureMatch = new IconTextureMatch()
			{
				FormatSelect = this.FormatSelect.TextValue,
				CheckFormat = true,

				Start = (r, t) =>
				{
					Button2.Text = "终止";
					this.FormatSelect.Enabled = this.checkBox1.Enabled = this.Switch_Mode.Enabled = this.pictureBox1.Enabled = false;
				},

				Finish = (r, t) =>
				{
					//委托要求关闭线程
					this.Invoke(() => Thread_ItemIcon = null);

					this.FormatSelect.Enabled = this.checkBox1.Enabled = this.Switch_Mode.Enabled = this.pictureBox1.Enabled = true;
					Button2.Text = "输出物品图标";
				},
			};
			IconTextureMatch.StartMatch(new Util.Paks.Textures.ItemIcon(Path_GameFolder.Text)
			{
				OutputDirectory = this.Path_ResultPath.Text + @"\物品",
				ChvPath = TextBox1.Text,
				UseBackground = this.checkBox1.Checked,
				isWhiteList = !Switch_Mode.Checked,
			}, ref Thread_ItemIcon, act => this.Invoke(() => Footer.Text = act));
			#endregion
		}




		private Thread Thread_GoodIcon;

		private void Button9_Click(object sender, EventArgs e)
		{
			var IconTextureMatch = new IconTextureMatch()
			{
				FormatSelect = null,
				CheckFormat = false,

				Start = (r, t) =>
				{
					Button9.Text = "终止";
				},

				Finish = (r, t) =>
				{
					Button9.Text = "输出商品图标";
				},
			};


			IconTextureMatch.StartMatch(new Util.Paks.Textures.GoodIcon(Path_GameFolder.Text)
			{
				OutputDirectory = this.Path_ResultPath.Text + @"\商品",

			}, ref Thread_GoodIcon,

			act => this.Invoke(new Action(() => Footer.Text = act)));
		}
		#endregion




		#region 合成图标
		private ItemImageCompose ImageCompose;

		bool IsInitialization = false;

		private string IconPath;



		private void ImageCompose_Reset_Click(object sender, EventArgs e)
		{
			if (ComboBox1.Source.Count > 7) ComboBox1.TextValue = ComboBox1.Source[7];
			if (ComboBox2.Source.Count != 0) ComboBox2.TextValue = ComboBox2.Source[0];
			if (ComboBox3.Source.Count != 0) ComboBox3.TextValue = ComboBox3.Source[0];
			IsInitialization = false;


			this.ImageCompose = new();
			this.ImageCompose.RefreshHandle += new(() => pictureBox1.Image = ImageCompose.DrawICON(Radio_64px.Checked ? null : 2));

			this.ComboBox1_TextChanged(sender, e);
		}

		private byte ImageCompose_GetGrade() => CombineOption.Grades.Find(o => o.Name == ComboBox1.TextValue)?.ItemGrade ?? 0;


		private void ComboBox1_TextChanged(object sender, EventArgs e)
		{
			if (IsInitialization) return;

			this.ImageCompose.GradeImage = ImageCompose_GetGrade().GetBackGround(ucCheckBox1.Checked);
			this.ImageCompose.Refresh();
		}

		private void ComboBox2_TextChanged(object sender, EventArgs e)
		{
			if (IsInitialization) return;

			this.ImageCompose.BottomLeft = CombineOption.BLImage.Find(o => o.Name == ComboBox2.TextValue);
			this.ImageCompose.Refresh();
		}

		private void ComboBox3_TextChanged(object sender, EventArgs e)
		{
			if (IsInitialization) return;

			this.ImageCompose.TopRight = CombineOption.TRImage.Find(o => o.Name == ComboBox3.TextValue);
			this.ImageCompose.Refresh();
		}

		private void Button4_Click(object sender, EventArgs e)
		{
			//获取道具名称
			string ItemName = string.IsNullOrEmpty(this.IconPath) ? "道具名称" : this.IconPath + "_" + ImageCompose_GetGrade();


			SaveFileDialog.FileName = ItemName;
			SaveFileDialog.Filter = "PNG格式|*.png|GIF格式|*.gif|JPEG格式|*.jpg|位图格式|*.bmp|ICO格式|*.ico";

			if (SaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				ImageFormat Format = ImageFormat.Png;

				switch (SaveFileDialog.DefaultExt)
				{
					case ".png": Format = ImageFormat.Png; break;
					case ".gif": Format = ImageFormat.Gif; break;
					case ".jpg": Format = ImageFormat.Jpeg; break;
					case ".bmp": Format = ImageFormat.Bmp; break;
					case ".ico": Format = ImageFormat.Icon; break;
				}

				pictureBox1.Image.Save(SaveFileDialog.FileName, Format);
			}
		}


		private void IconOperator_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
				e.Effect = DragDropEffects.All;
		}

		private void IconOperator_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length == 0) return;

			this.IconPath = Path.GetFileNameWithoutExtension(files[0]);


			Stream stream = new FileStream(files[0], FileMode.Open);
			var bitmap = (Bitmap)Image.FromStream(stream);

			//配置默认另存选项
			if (bitmap.Width > 64) Radio_128px.Checked = true;
			else Radio_64px.Checked = true;


			this.ImageCompose.Icon = bitmap;
			this.ImageCompose.Refresh();
		}
		#endregion

		#region 合成八卦牌 
		private void GemPage_DragDrop(object sender, DragEventArgs e)
		{
			var Files = ((string[])e.Data.GetData(DataFormats.FileDrop)).ToList();
			if (Files.Count >= 2)
			{
				if (GetBitmap(Files, "pos1", out Bitmap temp)) GemCircle.Meta1 = temp;
				if (GetBitmap(Files, "pos2", out temp)) GemCircle.Meta2 = temp;
				if (GetBitmap(Files, "pos3", out temp)) GemCircle.Meta3 = temp;
				if (GetBitmap(Files, "pos4", out temp)) GemCircle.Meta4 = temp;
				if (GetBitmap(Files, "pos5", out temp)) GemCircle.Meta5 = temp;
				if (GetBitmap(Files, "pos6", out temp)) GemCircle.Meta6 = temp;
				if (GetBitmap(Files, "pos7", out temp)) GemCircle.Meta7 = temp;
				if (GetBitmap(Files, "pos8", out temp)) GemCircle.Meta8 = temp;
			}
			else if (Files.Count != 0)
			{
				var bitmap = (Bitmap)Image.FromFile(Files.First());

				switch (GemCircle.PartSel)
				{
					case GemCircle.PartSection.Part1: GemCircle.Meta1 = bitmap; break;
					case GemCircle.PartSection.Part2: GemCircle.Meta2 = bitmap; break;
					case GemCircle.PartSection.Part3: GemCircle.Meta3 = bitmap; break;
					case GemCircle.PartSection.Part4: GemCircle.Meta4 = bitmap; break;
					case GemCircle.PartSection.Part5: GemCircle.Meta5 = bitmap; break;
					case GemCircle.PartSection.Part6: GemCircle.Meta6 = bitmap; break;
					case GemCircle.PartSection.Part7: GemCircle.Meta7 = bitmap; break;
					case GemCircle.PartSection.Part8: GemCircle.Meta8 = bitmap; break;

					default:
					{
						Xylia.Tip.Message("请先通过鼠标选择一个八卦牌部位");
						Console.WriteLine("暂不支持类型：" + GemCircle.PartSel);
					}
					break;
				}
			}
		}

		private void GemPage_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
				e.Effect = DragDropEffects.All;
		}

		private void GemPage_Click(object sender, EventArgs e)
		{
			this.GemCircle.PartSel = GemCircle.PartSection.Init;
		}



		private static bool GetBitmap(List<string> Files, string Key, out Bitmap Bitmap)
		{
			var fs = Files.Where(f => Path.GetFileNameWithoutExtension(f).ToLower().Contains(Key.ToLower()));
			if (fs.Any())
			{
				Bitmap = null; // SetImage.Load(fs.First());
				return true;
			}

			Bitmap = null;
			return false;
		}




		private void Button6_Click(object sender, EventArgs e)
		{
			SaveFileDialog.Filter = "PNG格式|*.png|GIF格式|*.gif|JPEG格式|*.jpg|位图格式|*.bmp|ICO格式|*.ico";
			SaveFileDialog.FileName = "ItemSet_Compose";


			if (SaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				ImageFormat Format = ImageFormat.Png;

				switch (SaveFileDialog.DefaultExt)
				{
					case ".png": Format = ImageFormat.Png; break;
					case ".gif": Format = ImageFormat.Gif; break;
					case ".jpg": Format = ImageFormat.Jpeg; break;
					case ".bmp": Format = ImageFormat.Bmp; break;
					case ".ico": Format = ImageFormat.Icon; break;
				}

				GemCircle.Image.Save(SaveFileDialog.FileName, Format);
			}
		}

		private void Button7_Click(object sender, EventArgs e)
		{
			this.GemCircle.Clear();
			this.GemCircle.PartSel = GemCircle.PartSection.Part1;
		}

		private void GemCircle_SelectPartChanged(object sender, EventArgs e)
		{
			string PartName = GemCircle.PartConvert.ContainsKey(this.GemCircle.PartSel) ? GemCircle.PartConvert[this.GemCircle.PartSel] : this.GemCircle.PartSel.ToString();

			Label6.Text = $"需要更改部位时, 请点击对应的区域\n\n当前选择：{PartName}";
		}

		private void ucSwitch1_CheckedChanged(object sender, EventArgs e)
		{
			this.GemCircle.Transparent = !this.GemCircle.Transparent;
		}

		private void Radio_64px_CheckedChangeEvent(object sender, EventArgs e)
		{
			this.pictureBox1.Size = new Size(
				Radio_64px.Checked ? 64 : 128,
				Radio_64px.Checked ? 64 : 128);

			this.ImageCompose.Refresh();
		}
		#endregion
	}
}