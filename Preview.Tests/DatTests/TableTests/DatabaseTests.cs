using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Tests.DatTests;
public sealed class TestDatabase(IDataProvider provider, string outputPath) : BnsDatabase(provider)
{
	/// <summary>
	/// from external files
	/// </summary>
	/// <param name="files"></param>
	public void Output(params FileInfo[] files)
	{
		var defs = TableDefinitionHelper.LoadTableDefinition(null, files);
		if (Provider is DefaultProvider game) game.Detect.ParseType(defs);

		Parallel.ForEach(defs, definition =>
		{
			var table = Provider.Tables[definition.Type];
			if (table is null)
			{
				Console.WriteLine("detect failed: " + definition.Name);
				return;
			}

			if (definition is null || definition.IsEmpty) return;

			table.Definition = definition;
			table.CheckSize();
			table.WriteXml(outputPath);
		});
	}
}