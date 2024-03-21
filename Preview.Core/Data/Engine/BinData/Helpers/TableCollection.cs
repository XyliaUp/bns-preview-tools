using System.Collections.ObjectModel;
using System.Diagnostics;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.BinData.Helpers;
public class TableCollection : Collection<Table>
{
	#region Fields
	Dictionary<ushort, Table> _tableByType;
	Dictionary<string, Table> _tableByName;

	readonly Dictionary<Table, object> _tables = [];
	#endregion

	#region Methods
	public Table this[ushort index]
	{
		get
		{
			lock (this)
			{
				return (_tableByType ??= this.Where(x => x.Type > 0).ToDistinctDictionary(o => o.Type))
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
				if (ushort.TryParse(index, out var type)) return this[type];
				if (index.Equals("skill", StringComparison.OrdinalIgnoreCase)) index = "skill3";

				// Create hashmap
				var table = (_tableByName ??= this.ToDistinctDictionary(x => x.Name, TableNameComparer.Instance)).GetValueOrDefault(index);
				if (table is null) Debug.WriteLine($"Invalid typed reference, refered table doesn't exist: '{index}'");

				return table;
			}
		}
	}


	public Record GetRef(ushort type, Ref Ref)
	{
		if (Ref == default) return null;

		return this[type]?[Ref];
	}

	public string GetRef(IconRef Ref)
	{
		if (Ref == default) return null;

		var table = this["icontexture"];
		return table?[Ref] + $",{Ref.IconTextureIndex}";
	}

	public string GetRef(TRef Ref)
	{
		if (Ref == default) return null;

		// possible return null if it is a xml table
		// cause due to definition issue 
		var table = this[(ushort)Ref.Table];
		if (table is null) return Ref.ToString();

		return $"{table.Name}:{table[Ref]}";
	}

	public string GetSub(ushort type, Sub sub)
	{
		return this[type]?.Definition.ElRecord.SubtableByType(sub.Subclass).Name;
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
	#endregion

	#region GameDataTable
	public GameDataTable<T> Get<T>(string name = null, bool reload = false) where T : ModelElement =>
		Get<T>(this[name ?? typeof(T).Name], reload);

	public GameDataTable<T> Get<T>(Table table, bool reload) where T : ModelElement
	{
		if (table is null) return null;

		lock (_tables)
		{
			if (reload || !_tables.TryGetValue(table, out var Models))
				_tables[table] = Models = new GameDataTable<T>(table);

			return Models as GameDataTable<T>;
		}
	}

	protected override void ClearItems()
	{
		_tables.Clear();
		_tableByType?.Clear();
		_tableByName?.Clear();

		base.ClearItems();
	}
	#endregion
}