using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.i18N;

namespace CUE4Parse.BNS.Objects;
public struct StringProperty : IUStruct
{
	public ResolvedObject fontset;
	public FText LabelText;
	public float SpaceBetweenLines;
	public HorizontalAlignment HorizontalAlignment;
	public VerticalAlignment VerticalAlignment;
	public ClipMode ClipMode;

	public static StringProperty? Load(FStructFallback data) => data is null ? null : new()
	{
		fontset = data.GetOrDefault<ResolvedObject>(nameof(fontset)),
		LabelText = data.GetOrDefault<FText>(nameof(LabelText)),
	};
}

public enum ClipMode
{
	BNSCustomTextClipMode_Ellipsis,
}