using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;

using CUE4Parse_Conversion.Sounds;
using CUE4Parse_Conversion.Textures;

using SkiaSharp;

namespace CUE4Parse.BNS.Assets.Exports;

[StructFallback]
public struct ImageProperty : IUStruct
{
	public FPackageIndex BaseImageTexture;
	public FVector2D ImageUV;
	public FVector2D ImageUVSize;
	public HorizontalAlignment HorizontalAlignment;
	public VerticalAlignment VerticalAlignment;

	public float Opacity;
	public bool EnableDrawImage;
	public bool EnableSkinColor;
	public string SperateImageType;
	public object[] CoordinatesArray;

	public readonly SKBitmap Image
	{
		get
		{
			var obj = BaseImageTexture?.Load();
			if (obj is UTexture texture) return texture.Decode()?.Clone(ImageUV, ImageUVSize);
			//if (obj is MaterialInstanceConstant texture)

			return null;
		}
	}
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