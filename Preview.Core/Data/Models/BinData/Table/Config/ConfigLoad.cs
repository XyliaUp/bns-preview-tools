using System.Configuration;
using System.Data;
using System.Xml;

using BnsBinTool.Core.Definitions;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.BinData.Table.Config;
public static partial class ConfigLoad
{
	#region Load
	public static List<TableDefinition> LoadFromFile(ConfigParam param, params string[] Files) =>
		Load(param, Files.Where(File.Exists).Select(File.ReadAllText).ToArray());

	public static List<TableDefinition> Load(ConfigParam param, params string[] XmlContents)
	{
		var tables = new List<TableDefinition>();
		foreach (var Content in XmlContents)
		{
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(Content);


			var table = TableDefinitionEx.LoadFrom(param, xmlDoc.DocumentElement);
			if (table is null) continue;

			tables.Add(table);
		}

		return tables;
	}
	#endregion

	#region Tables
	public static void GetVersion(this string VerInfo, out ushort MajorVersion, out ushort MinorVersion)
	{
		MajorVersion = MinorVersion = 0;
		if (VerInfo is null) return;

		var VersionText = VerInfo.Split('.');
		MajorVersion = (ushort)VersionText[0].ToInt16();
		MinorVersion = (ushort)VersionText[1].ToInt16();
	}
	#endregion
}


public sealed class ConfigParam
{
	public readonly Dictionary<string, SeqInfo> PublicSeq = new(StringComparer.OrdinalIgnoreCase);

	public static ConfigParam LoadFrom(params string[] XmlContents)
	{
		var param = new ConfigParam();
		foreach (var content in XmlContents)
		{
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(content);

			foreach (XmlElement record in xmlDoc.SelectNodes("table/record"))
			{
				string name = record.Attributes["name"]?.Value?.Trim();
				if (param.PublicSeq.ContainsKey(name))
					throw new ConfigurationErrorsException($"seq `{name}` has existed");

				var seq = SeqInfo.LoadFrom(record, name);
				if (seq != null) param.PublicSeq[name] = seq;
			}
		}

		return param;
	}
}