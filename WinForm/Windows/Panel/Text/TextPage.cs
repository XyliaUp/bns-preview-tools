using System.ComponentModel;
using System.IO;

using HZH_Controls.Forms;

using Xylia.Configure;
using Xylia.Extension;
using Xylia.Match.Util.ItemList;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;
using Xylia.Preview.Data.Models.BinData.Table;
using Xylia.Preview.Data.Models.DatData.DataProvider;
using Xylia.Preview.Data.Models.DatData.DatDetect;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Properties;

namespace Xylia.Match.Windows.Panel.TextInfo;

[DesignTimeVisible(false)]
public partial class TextPage : UserControl
{
	public TextPage()
	{
		InitializeComponent();

		this.BackColor = Color.Transparent;

		this.Path_Local1.Text = Ini.ReadValue(this.GetType(), nameof(Path_Local1));
		this.Path_Local2.Text = Ini.ReadValue(this.GetType(), nameof(Path_Local2));
		this.SaveAsBin.Checked = Ini.ReadValue(this.GetType(), nameof(SaveAsBin)).ToBool();
		this.TextBox2.Text = Ini.ReadValue(this.GetType(), nameof(TextBox2));


		this.ucCheckBox1.Checked = false;

		string OutPath = Ini.ReadValue(this.GetType(), nameof(TextBox1));
		this.filePath.Text = this.Local_SourcePath;
		if (!string.IsNullOrWhiteSpace(OutPath) && File.Exists(OutPath)) TextBox1.Text = OutPath;
		else TextBox1.Text = Path.GetDirectoryName(filePath.Text) + @"\汉化数据.xlsx";
	}


	#region browser
	private void Button2_Click(object sender, EventArgs e) => OpenLocal(Path_Local1);
	private void Button1_Click(object sender, EventArgs e) => OpenLocal(Path_Local2);

	private void pictureBox1_Click(object sender, EventArgs e) => (Path_Local2.Text, Path_Local1.Text) = (Path_Local1.Text, Path_Local2.Text);

	private void DataPath_TextChanged(object sender, EventArgs e)
	{
		string path = ((Control)sender).Text;
		string ext = null;

		if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
		{
			ext = Path.GetExtension(path);
			Ini.WriteValue(this.GetType(), ((Control)sender).Name, path);
		}

		//显示输出按钮
		(sender == Path_Local1 ? ucBtnFillet2 : ucBtnFillet3).Visible = ext == ".dat" || ext == ".bin";
	}

	private void OpenLocal(TextBox Text)
	{
		Open.Filter = @"game text file|local*.dat|output text file|TextData*.xml|All files|*.*";
		Open.RestoreDirectory = false;

		if (Open.ShowDialog() == DialogResult.OK)
		{
			Text.Text = Open.FileName;
		}
	}
	#endregion

	#region output
	private void Btn_OutLocal_1_Click(object sender, EventArgs e) => OutLocal(Path_Local1.Text);
	private void Btn_OutLocal_2_Click(object sender, EventArgs e) => OutLocal(Path_Local2.Text);

	public void OutLocal(string Source)
	{
		Save.FileName = "TextData";
		Save.Filter = "txt file|*.txt|xml file|*.xml";
		if (Save.ShowDialog() != DialogResult.OK) return;

		Step1.StepIndex = 2;
		Extract(Source, Save.FileName);
		Step1.StepIndex = 3;
	}

	public static void Extract(string Source, string OutPath, bool OnlyKorean = false)
	{
		new Thread(act =>
		{
			using var set = new LocalDataTableSet(Source);
			var table = set.Text;
			table.TryLoad();

			switch (Path.GetExtension(OutPath))
			{
				case ".xml":
					table.ProcessTable(Path.GetDirectoryName(OutPath));
					break;

				case ".xlsx":
					new OutSet<Text>() { Filter = (record) => OnlyKorean && record.text.RegexMatch("[\uAC00-\uD7A3]+") }
						.Output(table, OutPath);
					break;
			}

			GC.Collect();
		}).Start();
	}
	#endregion

	#region compare
	Thread CompareThread { get; set; }

	private void Btn_End_BtnClick(object sender, EventArgs e)
	{
		if (CompareThread == null) CompareExec();
		else if (FrmDialog.ShowDialog(this, "是否确认终止操作？", blnShowCancel: true) == DialogResult.OK) CompareEnd();
	}


	public void CompareEnd()
	{
		try
		{
			this.Invoke(() =>
			{
				CompareThread.Interrupt();
				CompareThread = null;

				FrmTips.ShowTips("操作已强制终止!");
				ucBtnFillet1.Enabled = ucBtnFillet4.Enabled = pictureBox1.Enabled = true;
				Btn_StartWithEnd.Text = "开始";
			});
		}
		catch
		{

		}
	}

	public void CompareExec()
	{
		if (!Directory.Exists(CommonPath.OutputFolder))
		{
			FrmTips.ShowTipsWarning("请先设置导出文件夹路径！");
			return;
		}

		if (!File.Exists(Path_Local1.Text))
		{
			FrmTips.ShowTipsError("旧版本的汉化文件不存在！");
			return;
		}

		if (!File.Exists(Path_Local2.Text))
		{
			FrmTips.ShowTipsError("新版本的汉化文件不存在！");
			return;
		}


		Step1.StepIndex = 1;
		ucBtnFillet1.Enabled = ucBtnFillet4.Enabled = pictureBox1.Enabled = false;
		Btn_StartWithEnd.Text = "终止";

		CompareThread = new Thread(() =>
		{
			string OutPath = Path.Combine(ItemMatch.OUTDIR, $@"text\{ItemMatch.OUTTIME()}.html");
			Directory.CreateDirectory(Path.GetDirectoryName(OutPath));

			var sw = OutPath.CreateWriter();

			try
			{
				var Text1 = GetText(Path_Local1.Text);
				var Text2 = GetText(Path_Local2.Text);

				Step1.StepIndex = 2;


				#region Compare
				bool HasChanged = false;
				foreach (var item in Text2.Values)
				{
					var other = Text1[item.alias];
					if (other is null)
					{
						HasChanged = true;
						sw.Write(HtmlSupport.HtmlConvert($"""
						<div class="Content">
							<div class="Content_Title">
								<span class="Sign">[新增] 特征值：</span>
								<span class="Text">{item.alias}</span>
							</div>
							<div class="Content_New" >
								<span class="Sign">文本：</span>
								<span class="Text">{HtmlSupport.ToHTML(item.text, out _)}</span>
							</div>
							<div class="Separator">{NewLine}</div>
						</div>
						"""));
					}
					else if (item.text != other.text)
					{
						HasChanged = true;

						string Txt1 = HtmlSupport.ToHTML(other.text, out bool IsMultiLine1);
						string Txt2 = HtmlSupport.ToHTML(item.text, out bool IsMultiLine2);

						sw.Write(HtmlSupport.HtmlConvert($"""
						<div class="Content">
							<div class="Content_Title">
								<span class="Sign">[修改] 特征值：</span>
								<span class="Text">{item.alias}</span>
							</div>
							<div class="Content_Old" >
								<span class="Sign">旧版本：</span>
								<span class="Text">{Txt1}</span>
							</div>
								  {(IsMultiLine1 || IsMultiLine2 ? "<br/>" : null) /* 当文本信息大于单行时进行换行显示 */ }
							<div class="Content_New" >
								<span class="Sign">新版本：</span>
								<span class="Text">{Txt2}</span>
							</div>
							<div class="Separator">{NewLine}</div>
						</div>
						"""));
					}
				}

				foreach (var item in Text1.Values)
				{
					var other = Text2[item.alias];
					if (other is null)
					{
						HasChanged = true;
						sw.Write(HtmlSupport.HtmlConvert($"""
						<div class="Content">
							<div class="Content_Title">
								<span class="Sign">[删除] 特征值：</span>
								<span class="Text">{item.alias}</span>
							</div>
							<div class="Content_Old" >
								<span class="Sign">文本：</span>
								<span class="Text">{HtmlSupport.ToHTML(item.text, out _)}</span>
							</div>
							<div class="Separator">{NewLine}</div>
						</div>
						"""));
					}
				}
				#endregion

				#region End
				Step1.StepIndex = 3;
				Text1.Clear();
				Text2.Clear();

				sw.Flush();
				sw.Close();
				sw.Dispose();


				ucBtnFillet1.Enabled = ucBtnFillet4.Enabled = pictureBox1.Enabled = true;
				Btn_StartWithEnd.Text = "开始";
				CompareThread = null;

				GC.Collect();
				this.Invoke(() => FrmTips.ShowTipsSuccess(HasChanged ? "执行已经结束,请在输出目录查看" : "执行已结束, 但是未发现任何变更"));
				#endregion
			}
			catch (Exception ee)
			{
				if (CompareThread != null && CompareThread.IsAlive) Tip.Message(ee.ToString());
				CompareEnd();
			}
		});

		CompareThread.SetApartmentState(ApartmentState.STA);
		CompareThread.Start();
	}


	public static string NewLine => "﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊﹋﹊";

	private static Dictionary<string, Text> GetText(string file) => GetText(new FileInfo(file));

	private static Dictionary<string, Text> GetText(FileInfo file)
	{
		DataTable<Text> Table;
		if (file.Extension == ".xml")
		{
			Table = new() { DataProvider = new FileProvider(file) };
		}
		else
		{
			Table = new LocalDataTableSet(file.FullName).Text;
		}
			

		return Table.Where(o => o.alias is not null).GroupBy(o => o.alias).Select(o => o.First()).ToDictionary(o => o.alias);
	}
	#endregion










	private const string OutputFilter = "受支持的文件|*.xlsx;*.xls;*.xml|Excel Files|*.xlsx;*.xls|Xml Files|*.xml|All files|*.*";

	public string Local_SourcePath { get => Ini.ReadValue(this.GetType(), "SourcePath"); set => Ini.WriteValue(this.GetType(), "SourcePath", value); }

	private void ucBtnFillet5_BtnClick(object sender, EventArgs e)
	{
		var OpenFile = new OpenFileDialog
		{
			Filter = OutputFilter,
			FileName = "汉化数据",

			CheckFileExists = true,
			CheckPathExists = false,
		};

		if (OpenFile.ShowDialog() == DialogResult.OK) TextBox2.Text = OpenFile.FileName;
		else FrmTips.ShowTipsError("用户退出操作");
	}

	private void ucBtnFillet6_BtnClick(object sender, EventArgs e)
	{
		#region Initialize
		if (!File.Exists(filePath.Text))
		{
			FrmTips.ShowTipsError("未选择local.dat文件");
			return;
		}
		else if (File.Exists(TextBox1.Text))
		{
			var result = MessageBox.Show("继续操作会覆盖数据, 请更名或备份数据！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
			if (result != DialogResult.OK)
			{
				FrmTips.ShowTipsSuccess("用户结束操作");
				return;
			}
		}
		#endregion

		Extract(filePath.Text, TextBox1.Text, ucCheckBox2.Checked);
	}

	private void ucBtnFillet8_BtnClick(object sender, EventArgs e)
	{
		OpenFileDialog fileDialog = new()
		{
			Filter = "game file|local*.dat"
		};

		if (!string.IsNullOrWhiteSpace(filePath.Text) && Directory.Exists(filePath.Text))
		{
			fileDialog.InitialDirectory = filePath.Text;
		}

		if (fileDialog.ShowDialog() == DialogResult.OK)
		{
			filePath.Text = fileDialog.FileName;
		}
	}

	private void ucBtnFillet9_BtnClick(object sender, EventArgs e)
	{
		FileDialog Dialog = ucCheckBox1.Checked ? new OpenFileDialog() : new SaveFileDialog();
		Dialog.Filter = ucCheckBox1.Checked ? "game file|*.dat" : OutputFilter;
		Dialog.FileName = ucCheckBox1.Checked ? "local64.dat" : "汉化数据";

		Dialog.CheckFileExists = ucCheckBox1.Checked;


		if (Dialog.ShowDialog() == DialogResult.OK) TextBox1.Text = Dialog.FileName;
		else FrmTips.ShowTipsError("用户退出操作");
	}

	private void ucBtnFillet11_BtnClick(object sender, EventArgs e)
	{
		#region Initialize
		string DatPath = filePath.Text;
		bool is64 = DatPath.Judge64Bit();
		if (!File.Exists(DatPath))
		{
			Tip.Message("没有选择local(64).dat文件");
			return;
		}

		string SavePath = DatPath;
		if (SaveAsBin.Checked)
			SavePath = Path.GetDirectoryName(DatPath) + $@"\localfile{(is64 ? "64" : null)}.bin";
		#endregion



		var loader = new TextLoader();
		loader.Load(new FileInfo(TextBox1.Text));


		throw new NotImplementedException();
	}

	private void ucCheckBox1_CheckedChangeEvent(object sender, EventArgs e)
	{
		this.Label2.Visible = this.TextBox2.Visible = ucBtnFillet5.Visible = !ucCheckBox1.Checked;


		if (ucCheckBox1.Checked)
		{
			this.ucBtnFillet3.Text = "替换";
			this.Label1.Text = "替换来源";
			this.TextBox1.Text = null;
		}
		else
		{
			this.ucBtnFillet3.Text = "封包";
			this.Label1.Text = "汉化文件";
			this.filePath_TextChanged(null, null);
		}
	}

	private void filePath_TextChanged(object sender, EventArgs e)
	{
		var path = this.Local_SourcePath = filePath.Text;
		if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
			TextBox1.Text = Path.GetDirectoryName(path) + @"\汉化数据.xlsx";
	}
}