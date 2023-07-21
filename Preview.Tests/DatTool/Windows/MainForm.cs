using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;

using BnsBinTool.Core.Models;

using Xylia.Configure;
using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData;
using Xylia.Preview.Data.Models.BinData.Table.Config;
using Xylia.Preview.Data.Models.DatData;
using Xylia.Preview.Data.Models.DatData.DatDetect;
using Xylia.Preview.Data.Models.DatData.Third;
using Xylia.Preview.Data.Models.Util.Sort;
using Xylia.Preview.Data.Models.Util.Sort.Common;
using Xylia.Preview.Data.Models.Util.Writer;
using Xylia.Preview.Data.Models.ZoneData.RegionData;
using Xylia.Preview.Data.Models.ZoneData.TerrainData;
using Xylia.Preview.Tests.DatTool.Utils;
using Xylia.Preview.Tests.DatTool.Utils.Extract;
using Xylia.Windows.CustomException;
using Xylia.Xml;

using static Xylia.Preview.Data.Models.DatData.BXML_CONTENT;
using static Xylia.Preview.Data.Models.DatData.Third.MySpport;

namespace Xylia.Preview.Tests.DatTool.Windows;

public partial class MainForm : Form
{
	#region Constructor
	public MainForm()
	{
		InitializeComponent();

		var o = new StrWriter(richOut);
		CheckForIllegalCrossThreadCalls = false;

		this.trackBar1.Value = this.trackBar1.Minimum;
		this.Text = $"{this.Text}  (Build Date {AssemblyEx.BuildTime:yyMMdd})";
	}
	#endregion

	#region TestFunc
	private void MainForm_Load(object sender, EventArgs e)
	{
		txbDatFile.Text = Ini.ReadValue("Path", "Data_DatFile");
		txbRpFolder.Text = Ini.ReadValue("Path", "Data_OutFolder");

		this.ReadConfig();
	}

	private void MainForm_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Modifiers == Keys.Control)
		{
			if (e.KeyCode == Keys.F12) MenuItem_DevTools_Click(null, null);
		}
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			this.Hide();
			Environment.Exit(0);
		}
		catch
		{

		}
	}

	private void TxtSavePath(object sender, EventArgs e)
	{
		((Control)sender).SavePath();
	}

	private void SaveCheckStatus(object sender, EventArgs e)
	{
		var ctl = (CheckBox)sender;
		Ini.WriteValue("Config", this.Name + "_" + ctl.Name, ctl.Checked);
	}

	private void DoubleClickPath(object sender, EventArgs e)
	{
		((Control)sender).DoubleClickPath();
	}

	private void ClearLog(object sender, EventArgs e)
	{
		this.richOut.Clear();
	}
	#endregion



	#region Dat
	private void txbDatFile_TextChanged(object sender, EventArgs e)
	{
		var s = (Control)sender;
		string Text = s.Text.Trim();
		Ini.WriteValue("Path", "Data_DatFile", Text);

		//目录
		if (Directory.Exists(Text))
		{
			var dir = new DirectoryInfo(Text);
			var files = dir.GetFiles("*.dat", SearchOption.AllDirectories);

			s.Text = files.FirstOrDefault()?.FullName;
		}
		else if (File.Exists(Text))
		{
			txbRpFolder.Text = Path.GetDirectoryName(Text) + @"\Export\" + Path.GetFileNameWithoutExtension(Text) + @"\files";
		}
	}

	private void txbRpFolder_TextChanged(object sender, EventArgs e)
	{
		Ini.WriteValue("Path", "Data_OutFolder", ((TextBox)sender).Text);
	}

	private void textBox8_TextChanged(object sender, EventArgs e)
	{
		if (Directory.Exists(((TextBox)sender).Text)) Ini.WriteValue("Server", "Folder", ((TextBox)sender).Text);
	}



	private void bntSearchDat_Click(object sender, EventArgs e) => txbDatFile.OpenPath();

	private void button21_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFile = new()
		{
			Filter = "所有文件|*"
		};

		if (openFile.ShowDialog() == DialogResult.OK)
		{
			new Thread(() =>
			{
				string RelativePath = Path.GetFileName(openFile.FileName);
				if (RelativePath.MyStartsWith("questdata")) RelativePath = @"quest\" + RelativePath;
				else if (RelativePath.MyStartsWith("charactertoolappearance_")) RelativePath = @"engine\" + RelativePath;


				throw new NotImplementedException();
				//var FileData = File.ReadAllBytes(openFile.FileName);
				//new BNSDat(txbDatFile.Text).CompressFiles(RelativePath, FileData);

			}).Start();
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
			if (FileNode is null)
			{

			}
			else if (FileNode is XmlAttribute attribute)
			{
				var Target = ConfigNode.Attributes["value"];
				if (Target != null)
				{
					attribute.Value = ConfigNode.Attributes["value"].Value;
					Console.WriteLine("已修改 " + attribute.Name + " => " + attribute.Value);
				}
			}
			else
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

		string datpath = txbDatFile.Text;
		string outpath = txbRpFolder.Text ?? Path.GetDirectoryName(datpath) + @"\Export\" + Path.GetFileNameWithoutExtension(datpath) + @"\files";

		new Thread(o => new BNSDat(datpath).Extract(outpath)).Start();
	}

	private void btnRepack_Click(object sender, EventArgs e)
	{
		string outdir = txbRpFolder.Text;
		if (!Directory.Exists(outdir))
		{
			Tip.Message("请选择封包文件夹", "Error");
			return;
		}

		new Thread(o =>
		{
			try
			{
				Console.WriteLine("封包开始");

				PackParam param = new()
				{
					Aes = KeyInfo.AES_2020_05,
					FolderPath = outdir,
					PackagePath = txbDatFile.Text,
					Bit64 = txbDatFile.Text.Judge64Bit(),
					CompressionLevel = (bnscompression.CompressionLevel)this.trackBar1.Value,
				};

				if (checkBox1.Checked) MySpport.Pack(param);
				else BNSDat.Pack(param);

				Console.WriteLine("封包结束");
			}
			catch (Exception ee)
			{
				Tip.Message(ee.ToString());
			}

		}).Start();
	}

	private void trackBar1_ValueChanged(object sender, EventArgs e) => this.label6.Text = $"->       处理速度变慢，压缩率提高 ({this.trackBar1.Value}级)";
	#endregion

	#region Bin
	private void Button2_Click(object sender, EventArgs e) => Txt_Bin_Data.OpenDirPath();



	private void HeadDump_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("即将开始提取数据，是否确认?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK) return;

		Thread thread = null;
		thread = new Thread(() =>
		{
			//try
			//{
			//	var GetDataPath = new GetDataPath(Txt_Bin_Data.Text, null, this.checkBox13.Checked);

			//	//载入汉化
			//	TextBinData TextData = null;
			//	if (false && File.Exists(GetDataPath.TargetLocal))
			//		TextData = new(GetDataPath.TargetLocal);


			//	bool is64 = GetDataPath.TargetXml.Judge64Bit();
			//	string saveFolder = Path.GetDirectoryName(GetDataPath.TargetXml) + @"\Export\" + Path.GetFileNameWithoutExtension(GetDataPath.TargetXml);

			//	//执行提取
			//	new BinData().ExportAliasTable(GetDataPath.TargetXml, saveFolder, is64);
			//}
			//catch (Exception ee)
			//{
			//	if (ee is not ThreadAbortException) 
			//		Tip.Message("输出配置文件时发生异常。\n\n" + ee, "发生系统错误");
			//}

			//GC.Collect();
		});

		thread.Start();
	}

	private void button38_Click(object sender, EventArgs e)
	{
		//if (MessageBox.Show("即将开始重新封包，是否确认?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK) return;

		//Thread thread = null;
		//thread = new Thread(() =>
		//{
		//	try
		//	{
		//		//Initialize
		//		using var GetDataPath = new Read.GetDataPath(Txt_Bin_Data.Text, null, this.checkBox13.Checked);
		//		bool is64 = GetDataPath.TargetXml.Judge64Bit();

		//		string SavePath = Path.GetDirectoryName(GetDataPath.TargetXml) + @"\Export\" +
		//			 Path.GetFileNameWithoutExtension(GetDataPath.TargetXml);

		//		//强制指定路径
		//		if (checkBox2.Checked) SavePath = @"D:\Blade_and_Soul\contents\Local\NCSoft\data\Export\xml64";

		//		Console.WriteLine($"当前处理: " + SavePath);

		//		//入口Functions
		//		using var bDat = new BinData();
		//		bDat.ReplaceHead(GetDataPath.TargetXml, is64, SavePath);
		//	}
		//	catch (Exception ee)
		//	{
		//		if (ee is not ThreadAbortException)
		//		{
		//			Tip.Message($"{ ee.Message }", "发生系统错误");
		//			LogWriter.WriteLine(ee);
		//		}
		//	}

		//	GC.Collect();
		//});
		//thread.Start();
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("即将开始提取数据，是否确认?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK) return;

		Thread thread = null;
		thread = new Thread(act =>
		{
			try
			{
				var GetDataPath = new GetDataPath(Txt_Bin_Data.Text, this.checkBox13.Checked ? ResultMode.SelectBin : ResultMode.SelectDat);
				string OutFolder = Path.GetDirectoryName(GetDataPath.XmlData.Path) +
					@"\Export\" + Path.GetFileNameWithoutExtension(GetDataPath.XmlData.Path) + @"\json\";


				var data = Datafile.ReadFromBytes(GetDataPath.XmlData.ExtractBin(), is64Bit: GetDataPath.XmlData.Path.Judge64Bit());
				var detect = new DatafileDetect();
				detect.Detect(data);

				data.Tables.Dump(detect, OutFolder, new ExportOption()
				{
					OutputFieldAlias = true,
					OutputListAlias = true
				});
			}
			catch (UserExitException)
			{

			}
			catch (Exception ee)
			{
				if (ee is not ThreadAbortException) Tip.Message("输出配置文件时发生异常。\n\n" + ee, "发生系统错误");
			}

			GC.Collect();
		});

		thread.Start();
	}

	private void button16_Click(object sender, EventArgs e)
	{
		throw new NotImplementedException();

		//using var GetDataPath = new Read.GetDataPath(Txt_Bin_Data.Text, null, checkBox13.Checked);

		//new Thread(() =>
		//{
		//	#region Initialize
		//	var bDat = new BDat();
		//	bDat.BinHandle.Load(GetDataPath.TargetLocal);

		//	var Tables = new BlockingCollection<TextData>();

		//	var blist = bDat.BinHandle["Text"];
		//	if (blist is BDAT_ARCHIVE Archive)
		//	{
		//		Parallel.ForEach(Archive.Tables, TABLE =>
		//		{
		//			var Content = TABLE.Lookup.TextList.Strings;
		//			Tables.Add(new TextData(Content[0], Content[1], TABLE.FID));
		//		});
		//	}

		//	var TableInfo = Tables.ToList();
		//	Tables = null;
		//	TableInfo.Sort(new AliasSort());
		//	#endregion

		//	#region 生成为外部文件
		//	var table = new TableInfo();
		//	table.Type = "Text";
		//	table.RealVersion = blist.Version;

		//	XmlInfo xi = table.CreateXml();
		//	foreach (var t in TableInfo)
		//	{
		//		var Xe = xi.CreateElement("record");
		//		Xe.SetAttribute("alias", t.alias());

		//		if (string.IsNullOrEmpty(t.Text)) ;
		//		else if (t.Text.Contains('<') || t.Text.Contains('>') || t.Text.Contains('&')) Xe.AppendChild(xi.CreateCData(t.Text));
		//		else Xe.InnerText = t.Text;

		//		xi.AppendChild(Xe);
		//	}

		//	xi.Save(Path.GetDirectoryName(GetDataPath.TargetLocal), "TextData3.x16", true);
		//	Console.WriteLine("OK");

		//}).Start();
		//#endregion
	}

	private void button7_Click(object sender, EventArgs e)
	{
		Console.WriteLine("正在加载文件数据");

		var thread = new Thread(Act =>
		{
			//var loader = new TextLoader
			//{
			//	MajorAlias = false
			//};

			//loader.Load(@"D:\Blade_and_Soul\contents\Local\NCSoft\korean\data\汉化数据.xlsx");

			try
			{
				//using var loader = new TextLoader();
				////loader.LoadTextData(@"F:\Bns\client\text");
				//loader.LoadTextData(@"F:\Build\server\临时版本\新建文件夹");


				//var Path = @"D:\Blade_and_Soul\contents\Local\NCSoft\korean\data\Export\local64\files\localfile64.bin";
				//Path = @"F:\Build\server\临时版本\前端\localfile64.bin";

				////using var tran = new BTranslate(@"D:\Blade_and_Soul\contents\Local\NCSoft\korean\data\local64.dat");

				//new BinData(Path).Translate(Path, loader, false);
			}
			catch (Exception ee)
			{
				Console.WriteLine(" == CRASH == " + ee);
			}

			GC.Collect();
		});

		thread.Start();
	}
	#endregion


	#region Zone
	private void button37_Click(object sender, EventArgs e) => Txt_Cterrain_Path.OpenPath();

	private void button31_Click(object sender, EventArgs e) => Txt_Region_Path.OpenPath();

	private void button22_Click(object sender, EventArgs e)
	{
		var Region = new RegionFile();
		Region.Read(Txt_Region_Path.Text);

		string Path = $@"F:\Bns\test\region\{Region.RegionID}.xml";
		Region.OutputTest(Path);
	}

	private void Btn_Debug_Input_Click(object sender, EventArgs e)
	{
		var Region = new RegionFile();
		Region.Read(Txt_Region_Path.Text);

		string Path = $@"F:\Bns\test\region\{Region.RegionID}.xml";

		Region.InputTest(Path);
		Region.Save($@"F:\Bns\test\region\{Region.RegionID}.region", false);
	}

	private void button23_Click(object sender, EventArgs e)
	{
		var CterrainFile = new CterrainFile();
		var XmlPath = $@"F:\Bns\test\{Path.GetFileNameWithoutExtension(Txt_Cterrain_Path.Text)}.xml";

		CterrainFile.Read(Txt_Cterrain_Path.Text);
		CterrainFile.OutputTest(XmlPath);


		Console.WriteLine("读取完成");
	}

	private void button20_Click(object sender, EventArgs e)
	{
		var CterrainFile = new CterrainFile();
		var XmlPath = $@"F:\Bns\test\{Path.GetFileNameWithoutExtension(Txt_Cterrain_Path.Text)}.xml";

		CterrainFile.InputTest(XmlPath);
		CterrainFile.Write($@"F:\Bns\test\{CterrainFile.TerrainID}.cterrain");

		Console.WriteLine("读取完成");
	}

	private void button36_Click(object sender, EventArgs e)
	{
		if (int.TryParse(textBox12.Text, out int result))
		{
			//.Cterrain
			var CterrainFile = new CterrainFile();

			BinaryReader br = new(new FileStream(Txt_Cterrain_Path.Text, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite));
			CterrainFile.Read(br);

			CterrainFile.TerrainID = result;

			string Path = System.IO.Path.GetDirectoryName(Txt_Cterrain_Path.Text) + $"\\{result}.cterrain";
			if (File.Exists(Path)) Path += "_new";

			CterrainFile.Save(Path);
		}
	}

	private void button32_Click(object sender, EventArgs e)
	{
		var ZoneInfo = Txt_Zone.Text;
		if (short.TryParse(ZoneInfo, out var RegionID))
		{
			var regionFile = new RegionFile();
			regionFile.Read(Txt_Region_Path.Text);

			regionFile.RegionID = RegionID;
			if (short.TryParse(Region_XMin_input.Text, out var tmp)) regionFile.Xmin = tmp;
			if (short.TryParse(Region_XMax_input.Text, out tmp)) regionFile.Xmax = tmp;
			if (short.TryParse(Region_YMin_input.Text, out tmp)) regionFile.Ymin = tmp;
			if (short.TryParse(Region_YMax_input.Text, out tmp)) regionFile.Ymax = tmp;


			string Path = System.IO.Path.GetDirectoryName(Txt_Region_Path.Text) + $"\\{RegionID}.region";
			if (File.Exists(Path)) Path += "_new";

			regionFile.Save(Path);
		}
	}
	#endregion


	#region ServerData
	private void button33_Click(object sender, EventArgs e) => textBox7.OpenPath("配置文件|*.xml", true);

	private void button34_Click(object sender, EventArgs e) => textBox11.OpenPath("配置文件|*.xml");

	private void textBox11_TextChanged(object sender, EventArgs e)
	{
		TxtSavePath(sender, e);
		ModifyCfgPath(textBox7, ((Control)sender).Text, textBox7.Text);
	}

	private static void ModifyCfgPath(Control ctl, string CfgPath, string TargetDir)
	{
		new Thread(() =>
		{
			try
			{
				string ConvertPath = TargetDir.Split('|')[0];
				string Result = null;


				if (File.Exists(CfgPath) && !string.IsNullOrWhiteSpace(ConvertPath))
				{
					XmlDocument tmp = new();
					tmp.Load(CfgPath);

					string Relative = tmp.DocumentElement.SelectSingleNode(".//list")?.Property()?.Attributes["relative-path", "path"]?.ToLower();

					//判断目录
					string TarDir = Path.GetDirectoryName(ConvertPath);
					if (!string.IsNullOrWhiteSpace(Relative) && Directory.Exists(TarDir))
					{
						//var Files = FileGet.GetFiles(TarDir, Relative, ".xml", false);

						//if (Files.Count != 0) Result = Files[0].FullName;
						//else
						//{
						//	Files = FileGet.GetFiles(Path.GetDirectoryName(TarDir), Relative, ".xml", false);
						//	if (Files.Count != 0) Result = Files[0].FullName;
						//}
					}
				}

				if (!string.IsNullOrWhiteSpace(Result)) ctl.Text = Result;
			}
			catch
			{

			}

		}).Start();
	}

	
	private void button13_Click(object sender, EventArgs e)
	{
		new Thread(() =>
		{
			DirectoryInfo dir = new(textBox8.Text);
			ServerUnpack(dir.GetFiles());

			foreach (DirectoryInfo subDir in dir.GetDirectories())
				ServerUnpack(subDir.GetFiles());
		}).Start();
	}

	private void button14_Click(object sender, EventArgs e)
	{
		new Thread(o =>
		{
			var files = new DirectoryInfo(textBox8.Text).GetFiles();
			foreach (FileInfo fi in files)
			{
				if (fi.Name.EndsWith("xml") || fi.Name.EndsWith(".x16"))
				{
					using FileStream fileStream = new(fi.FullName, FileMode.Open);
					File.WriteAllBytes(fi.FullName, BXML_CONTENT.Convert(fileStream, BXML_TYPE.BXML_BINARY).ToArray());
				}
			}
		}).Start();
	}

	public static void ServerUnpack(FileInfo[] fis)
	{
		foreach (FileInfo fi in fis)
		{
			if (fi.Name.EndsWith("xml") || fi.Name.EndsWith(".x16"))
			{
				using FileStream fileStream = new(fi.FullName, FileMode.Open);
				using MemoryStream memoryStream = BXML_CONTENT.Convert(fileStream, BXML_TYPE.BXML_PLAIN);

				File.WriteAllBytes(fi.FullName, memoryStream.ToArray());
			}
		}
	}

	private void checkBox2_CheckedChanged(object sender, EventArgs e)
	{
		this.label30.Visible = this.textBox7.Visible = button33.Visible = !this.Chk_ServerData_AutoGet.Checked;
		this.SaveCheckStatus(sender, e);
	}
	#endregion

	#region Combine
	private void button10_Click(object sender, EventArgs e) => textBox4.OpenPath(null, false);


	IEnumerable<string> FilePathes = null;

	private void button9_Click(object sender, EventArgs e)
	{
		if ((FilePathes = textBox2.OpenPath(null, true)) != null)
			this.textBox2.Text = "{对象集合}";
	}

	private void button17_Click(object sender, EventArgs e) => textBox6.OpenPath();

	private void checkBox12_CheckedChanged(object sender, EventArgs e) => checkBox12.Text = checkBox12.Checked ? "alias作为主键" : "id作为主键";

	private void checkBox14_CheckedChanged(object sender, EventArgs e) => this.label8.Visible = this.textBox6.Visible = this.button17.Visible = this.checkBox14.Checked;

	private void button11_Click(object sender, EventArgs e)
	{
		Thread t = new(act =>
		{
			try
			{
				if (FilePathes is null) return;

				#region 读取合并的信息
				var Info = new Dictionary<string, XmlProperty>(StringComparer.OrdinalIgnoreCase);
				foreach (var Path in FilePathes)
				{
					if (!string.IsNullOrWhiteSpace(Path) && File.Exists(Path))
					{
						XmlDocument doc = new();
						doc.Load(Path);

						doc.SelectNodes(".//record").OfType<XmlElement>().ToList().ForEach(Case =>
						{
							var xp = Case.Property();

							//存储用的关键Key
							string Key = checkBox12.Checked ? xp.Attributes["alias"] : xp.Attributes["id"];
							//if (true) Key = xp.Attributes["job"] + "_" + xp.Attributes["level"];


							if (Key == null) Console.WriteLine("无效对象：" + Case.OuterXml);
							else if (Info.ContainsKey(Key)) Console.WriteLine("重复：" + Key);
							else Info.Add(Key, xp);
						});
					}
				}
				#endregion

				#region 生成白名单Fields名
				List<string> AttrNames = null;
				if (checkBox14.Checked)
				{
					//var Lists = ConfigLoad.Load(textBox6.Text);
					//if (Lists is null || Lists.Count == 0) throw new Exception("没有载入任何配置数据，请确定路径是否正确");

					//AttrNames = new List<string>();
					//foreach (var record in Lists[0].Records.Where(r => !r.Client))
					//{
					//	for (int x = 1; x <= record.Repeat; x++)
					//		AttrNames.Add(record.GetAlias(x));

					//	//if (record is RepeatRecord repeatRecord)
					//	//{
					//	//	for (int x = repeatRecord.StartNumber; x <= repeatRecord.FinishNumber; x++) AttrNames.Add(repeatRecord.GetAlias(x));
					//	//}
					//	//else AttrNames.Add(record.GetAlias(0));
					//}
				}
				#endregion



				#region 处理被合并的数据源
				foreach (var tmp in textBox4.Text.Split('|'))
				{
					var Source = new XmlDocument();
					Source.Load(tmp);
					Source.SelectNodes(".//record").OfType<XmlElement>().ToList().ForEach(Case =>
					{
						var xp = Case.Property();
						if (true)
						{
							//唯一主键
							string CaseKey = checkBox12.Checked ? xp.Attributes["alias"] : xp.Attributes["id"];
							//if (true) CaseKey = xp.Attributes["job"] + "_" + xp.Attributes["level"];


							if (string.IsNullOrWhiteSpace(CaseKey))
								throw new Exception($"主键不能为空 ({Case.OuterXml})");


							//如果被合并源中包含当前信息
							if (Info.ContainsKey(CaseKey))
							{
								#region 清除原信息
								if (false)
								{
								}
								#endregion


								//由于清除后信息发生变化，需要重新生成数据
								xp = Case.Property();

								//筛选出属性
								foreach (var a in Info[CaseKey].Attributes.Where(a => a.Value != null))
								{
									//判断Fields是否需要处理
									if (AttrNames != null && !AttrNames.Contains(a.Name))
										continue;


									//指示被合并信息中包含此属性
									bool IsContain = xp.Attributes.ContainsName(a.Name, true);
									bool Flag = checkBox11.Checked || !IsContain;
									//Flag = a.Name != "id";

									if (Flag)
									{
										string TarVal = a.Value.ToString();

										//如果不包含此属性
										if (!IsContain)
										{
											Case.SetAttribute(a.Name, TarVal);
										}

										//如果包含此属性
										else
										{
											string OriVal = xp.Attributes[a.Name];

											//两个文档中属性值不同时进行处理
											if (!OriVal.Equals(TarVal))
											{
												string msg = $"重复的属性 ({a.Name})：{OriVal} => {TarVal}";

												//将文本应用为合并源数据
												Case.SetAttribute(a.Name, TarVal);
											}
										}
									}
								}
							}
						}
					});

					Source.Save(tmp);
				}
				#endregion

				Console.WriteLine("合并完成");
			}
			catch (Exception ee)
			{
				Trace.WriteLine(ee);
				Tip.Message(ee.Message);
			}

		});

		t.Start();
	}

	private void button18_Click(object sender, EventArgs e)
	{
		MyComparer.AliasModify(textBox2.Text, textBox4.Text);
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
		TxtSavePath(sender, e);

		if (File.Exists(this.textBox2.Text))
		{
			this.FilePathes = new string[] { this.textBox2.Text };
		}
	}
	#endregion

	#region	Functions
	private void MenuItem_DevTools_Click(object sender, EventArgs e) => new DevTools.Frm1().Show();

	private void 生成标记ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		NumSelect numSelect = new();
		numSelect.Confirming += new((o, a) => ClearLog(o, a));
		numSelect.Show();
	}

	private void 重新排列ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		OpenFileDialog Open = new();
		if (Open.ShowDialog() != DialogResult.OK) return;

		new Thread(() =>
		{
			try
			{
				Console.WriteLine("       ☆★ 开始计算 ★☆");
				var document = new XmlDocument();
				document.Load(Open.FileName);

				XmlAttributeSort.Sort(document.DocumentElement);
				document.Save(Open.FileName);

				Console.WriteLine("已重新排列");
			}
			catch (Exception ee)
			{
				Console.WriteLine("不是xml文件或者文件存在异常。\n" + ee.Message);
			}

		}).Start();
	}

	private void 生成任务数据ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string OutFolder = @"D:\Blade_and_Soul\contents\Local\NCSoft\data\Export\xml64\files\quest";

		Directory.Delete(OutFolder, true);
		Directory.CreateDirectory(OutFolder);

		//QuestExtension.LoadQuests(MainFolder, (q) => q.Save(OutFolder, Enums.ReleaseSide.Client));
	}
	#endregion

	#region Xml操作
	private void 列出所有属性ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFile = new()
		{
			Filter = "所有文件|*",
			Multiselect = true,
		};

		if (openFile.ShowDialog() == DialogResult.OK)
		{
			List<string> attrs = new();
			foreach (var f in openFile.FileNames)
			{
				XmlDocument xmlDocument = new();
				xmlDocument.Load(openFile.FileName);
				attrs.AddRange(from node in xmlDocument.SelectNodes("table/*").OfType<XmlElement>()
							   from test in node.Property().Attributes
							   where !attrs.Contains(test.Name)
							   select test.Name);
			}

			attrs.Sort(new SortByString());
			attrs.ForEach(a => Console.WriteLine("#notime#" + a));
		}
	}

	private void 列出指定属性范围ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFile = new()
		{
			Filter = "所有文件|*",
			Multiselect = true,
		};

		if (openFile.ShowDialog() == DialogResult.OK)
		{
			List<string> Range = new();
			string AttrName = "skill-result-rule";

			foreach (var f in openFile.FileNames)
			{
				XmlDocument xmlDocument = new();
				xmlDocument.Load(openFile.FileName);
				foreach (var node in xmlDocument.SelectNodes("table/*").OfType<XmlElement>())
				{
					if (node.Attributes[AttrName] != null)
					{
						var Value = node.Attributes[AttrName].Value;
						if (!Range.Contains(Value)) Range.Add(Value);
					}
				}
			}

			//排序整理
			//attrs.Sort(new Xylia.Sort.SortByString());
			Range.ForEach(a => Console.WriteLine("#notime#" + a));
		}
	}

	private void 合并文档ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFile = new()
		{
			Filter = "所有文件|*",
			Multiselect = true
		};



		if (openFile.ShowDialog() == DialogResult.OK)
		{
			Console.WriteLine("正在处理中...");

			new Thread(() =>
			{
				try
				{
					XmlInfo xi = null;

					#region 读取数据
					var Group = new Dictionary<int, List<XmlElement>>();
					var AliasGroup = new Dictionary<string, XmlElement>();


					bool Status = false;
					openFile.FileNames.ToList().ForEach(f =>
					{
						XmlDocument xmlDocument = new();
						xmlDocument.Load(f);

						if (!Status)
						{
							xi = new XmlInfo(xmlDocument.DocumentElement);
							Status = true;
						}

						foreach (XmlElement record in xmlDocument.SelectNodes("table/*"))
						{
							string alias = record.Attributes["alias"]?.Value;


							string MajorKey = null; // xp.Attributes["id", "zone"];
							if (string.IsNullOrWhiteSpace(MajorKey))
							{
								if (!AliasGroup.ContainsKey(alias)) AliasGroup[alias] = record;
								else Console.WriteLine("重复的alias => " + alias);
							}
							else
							{
								int Key = MajorKey.ToInt();
								if (!Group.ContainsKey(Key)) Group.Add(Key, new());

								Group[Key].Add(record);
							}
						}
					});
					#endregion


					if (Group.Count == 0)
					{
						foreach (var t in AliasGroup)
						{
							//属性筛选
							XmlProperty xp = t.Value.Property();

							t.Value.Attributes.RemoveAll();
							foreach (var a in xp.Attributes)
								t.Value.SetAttribute(a.Name, a.Value.ToString());

							xi.AppendChild(t.Value);
						}
					}
					else
					{
						var tmpGroup = Group.ToList();

						tmpGroup.Sort(new SortByKeyNum<List<XmlElement>>());
						tmpGroup.ForEach(g => g.Value.ToList().ForEach(t =>
						{
							//属性筛选
							//XmlProperty xp = t.Property();
							//t.Attributes.RemoveAll();

							//foreach (var a in xp.Attrs)
							//{
							//	if (a.Name != "alias"
							//	&& !a.Name.MyStartsWith("cast-caster-dispel-attribute-")
							//	&& !a.Name.MyStartsWith("melee-counter-dir-")
							//	&& a.Name != "skill-modify-limit"
							//	) continue;

							//	t.SetAttr(a.Name, a.Value);
							//}

							xi.AppendChild(t);
						}));
					}


					string CombinePath = Path.GetDirectoryName(Path.GetDirectoryName(openFile.FileNames[0])) + @"\" +
							 Regex.Replace(openFile.SafeFileNames[0], "[0-9]", "", RegexOptions.IgnoreCase).Replace("..", ".");


					xi.Save($@"{CombinePath}");

					Console.WriteLine("完成");
				}
				catch (Exception ee)
				{
					Tip.Message(ee.ToString());
				}

			}).Start();
		}
	}
	#endregion



	#region Table
	private void button4_Click(object sender, EventArgs e)
	{
		var defs = ConfigLoad.Load(null, File.ReadAllText(textBox11.Text));
		foreach (var def in defs)
		{
			foreach (var attribute in def.ExpandedAttributes)
			{
				Console.WriteLine($"#notime#{attribute.Offset}  -  {attribute.Name}");
			}

			foreach (var sub in def.Subtables)
			{
				foreach (var attribute in sub.ExpandedAttributesSubOnly)
				{
					Console.WriteLine($"#notime#[{sub.Name}] {attribute.Offset}  -  {attribute.Name}");
				}
			}
		}
	}


	private TestSet set;
	private void button5_Click(object sender, EventArgs e)
	{
		try
		{
			set ??= new TestSet();
			set.Output(textBox11.Text.Split('|'));
		}
		catch(Exception ex)
		{
			Console.WriteLine("[error] " + ex);
		}
	}



	private void button12_Click(object sender, EventArgs e)
	{
		string ServerPath = Path.GetDirectoryName(textBox7.Text.Split('|')[0]);
		ServerPath = (checkBox9.Checked ? ServerPath : Path.GetDirectoryName(ServerPath)) + @"\server";
		ServerPath = BNSFileHelper.PublicOutFolder;

		if (Directory.Exists(ServerPath)) Process.Start(ServerPath);
		else Console.WriteLine("#cRed#文件夹不存在，请稍后再试");
	}
	#endregion
}