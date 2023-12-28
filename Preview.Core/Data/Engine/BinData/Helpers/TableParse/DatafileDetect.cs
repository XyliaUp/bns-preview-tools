using System.Collections.ObjectModel;
using Xylia.Preview.Common.Extension;
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

		if (name == "board-gacha") AddList("board-gacha-reward", type + 1);
		else if (name == "challengelistreward") AddList("challengelist", type - 1);
		else if (name == "collecting")
		{
			AddList("closet-collecting-grade", type - 2);
			AddList("closet-group", type - 1);
		}
		else if (name == "equip-gem-piece")
		{
			AddList("equipitemgroup", type + 1);
			AddList("equip-item-guide", type + 2);
			AddList("equip-item-guide-item-list", type + 3);
		}
		else if (name == "faction") AddList("faction-level", type + 1);
		else if (name == "glyph") AddList("glyph-page", type + 1);
		else if (name == "item-graph-seed-group") AddList("item-graph", type - 1);
		else if (name == "item-brand") AddList("item-brand-tooltip", type + 1);
		else if (name == "item-improve-option")
		{
			AddList("item-improve", type - 1);
			AddList("item-improve-option-list", type + 1);
		}
		else if (name == "job-style")
		{
			AddList("job", type - 2);
			AddList("jobskillset", type - 1);
		}
		else if (name == "jumpingcharacter") AddList("jumpingcharacter2", type - 1);
		else if (name == "linkmoveanim") AddList("level", type - 1);
		else if (name == "mapinfo")
		{
			AddList("mapoverlay", type + 1);
			AddList("mapunit", type + 2);
		}
		else if (name == "map-group-1") AddList("map-group-1-expedition", type + 1);
		else if (name == "mentoring")
		{
			AddList("mastery-level", type - 3);
			AddList("mastery-stat-point", type - 2);
			AddList("mastery-stat-point-pick", type - 1);
		}
		else if (name == "npc")
		{
			AddList("npccombatmoveanim", type - 1);

			AddList("npcindicatormoveanim", type + 1);
			AddList("npcmoveanim", type + 2);
		}
		else if (name == "questbonusrewardsetting") AddList("questbonusreward", type - 1);
		else if (name == "random-store-item") AddList("random-store-item-display", type + 1);
		else if (name == "skillskin")
		{
			AddList("skillshow3", type - 1);
			AddList("skillskineffect", type + 1);
		}
		else if (name == "skilltooltipattribute") AddList("skilltooltip", type + 1);
		else if (name == "skill-train-combo-action") AddList("skill-train-category", type - 1);
		else if (name == "unlocated-store") AddList("unlocated-store-ui", type + 1);
		else if (name == "vehicle" && by_id.GetValueOrDefault(type + 1) == "vehicle-appearance")
		{
			by_id[type] = "vehicle-appearance";
			by_id[type + 1] = "vehicle";
		}
		else if (name == "vehicle-appearance" && by_id.GetValueOrDefault(type - 1) == "vehicle")
		{
			by_id[type - 1] = "vehicle-appearance";
			by_id[type] = "vehicle";
		}
	}

	private void CreateNameMap(IList<TableDefinition> definitions)
	{
		by_name.Clear();

		int LastId = 1;
		string LastName = null;
		foreach (var o in by_id)
		{
			if (string.IsNullOrEmpty(o.Value)) continue;

			for (int i = 0; i < definitions.Count; i++)
			{
				if (definitions[i].Name == LastName || LastName == null)
				{
					int j;
					for (j = i; j < definitions.Count; j++)
					{
						if (definitions[j].Name == o.Value) break;
					}

					if (j - i  == o.Key - LastId)
					{
						for (; i < j; i++)
						{
							var d = definitions[i];
							by_name[d.Name] = (ushort)(LastId + j - i + 1);
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
		Read(data.Tables, data.NameTable?.CreateTable());
		CreateNameMap(definitions);
	}

	/// <summary>
	/// create map by detect data
	/// </summary>
	/// <param name="tables"></param>
	/// <param name="AliasTable"></param>
	private void Read(IEnumerable<Table> tables, List<AliasTable> AliasTable)
	{
		tables.ForEach(table => by_id[table.Type] = "");
		Parallel.ForEach(tables, table =>
		{
			if (table.XmlPath != null || table.Records.Count == 0) return;

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
				foreach (var lst in AliasTable)
				{
					if (lst.HasCheck) continue;
					if (!str1.Contains(lst[record1.Ref]?.Alias)) continue;
					if (!str2.Contains(lst[record2.Ref]?.Alias)) continue;

					lst.HasCheck = true;
					AddList(lst.Name, table.Type);
					return;
				}
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