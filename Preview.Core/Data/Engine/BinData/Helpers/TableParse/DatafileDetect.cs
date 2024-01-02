using System.Collections.ObjectModel;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.BinData.Helpers;
/// <summary>
/// parse by auto detect  
/// </summary>
/// <remarks>This will to load NameTable and LazyTable, cause large memory usage.</remarks>
public sealed class DatafileDetect : ITableParseType
{
	#region Helpers
	readonly Dictionary<int, string> by_id = new();
	readonly Dictionary<string, ushort> by_name = new(new TableNameComparer());

	public bool TryGetName(ushort key, out string name) => by_id.TryGetValue(key, out name);

	public bool TryGetKey(string name, out ushort key) => by_name.TryGetValue(name, out key);

	private void AddList(string name, int type)
	{
		by_id[type] = name;

		 if (name == "unlocated-store") AddList("unlocated-store-ui", type + 1);
	}

	private void CreateNameMap(TableDefinition[] definitions)
	{
		by_name.Clear();

		int LastId = 1;
		string LastName = null;
		foreach (var o in by_id)
		{
			if (string.IsNullOrEmpty(o.Value)) continue;

			for (int i = 0; i < definitions.Length; i++)
			{
				if (o.Key - LastId <= 1) break;

				if (definitions[i].Name == LastName || LastName == null)
				{
					int j;
					for (j = i; j < definitions.Length; j++)
					{
						if (definitions[j].Name == o.Value) break;
					}

					if (j - i == o.Key - LastId)
					{
						for (int x = i; x < j; x++)
						{
							var d = definitions[x];
							by_name[d.Name] = (ushort)(LastId + x - i);
						}
					}

					break;
				}
			}

			by_name[o.Value] = (ushort)o.Key;
			LastId = o.Key;
			LastName = o.Value;
		}
	}
	#endregion


	#region Load Methods
	public DatafileDetect(Datafile data, Collection<TableDefinition> definitions)
	{
		Read(data.Tables, AliasTableUnit.Split(data.AliasTable));
		CreateNameMap(definitions.Where(x => x.Module != TableModule.Server).ToArray());
	}

	/// <summary>
	/// create map by detect data
	/// </summary>
	/// <param name="tables"></param>
	/// <param name="AliasTable"></param>
	private void Read(IEnumerable<Table> tables, List<AliasTableUnit> AliasTable)
	{
		tables.ForEach(table => by_id[table.Type] = "");
		Parallel.ForEach(tables, table =>
		{
			if (table.SearchPattern != null || table.Records.Count == 0) return;

			static HashSet<string> GetLookup(Record record) =>
			   record.StringLookup.Strings.ToHashSet(StringComparer.OrdinalIgnoreCase);

			var record1 = table.Records[0];
			var record2 = table.Records[^1];
			var str1 = GetLookup(record1);
			var str2 = table.IsCompressed ? GetLookup(record2) : str1;

			#region common
			// local provider not has 
			if (AliasTable != null)
			{    
				var lsts = new List<string>();
				foreach (var lst in AliasTable)
				{
					if (!str1.Contains(lst[record1])) continue;
					if (!str2.Contains(lst[record2])) continue;

					// do not directly return
					// because exist issue if tables with identical aliases 
					lsts.Add(lst.Name);
				}

				lsts.ForEach(x => AddList(x, table.Type));
				return;
			}
			#endregion

			#region else
			if (table.IsCompressed)
			{
				if (table.Size > 5000000)
				{
					var FieldSize = record1.DataSize;
					if (FieldSize == 28 || FieldSize == 36)
					{
						AddList("text", table.Type);
						return;
					}
				}
			}
			else
			{
				if (str1.Contains("00047888.BordGacha_Disable")) AddList("board-gacha", table.Type);
				else if (str1.Contains("ShopSale-1")) AddList("content-quota", table.Type);
				else if (str1.Contains("00008603.Indicator.CN_BlueDiamond"))
				{
					AddList("goodsicon", table.Type - 1);
					AddList("gradebenefits", table.Type);
				}
				else if (str1.Contains("DropItem_Anim")) AddList("itempouchmesh2", table.Type);
				else if (str1.Contains("S,DOWN"))
				{
					AddList("key-cap", table.Type - 1);
					AddList("key-command", table.Type);
				}
				else if (str1.Contains("CharPos_JinM")) AddList("lobby-pc", table.Type);
				else if (str1.Contains("00055945.Thunderer_JinM_Animset"))
				{
					AddList("pc-appearance", table.Type);
					AddList("pc", table.Type + 1);
				}
				else if (str1.Contains("00007975.PC.MaleChild01_BladeMaster"))
				{
					AddList("pc-voice", table.Type - 1);
					AddList("pc-voice-set", table.Type);
				}
				else if (str1.Contains("00009076.Race_Gun")) AddList("race", table.Type);
				else if (str1.Contains("76_PCSpawnPoint_1")) AddList("zonepcspawn", table.Type);
			}
			#endregion
		});

		GC.Collect();
	}
	#endregion
}

internal class AliasTableUnit
{
	public string Name;

	public Dictionary<Ref, string> Table { get; } = [];

	public AliasTableUnit(string name)
	{
		Name = name;
	}

	public string this[Ref Ref] => Table.GetValueOrDefault(Ref);

	public void Add(Ref Ref, string alias)
	{
		Table[Ref] = alias;
	}


	internal static List<AliasTableUnit> Split(AliasTable aliasTable)
	{
		if (aliasTable is null) return null;


		var tables = new Dictionary<string, AliasTableUnit>(StringComparer.OrdinalIgnoreCase);

		foreach (var table in aliasTable.Table)
		{
			var ls = table.Key.Split(':', 2);
			if (ls.Length < 2) continue;

			var name = ls[0];
			var alias = ls[1];

			if (!tables.TryGetValue(name, out var collection))
				tables[name] = collection = new(name);

			collection.Add(table.Value, alias);
		}

		return [.. tables.Values];
	}
}