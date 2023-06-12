using System.Data;

using HZH_Controls.Controls;

using Xylia.Preview.Data.Models.BinData.Table.Attributes;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	public partial class DataGridScene : Form
	{
		public DataGridScene(ParamTable ParamTable, IAttributeCollection Attributes)
		{
			this.InitializeComponent();

			string field1 = "key";
			string field2 = "value";

			this.dataGridView.Columns = new List<DataGridViewColumnEntity>
			{
				new() { DataField = field1, HeadText = "字段", Width = 40, WidthType = SizeType.Percent , Format = o => ParamTable.Convert(o) },
				new() { DataField = field2, HeadText = "数值", Width = 60, WidthType = SizeType.Percent },
			};


			DataTable dt = new();
			dt.Columns.Add(field1);
			dt.Columns.Add(field2);

			foreach(var a in Attributes)
			{
				DataRow dr = dt.NewRow();
				dr[field1] = a.Key;
				dr[field2] = a.Value;

				dt.Rows.Add(dr);
			}

			this.dataGridView.DataSource = dt;
		}


		public class ParamTable
		{
			public Dictionary<string, string> ParamDef = new(StringComparer.InvariantCultureIgnoreCase);
			
			public string Convert(object ParamKey)
			{
				string ParamName = ParamKey.ToString();
				if (ParamDef != null && !string.IsNullOrWhiteSpace(ParamName))
				{
					if (ParamDef.TryGetValue(ParamName, out var Name))
						return Name;
				}

				return ParamName;
			}
		}
	}
}