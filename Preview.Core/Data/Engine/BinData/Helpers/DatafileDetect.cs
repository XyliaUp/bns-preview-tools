using System.Text;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.BinData.Helpers;

/// <summary>
/// Type auto detect 
/// </summary>
public sealed class DatafileDetect
{
    #region Datafile Helper
    readonly Dictionary<int, string> by_id = new();
    readonly Dictionary<string, short> by_name = new(new TableNameComparer());

    private void AddList(string Name, int Type)
    {
        by_id[Type] = Name;

        if (Name == "account-post-charge")
        {
            AddList("account-level", Type - 1);
        }
        else if (Name == "board-gacha") AddList("board-gacha-reward", Type + 1);
        else if (Name == "challengelistreward") AddList("challengelist", Type - 1);
        else if (Name == "collecting")
        {
            AddList("closet-collecting-grade", Type - 2);
            AddList("closet-group", Type - 1);
        }
        else if (Name == "equip-gem-piece")
        {
            AddList("equipitemgroup", Type + 1);
            AddList("equipment-guide", Type + 2);
            AddList("equipment-guide-item", Type + 3);
        }
        else if (Name == "faction") AddList("faction-level", Type + 1);
        else if (Name == "glyph") AddList("glyph-page", Type + 1);
        else if (Name == "item-graph-seed-group") AddList("item-graph", Type - 1);
        else if (Name == "item-brand") AddList("item-brand-tooltip", Type + 1);
        else if (Name == "item-improve-option")
        {
            AddList("item-improve", Type - 1);
            AddList("item-improve-option-list", Type + 1);
        }
        else if (Name == "job-style")
        {
            AddList("job", Type - 2);
            AddList("jobskillset", Type - 1);
        }
        else if (Name == "jumpingcharacter") AddList("jumpingcharacter2", Type - 1);
        else if (Name == "linkmoveanim") AddList("level", Type - 1);
        else if (Name == "mapinfo")
        {
            AddList("mapoverlay", Type + 1);
            AddList("mapunit", Type + 2);
        }
        else if (Name == "map-group-1") AddList("map-group-1-expedition", Type + 1);
        else if (Name == "mentoring")
        {
            AddList("mastery-level", Type - 3);
            AddList("mastery-stat-point", Type - 2);
            AddList("mastery-stat-point-pick", Type - 1);
        }
        else if (Name == "npc")
        {
            AddList("npccombatmoveanim", Type - 1);

            AddList("npcindicatormoveanim", Type + 1);
            AddList("npcmoveanim", Type + 2);
        }
        else if (Name == "questbonusrewardsetting") AddList("questbonusreward", Type - 1);
        else if (Name == "random-store-item") AddList("random-store-item-display", Type + 1);
        else if (Name == "skillskin")
        {
            AddList("skillshow3", Type - 1);
            AddList("skillskineffect", Type + 1);
        }
        else if (Name == "skilltooltipattribute") AddList("skilltooltip", Type + 1);
        else if (Name == "skill-train-combo-action") AddList("skill-train-category", Type - 1);
        else if (Name == "slatestone")
        {
            AddList("slatescroll", Type - 2);
            AddList("slatescrollstone", Type - 1);
        }
        else if (Name == "unlocated-store") AddList("unlocated-store-ui", Type + 1);
        else if (Name == "vehicle" && by_id.GetValueOrDefault(Type + 1, null) == "vehicle-appearance")
        {
            by_id[Type] = "vehicle-appearance";
            by_id[Type + 1] = "vehicle";
        }
        else if (Name == "vehicle-appearance" && by_id.GetValueOrDefault(Type - 1, null) == "vehicle")
        {
            by_id[Type - 1] = "vehicle-appearance";
            by_id[Type] = "vehicle";
        }
    }

    private void CreateNameMap()
    {
        by_name.Clear();
        foreach (var o in by_id)
        {
            if (string.IsNullOrWhiteSpace(o.Value))
                continue;

            by_name[o.Value] = (short)o.Key;
        }
    }

    /// <summary>
    /// create map by detect data
    /// </summary>
    /// <param name="data"></param>
    public void Read(Datafile data) => Read(data.Tables, data.NameTable.CreateTable());

    /// <summary>
    /// create map by detect data
    /// </summary>
    /// <param name="tables"></param>
    /// <param name="AliasTable"></param>
    public void Read(IEnumerable<Table> tables, List<AliasCollection> AliasTable)
    {
        tables.ForEach(table => by_id[table.Type] = "");
        Parallel.ForEach(tables, table =>
        {
            if (table.XmlPath != null ||
                table.Records.Count == 0) return;

            HashSet<string> GetLookup(Record record) => Encoding.Unicode.GetString(record.StringLookup.Data)
                .Split('\0', StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);


            var record1 = table.Records[0];
            var record2 = table.Records[^1];
            var str1 = GetLookup(record1);
            var str2 = table.IsCompressed ? GetLookup(record2) : str1;

            #region common
            if (AliasTable != null)
            {
                bool HasCheck = false;
                foreach (var alist in AliasTable.Where(lst => !lst.HasCheck))
                {
                    if (alist.Table != "npctalkmessage" && alist.Table != "effect" &&
                         table.Records.Count != alist.Count &&
                         table.Records.Count != alist.Count + 1) continue;

                    if (!str1.Contains(alist[record1.Ref]?.Alias)) continue;
                    if (!str2.Contains(alist[record2.Ref]?.Alias)) continue;

                    HasCheck = alist.HasCheck = true;
                    AddList(alist.Table, table.Type);
                    break;
                }

                if (HasCheck) return;
            }
            #endregion

            #region else
            if (table.IsCompressed)
            {
                if (table.Size > 5000000)
                {
                    var FieldSize = table.Records[0].DataSize;
                    if (FieldSize > 2000)
                    {
                        AddList("item", table.Type);
                        return;
                    }
                    else if (FieldSize == 28 || FieldSize == 36)
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

		CreateNameMap();

		GC.Collect();
    }
	#endregion

	#region Methods
	public bool TryGetName(short key, out string name) => by_id.TryGetValue(key, out name);

	public bool TryGetKey(string name, out short key) => by_name.TryGetValue(name, out key);

	/// <summary>
	/// convert reference table name to key
	/// </summary>
	public void Convert(List<TableDefinition> definitions)
    {
        foreach (var defs in definitions)
        {
            if (defs.Type == 0 && TryGetKey(defs.Name, out var _type))
                defs.Type = _type;

            foreach (var attribute in defs.ElRecord.ExpandedAttributes)
            {
                var TypeName = attribute.ReferedTableName;
                if (TypeName != null && TryGetKey(TypeName, out var type))
                    attribute.ReferedTable = type;
            }

            foreach (var subtable in defs.ElRecord.Subtables)
            {
                foreach (var attribute in subtable.ExpandedAttributes)
                {
                    var TypeName = attribute.ReferedTableName;
                    if (TypeName != null && TryGetKey(TypeName, out var type))
                        attribute.ReferedTable = type;
                }
            }
        }
    }
	#endregion


	#region Output Map
	//public void Read(string path = null)
 //   {
 //       path ??= Path.Combine(Settings.Default.OutputFolder, "defs", "table.csv");

 //       using var csv = new CsvReader(new StreamReader(path), new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false });
 //       while (csv.Read())
 //       {
 //           var type = csv.GetField<short>(0);
 //           var name = csv.GetField(1);

 //           AddList(name, type);
 //       }
 //   }
    #endregion
}