using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using BnsBinTool.Core.Definitions;

using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Analyse.Load;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Record;

namespace Xylia.Preview.Data.Definition;

public static class DefinitionHelper
{
	public static List<MyTableDefinition> LoadTableDefinition()
	{
		var _assembly = Assembly.GetExecutingAssembly();

		//public
		var PublicSeq = ConfigLoad.GetPublicSeq(_assembly.GetManifestResourceNames()
			.Where(name => name.StartsWith("Xylia.Preview.Data.Records.TableDef_Global."))
			.Select(name => new StreamReader(_assembly.GetManifestResourceStream(name)).ReadToEnd())
			.ToArray());

		//result
		return _assembly.GetManifestResourceNames()
			.Where(name => name.StartsWith("Xylia.Preview.Data.Records.TableDef."))
			.Select(name => name.GetResource())
			.SelectMany(res => ConfigLoad.Load(PublicSeq, res))     //load config
			.Select(table => MyTableDefinition.LoadFrom(table))     //convert
			.ToList();
	}


	public static string GetResource(this string name)
	{
		var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
		if (stream is null) return null;

		return new StreamReader(stream).ReadToEnd();
	}










	public static List<MyTableDefinition> LoadTableDefinition(params string[] FilePathes)
	{
		var TableInfo = new List<TableInfo>();
		foreach (var f in FilePathes)
			TableInfo.AddRange(ConfigLoad.LoadFromFile(null, f));

		return TableInfo.Select(table => MyTableDefinition.LoadFrom(table)).ToList();
	}

	public static DatafileDefinition LoadDefinition(List<MyTableDefinition> tableDefinitions, DataTableSet set, bool mergeDuplicatedSequences = false, bool is64Bit = false)
	{
		foreach (var tableDef in tableDefinitions)
		{
			set.GetHelper(tableDef.Name).Definition = tableDef;
			if (tableDef.Type == 0) tableDef.Type = set.GetTableType(tableDef.TypeName);


			foreach (var attr in tableDef.ExpandedAttributes)
			{
				var referedTableName = ((MyAttributeDefinition)attr).ReferedTableName;
				if (referedTableName != null) attr.ReferedTable = set.GetTableType(referedTableName);
			}

			foreach (var subtable in tableDef.Subtables)
			{
				foreach (var attr in subtable.ExpandedAttributes)
				{
					var referedTableName = ((MyAttributeDefinition)attr).ReferedTableName;
					if (referedTableName != null) attr.ReferedTable = set.GetTableType(referedTableName);
				}
			}
		}


		var defs = tableDefinitions.DistinctBy(o => o.Type).Select(d => (TableDefinition)d).ToList();
		var datafileDeinition = new DatafileDefinition(defs) { Is64Bit = is64Bit };
		datafileDeinition.SequenceDefinitions.AddRange(new SequenceDefinitionLoader().LoadFor(defs, mergeDuplicatedSequences));
		return datafileDeinition;
	}
}