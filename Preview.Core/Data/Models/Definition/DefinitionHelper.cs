using System.Reflection;

using BnsBinTool.Core.Definitions;

using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table;
using Xylia.Preview.Data.Models.BinData.Table.Config;

namespace Xylia.Preview.Data.Models.Definition;
public static class DefinitionHelper
{
	public static string GetResource(this string name)
	{
		var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
		if (stream is null) return null;

		return new StreamReader(stream).ReadToEnd();
	}

	public static bool CheckRef(this AttributeDefinition def, string TableName)
		=> def.Type == AttributeType.TRef && FileCache.Data.detect.TryGetKey(TableName, out var key) && def.ReferedTable == key;




    /// <summary>
    /// load <see cref="TableDefinition"/> from program
    /// </summary>
    public static List<TableDefinition> LoadTableDefinition()
	{
		var _assembly = Assembly.GetExecutingAssembly();

		//public
		var PublicSeq = ConfigParam.LoadFrom(_assembly.GetManifestResourceNames()
			.Where(name => name.StartsWith("Xylia.Preview.Data.Records.TableDef_Global."))
			.Select(name => new StreamReader(_assembly.GetManifestResourceStream(name)).ReadToEnd())
			.ToArray());

		//result
		return _assembly.GetManifestResourceNames()
			.Where(name => name.StartsWith("Xylia.Preview.Data.Records.TableDef."))
			.Select(name => name.GetResource())
			.SelectMany(res => ConfigLoad.Load(PublicSeq, res))     //load config
			.ToList();
	}

    /// <summary>
    /// load <see cref="TableDefinition"/> from files
    /// </summary>
    public static List<TableDefinition> LoadTableDefinition(params string[] files) => files.SelectMany(f => ConfigLoad.LoadFromFile(null, f)).ToList();

}