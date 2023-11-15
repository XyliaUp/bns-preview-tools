using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

using CUE4Parse_Conversion.Sounds;
using CUE4Parse_Conversion.Textures;

using SkiaSharp;

namespace CUE4Parse.BNS.Assets.Exports;
[StructFallback]
public struct ExpansionComponent : IUStruct
{
	public bool bVisibleFlag;
	public bool bShow;
	public FName ExpansionType;
	public FName ExpansionName;
	public string MetaData;
	public string WidgetState;      //BNSCustomWidgetState_None
	public bool bEnableSubState;
	public string WidgetSubState;   //Expansion_WidgetSubState_Normal
	public bool bPostExpansitonRender;
	public FStructFallback PublisherVisible;
	public FStructFallback MetaDataByPublisher;
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
	public FStructFallback TintColor;
	public float GrayWeightValue;
	public FVector2D StaticPadding;
	public FVector2D Offset;
	public float Opacity;
	public float ImageScale;
	public string HorizontalAlignment;
	public string VerticalAlignment;
	public string SperateType;
	public string SperateImageType;
	public FStructFallback CoordinatesArray;
	public bool EnableMultiImage;


	public readonly SKBitmap Image => ImageProperty.BaseImageTexture?.Load<UTexture>()?.Decode()?.Clone(ImageProperty.ImageUV, ImageProperty.ImageUVSize);
}

[StructFallback]
public struct ImageComponentProperty
{
	public FPackageIndex BaseImageTexture;
	public bool EnableImageSet;
	public FPackageIndex ImageSet;
	public bool EnableBrushOnly;
	public FStructFallback ImageBrush;
	public FVector2D ImageUV;
	public FVector2D ImageUVSize;

	public FPackageIndex ResourceObject;
	public string ResourceName;
	public FStructFallback UVRegion;
	public string DrawAs;
	public string Tiling;
	public string Mirroring;
	public string ImageType;
	public bool bIsDynamicallyLoaded;
}