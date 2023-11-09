using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Helpers;

namespace Xylia.Preview.Data.Engine.DatData;
public class FolderProvider : IDataProvider
{
	#region Constructor
	private readonly DirectoryInfo directory;
	public FolderProvider(string path) => directory = new(path);
	#endregion

	#region Properties
	public string Name => directory.Name;

	public TableCollection Tables { get; set; }
	#endregion


	#region Methods
	Stream[] IDataProvider.GetFiles(string pattern) => directory.GetFiles(pattern, SearchOption.AllDirectories).Select(x => x.Open(FileMode.Open)).ToArray();

	public void LoadData(List<TableDefinition> definitions)
	{
		foreach (var definition in definitions)
		{
			string path = definition.Name switch
			{
				"quest" => @"quest\questdata*.xml",
				"contextscript" => "skill3_contextscriptdata*.xml",

				_ => $"{definition.Name.TitleCase()}Data*.xml",
			};

			Tables.Add(new() { Name = definition.Name, XmlPath = path });
		}
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}
	#endregion
}