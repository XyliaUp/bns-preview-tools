using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class MapInfo : BaseRecord
{
	public int Id;


	[Signal("group-id")]
	public short GroupId;

	public short Floor;

	public Text Name2;

	[Signal("parent-mapinfo")]
	public MapInfo ParentMapinfo;

	public float Scale;

	public string District;

	[Signal("map-group-1")]
	public MapGroup1 MapGroup1;

	[Signal("map-group-2")]
	public MapGroup2 MapGroup2;

	[Signal("local-axis-x")]
	public float LocalAxisX;

	[Signal("local-axis-y")]
	public float LocalAxisY;

	[Signal("image-size")]
	public short ImageSize;

	public string Imageset;

	[Signal("imageset-alphamap")]
	public string ImagesetAlphamap;

	[Signal("use-pos-in-parent")]
	public bool UsePosInParent;

	[Signal("pos-in-parent-x")]
	public float PosInParentX;

	[Signal("pos-in-parent-y")]
	public float PosInParentY;

	public string Terrain;

	public float Zoom;

	[Signal("sort-no")]
	public short SortNo;

	[Signal("show-navigaion-list")]
	public bool ShowNavigaionList;


	[Signal("arena-dungeon-parent-mapinfo")]
	public string ArenaDungeonParentMapinfo;

	[Signal("arena-dungeon-use-pos-in-parent")]
	public bool ArenaDungeonUsePosInParent;

	[Signal("arena-dungeon-pos-in-parent-x")]
	public float ArenaDungeonPosInParentX;

	[Signal("arena-dungeon-pos-in-parent-y")]
	public float ArenaDungeonPosInParentY;


	#region Functions
	public static MapUnit.MapDepthSeq GetMapDepth(MapInfo MapInfo)
	{
		var ParentMapinfo = FileCache.Data.MapInfo[MapInfo.ParentMapinfo];
		if (ParentMapinfo != null) return GetMapDepth(ParentMapinfo) + 1;

		return MapUnit.MapDepthSeq.N1;
	}
	#endregion
}