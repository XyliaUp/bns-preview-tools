using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BnsBinTool.Core.Models;

using Xylia.Preview.Data.Models.BinData.AliasTable;

namespace Xylia.Preview.Data.Models.BinData
{
	public static class AliasTableHelper
    {
        public static ConcurrentDictionary<string, AliasCollection> CreateTable(this List<NameTableEntry> entries)
        {
            #region 寻找起始偏移
            int startIdx = -1;
            for (int idx = entries.Count - 1; idx >= 0; idx--)
            {
                //存在别名的数据表名
                var entry = entries[idx];
                if (entry.IsLeaf && entry.String == "account-post-charge:")
                {
                    startIdx = idx;
                    break;
                }
            }
            if (startIdx == -1) throw new Exception($"未能找到起始偏移");
            #endregion

            var array = entries.ToArray();
            var result = new ConcurrentDictionary<string, AliasCollection>(StringComparer.OrdinalIgnoreCase);

            Parallel.For(startIdx, array.Length, idx =>
            {
                BlockingCollection<AliasInfo> table = new();
                array[idx].CreateNodes(array, table);

                foreach (var record in table)
                {
                    string ParentTable = record.ParentTable;
                    if (!result.TryGetValue(ParentTable, out var infos))
                        infos = result[ParentTable] = new AliasCollection();

                    infos.Add(record);
                }
            });

            Parallel.ForEach(result, table => table.Value.Sort());
            return result;
        }

        private static void CreateNodes(this NameTableEntry entry, NameTableEntry[] entries, BlockingCollection<AliasInfo> NodesList, string CurPath = null)
        {
            //路径追加当前节点
            CurPath += entry.String;

            if (entry.IsLeaf)
            {
                var IndexA = entry.Begin / 2;
                var IndexB = entry.End;

                //获取当前对象子节点
                var Children = new NameTableEntry[IndexB - IndexA + 1];
                Array.Copy(entries, (int)IndexA, Children, 0, Children.Length);

                //按树形式逐级解析
                foreach (var node in Children)
                    node.CreateNodes(entries, NodesList, CurPath);
            }
            else NodesList.Add(new AliasInfo((entry.Begin - 1) / 2, entry.End / 2, CurPath));
        }
    }

    public static class TableHelper
    {
        public static Dictionary<short, string> DetectIndices(this IEnumerable<BnsBinTool.Core.Models.Table> tables, ConcurrentDictionary<string, AliasCollection> AliasTable = null)
        {
            Dictionary<short, string> set = new();
            foreach (var table in tables)
                set[table.Type] = null;

            Parallel.ForEach(tables, table =>
            {
                if (table.Records.Count == 0) return;
                var str1 = new StringList(table.Records[0].StringLookup);
                var str2 = table.IsCompressed ? new StringList(table.Records[^1].StringLookup) : str1;

                #region 通用处理Functions
                if (AliasTable != null)
                {
                    bool HasCheck = false;
                    foreach (var alist in AliasTable.Where(lst => !lst.Value.HasCheck))
                    {
                        if (table.Records.Count == 0) continue;
                        if (alist.Key != "npctalkmessage" && alist.Key != "effect" &&
                             table.Records.Count != alist.Value.Count &&
                             table.Records.Count != alist.Value.Count + 1) continue;


                        if (!str1.Contains(alist.Value.First().Alias)) continue;
                        if (!str2.Contains(alist.Value.Last().Alias)) continue;

                        //统计结果
                        HasCheck = alist.Value.HasCheck = true;
                        AddList(set, alist.Key, table.Type);
                        break;
                    }

                    if (HasCheck) return;
                }
                #endregion

                #region 获取其他表
                if (table.IsCompressed)
                {
                    if (table.Size > 5000000)
                    {
                        var FieldSize = table.Records[0].DataSize;
                        if (FieldSize > 2000)
                        {
                            AddList(set, "item", table.Type);
                            return;
                        }
                        else if (FieldSize == 28 || FieldSize == 36)
                        {
                            AddList(set, "text", table.Type);
                            return;
                        }
                    }
                }
                else
                {
                    if (str1.Contains("00047888.BordGacha_Disable")) AddList(set, "board-gacha", table.Type);
                    else if (str1.Contains("ShopSale-1")) AddList(set, "content-quota", table.Type);
                    else if (str1.Contains("00008603.Indicator.CN_BlueDiamond")) AddList(set, "gradebenefits", table.Type);
                    else if (str1.Contains("S,DOWN")) AddList(set, "key-command", table.Type);
                    else if (str1.Contains("76_PCSpawnPoint_1")) AddList(set, "zonepcspawn", table.Type);
                }
                #endregion
            });

            return set;
        }

        private static void AddList(Dictionary<short, string> set, string TypeName, int Type)
        {
            set[(short)Type] = TypeName;


            if (TypeName == "account-post-charge")
            {
                AddList(set, "account-level", Type - 1);
            }
            else if (TypeName == "board-gacha") AddList(set, "board-gacha-reward", Type + 1);
            else if (TypeName == "challengelistreward") AddList(set, "challengelist", Type - 1);
            else if (TypeName == "collecting")
            {
                AddList(set, "closet-collecting-grade", Type - 2);
                AddList(set, "closet-group", Type - 1);
            }
            else if (TypeName == "faction") AddList(set, "faction-level", Type + 1);
            else if (TypeName == "glyph") AddList(set, "glyph-page", Type + 1);
            else if (TypeName == "gradebenefits") AddList(set, "goodsicon", Type - 1);
            else if (TypeName == "item-graph-seed-group") AddList(set, "item-graph", Type - 1);
            else if (TypeName == "item-brand") AddList(set, "item-brand-tooltip", Type + 1);
            else if (TypeName == "item-improve-option")
            {
                AddList(set, "item-improve", Type - 1);
                AddList(set, "item-improve-option-list", Type + 1);
            }
            else if (TypeName == "job-style")
            {
                AddList(set, "job", Type - 2);
                AddList(set, "jobskillset", Type - 1);
            }
            else if (TypeName == "jumpingcharacter") AddList(set, "jumpingcharacter2", Type - 1);
            else if (TypeName == "key-command") AddList(set, "key-cap", Type - 1);
            else if (TypeName == "linkmoveanim") AddList(set, "level", Type - 1);
            else if (TypeName == "mapinfo")
            {
                AddList(set, "mapoverlay", Type + 1);
                AddList(set, "mapunit", Type + 2);
            }
            else if (TypeName == "map-group-1") AddList(set, "map-group-1-expedition", Type + 1);
            else if (TypeName == "mentoring")
            {
                AddList(set, "mastery-level", Type - 3);
                AddList(set, "mastery-stat-point", Type - 2);
                AddList(set, "mastery-stat-point-pick", Type - 1);
            }
            else if (TypeName == "questbonusrewardsetting") AddList(set, "questbonusreward", Type - 1);
            else if (TypeName == "random-store-item") AddList(set, "random-store-item-display", Type + 1);
            else if (TypeName == "skillskin")
            {
                AddList(set, "skillshow3", Type - 1);
                AddList(set, "skillskineffect", Type + 1);
            }
            else if (TypeName == "skilltooltipattribute") AddList(set, "skilltooltip", Type + 1);
            else if (TypeName == "skill-train-combo-action") AddList(set, "skill-train-category", Type - 1);
            else if (TypeName == "slatestone")
            {
                AddList(set, "slatescroll", Type - 2);
                AddList(set, "slatescrollstone", Type - 1);
            }
        }
    }
}