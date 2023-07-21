using System.Diagnostics;
using System.Xml.Linq;

using BnsBinTool.Core.Definitions;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Table.Config;

namespace Xylia.Preview.Tests.DatTool.Windows;

public partial class NumSelect : Form
{
	public NumSelect() => InitializeComponent();


	public event EventHandler Confirming;



	private void NumSelect_Load(object sender, EventArgs e)
	{
		foreach (var @type in Enum.GetValues(typeof(AttributeType)))
			comboBox1.Items.Add(@type.ToString());

		comboBox1.Text = AttributeType.TInt32.ToString();
	}

	private void checkBox2_CheckedChanged(object sender, EventArgs e)
	{
		this.label5.Text = this.checkBox2.Checked ? "结束编号" : "终止索引";
	}

	private void Btn_Confirm_Click(object sender, EventArgs e)
	{
		Confirming?.Invoke(sender, e);

		try
		{
			#region Initialize
			int StartVal = (int)this.StartVal.Value;
			int LoopOrEndVal = (int)this.EndVal.Value;

			var type = this.comboBox1.Text.ToEnum<AttributeType>();
			int Interval = AttributeDef.GetSize(type, true);
			#endregion

			#region 实际处理
			int InfoIndex = 0;
			int CurIdx = StartVal;
			while (true)
			{
				//判断是否结束
				if (this.checkBox2.Checked && InfoIndex == LoopOrEndVal) break;
				if (!this.checkBox2.Checked && CurIdx > LoopOrEndVal) break;

				//增加编号
				InfoIndex++;

				#region name
				string name = "unk-";
				string desc = null;

				if (!string.IsNullOrWhiteSpace(this.textBox1.Text)) name = this.textBox1.Text + InfoIndex;
				if (!string.IsNullOrWhiteSpace(this.textBox2.Text)) desc = this.textBox2.Text + InfoIndex;
				#endregion


				var xe = new XElement("record");
				xe.SetAttributeValue("name", name);
				xe.SetAttributeValue("type", type.ToString()[1..]);

				if (radioButton1.Checked) xe.SetAttributeValue("start", CurIdx);
				else if (radioButton3.Checked) xe.SetAttributeValue("start-show", CurIdx);

				if (desc != null) xe.SetAttributeValue("desc", desc);
				if (type == AttributeType.TRef) xe.SetAttributeValue("ref", this.textBox3.Text.Trim());

				Console.WriteLine($"#notime##repeat#		{xe}");

				CurIdx += Interval;
			}
			#endregion
		}
		catch (Exception ee)
		{
			Trace.WriteLine(ee);
		}
	}
}