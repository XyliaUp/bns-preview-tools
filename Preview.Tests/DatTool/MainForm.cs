using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using Newtonsoft.Json;

using Xylia.Configure;
using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Engine.DatData.Third;
using Xylia.Preview.Data.Engine.ZoneData.RegionData;
using Xylia.Preview.Data.Engine.ZoneData.TerrainData;
using Xylia.Preview.Tests.TableTests;

using static Xylia.Preview.Data.Engine.DatData.Third.MySpport;

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

		this.trackBar1.Value = this.trackBar1.Minimum;
		this.txbDatFile.Text = Ini.Instance.ReadValue("Path", "Data_DatFile");
		this.txbRpFolder.Text = Ini.Instance.ReadValue("Path", "Data_OutFolder");

		ReadConfig(this);
	}
	#endregion

	#region TestFunc
	private void SaveConfig(object sender, EventArgs e)
	{
		var c = (Control)sender;
		Ini.Instance.WriteValue("Config", $"{c.FindForm().Name}_{c.Name}", c.Text);
	}

	public static void ReadConfig(Control container)
	{
		foreach (Control c in container.Controls)
		{
			ReadConfig(c);

			var val = Ini.Instance.ReadValue("Config", $"{c.FindForm().Name}_{c.Name}");
			if (string.IsNullOrWhiteSpace(val)) continue;

			if (c is CheckBox checkBox) checkBox.Checked = val.ToBool();
			else c.Text = val;
		}
	}


	private void SaveCheckStatus(object sender, EventArgs e)
	{
		var ctl = (CheckBox)sender;
		Ini.Instance.WriteValue("Config", this.Name + "_" + ctl.Name, ctl.Checked);
	}

	private static DateTime m_LastTime = DateTime.MinValue;
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
		Ini.Instance.WriteValue("Path", "Data_DatFile", Text);

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
		Ini.Instance.WriteValue("Path", "Data_OutFolder", ((TextBox)sender).Text);
	}

	private void textBox8_TextChanged(object sender, EventArgs e)
	{
		if (Directory.Exists(((TextBox)sender).Text)) Ini.Instance.WriteValue("Server", "Folder", ((TextBox)sender).Text);
	}


	private void bntSearchDat_Click(object sender, EventArgs e) => OpenPath(txbDatFile);

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

		string datpath = txbDatFile.Text;
		string outpath = txbRpFolder.Text ?? Path.GetDirectoryName(datpath) + @"\Export\" + Path.GetFileNameWithoutExtension(datpath) + @"\files";

		// output
		Task.Run(() =>
		{
			Parallel.ForEach(new BNSDat(datpath).FileTable, file =>
			{
				string path = Path.Combine(outpath, file.FilePath);
				Directory.CreateDirectory(Path.GetDirectoryName(path));

				File.WriteAllBytes(path, file.Data);
			});
		});
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
				PackParam param = new()
				{
					Aes = KeyInfo.AES_2020_05,
					FolderPath = outdir,
					PackagePath = txbDatFile.Text,
					Bit64 = txbDatFile.Text.Judge64Bit(),
					CompressionLevel = (BnsCompression.CompressionLevel)this.trackBar1.Value,
				};

				if (checkBox1.Checked) MySpport.Pack(param);
				else BNSDat.Pack(param);
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
			foreach (var attribute in def.ElRecord.ExpandedAttributes)
			{
				Console.WriteLine($"#notime#{attribute.Offset}  -  {attribute.Name}");
			}

			foreach (var sub in def.ElRecord.Subtables)
			{
				foreach (var attribute in sub.ExpandedAttributesSubOnly)
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

	#region Zone
	private void button37_Click(object sender, EventArgs e) => OpenPath(Txt_Cterrain_Path);

	private void button31_Click(object sender, EventArgs e) => OpenPath(Txt_Region_Path);

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
		Task.Run(() =>
		{
			var files = new DirectoryInfo(textBox8.Text).GetFiles();
			foreach (FileInfo fi in files)
			{
				if (fi.Name.EndsWith("xml") || fi.Name.EndsWith(".x16"))
				{
					using FileStream fileStream = new(fi.FullName, FileMode.Open);
					//File.WriteAllBytes(fi.FullName, BXML_CONTENT.Convert(fileStream, BXML_TYPE.BXML_BINARY).ToArray());
				}
			}
		});
	}

	public static void ServerUnpack(FileInfo[] fis)
	{
		foreach (FileInfo fi in fis)
		{
			if (fi.Name.EndsWith("xml") || fi.Name.EndsWith(".x16"))
			{
				//using FileStream fileStream = new(fi.FullName, FileMode.Open);
				//using MemoryStream memoryStream = BXML_CONTENT.Convert(fileStream, BXML_TYPE.BXML_PLAIN);

				//File.WriteAllBytes(fi.FullName, memoryStream.ToArray());
			}
		}
	}
	#endregion

	#region Combine
	private void button10_Click(object sender, EventArgs e) => OpenPath(textBox4, null, false);


	IEnumerable<string> FilePathes = null;

	private void button9_Click(object sender, EventArgs e)
	{
		if ((FilePathes = OpenPath(textBox2, null, true)) != null)
			this.textBox2.Text = "{对象集合}";
	}

	private void button17_Click(object sender, EventArgs e) => OpenPath(textBox6);

	private void checkBox12_CheckedChanged(object sender, EventArgs e) => checkBox12.Text = checkBox12.Checked ? "alias作为主键" : "id作为主键";

	private void checkBox14_CheckedChanged(object sender, EventArgs e) => this.label8.Visible = this.textBox6.Visible = this.button17.Visible = this.checkBox14.Checked;

	private void button11_Click(object sender, EventArgs e)
	{

	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
		SaveConfig(sender, e);

		if (File.Exists(this.textBox2.Text))
		{
			this.FilePathes = new string[] { this.textBox2.Text };
		}
	}
	#endregion


	#region	Xml操作
	private void MenuItem_DevTools_Click(object sender, EventArgs e) => new ToolFrm().Show();

	private void ToolStripMenuItem_NumSelect_Click(object sender, EventArgs e)
	{
		var frm = new NumSelect();
		frm.Confirming += ClearLog;
		frm.Show();
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

				//XmlAttributeSort.Sort(document.DocumentElement);
				document.Save(Open.FileName);

				Console.WriteLine("已重新排列");
			}
			catch (Exception ee)
			{
				Console.WriteLine("不是xml文件或者文件存在异常。\n" + ee.Message);
			}

		}).Start();
	}

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

				foreach (var node in xmlDocument.SelectNodes("table/*").OfType<XmlElement>())
				{
					foreach (XmlAttribute attr in node.Attributes)
					{
						if (!attrs.Contains(attr.Name))
							attrs.Add(attr.Name);
					}
				}
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
							//xi = new XmlInfo(xmlDocument.DocumentElement);
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
								int Key = MajorKey.ToInt32();
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
							//XmlProperty xp = t.Value.Property();

							//t.Value.Attributes.RemoveAll();
							//foreach (var a in xp.Attributes)
							//	t.Value.SetAttribute(a.Name, a.Value.ToString());

							//xi.AppendChild(t.Value);
						}
					}
					else
					{
						var tmpGroup = Group.ToList();

						//tmpGroup.Sort(new SortByKeyNum<List<XmlElement>>());
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

							//xi.AppendChild(t);
						}));
					}


					string CombinePath = Path.GetDirectoryName(Path.GetDirectoryName(openFile.FileNames[0])) + @"\" +
							 Regex.Replace(openFile.SafeFileNames[0], "[0-9]", "", RegexOptions.IgnoreCase).Replace("..", ".");


					//xi.Save($@"{CombinePath}");

					Console.WriteLine("完成");
				}
				catch (Exception ee)
				{
					MessageBox.Show(ee.ToString());
				}

			}).Start();
		}
	}
	#endregion




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
}