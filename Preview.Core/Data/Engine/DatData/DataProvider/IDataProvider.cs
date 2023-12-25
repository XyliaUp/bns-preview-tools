using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Serialization;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Engine.DatData;
/// <summary>
/// bns data package provider
/// </summary>
public interface IDataProvider : IDisposable
{
	#region Properties
	/// <summary>
	/// DataSource Name
	/// </summary>
	string Name { get; }

	/// <summary>
	/// DataSource Timestamp
	/// </summary>
	DateTime CreatedAt { get; }

	/// <summary>
	/// DataSource Version
	/// </summary>
	BnsVersion ClientVersion { get; }

	/// <summary>
	/// bns data table
	/// </summary>
	TableCollection Tables { get; }

	/// <summary>
	/// DataSource localized information
	/// </summary>
	Locale Locale => null;
	#endregion


	#region Methods
	/// <summary>
	/// Get raw file
	/// </summary>
	/// <param name="pattern"></param>
	/// <returns></returns>
	Stream[] GetFiles(string pattern);

	/// <summary>
	/// Load package
	/// </summary>
	/// <remarks>In some cases, require automatic parse <see langword="definitions"/></remarks>
	/// <param name="definitions"></param>
	public void LoadData(DatafileDefinition definitions);

	/// <summary>
	/// Write package
	/// </summary>
	/// <param name="folder"></param>
	/// <param name="settings"></param>
	public void WriteData(string folder, PublishSettings settings) => throw new NotImplementedException();
	#endregion
}