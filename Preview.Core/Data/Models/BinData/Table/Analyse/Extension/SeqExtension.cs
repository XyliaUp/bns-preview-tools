using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Seq;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Extension;

public static class SeqExtension
{
    public static SeqInfo GetSeqInfo(this XmlElement element, string name, Dictionary<string, SeqInfo> globalSeq = null)
    {
        SeqInfo Result = null;

        var CaseNodes = element.ChildNodes.OfType<XmlElement>();
        if (CaseNodes.Any())
        {
            Result = new();
            short CurKey = 0;
            foreach (var CaseNode in CaseNodes)
            {
                #region 获取主键
                string Key = CaseNode.Attributes["key"]?.Value;

                bool Flag = short.TryParse(Key, out var _key);
                if (!Flag)
                {
                    //如果缺省，则进行自动编号
                    if (string.IsNullOrWhiteSpace(Key)) _key = CurKey++;
                    else continue;
                }


                if (Result.ContainsKey(_key))
                    throw new Exception($"枚举 {Result.Name}主键不能一致：" + _key);


                //在异常状态下也可以保持序号增加
                //取大值是为了防止出现乱序编号
                CurKey = (short)Math.Max(CurKey, _key + 1);
                #endregion

                #region 获取其他信息
                string Alias = CaseNode.Attributes["alias"]?.Value?.Trim();   //别名
                if (Alias == "unk-") Alias = "unk" + CurKey;

                string Name = CaseNode.Attributes["name"]?.Value?.Trim();     //名称
                bool IsDefault = CaseNode.Attributes["default"]?.Value?.ToBool() ?? false;  //默认值

                //如果键别名为空
                if (string.IsNullOrWhiteSpace(Alias))
                {
                    Console.WriteLine($"字典 {name} 中的键 {_key} 没有有效的别名");
                    continue;
                }
                else if (Result.ContainsAlias(Alias)) throw new Exception($"字典 {name} 中键别名 {Alias} 发生重复");
                #endregion

                #region 存储内容
                //生成对象
                var cell = new SeqCell()
                {
                    Alias = Alias,
                    Key = _key,
                    Name = Name,
                };


                Result.Add(cell);

                // 设定默认值
                if (IsDefault)
                {
                    if (Result.DefaultCell is not null)
                        throw new Exception($"字典 {name} 中重复定义默认值 (prev:{Result.DefaultCell.Alias},now:{Alias})");

                    Result.DefaultCell = cell;
                }
                #endregion
            }
        }
        else
        {
            var SeqName = element.Attributes["seq"]?.Value?.Trim();
            if (string.IsNullOrWhiteSpace(SeqName)) return null;

            if (globalSeq is null || !globalSeq.TryGetValue(SeqName, out var TSeq))
            {
                System.Diagnostics.Trace.WriteLine($"未获取到枚举定义信息 -> {SeqName}");
                return null;
            }

            //进行对象克隆
            Result = TSeq.Clone() as SeqInfo;
        }

        Result.Name = name;
        return Result;
    }

    public static void LoadPublicSeq(this Dictionary<string, SeqInfo> globalSeq, XmlElement element, string name = null)
    {
        if (name is null)
            name = element.Attributes["alias"]?.Value?.Trim();

        if (globalSeq.ContainsKey(name))
        {
            LogWriter.WriteLine($"== CRASH == 字典{name}已经存在");
            return;
        }

        var SeqInfo = element.GetSeqInfo(name);
        if (SeqInfo != null) globalSeq[name] = SeqInfo;
    }
}
