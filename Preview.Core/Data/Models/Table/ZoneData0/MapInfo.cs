using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class MapInfo : ModelElement  ,IHaveName
{
	public int Id { get; set; }
	public string Alias { get; set; }


	public short GroupId { get; set; }

	public short Floor { get; set; }

	public Ref<Text> Name2 { get; set; }

	public Ref<MapInfo> ParentMapinfo { get; set; }

	public float Scale { get; set; }

	public float LocalAxisX { get; set; }

	public float LocalAxisY { get; set; }

	public short ImageSize { get; set; }

	public string Imageset { get; set; }

	public string ImagesetAlphamap { get; set; }

	public bool UsePosInParent { get; set; }

	public float PosInParentX { get; set; }

	public float PosInParentY { get; set; }

	public string Terrain { get; set; }

	public float Zoom { get; set; }

	public short SortNo { get; set; }

	public string ArenaDungeonParentMapinfo { get; set; }

	public bool ArenaDungeonUsePosInParent { get; set; }

	public float ArenaDungeonPosInParentX { get; set; }

	public float ArenaDungeonPosInParentY { get; set; }


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