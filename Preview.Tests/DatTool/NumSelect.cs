using System.Xml.Linq;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.BinData.Definitions;

namespace Xylia.Preview.Tests.DatTool;
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

	private void Btn_Confirm_Click(object sender, EventArgs e)
	{
		Confirming?.Invoke(sender, e);

		#region Initialize
		int StartVal = (int)this.StartVal.Value;
		int LoopOrEndVal = (int)this.EndVal.Value;

		var type = this.comboBox1.Text.ToEnum<AttributeType>();
		int Interval = AttributeDefinition.GetSize(type, true);
		#endregion

		#region Create
		int CurIdx = StartVal;
		while (true)
		{
			if (CurIdx > LoopOrEndVal) break;

			var xe = new XElement("attribute");
			xe.SetAttributeValue("name", "unk-");
			xe.SetAttributeValue("type", type.ToString()[1..]);

			if (radioButton1.Checked) xe.SetAttributeValue("start", CurIdx);
			else if (radioButton3.Checked) xe.SetAttributeValue("start-show", CurIdx);

			if (type == AttributeType.TRef) xe.SetAttributeValue("ref", "");

			Console.WriteLine($"#notime##repeat#		{xe}");

			CurIdx += Interval;
		}
		#endregion
	}
}