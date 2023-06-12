using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Analyse.Enums;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Seq.Type;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Extension;    

public static partial class LoadExtension
{
    /// <summary>
    /// 获得过滤组
    /// </summary>
    /// <param name="FilterInfo"></param>
    /// <param name="TypeRange"></param>
    /// <returns></returns>
    public static List<ushort> GetFilters(this string FilterInfo, TypeInfo TypeRange)
    {
        #region Initialize
        var Filters = new List<ushort>();
        if (string.IsNullOrWhiteSpace(FilterInfo)) return Filters;

        var fs = FilterInfo.SplitToList();
        if (fs.Count == 0) return Filters;
        #endregion


        //考虑筛选数组情况
        foreach (var f in fs)
        {
            if (ushort.TryParse(f, out var Filter)) Filters.Add(Filter);
            else if (TypeRange is null)
            {
                System.Diagnostics.Trace.WriteLine(f + " 类型字典为Null");
                continue;
            }
            else if (TypeRange.ContainsAlias(f)) Filters.Add((ushort)TypeRange.GetCell(f).Key);
            else Console.WriteLine($"未知筛选器别名: {f}, 不进行处理");
        }

        return Filters;
    }

    /// <summary>
    /// 文本拆分
    /// </summary>
    /// <returns></returns>
    public static List<string> SplitToList(this string Txt, char SplitSign = '|')
    {
        if (string.IsNullOrWhiteSpace(Txt)) return new();

        Txt += SplitSign;
        return Txt
            .Split(SplitSign)
            .Where(w => !string.IsNullOrEmpty(w))
            .Select(w => w.ToLower().Trim())
            .ToList();
    }

    /// <summary>
    /// 获取list规则
    /// </summary>
    /// <param name="RuleInfo"></param>
    /// <param name="TipMsg"></param>
    /// <returns></returns>
    public static List<ListRule> GetListRules(this string RuleInfo, out string TipMsg)
    {
        #region 获取规则
        TipMsg = null;

        List<ListRule> Rules = new();
        if (!string.IsNullOrWhiteSpace(RuleInfo))
        {
            foreach (var rule in (RuleInfo + "|").Split('|'))
            {
                //判断后新增
                var tmp = rule.GetListRule();
                if (tmp != ListRule.None && !Rules.Contains(tmp)) Rules.Add(tmp);
            }
        }
        #endregion


        if (!Rules.Contains(ListRule.Complete) && !Rules.Contains(ListRule.Simple))
            Rules.Add(ListRule.Complete);

        if (!Rules.Contains(ListRule.Complete) && !Rules.Contains(ListRule.Simple))
        {
            if (System.Reflection.Assembly.GetExecutingAssembly() == System.Reflection.Assembly.GetEntryAssembly())
                TipMsg = "未使用complete规则，将在原数据基础上修改";
        }

        return Rules;
    }

    /// <summary>
    /// 读取列表规则
    /// </summary>
    /// <param name="Info"></param>
    /// <returns></returns>
    public static ListRule GetListRule(this string Info)
    {
        if (string.IsNullOrWhiteSpace(Info)) return ListRule.None;
        else if (Enum.TryParse<ListRule>(Info, true, out var result)) return result;
        else return Info?.ToLower()?.Trim() switch
        {
            "complete" or "cpl" => ListRule.Complete,
            "level" or "variation" => ListRule.UseAutoVariation,
            "sort-id" => ListRule.SortId,
            _ => ListRule.None,
        };
    }

    /// <summary>
    /// 获取应用默认值的类型限制
    /// </summary>
    /// <param name="Info"></param>
    /// <returns></returns>
    public static ApplyMode GetApplyMode(this string Info)
    {
        if (!string.IsNullOrWhiteSpace(Info))
        {
            if (Info.MyEquals("default")) return ApplyMode.HideDefault;
            else if (Info.MyEquals("must") || Info.MyEquals("exist")) return ApplyMode.Exist;
            else if (Info.MyEquals("apply")) return ApplyMode.Force;
            else if (Info.MyEquals("auto")) return ApplyMode.Auto;
            else System.Diagnostics.Trace.WriteLine("无效的应用类型: " + Info);
        }

        return ApplyMode.None;
    }
}