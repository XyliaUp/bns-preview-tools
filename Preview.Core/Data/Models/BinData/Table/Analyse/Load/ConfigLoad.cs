using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Xylia.Preview.Data.Models.BinData.Analyse.Extension;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Record;
using Xylia.Xml;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Load;

public static class ConfigLoad
{
    public static ConfigParam GetPublicSeq(params string[] XmlContents)
    {
        var param = new ConfigParam();
        param.PublicSeq = new(StringComparer.OrdinalIgnoreCase);

        foreach (var content in XmlContents)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);

            foreach (XmlElement record in xmlDoc.SelectNodes("//public/record"))
                param.PublicSeq.LoadPublicSeq(record);
        }

        return param;
    }

    public static ConfigParam GetPublicSeqFromFile(params string[] Files) =>
        GetPublicSeq(Files.Where(path => File.Exists(path)).Select(cfg => File.ReadAllText(cfg)).ToArray());




    public static List<TableInfo> Load(ConfigParam param, params string[] XmlContents)
    {
        #region 读取为XML文档
        var xmlDocs = new List<XmlDocument>();
        foreach (var Content in XmlContents)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Content);
            xmlDocs.Add(xmlDoc);
        }
        #endregion

        #region 将多个文档合并处理
        XmlInfo XmlInfo = null;
        foreach (var xmlDoc in xmlDocs)
        {
            //创建根节点
            if (XmlInfo is null) XmlInfo = new XmlInfo(xmlDoc.DocumentElement);

            //复制内容节点
            XmlInfo.AppendChild(xmlDoc.DocumentElement.ChildNodes, true);
        }

        return Load(param, XmlInfo.Doc);
        #endregion
    }

    public static List<TableInfo> Load(ConfigParam param, XmlDocument xmlDoc)
    {
        var Lists = new List<TableInfo>();
        foreach (XmlNode node in xmlDoc.SelectNodes("//list"))
            Lists.TableConfigRead(node, param);

        return Lists;
    }

    public static List<TableInfo> LoadFromFile(ConfigParam param, params string[] Files) =>
        Load(param, Files.Where(path => File.Exists(path)).Select(cfg => File.ReadAllText(cfg)).ToArray());
}