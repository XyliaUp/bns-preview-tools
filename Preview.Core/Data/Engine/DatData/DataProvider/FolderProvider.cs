using CUE4Parse.Utils;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Engine.DatData;
public class FolderProvider(string path) : IDataProvider
{
	#region Properties
	public virtual string Name => path.SubstringAfterLast('\\');	  
	public virtual DateTimeOffset CreatedAt => default;
	public virtual BnsVersion ClientVersion => default;
	public TableCollection Tables { get; private set; }
	#endregion

	#region Methods
	public virtual Stream[] GetFiles(string pattern) => new DirectoryInfo(path).GetFiles(pattern, SearchOption.AllDirectories).Select(x => x.OpenRead()).ToArray();

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

			Tables.Add(new() { Owner = this, Name = definition.Name, SearchPattern = path });
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