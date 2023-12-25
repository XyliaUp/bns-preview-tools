using System.Diagnostics;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Document;

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
				return (_tableByType ??= this.Where(x => x.Type > 0).ToDistinctDictionary(o => o.Type, null))
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

				var table = (_tableByName ??= this.ToDistinctDictionary(x => x.Name, new TableNameComparer())).GetValueOrDefault(index);
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

		// possible return null if it is a xml table
		// actually, this is a definition issue
		var table = this[(short)Ref.Table];
		if (table is null) return Ref.ToString();

		return $"{table.Name}:{table[Ref, false]}";
	}

	public string GetSub(short type, Sub sub)
	{
		if (sub == default) return null;

		return this[type]?.Definition.ElRecord.SubtableByType(sub.Type).Name;
	}



	public Record GetRecord(string table, string alias) => this[table]?[alias];

	public Record GetRecord(string value)
	{
		if (value is null) return null;

		var array = value.Split(':', 2);
		if (array.Length < 2)
		{
			Serilog.Log.Warning($"TRef get failed, value: {value}");
			return null;
		}

		return GetRecord(array[0], array[1]);
	}

	public Record GetIconRecord(string value, out ushort index)
	{
		index = 0;
		if (value is null) return null;

		var colon = value.LastIndexOf(',');
		if (colon == -1) return null;

		var array = new[] { value[..colon], value[(colon + 1)..] };
		index = ushort.Parse(array[1]);
		return GetRecord("icontexture", array[0]);
	}
}