using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.Core.i18N;

namespace CUE4Parse.BNS.Assets.Exports;

[StructFallback]
public struct StringProperty : IUStruct
{
	public UFontSet fontset;
	public FText LabelText;
	public float SpaceBetweenLines;
	public HorizontalAlignment HorizontalAlignment;
	public VerticalAlignment VerticalAlignment;
	public ClipMode ClipMode;	
}

public enum ClipMode
{
	BNSCustomTextClipMode_Ellipsis,
}