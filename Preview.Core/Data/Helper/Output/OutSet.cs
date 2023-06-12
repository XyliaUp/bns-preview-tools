using System;
using System.Collections.Generic;
using System.Configuration;

using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Workbook;
using Xylia.Xml;

namespace Xylia.Preview.Data.Helper.Output;

public class OutSet : OutBase
{
	#region Constructor
	public OutSet()
	{
		this.Table = GetTable();
	}

	public OutSet(string XmlString)
	{
		this.Table = GetTable() ?? XmlString.GetObject<OutSetTable>("table");
	}
	#endregion


	protected virtual OutSetTable GetTable() => null;

	protected readonly OutSetTable Table;

	protected override string Name => Table.type;

	protected override void CreateData()
	{
		#region check
		if (Table.attribute.Count == 0) throw new ConfigurationErrorsException("未配置属性");

		var table = Table.type.CastTable();
		if (table is null) throw new ConfigurationErrorsException("指定表不存在");

		//expand to
		var Attributes = new List<OutSetTable.Attribute>();
		foreach (var attribute in Table.attribute)
		{
			if (attribute.repeat <= 0) Attributes.Add(attribute);
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
		#endregion

		#region content
		foreach (var attribute in Attributes)
		{
			var name = attribute.text;
			if (string.IsNullOrWhiteSpace(name)) name = attribute.name;

			ExcelInfo.SetColumn(name);
		}

		foreach (var record in table)
		{
			var row = ExcelInfo.CreateRow();
			foreach (var attribute in Attributes)
			{
				if (!record.TryGetParam(attribute.name, out object value))
				{
					row.AddCell("#INVALID");
					continue;
				}
				else if (value is null)
				{
					row.AddCell(value);
					continue;
				}
				else if (value is Enum @enum) value = @enum.GetName() ?? value;



				//ref
				if (!string.IsNullOrWhiteSpace(attribute.Ref))
				{
					value = value.ToString().CastObject(attribute.Ref).GetName();
				}

				//extra
				if (!string.IsNullOrWhiteSpace(attribute.extra))
				{
					var extra = record.GetParam(attribute.extra);

					if (value is AttachAbility ability) value = ability.GetName((int)extra);
					else if (int.TryParse(extra.ToString(), out var count)) value += " " + GetCount(count);

				}

				row.AddCell(value);
			}
		}
		#endregion

		#region adjust column
		var sheet = ExcelInfo.MainSheet;
		for (int c = 0; c < Attributes.Count; c++)
		{
			if (sheet.GetColumnWidth(c) != 2560) continue;

			sheet.AutoSizeColumn(c);
			sheet.SetColumnWidth(c, (int)(1.2F * sheet.GetColumnWidth(c)));
		}
		#endregion
	}
}


public class OutSet<T> : OutSet where T : IOut, new()
{
	protected override string Name => typeof(T).Name;

	protected override OutSetTable GetTable() => new T().OutTable();
}