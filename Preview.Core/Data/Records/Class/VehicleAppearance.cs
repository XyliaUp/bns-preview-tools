using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public class VehicleAppearance : BaseRecord
{
	[Signal("anim-set-name")]
	public FPath AnimSetName;

	[Signal("move-anim-set-name")]
	public FPath MoveAnimSetName;

	[Signal("anim-tree-name")]
	public FPath AnimTreeName;

	[Signal("mesh-name")]
	public FPath MeshName;

	[Signal("material-name"), Repeat(3)]
	public FPath[] MaterialName;

}