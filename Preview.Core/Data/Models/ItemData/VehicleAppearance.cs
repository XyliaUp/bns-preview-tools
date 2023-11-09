using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class VehicleAppearance : Record
{
	public string Alias;


	public ObjectPath AnimSetName;
	public ObjectPath MoveAnimSetName;
	public ObjectPath AnimTreeName;
	public ObjectPath MeshName;

	[Repeat(3)]
	public ObjectPath[] MaterialName;
}