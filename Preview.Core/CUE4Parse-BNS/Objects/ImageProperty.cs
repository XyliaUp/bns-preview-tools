using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;

using SkiaSharp;

using Xylia.Extension;

namespace CUE4Parse.BNS.Objects;
public struct ImageProperty	: IUStruct
{
	public ResolvedObject BaseImageTexture;
	public FVector2D ImageUV;
	public FVector2D ImageUVSize;
	public HorizontalAlignment HorizontalAlignment;
	public VerticalAlignment VerticalAlignment;


	public float Opacity;
	public bool EnableDrawImage;
	public bool EnableSkinColor;
	public string SperateImageType;
	public object[] CoordinatesArray;


	public static ImageProperty? Load(FStructFallback data) => data is null ? null : new()
	{
		data = data,

		BaseImageTexture = data.GetOrDefault<ResolvedObject>(nameof(BaseImageTexture)),
		ImageUV = data.GetOrDefault<FVector2D>(nameof(ImageUV)),
		ImageUVSize = data.GetOrDefault<FVector2D>(nameof(ImageUVSize)),
		HorizontalAlignment = data.GetOrDefault<string>(nameof(HorizontalAlignment)).ToEnum<HorizontalAlignment>(),
		VerticalAlignment = data.GetOrDefault<string>(nameof(VerticalAlignment)).ToEnum<VerticalAlignment>(),
	};

	public FStructFallback data;



	public SKBitmap GetBitmap => BaseImageTexture?.Load()?.GetImage()?.Clone(ImageUV, ImageUVSize);
}



public enum HorizontalAlignment
{
	HAlign_Left,
	HAlign_Center,
	HAlign_Right,
}

public enum VerticalAlignment
{
	VAlign_Left,
	VAlign_Center,
	VAlign_Right,
}