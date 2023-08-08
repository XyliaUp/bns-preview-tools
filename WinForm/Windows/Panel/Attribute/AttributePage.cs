using System.ComponentModel;
using System.Xml;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

using Xylia.Extension;
using Xylia.Match.Windows.Attribute.Data;
using Xylia.Preview.UI.Resources;

namespace Xylia.Match.Windows.Attribute;

[DesignTimeVisible(false)]
public partial class AttributePage : UserControl
{
	#region Constructor
	const int CHART_MAX_VALUE = 50000;
	const int CHART_INTERVAL = 100;
	const byte DEFAULT_LEVEL = 60;

	public AttributePage()
	{
		InitializeComponent();
		//this.contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomToolStripColorTable());

		label1.Text = null;


		CartesianChart.AxisX.Add(new Axis
		{
			Separator = new Separator() { Step = 5000 },
			MinValue = 0,
		});

		CartesianChart.AxisY.Add(new Axis
		{
			LabelFormatter = val => val.ToString("P3"),
			MinValue = 0,
		});
	}
	#endregion

	#region Load 
	readonly Dictionary<TreeNode, ParaEntity> ParaRelative = new();

	private void Form1_Load(object sender, EventArgs e)
	{
		this.LoadDefaultData();
	}

	private void LoadDefaultData()
	{
		XmlDocument doc = new();
		doc.LoadXml(Resources.Resources.Attribute);

		Dictionary<string, TreeNode> category = new();
		foreach (XmlElement xmlNode in doc.SelectNodes("*/category"))
		{
			var alias = xmlNode.Attributes["alias"]?.Value;
			var text = xmlNode.Attributes["text"]?.Value;

			category[alias] = this.newTreeView1.Nodes.Add(text);
		}


		foreach (var para in ParaEntity.LoadParas(doc))
		{
			if (string.IsNullOrEmpty(para.category) || !category.TryGetValue(para.category, out TreeNode parentNode))
				parentNode = this.newTreeView1.Nodes[0];

			var curNode = new TreeNode(para.type.GetDescription());
			ParaRelative.Add(curNode, para);
			parentNode.Nodes.Add(curNode);
		}

		this.newTreeView1.Nodes[0].Expand();
	}
	#endregion


	#region Functions
	private void newTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		TreeNode SelNode = this.newTreeView1.SelectedNode;
		if (!ParaRelative.TryGetValue(SelNode, out var obj))
			return;

		Level.Visible = label5.Visible = obj.Φ != 0;


		#region Chart	
		byte level = DEFAULT_LEVEL;
		var values = new ChartValues<ObservablePoint>();
		for (int i = 0; i <= CHART_MAX_VALUE; i += CHART_INTERVAL)
			values.Add(new(i, obj.GetPercent(i, level)));

		CartesianChart.Series = new SeriesCollection
		{
			new LineSeries
			{
				Title = $"{this.newTreeView1.SelectedNode.Text} converted percent in Lv{level}",
				Values = values,
				LineSmoothness = 1,
			}
		};
		#endregion
	}

	private void Btn_Start_Click(object sender, EventArgs e)
	{
		#region check
		TreeNode SelNode = this.newTreeView1.SelectedNode;
		if (SelNode is null || !ParaRelative.TryGetValue(SelNode, out var obj))
		{
			Tip.Message($"请先选择计算项目。");
			return;
		}
		#endregion


		//尝试转换基础数值
		if (!double.TryParse(AttritubeValue.Text, out double Value)) return;

		//计算基于的目标等级
		byte level = (byte)(Level.Visible ? Level.Value : DEFAULT_LEVEL);
		double extra = (double)numericUpDown1.Value * 0.01;
		double percent = obj.GetPercent(Value, level) + extra;


		label1.Text = $"在{level}级时所对应的 {SelNode.Text}率:\n{Value} ({percent:0.000%})";
		if (UseCompare.Checked)
		{
			double value2 = Value + AttritubeValue_Extra.Text.ToInt32();
			double percent2 = obj.GetPercent(value2, level) + extra;

			label1.Text += $"\n{value2} ({percent2:0.000%})\n\n差值为 {percent2 - percent:0.000%}";
		}
	}

	private void TextBox1_TextChanged(object sender, EventArgs e)
	{
		if (float.TryParse(AttritubeValue.Text, out float Result))
		{
			AttritubeValue.Text = Result.ToString("N0");
			AttritubeValue.SelectionStart = AttritubeValue.Text.Length;
		}
	}

	private void CheckBox1_CheckedChanged(object sender, EventArgs e)
	{
		this.pictureBox1.Visible = this.AttritubeValue_Extra.Visible = UseCompare.Checked;
	}

	private void NewItem_Click(object sender, EventArgs e)
	{
		new SettingFrm().Show();
	}

	private void TextBox2_TextChanged(object sender, EventArgs e)
	{
		int Value = AttritubeValue_Extra.Text.ToInt32();
		if (Value > 0) this.pictureBox1.Image = Resource_Common.Arrow_Up_24px;
		else if (Value < 0) this.pictureBox1.Image = Resource_Common.Arrow_Down_24px;
		else this.pictureBox1.Image = null;
	}
	#endregion
}