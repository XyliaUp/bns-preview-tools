using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class MapInfo : ModelElement  ,IHaveName
{
	public int Id;
	public string Alias;


	[Name("group-id")]
	public short GroupId;

	public short Floor;

	public Ref<Text> Name2;

	[Name("parent-mapinfo")]
	public Ref<MapInfo> ParentMapinfo;

	public float Scale;

	[Name("local-axis-x")]
	public float LocalAxisX;

	[Name("local-axis-y")]
	public float LocalAxisY;

	[Name("image-size")]
	public short ImageSize;

	public string Imageset;

	[Name("imageset-alphamap")]
	public string ImagesetAlphamap;

	[Name("use-pos-in-parent")]
	public bool UsePosInParent;

	[Name("pos-in-parent-x")]
	public float PosInParentX;

	[Name("pos-in-parent-y")]
	public float PosInParentY;

	public string Terrain;

	public float Zoom;

	[Name("sort-no")]
	public short SortNo;

	[Name("arena-dungeon-parent-mapinfo")]
	public string ArenaDungeonParentMapinfo;

	[Name("arena-dungeon-use-pos-in-parent")]
	public bool ArenaDungeonUsePosInParent;

	[Name("arena-dungeon-pos-in-parent-x")]
	public float ArenaDungeonPosInParentX;

	[Name("arena-dungeon-pos-in-parent-y")]
	public float ArenaDungeonPosInParentY;


	#region Methods
	public string Text => Name2.GetText();

	public static MapUnit.MapDepthSeq GetMapDepth(MapInfo MapInfo)
	{
		var ParentMapinfo = MapInfo.ParentMapinfo.Instance;
		if (ParentMapinfo != null) return GetMapDepth(ParentMapinfo) + 1;

		return MapUnit.MapDepthSeq.N1;
	}
	#endregion
}