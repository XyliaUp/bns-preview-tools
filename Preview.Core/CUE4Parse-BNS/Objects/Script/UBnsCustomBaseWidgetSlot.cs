using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.BNS.Objects.Script;
public class UBnsCustomBaseWidgetSlot : UObject
{
	public FStructFallback LayoutData;
	public ResolvedObject Parent;
	public ResolvedObject Content;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		LayoutData = GetOrDefault<FStructFallback>(nameof(LayoutData));
		Parent = GetOrDefault<ResolvedObject>(nameof(Parent));
		Content = GetOrDefault<ResolvedObject>(nameof(Content));
	}


	public struct FLayoutData
	{
		public Offset Offsets;

		public struct Offset
		{
			public float Left;
			public float Top;
			public float Right;
			public float Bottom;
		}
	}
}