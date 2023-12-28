using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Engine.DatData;
public class FolderProvider(DirectoryInfo directory) : IDataProvider
{
	public FolderProvider(string path): this(new DirectoryInfo(path))
	{

	}


	#region Properties
	public string Name => directory.Name;	  
	public DateTime CreatedAt => default;
	public BnsVersion ClientVersion => default;
	public TableCollection Tables { get; private set; }
	#endregion

	#region Methods
	Stream[] IDataProvider.GetFiles(string pattern) => directory.GetFiles(pattern, SearchOption.AllDirectories).Select(x => x.OpenRead()).ToArray();

	public void LoadData(DatafileDefinition definitions)
	{
		this.Tables = [];

		// TODO: ignore server table
		foreach (var definition in definitions.OrderBy(x => x.Name))
		{
			string path = definition.Name switch
			{
				"quest" => @"quest\questdata*.xml",
				"contextscript" => "skill3_contextscriptdata*.xml",

				_ => $"{definition.Name.TitleCase()}Data*.xml",
			};

			Tables.Add(new() { Owner = this, Name = definition.Name, XmlPath = path });
		}
	}

	public void Dispose()
	{
		Tables?.Clear();
		Tables = null;

		GC.SuppressFinalize(this);
	}
	#endregion
}