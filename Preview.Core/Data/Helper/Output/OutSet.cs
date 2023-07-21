using System.Configuration;

using BnsBinTool.Core.Definitions;

using NPOI.SS.UserModel;

using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Models.BinData.Table.Config;
using Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
using Xylia.Preview.Data.Record;
using Xylia.Workbook;
using Xylia.Xml;

using static Xylia.Preview.Data.Helper.Output.OutSetTable;

namespace Xylia.Preview.Data.Helper.Output;
public sealed class OutSet<T> : OutBase where T : IOut, new()
{
	protected override string Name => Table.type ?? typeof(T).Name;
	private readonly OutSetTable Table;
	public Func<T, bool> Filter;


	#region Constructor
	public OutSet() : this(new T().OutTable())
	{

	}

	public OutSet(string XmlString) : this(XmlString.GetObject<OutSetTable>("table"))
	{

	}

	private OutSet(OutSetTable table)
	{
		this.Table = table;
	}
	#endregion


	protected override void CreateData()
	{
		#region check
		if (Table.attribute.Count == 0)
			throw new ConfigurationErrorsException("未配置属性");

		//expand to
		var Attributes = new List<OutSetTable.Attribute>();
		foreach (var attribute in Table.attribute)
		{
			if (attribute.repeat == 1) Attributes.Add(attribute);
			else
			{
				for (int i = 1; i <= attribute.repeat; i++)
				{
					var tmp = attribute.Clone();
					tmp.name = $"{attribute.name}-{i}";
					tmp.text = string.IsNullOrWhiteSpace(attribute.text) ? null : $"{attribute.text} {i}";
					tmp.extra = string.IsNullOrWhiteSpace(attribute.extra) ? null : $"{attribute.extra}-{i}";

					Attributes.Add(tmp);
				}
			}
		}

		//files
		var files = new Dictionary<string, FileConfig>();
		Table.file.ForEach(o => files[o.alias] = o);
		bool HasGroup = files.Any();

		// the code only get program-defined table
		data ??= Table.type.CastTable() ?? throw new ConfigurationErrorsException($"table `{Table.type}` not found!");
		#endregion

		#region content
		//title
		if (HasGroup) ExcelInfo.SetColumn("group");
		foreach (var attribute in Attributes)
		{
			var name = attribute.text;
			if (string.IsNullOrWhiteSpace(name))
				name = attribute.name;

			ExcelInfo.SetColumn(name, attribute.width);
		}

		//records
		foreach (BaseRecord record in data)
		{
			if (record is T instance && Filter != null && Filter(instance)) continue;


			//group
			if (HasGroup && record.TryGetParam("alias", out object alias))
			{
				if (files.TryGetValue(alias.ToString(), out var config))
				{
					var grow = ExcelInfo.CreateRow();
					grow.AddCell($"{Name}Data" +
						(string.IsNullOrWhiteSpace(config.name) ? null : $"_{config.name}"));
				}
			}

			//row
			var row = ExcelInfo.CreateRow();
			if (HasGroup) row.AddCell("");

			Attributes.ForEach(attr => CreateAttribute(row, record, attr));
		}
		#endregion



		//	if (Row >= 1000000)
		//	{
		//		Row = 0;
		//		excel.MainSheet = CreateOutputSheet(excel.Workbook, "汉化文档_" + (excel.Workbook.NumberOfSheets + 1));
		//	}
	}

	private void CreateAttribute(IRow row, BaseRecord record, OutSetTable.Attribute attribute)
	{
		if (record.Attributes is not DbData db) throw new NotImplementedException();
		if (!db.ContainsKey(attribute.name, out var def, out var value))
		{
			row.AddCell("#INVALID");
			return;
		}
		else if (value is null)
		{
			row.AddCell(value);
			return;
		}


		// hoping to use define info instead of real class field
		// influence: CastObject / Seq 

		//ref
		if (def.Type == AttributeType.TRef && def is AttributeDef temp)
		{
			var _ref = value.CastObject<BaseRecord>(temp.ReferedTableName);
			value = _ref is Text text ? text.GetText() : _ref.GetName();
		}

		//extra
		if (!string.IsNullOrWhiteSpace(attribute.extra) && db.ContainsKey(attribute.extra, out _, out var value2))
		{
			if (int.TryParse(value2, out var count) && count != 0) value += " " + count;
		}

		row.AddCell(value);
	}
}