using System.ComponentModel;
using System.Data;

using HZH_Controls.Controls;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Table.Record;

namespace Xylia.Preview.Art.GameUI.Scene.Game_ToolTip.Game_ToolTipScene.ItemTooltipPanel;
public partial class DataGridScene : Form
{
	#region Constructor
	readonly DataTable table;
	const string field1 = "key";
	const string field2 = "value";

	public DataGridScene(IEnumerable<KeyValuePair<string, string>> attrs)
	{
		this.InitializeComponent();

		#region columns
		var columns = new List<DataGridViewColumnEntity>
		{
			new() { DataField = field1, Width = 40, WidthType = SizeType.Percent /*, Format = ParamTable.Convert*/  },
			new() { DataField = field2, Width = 60, WidthType = SizeType.Percent },
		};

		var resources = new ComponentResourceManager(typeof(DataGridScene));
		foreach (var column in columns)
		{
			column.HeadText = resources.GetString("DataField_" + column.DataField);
		}

		this.dataGridView.Columns = columns;
		#endregion


		this.table = GetSource(attrs.Select(a => new Fields(a)));
	}
	#endregion

	#region Functions
	private void DataGridScene_SizeChanged(object sender, EventArgs e)
	{
		this.dataGridView.Height = this.ClientSize.Height - 50;
	}

	private void Filter_SearchClick(object sender, EventArgs e)
	{
		List<Fields> pairs = new();
		foreach (DataRow row in table.Rows)
		{
			var key = (string)row[field1];
			var value = (string)row[field2];

			pairs.Add(new(key, value));
		}


		GetSource(pairs, (o) =>
			string.IsNullOrWhiteSpace(this.Filter.InputText) ||
			o.Key.MyContains(this.Filter.InputText));
	}

	private DataTable GetSource(IEnumerable<Fields> pairs, Func<Fields, bool> func = null)
	{
		var table = new DataTable();
		table.Columns.Add(field1);
		table.Columns.Add(field2);

		foreach (var attr in pairs)
		{
			if (func != null && !func(attr))
				continue;

			DataRow row = table.NewRow();
			row[field1] = attr.Key;
			row[field2] = attr.Value;
			table.Rows.Add(row);
		}

		this.dataGridView.DataSource = table;
		return table;
	}
	#endregion


	public class Fields : IAttribute
	{
		public Fields(string Key, string Value) : base(Key, Value)
		{

		}

		public Fields(KeyValuePair<string, string> pair) : base(pair)
		{

		}
	}
}