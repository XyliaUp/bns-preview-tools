using System.Diagnostics;

using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.BinData.Helpers;
public class TableCollection : List<Table>
{
	Dictionary<short, Table> _tableByType;
	Dictionary<string, Table> _tableByName;

	public Table this[short index]
	{
		get
		{
			lock (this)
			{
				return (_tableByType ??= this.Where(x => x.Type > 0).ToDictionary(o => o.Type))
					 .GetValueOrDefault(index);
			}
		}
	}

	public Table this[string index]
	{
		get
		{
			lock (this)
			{
				if (string.IsNullOrWhiteSpace(index)) return default;
				if (short.TryParse(index, out var type)) return this[type];
				if (index.Equals("skill", StringComparison.OrdinalIgnoreCase)) index = "skill3";

				var table = (_tableByName ??= this.ToDictionary(x => x.Name, new TableNameComparer())).GetValueOrDefault(index);
				if (table is null) Debug.WriteLine($"Invalid typed reference, refered table doesn't exist: '{index}'");

				return table;
			}
		}
	}





	public Record GetRef(short type, Ref Ref)
	{
		if (Ref == default) return null;

		return this[type]?[Ref, false];
	}

	public string GetRef(IconRef Ref)
	{
		if (Ref == default) return null;

		var table = this["icontexture"];
		return table?[Ref, false] + $",{Ref.IconTextureIndex}";
	}

	public string GetRef(TRef Ref)
	{
		if (Ref == default) return null;

		var table = this[(short)Ref.Table];
		return $"{table.Name}:{table?[Ref, false]}";
	}


	public Record GetRecord(string table, string alias) => this[table]?[alias];

	public Record GetRecord(string value)
	{
		var array = value.Split(':', 2);
		if (array.Length < 2)
		{
			Debug.WriteLine($"TRef get failed, value: {value}");
			return null;
		}

		return GetRecord(array[0], array[1]);
	}

	public Record GetIconRecord(string value, out ushort index)
	{
		index = 0;

		var colon = value.LastIndexOf(',');
		if (colon == -1) return null;

		var split = new[] { value[..colon], value[(colon + 1)..] };
		index = ushort.Parse(split[1]);
		return GetRecord("icontexture", split[0]);
	}
}