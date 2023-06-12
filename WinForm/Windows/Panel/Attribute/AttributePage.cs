using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

using Xylia.Extension;
using Xylia.Match.Properties;
using Xylia.Match.Windows.Attribute.Data;
using Xylia.Preview.Resources;

namespace Xylia.Match.Windows.Attribute
{
	[DesignTimeVisible(false)]
	public partial class AttributePage : UserControl
	{
		#region Constructor
		public AttributePage()
		{
			InitializeComponent();
			//this.contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomToolStripColorTable());

			int i = 150;
			this.ucWaveChart1.AddSource(i.ToString(), i);
			this.ucWaveChart1.AddSource(i.ToString(), i);
			this.ucWaveChart1.AddSource(i.ToString(), i);
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
			doc.LoadXml(Resources.Attribute);


			Dictionary<string, TreeNode> category = new();
			foreach (XmlElement xmlNode in doc.SelectNodes("*/category"))
			{
				var alias = xmlNode.Attributes["alias"]?.Value;
				var text = xmlNode.Attributes["text"]?.Value;

				category[alias] = this.newTreeView1.Nodes[0].Nodes.Add(text);
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

			Level.Visible = obj.Φ != 0;
		}

		private void Btn_Start_Click(object sender, EventArgs e)
		{
			#region 初始校验
			TreeNode SelNode = this.newTreeView1.SelectedNode;
			if (SelNode is null || !ParaRelative.ContainsKey(SelNode))
			{
				Tip.Message($"请先选择计算项目。");
				return;
			}

			//目标参数
			var TarPara = ParaRelative[SelNode];
			#endregion


			//尝试转换基础数值
			if (!double.TryParse(AttritubeValue.Text, out double Value)) return;


			byte level = (byte)(Level.Visible ? Level.Value : 60);       //计算基于的目标等级
			double extra = (double)numericUpDown1.Value * 0.01;          //额外百分比
			double percent = TarPara.GetPercent(Value, level) + extra;   //Property百分比



			label1.Text = $"在{Level.Value}级时所对应的 {SelNode.Text}率:\n{Value} ({percent:0.000%})";
			if (UseCompare.Checked)
			{
				double value2 = Value + AttritubeValue_Extra.Text.ToInt();
				double percent2 = TarPara.GetPercent(value2, level) + extra;

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
			int Value = AttritubeValue_Extra.Text.ToInt();
			if (Value > 0) this.pictureBox1.Image = Resource_Common.Arrow_Up_24px;
			else if (Value < 0) this.pictureBox1.Image = Resource_Common.Arrow_Down_24px;
			else this.pictureBox1.Image = null;
		}
		#endregion
	}
}