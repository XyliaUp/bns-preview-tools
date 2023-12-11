using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public class VehicleAppearance : ModelElement
{
	public ObjectPath AnimSetName;
	public ObjectPath MoveAnimSetName;
	public ObjectPath AnimTreeName;
	public ObjectPath MeshName;

	[Repeat(3)]
	public ObjectPath[] MaterialName;
}