using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using Xylia.Extension;
using Xylia.Preview.Data.Models.Util.Writer;

namespace Xylia.Preview.Tests.DatTool.Windows.DevTools;

public partial class Frm1 : Form
{
	public Frm1()
	{
		InitializeComponent();


		#region Initialize 
		//this.Searcher = new Xylia.Preview.Tests.DatTool.Windows.Controls.Searcher(this.richOut);
		//this.Searcher.Location = new System.Drawing.Point(richOut.Location.X + richOut.Width - 200, 90);
		//this.Searcher.Size = new System.Drawing.Size(400, 200);
		//this.Searcher.BringToFront();

		//this.Page2.Controls.Add(this.Searcher);
		#endregion
	}

	private void DevTools_Shown(object sender, EventArgs e)
	{
		textBox3.Text = Xylia.Configure.Ini.ReadValue("Hex", textBox3.Name);
		textBox4.Text = Xylia.Configure.Ini.ReadValue("Hex", textBox4.Name);
	}

	private void Button4_Click(object sender, EventArgs e)
	{
		richOut.Clear();

		try
		{
			StringBuilder Same = new();
			StringBuilder Diff1 = new();
			StringBuilder Diff2 = new();

			string StrA = textBox3.Text.StringUnzip();
			string StrB = textBox4.Text.StringUnzip();

			if (checkBox3.Checked)
			{
				var BytesA = StrA.ToBytes();
				var BytesB = StrB.ToBytes();


				BytesInfo SameBytes = new();
				BytesInfo DiffBytesA = new();
				BytesInfo DiffBytesB = new();

				for (int i = 0; i < BytesA.Length; i++)
				{
					#region 获得字节数值
					byte A = BytesA[i];

					byte? B = null;
					if (BytesB.Length > i) B = BytesB[i];
					#endregion

					#region 对比
					if (B.HasValue && A == B)
					{
						bool Flag = false;

						if (!DiffBytesA.Any()) Flag = true;
						else
						{
							if (i > 0 && i % 4 == 0)
							{
								richOut.AppendText($"第{DiffBytesA.Index}位\t\t  {DiffBytesA.Data.ToHex(true)}\t\t\t{DiffBytesB.Data.ToHex(true)}\n");

								DiffBytesA.Clear();
								DiffBytesB.Clear();
								Flag = true;
							}
						}

						if (Flag)
						{
							SameBytes.Add(A, i);
							continue;
						}
					}
					#endregion


					#region 
					if (SameBytes.Any())
					{
						richOut.AppendText($"{SameBytes.Data.ToHex()}\n");
						SameBytes.Clear();
					}

					if (i > 0 && i % 4 == 0 && DiffBytesA.Any())
					{
						richOut.AppendText($"第{DiffBytesA.Index}位\t\t  {DiffBytesA.Data.ToHex(true)}\t\t\t{DiffBytesB.Data.ToHex(true)}\n");
						//if (ShowDecimal)
						//{
						//	long? DecimalVal = null;

						//	if (ByteArray.Length == 1) DecimalVal = ByteArray[0];
						//	else if (ByteArray.Length == 2) DecimalVal = BitConverter.ToInt16(ByteArray, 0);
						//	else if (ByteArray.Length == 4) DecimalVal = BitConverter.ToInt32(ByteArray, 0);
						//	else if (ByteArray.Length == 8) DecimalVal = BitConverter.ToInt64(ByteArray, 0);

						//	if (DecimalVal.HasValue) tmp += $"  ({ DecimalVal })";
						//}


						DiffBytesA.Clear();
						DiffBytesB.Clear();
					}



					DiffBytesA.Add(A, i);
					if (B.HasValue) DiffBytesB.Add(B.Value, i);
					#endregion
				}
			}
			else
			{
				for (int f = 0; f < StrA.Length; f++)
				{
					if (StrB.Length > f)
					{
						if (StrA[f] != StrB[f])
						{
							if (Same.Length != 0)
							{
								richOut.AppendText(Same.ToString().StringZip() + "\n");
								Same.Clear();
							}

							Diff1.Append(StrA[f]);
							Diff2.Append(StrB[f]);
						}
						else
						{
							Output(Diff1, Diff2, f - Diff1.ToString().Length);
							Same.Append(StrA[f]);
						}
					}
					else
					{

					}
				}

				if (Same.Length != 0)
				{
					richOut.AppendText(Same.ToString().StringZip() + "\n");
					Same.Clear();
				}

				Output(Diff1, Diff2, StrA.Length - Diff1.ToString().Length);
			}
		}
		catch (Exception ee)
		{
			Xylia.Tip.Message(ee.Message);
		}
	}

	public void Output(StringBuilder diff1, StringBuilder diff2, int Location)
	{
		if (diff1.Length != 0)
		{
			richOut.AppendText($"第{Location + 1}位\t\t{diff1.ToString().StringZip()}\t\t\t{diff2.ToString().StringZip()}\n\n");
			diff1.Clear();
			diff2.Clear();
		}
	}

	private void Btn_Split_Click(object sender, EventArgs e)
	{
		OutIndexs.Clear();


		try
		{
			var data = richOut.Text
				.Replace("\r\n", null)
				.Replace(" ", null)
				.Replace("-", null)
				.StringUnzip()
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

			richOut.Text = output.ToString();
		}
		catch (Exception ee)
		{
			Console.WriteLine(ee.Message);
		}
	}


	private void TextBox3_TextChanged(object sender, EventArgs e)
	{
		HexTextChanged(textBox3, lbl_length4);
	}

	private void TextBox4_TextChanged(object sender, EventArgs e)
	{
		HexTextChanged(textBox4, lbl_length5);
	}

	private static void HexTextChanged(Control HexText, Control LengthShow)
	{
		try
		{
			if (!string.IsNullOrWhiteSpace(HexText.Text))
				LengthShow.Text = $"长度：{HexText.Text.StringUnzip().Length / 2}";

			Xylia.Configure.Ini.WriteValue("Hex", HexText.Name, HexText.Text);
		}
		catch (Exception ee)
		{
			LogWriter.WriteLine(ee);
		}
	}


	private void Lbl_Length1_TextChanged(object sender, EventArgs e)
	{
		lbl_Warning2.Visible = !Equals(textBox3.Text, textBox4.Text);
	}

	private void lbl_Warning3_TextChanged(object sender, EventArgs e)
	{
		lbl_Warning3.Visible = true;
	}

	private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		this.richOut.AppendText((string)Clipboard.GetDataObject().GetData(DataFormats.Text));
	}

	private void Find_Click(object sender, EventArgs e)
	{
		string Txt = richOut.Text;
		if (string.IsNullOrWhiteSpace(Txt))
			return;

		//清理资源
		if (OutIndexs.Count == 0)
		{
			Regex reg = new(RuleText.Text);
			var mat = reg.Match(Txt);

			while (mat.Success)
			{
				OutIndexs.Add(mat.Index);
				LogWriter.WriteLine(mat.Index);  //位置
				mat = reg.Match(Txt, mat.Index + mat.Length);
			}
		}

		OutGoTo();
	}

	private void OutGoTo()
	{
		//异常处理
		if (OutIndexs.Count == 0)
		{
			Console.WriteLine("没有找到对应的数值");
			return;
		}


		//判断是否在范围内
		if (OutIndexs.Count >= IndexId + 1)
		{
			// 移动滚轮位置
			richOut.SelectionStart = OutIndexs[IndexId++];
			richOut.ScrollToCaret();
		}
		else
		{
			IndexId = 0;
			OutGoTo();
		}

		//int rowCount = richOut.GetLineFromCharIndex(richOut.SelectionStart) + 1;
	}

	private readonly List<int> OutIndexs = new();
	private int IndexId;

	private void RuleText_TextChanged(object sender, EventArgs e)
	{
		//清理资源
		OutIndexs.Clear();
		IndexId = 0;
	}



	#region 
	private void Btn_HexToDecimal_Click(object sender, EventArgs e)
	{
		lbl_Warning3.Text = null;
		string text = Txt_HEX.Text.Replace('-' , ' ');

		try
		{
			if (radioButton2.Checked)
			{
				Txt_Decimal.Text = long.Parse(text, System.Globalization.NumberStyles.HexNumber).ToString();
				return;
			}


			var bytes = text.ToBytes();
			CommonConvert CC = new(bytes);

			if (CC.Data.Length == 2) Txt_Decimal.Text = CC.Short1.ToString();
			else if (CC.Data.Length >= 8) Txt_Decimal.Text = CC.Long.ToString();
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



			byte[] Source = null;

			if (long.TryParse(Txt_Decimal.Text, out long result))
			{
				CommonConvert CC = new(result);
				if (result < int.MaxValue) CC = new((int)result);

				label2.Text = "Short型：" + CC.Short1 + " | " + CC.Short2 + "\nFloat型：" + CC.Float;
				Source = CC.Data;
			}
			else if (float.TryParse(Txt_Decimal.Text, out float result2))
			{
				Source = BitConverter.GetBytes(result2);
			}





			//转换为Hex信息
			if (Source != null)
			{
				Txt_HEX.Text = Source.ToHex(true);
			}
		}
		catch (Exception ee)
		{
			lbl_Warning3.Text = ee.Message;
			Console.WriteLine(ee.Message);
		}
	}

	private void Txt_HEX_TextChanged(object sender, EventArgs e)
	{
		if (((Control)sender) == this.ActiveControl)
		{
			Btn_HexToDecimal_Click(null, null);
		}
	}

	private void Txt_Decimal_TextChanged(object sender, EventArgs e)
	{
		if (((Control)sender) == this.ActiveControl)
		{
			Btn_DecimalToHex_Click(null, null);
		}
	}
	#endregion


	private void button6_Click(object sender, EventArgs e)
	{
		#region 获取数值信息
		List<KeyValuePair<string, List<int>>> info = new();

		string CurItemSkill = null;
		List<int> data = new();

		foreach (var line in richTextBox2.Lines)
		{
			if (line.MyStartsWith("ItemSkill_"))
			{
				if (data.Count != 0)
				{
					info.Add(new KeyValuePair<string, List<int>>(CurItemSkill, data));
					data = new List<int>();
				}

				CurItemSkill = line;
			}
			else
			{
				Regex regex = new(@"[1-9]\d*%");
				if (regex.IsMatch(line))
				{
					var PowerPercent = int.Parse(regex.Match(line).Value.Replace("%", null));
					data.Add(PowerPercent);
				}
			}
		}

		//插入最后一个值
		if (data.Count != 0)
		{
			info.Add(new KeyValuePair<string, List<int>>(CurItemSkill, data));
			data = new List<int>();
		}

		if (info.Count == 0)
		{
			Tip.Message("无效信息");
			return;
		}
		#endregion

		#region 插入属性
		XmlDocument tmp = new();
		tmp.LoadXml($"<?xml version=\"1.0\"?>\n<table>{richTextBox3.Text?.Trim()}</table>");

		XmlNodeList records = tmp.SelectNodes("table/record");

		//获取周期数
		double Recycle = 0;
		if (radioButton3.Checked) Recycle = records.Count / info.Count;
		else if (radioButton4.Checked) Recycle = tmp.SelectNodes("table/record[not(@flag-1)]").Count / info.Count;


		if (radioButton3.Checked)
		{
			if (records.Count % info.Count != 0)
			{
				if (MessageBox.Show($"信息的数量不一致，是否仍然继续？ (info:{info.Count},records:{records.Count})", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
				{
					Recycle = 1;
				}
				else return;
			}
		}
		else if (radioButton4.Checked)
		{
			//if (records.sele.s.Count % info.Count != 0)
			//{
			//	Tip.Message($"数量不一致 (info:{ info.Count },records:{ records.Count })");
			//	return;
			//}
		}
		else
		{
			Tip.Message($"模式无效");
			return;
		}



		int idx = 0;
		foreach (XmlElement record in records)
		{
			if (radioButton4.Checked && record.Attributes["flag-1"] is not null) continue;

			idx++;

			record.RemoveAttribute("power-percent-max");
			record.RemoveAttribute("power-percent-min");
			record.RemoveAttribute("broadcast-type");

			//真气石模式
			if (radioButton3.Checked)
			{
				//获取目标索引
				var InfoIdx = Math.Min(info.Count, (int)Math.Ceiling(idx / Recycle)) - 1;
				if (InfoIdx >= info.Count) throw new ArgumentException("超出分组数量");
				var Info = info[InfoIdx];

				var ValueIdx = (int)numericUpDown1.Value - 1;
				if (ValueIdx >= Info.Value.Count) throw new ArgumentException($"超出组内部数量 ([{InfoIdx + 1}] >> {ValueIdx + 1})");
				var Value = Info.Value[ValueIdx];

				record.SetAttribute("power-percent-max", Value.ToString());
				record.SetAttribute("power-percent-min", Value.ToString());
				record.SetAttribute("broadcast-type", "normal");
			}

			//手镯模式
			else if (radioButton4.Checked)
			{
				var InfoIdx = (int)Math.Floor((idx - 1) / Recycle);
				if (InfoIdx >= info.Count) throw new ArgumentException("超出分组数量");
				var Info = info[InfoIdx];


				var ValueIdx = (int)((idx - 1) % Recycle);
				if (ValueIdx >= Info.Value.Count) throw new ArgumentException($"超出组内部数量 ([{InfoIdx + 1}] >>{ValueIdx + 1})");
				var Value = Info.Value[ValueIdx];

				record.SetAttribute("power-percent-max", Value.ToString());
				record.SetAttribute("power-percent-min", Value.ToString());
				record.SetAttribute("broadcast-type", "normal");
			}
		}
		#endregion

		string Result = "  " + tmp.SelectSingleNode("table").InnerXml.Replace("/>", "/>\n  ");
		Clipboard.SetText(Result);
	}




	public static string GetAsc2Code(string txt)
	{
		string result = null;
		foreach (var t in txt) result += "\\u" + (int)t + "  ";

		return result;
	}

	private void button11_Click(object sender, EventArgs e)
	{
		string Data = richTextBox3.Text.TrimEnd();

		int StartLevel = (int)numericUpDown2.Value;
		int EndLevel = (int)numericUpDown3.Value;

		StringBuilder sb = new();

		if (StartLevel == EndLevel) return;
		else if (StartLevel < EndLevel)
		{
			for (int i = StartLevel; i <= EndLevel; i++)
			{
				string Text = Data
					.Replace("Lv" + StartLevel, "Lv" + i)
					.Replace($"第{StartLevel}位", $"第{i}位")
					//.Replace(StartLevel.ToString(), i.ToString())
					;

				sb.Append(Text + "\n");
			}
		}
		else if (StartLevel > EndLevel)
		{
			for (int i = StartLevel; i >= EndLevel; i--)
			{
				string Text = Data.Replace("Lv" + StartLevel, "Lv" + i);
				sb.Insert(0, Text + "\n");
			}
		}

		Clipboard.SetText(sb.ToString());
	}

	private void radioButton3_CheckedChanged(object sender, EventArgs e)
	{
		this.numericUpDown1.Visible = this.radioButton3.Checked;
	}


	#region 其他功能 
	private void button7_Click(object sender, EventArgs e)
	{
		if (true)
		{
			var sb = new StringBuilder();
			foreach (var line in this.richTextBox1.Text.Split('\n').Where(l => !string.IsNullOrWhiteSpace(l)))
			{
				string[] ls = Regex.Split(line, "\\s+");
				sb.Append($"{ls[0]}=\"{ls[1].Replace("%", null)}\" ");
			}

			this.richTextBox1.Text = $"  <record {sb}/>\n";
		}
	}

	private void button8_Click(object sender, EventArgs e)
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

	private void button3_Click(object sender, EventArgs e) => richTextBox1.Text = CreateClass.Instance(richTextBox1.Text);

	private void button4_Click_1(object sender, EventArgs e)
	{
		StringBuilder result = new();

		XmlDocument tmp = new();
		tmp.LoadXml($"<?xml version=\"1.0\"?>\n<table>{richTextBox1.Text?.Trim()}</table>");

		foreach (XmlNode record in tmp.SelectNodes("table/case"))
		{
			var Alias = record.Attributes["alias"].Value;
			var Name = record.Attributes["name"]?.Value;
			var Default = record.Attributes["default"]?.Value.ToBool();


			if (Alias.Contains('-')) result.Append($"[Signal(\"{Alias}\")]\n");
			if (!string.IsNullOrWhiteSpace(Name)) result.Append($"[Description(\"{Name}\")]\n");

			result.Append($"{Alias.TitleCase()},\n\n");
		}

		richTextBox1.Text = result.ToString().RemoveSuffixString("\n\n");
	}
	#endregion
}