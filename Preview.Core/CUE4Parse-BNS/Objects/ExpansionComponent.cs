using System.Drawing;

using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

using SkiaSharp;

namespace CUE4Parse.BNS.Objects;
public struct ExpansionComponent : IUStruct
{
	public bool bVisibleFlag;
	public bool bShow;
	public FName ExpansionType;
	public FName ExpansionName;
	public string MetaData;
	public ImageComponentProperty ImageProperty;
	public FVector2D ImageUV;
	public FVector2D ImageUVSize;
	public bool EnableDrawImage;
	public bool EnableResourceSize;
	public bool EnableFullImage;
	public bool EnableFittedImage;
	public bool EnableFittedImage_MinifyOnly;
	public bool EnableClipping;
	public bool EnableSkinColor;
	public bool EnableSkinAlpha;
	public bool EnableAdditiveBlendMode;
	public bool EnableResourceGray;
	public bool EnableDrawColor;


	public float Opacity;
	public float ImageScale;
	public string HorizontalAlignment;
	public string VerticalAlignment;
	public string SperateType;
	public string SperateImageType;
	//public object CoordinatesArray;
	public bool EnableMultiImage;




	public FStructFallback data;

	public ExpansionComponent(FStructFallback data)
	{
		this.data = data;

		ExpansionType = data.GetOrDefault<FName>(nameof(ExpansionType));
		ExpansionName = data.GetOrDefault<FName>(nameof(ExpansionName));

		ImageProperty = new(data.GetOrDefault<FStructFallback>(nameof(ImageProperty)));
		ImageUV = data.GetOrDefault<FVector2D>(nameof(ImageUV));
		ImageUVSize = data.GetOrDefault<FVector2D>(nameof(ImageUVSize));
	}


	public SKBitmap GetBitmap => ImageProperty.BaseImageTexture?.Load()?.GetImage()?.Clone(ImageProperty.ImageUV, ImageProperty.ImageUVSize);
}

public struct ImageComponentProperty
{
	public ResolvedObject BaseImageTexture;
	public bool EnableImageSet;
	public ResolvedObject ImageSet;
	public bool EnableBrushOnly;
	public FStructFallback ImageBrush;
	public FVector2D ImageUV;
	public FVector2D ImageUVSize;

	public ResolvedObject ResourceObject;
	public string ResourceName;
	//UVRegion

	public string DrawAs;
	public string Tiling;
	public string Mirroring;
	public string ImageType;
	public bool bIsDynamicallyLoaded;


	public ImageComponentProperty(FStructFallback data)
	{
		BaseImageTexture = data.GetOrDefault<ResolvedObject>(nameof(BaseImageTexture));
		ImageUV = data.GetOrDefault<FVector2D>(nameof(ImageUV));
		ImageUVSize = data.GetOrDefault<FVector2D>(nameof(ImageUVSize));
	}
}