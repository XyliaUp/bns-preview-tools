using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Helpers;

namespace Xylia.Preview.Data.Engine.DatData;
/// <summary>
/// bns data package provider
/// </summary>
public interface IDataProvider : IDisposable
{
	#region Properties
	/// <summary>
	/// DataSource name
	/// </summary>
	string Name => null;

	bool is64Bit => true;

	DateTime CreatedAt => default;

	Common.DataStruct.Version ClientVersion => default;

	/// <summary>
	/// bns data table
	/// </summary>
	TableCollection Tables { get; set; }

	/// <summary>
	/// DataSource localized information
	/// </summary>
	Locale Locale => null;
	#endregion

	#region Methods
	Stream[] GetFiles(string pattern);

	/// <summary>
	/// Load table
	/// In some cases, require automatic conversion
	/// </summary>
	/// <param name="definitions"></param>
	public void LoadData(List<TableDefinition> definitions);
	#endregion
}