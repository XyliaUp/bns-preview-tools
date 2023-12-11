using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
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
	public DateTime CreatedAt => default;
	public BnsVersion ClientVersion => default;
	public TableCollection Tables { get; private set; }
	#endregion


	#region Methods
	Stream[] IDataProvider.GetFiles(string pattern) => directory.GetFiles(pattern, SearchOption.AllDirectories).Select(x => x.OpenRead()).ToArray();

	public void LoadData(DatafileDefinition definitions)
	{
		this.Tables = new();

		foreach (var definition in definitions)
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