using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.Tests.DatTool.Utils;
using Xylia.Preview.Tests.DatTool.Utils.DevTools;
using Xylia.Preview.Tests.Utils;

namespace Xylia.Preview.Tests.DatTool;
public partial class MainForm : Form
{
	#region Constructor
	[STAThread]
	static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new MainForm());
	}

	public MainForm()
	{
		InitializeComponent();

		new StrWriter(richOut);
		CheckForIllegalCrossThreadCalls = false;

		ReadConfig(this);
	}
	#endregion

	#region Static Methods
	public void ReadConfig(Control container)
	{
		string SECTION = "Test";

		foreach (Control c in container.Controls)
		{
			ReadConfig(c);

			var value = IniHelper.Instance.ReadValue(SECTION, $"{c.FindForm().Name}_{c.Name}");
			if (c is CheckBox checkBox)
			{
				if (!string.IsNullOrWhiteSpace(value)) checkBox.Checked = value.ToBool();
				checkBox.CheckedChanged += (s, e) => IniHelper.Instance.WriteValue(SECTION, this.Name + "_" + c.Name, checkBox.Checked);
			}
			else if (c is TextBox textBox)
			{
				if (!string.IsNullOrWhiteSpace(value)) c.Text = value;
				c.TextChanged += (s, e) => IniHelper.Instance.WriteValue(SECTION, $"{c.FindForm().Name}_{c.Name}", c.Text);
			}
		}
	}


	private DateTime m_LastTime = DateTime.MinValue;

	private void DoubleClickPath(object sender, EventArgs e)
	{
		if (DateTime.Now.Subtract(m_LastTime).TotalSeconds <= 2) return;
		m_LastTime = DateTime.Now;


		var selected = (sender as Control).Text?.Trim();
		if (selected.Contains('|'))
		{
			var FilePathes = selected.Split('|');
			selected = FilePathes[0];
		}

		if (Directory.Exists(selected))
		{
			Process.Start("Explorer.exe", selected);
			return;
		}

		ProcessStartInfo psi = new("Explorer.exe") { Arguments = "/e,/select," + selected };
		Process.Start(psi);
	}

	public static IEnumerable<string> OpenPath(Control link, string Filter = null, bool Multiselect = false)
	{
		var openFile = new OpenFileDialog();

		string FilePath = link.Text;
		if (!string.IsNullOrWhiteSpace(FilePath) && !FilePath.Contains('|'))
		{
			openFile.InitialDirectory = Directory.Exists(FilePath) ? FilePath : new FileInfo(FilePath).DirectoryName;
		}


		openFile.Filter = (Filter == null ? null : Filter + "|") + "所有文件|*";
		openFile.Multiselect = Multiselect;

		if (openFile.ShowDialog() == DialogResult.OK)
		{
			if (openFile.FileNames.Length == 1) link.Text = openFile.FileName;
			else
			{
				StringBuilder sb = new();

				foreach (var f in openFile.FileNames)
					sb.Append(f + "|");

				link.Text = sb.ToString();
			}

			return openFile.FileNames;
		}

		return null;
	}

	private static void OpenFolder(Control link)
	{
		var dialog = new FolderBrowserDialog();
		if (dialog.ShowDialog() == DialogResult.OK) link.Text = dialog.SelectedPath;
	}
	#endregion


	#region Dat
	private void bntSearchDat_Click(object sender, EventArgs e) => OpenPath(txbDatFile);

	private void txbDatFile_TextChanged(object sender, EventArgs e)
	{
		var s = (Control)sender;
		string Text = s.Text.Trim();

		if (Directory.Exists(Text))
		{
			var dir = new DirectoryInfo(Text);
			var files = dir.GetFiles("*.dat", SearchOption.AllDirectories);

			s.Text = files.FirstOrDefault()?.FullName;
		}
		else if (File.Exists(Text))
		{
			txbRpFolder.Text = Path.GetDirectoryName(Text) + @"\Export\" + Path.GetFileNameWithoutExtension(Text);
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFile = new()
		{
			Filter = "XML 文件|*.xml"
		};

		if (openFile.ShowDialog() == DialogResult.OK)
		{
			new Thread(() =>
			{
				XmlDocument xmlDocument = new();
				xmlDocument.Load(openFile.FileName);

				foreach (XmlNode patchNode in xmlDocument.SelectNodes("//patch"))
				{
					string RelaPath = patchNode?.Attributes["file"]?.Value;
					string FilePath = txbRpFolder.Text + "\\" + RelaPath;
					if (File.Exists(FilePath))
					{
						XmlDocument FileDoc = new();
						FileDoc.Load(FilePath);

						ModifyNodes(patchNode, FileDoc.DocumentElement);
						FileDoc.Save(FilePath);
					}
				}

			}).Start();
		}
	}

	public void ModifyNodes(XmlNode ConfigParentNode, XmlNode FileNode)
	{
		foreach (XmlNode ConfigNode in ConfigParentNode.SelectNodes("./select-node"))
		{
			string Query = ConfigNode?.Attributes["query"]?.Value;

			List<XmlNode> CurFileNode = new();
			if (Query != "/config") foreach (XmlNode node in FileNode.SelectNodes(Query)) ModifyNodes(ConfigNode, node);
			else ModifyNodes(ConfigNode, FileNode);
		}


		foreach (XmlNode ConfigNode in ConfigParentNode.SelectNodes("./set-value"))
		{
			if (FileNode is XmlAttribute attribute)
			{
				var target = ConfigNode.Attributes["value"];
				if (target != null) attribute.Value = target.Value;
			}
			else if (FileNode != null)
			{
				Console.WriteLine("不支持当前类型：" + FileNode.GetType());
			}
		}
	}

	private void BntStart_Click(object sender, EventArgs e)
	{
		if (!File.Exists(txbDatFile.Text)) throw new Exception("选择要解压缩的.dat文件.");
		if (Directory.Exists(txbRpFolder.Text))
		{
			if (MessageBox.Show("即将开始解包文件，是否确认?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
				return;
		}


		Task.Run(() => ThirdSupport.Extract(new PackageParam(txbDatFile.Text) { FolderPath = txbRpFolder.Text }));
	}

	private void btnRepack_Click(object sender, EventArgs e)
	{
		string outdir = txbRpFolder.Text;
		if (!Directory.Exists(outdir))
		{
			MessageBox.Show("请选择封包文件夹", "Error");
			return;
		}

		new Thread(o =>
		{
			try
			{
				var param = new PackageParam(txbDatFile.Text)
				{
					FolderPath = outdir,
					CompressionLevel = (CompressionLevel)this.trackBar1.Value,
				};

				if (checkBox1.Checked) ThirdSupport.Pack(param);
				else BNSDat.CreateFromDirectory(param);

				Console.WriteLine("Pack completed");
			}
			catch (Exception ee)
			{
				MessageBox.Show(ee.ToString());
			}

		}).Start();
	}

	private void trackBar1_ValueChanged(object sender, EventArgs e) => this.label6.Text = $"->       处理速度变慢，压缩率提高 ({this.trackBar1.Value}级)";
	#endregion

	#region Bin
	private void button2_Click(object sender, EventArgs e) => OpenFolder(Txt_Bin_Data);

	private void Button1_Click(object sender, EventArgs e)
	{
		Task.Run(() =>
		{
			if (MessageBox.Show("即将开始提取数据，是否确认?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
				return;

			try
			{
				using var provider = DefaultProvider.Load(Txt_Bin_Data.Text, this.checkBox13.Checked ? ResultMode.SelectBin : ResultMode.SelectDat);
				provider.LoadData(null);

				var folder = string.Concat(Path.GetDirectoryName(provider.XmlData.Path), @"\Export\json\");
				if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
				else foreach (string f in Directory.GetFiles(folder, "*.json")) File.Delete(f);


				int Count = 1;
				Parallel.ForEach(provider.Tables, table =>
				{
					Console.WriteLine($"输出配置文件: {Count++,-3}/{provider.Tables.Count}...{Count * 100 / provider.Tables.Count,3}%  (ListId: {table.Type})");
					if (table.XmlPath is not null) return;


					string FilePath = $@"{folder}\{table.Type}";
					if (provider.Detect.TryGetName(table.Type, out string TypeName) && TypeName != null) FilePath += $" ({TypeName})";

					using StreamWriter outfile = new(FilePath + ".json");
					outfile.WriteLine(JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

					table.Dispose();
				});
			}
			catch (Exception ee)
			{
				MessageBox.Show("输出配置文件时发生异常。\n\n" + ee, "发生系统错误");
			}

			Console.WriteLine("输出已完成!!");
			GC.Collect();
		});
	}


	private void button8_Click(object sender, EventArgs e) => OpenPath(textBox1);

	private void button4_Click(object sender, EventArgs e) => OpenFolder(textBox3);

	private void button7_Click(object sender, EventArgs e)
	{
		var defs = TableDefinitionHelper.LoadTableDefinition(null, new FileInfo(textBox1.Text));
		foreach (var def in defs)
		{
			foreach (var attribute in def.ElRecord.ExpandedAttributes.OrderBy(x => x.Offset))
			{
				Console.WriteLine($"#notime#{attribute.Offset}  -  {attribute.Name}");
			}

			foreach (var sub in def.ElRecord.Subtables)
			{
				foreach (var attribute in sub.ExpandedAttributesSubOnly.OrderBy(x => x.Offset))
				{
					Console.WriteLine($"#notime#[{sub.Name}] {attribute.Offset}  -  {attribute.Name}");
				}
			}
		}
	}


	private TestDatabase set;
	private void button6_Click(object sender, EventArgs e)
	{
		Task.Run(() =>
		{
			try
			{
				set ??= new TestDatabase(DefaultProvider.Load(Txt_Bin_Data.Text), textBox3.Text);
				set.Output(textBox1.Text.Split('|').Select(o => new FileInfo(o)).Where(o => o.Exists).ToArray());
			}
			catch (Exception ex)
			{
				Console.WriteLine("[error] " + ex);
			}
		});
	}
	#endregion


	#region ToolStrip
	private void ClearLog(object sender, EventArgs e)
	{
		this.richOut.Clear();
	}

	private void ToolStripMenuItem_NumSelect_Click(object sender, EventArgs e)
	{
		var frm = new NumSelect();
		frm.Confirming += ClearLog;
		frm.Show();
	}
	#endregion

	#region Page
	private void Btn_HexToDecimal_Click(object sender, EventArgs e)
	{
		lbl_Warning3.Text = null;
		string text = Txt_HEX.Text.Replace('-', ' ');

		try
		{
			if (radioButton2.Checked)
			{
				Txt_Decimal.Text = long.Parse(text, System.Globalization.NumberStyles.HexNumber).ToString();
				return;
			}


			var bytes = text.ToBytes();
			CommonConvert CC = new(bytes);

			if (CC.Length == 2) Txt_Decimal.Text = CC.Short1.ToString();
			else if (CC.Length >= 8) Txt_Decimal.Text = CC.Long.ToString();
			else Txt_Decimal.Text = CC.Int32.ToString();

			label2.Text = "Short型：" + CC.Short1 + " | " + CC.Short2 + "\nFloat型：" + CC.Float;
			lbl_Warning3.Text = null;
		}
		catch (Exception ee)
		{
			lbl_Warning3.Text = ee.Message;
		}
	}

	private void Btn_DecimalToHex_Click(object sender, EventArgs e)
	{
		lbl_Warning3.Text = null;
		label2.Text = null;

		try
		{
			if (radioButton2.Checked)
			{
				if (long.TryParse(Txt_Decimal.Text, out long LResult))
				{
					Txt_HEX.Text = LResult.ToString("X");
				}
				else throw new Exception("转换失败");

				return;
			}



			if (long.TryParse(Txt_Decimal.Text, out long result))
			{
				CommonConvert CC = new(result);
				if (result < int.MaxValue) CC = new((int)result);

				label2.Text = "Short型：" + CC.Short1 + " | " + CC.Short2 + "\nFloat型：" + CC.Float;
			}
		}
		catch (Exception ee)
		{
			lbl_Warning3.Text = ee.Message;
			Console.WriteLine(ee.Message);
		}
	}

	private void button12_Click(object sender, EventArgs e) => richTextBox1.Text = CreateClass.Instance(richTextBox1.Text);

	private void button5_Click(object sender, EventArgs e) => richTextBox1.Text = CreateEnum.Instance(richTextBox1.Text?.Trim());

	private void button16_Click(object sender, EventArgs e)
	{
		var sb = new StringBuilder();
		foreach (var line in this.richTextBox1.Text.Split('\n').Where(l => !string.IsNullOrWhiteSpace(l)))
		{
			string[] ls = Regex.Split(line, "\\s+");
			sb.Append($"{ls[0]}=\"{ls[1].Replace("%", null)}\" ");
		}

		this.richTextBox1.Text = $"  <record {sb}/>\n";
	}

	private void button15_Click(object sender, EventArgs e)
	{
		XmlDocument tmp = new();
		tmp.LoadXml($"<?xml version=\"1.0\"?>\n<table>{richTextBox1.Text?.Trim()}</table>");

		var record = tmp.SelectSingleNode("table/*");
		if (record is null) return;


		StringBuilder rtf = new();
		rtf.Append(@"{\rtf1 ");

		foreach (XmlAttribute attr in record.Attributes)
		{
			rtf.Append(@"\trowd");
			for (int j = 1; j <= 2; j++) rtf.Append($@"\cellx{j * 4000}");

			//create row
			rtf.Append($@"\intbl {GetAsc2Code(attr.Name)}\cell {GetAsc2Code(attr.Value)}\row");
		}

		rtf.Append(@"\pard ");
		rtf.Append('}');

		this.richTextBox1.Clear();
		this.richTextBox1.SelectedRtf = rtf.ToString();
	}

	public static string GetAsc2Code(string txt)
	{
		string result = null;
		foreach (var t in txt) result += "\\u" + (int)t + "  ";

		return result;
	}

	private void Btn_Split_Click(object sender, EventArgs e)
	{
		try
		{
			var data = richTextBox1.Text
				.Replace("\r\n", null)
				.Replace(" ", null)
				.Replace("-", null)
				.UnCompress()
				.ToBytes();


			StringBuilder output = new();
			for (int idx = 0; idx < data.Length; idx += 4)
			{
				output.Append(idx);
				output.Append("    |   ");
				output.Append(BitConverter.ToString(data, idx, 4));
				output.Append("    |   ");
				output.Append(BitConverter.ToInt32(data, idx));
				output.Append('\n');
			}

			richTextBox1.Text = output.ToString();
		}
		catch (Exception ee)
		{
			Console.WriteLine(ee.Message);
		}
	}
	#endregion
}