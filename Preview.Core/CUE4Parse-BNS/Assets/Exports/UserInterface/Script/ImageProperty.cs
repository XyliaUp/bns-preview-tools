using System.ComponentModel;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.Core.Serialization;
using CUE4Parse.UE4.Objects.UObject;
using CUE4Parse_Conversion.Sounds;
using CUE4Parse_Conversion.Textures;
using SkiaSharp;

namespace CUE4Parse.BNS.Assets.Exports;
[StructFallback]
public class ImageProperty : IUStruct
{
	[TypeConverter(typeof(FPackageIndexTypeConverter))] public FPackageIndex BaseImageTexture { get; set; }
	[TypeConverter(typeof(FPackageIndexTypeConverter))] public FPackageIndex ImageSet { get; set; }
	public FStructFallback ImageBrush { get; set; }
	[TypeConverter(typeof(Vector2DConverter))] public FVector2D ImageUV { get; set; }
	[TypeConverter(typeof(Vector2DConverter))] public FVector2D ImageUVSize { get; set; }

	public bool EnableImageSet { get; set; }
	public bool EnableBrushOnly { get; set; }
	public bool EnableDrawImage { get; set; }
	public bool EnableResourceSize { get; set; }
	public bool EnableFullImage { get; set; }
	public bool EnableFittedImage { get; set; }
	public bool EnableFittedImage_MinifyOnly { get; set; }
	public bool EnableClipping { get; set; }
	public bool EnableSkinColor { get; set; }
	public bool EnableSkinAlpha { get; set; }
	public bool EnableAdditiveBlendMode { get; set; }
	public bool EnableResourceGray { get; set; }
	public bool EnableDrawColor { get; set; }
	public bool EnableMultiImage { get; set; }
	public TintColor TintColor { get; set; }
	public float GrayWeightValue { get; set; }
	[TypeConverter(typeof(Vector2DConverter))] public FVector2D StaticPadding { get; set; }
	[TypeConverter(typeof(Vector2DConverter))] public FVector2D Offset { get; set; }
	public float Opacity { get; set; }
	public float ImageScale { get; set; }
	public HAlignment HorizontalAlignment { get; set; }
	public VAlignment VerticalAlignment { get; set; }
	public string SperateType { get; set; }
	public string SperateImageType { get; set; }   //BNS_SperateImageType_3Frame

	public FVector2D[] CoordinatesArray;  // ignore output

	// ************************************************* //
	#region Methods
	public ImageProperty Clone()
	{
		return (ImageProperty)this.MemberwiseClone();
	}

	public SKBitmap Image
	{
		get
		{
			#region Raw
			SKBitmap bitmap = null;

			if (BaseImageTexture != null)
			{
				var obj = BaseImageTexture.LoadEx();
				if (obj is UTexture texture) bitmap = texture.Decode()?.Clone(ImageUV, ImageUVSize);
			}
			else if (ImageSet != null)
			{
				var obj = ImageSet.LoadEx();
				if (obj is UImageSet imageSet) bitmap = imageSet.GetImage();
			}

			if (bitmap is null) return null;
			#endregion

			#region Draw
			SKColor? tint = EnableDrawColor ? TintColor.SpecifiedColor.ToSKColor() : null;

			// HACK: why there have different ways ?
			float opacity = EnableDrawImage ? Opacity : (1 - Opacity);

			// EnableResourceGray

			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					var p = bitmap.GetPixel(i, j);

					// TODO: not working perfectly
					if (tint != null && p.Alpha > 64) p = tint.Value;
	
					// set alpha channel
					p = p.WithAlpha((byte)(p.Alpha * opacity));

					bitmap.SetPixel(i, j, p);
				}
			}
			#endregion

			return bitmap;
		}
	}

	public FVector2D Measure(FVector2D RenderSize, out SKBitmap image)
	{
		image = Image;
		if (image is null) return FVector2D.ZeroVector;

		if (ImageScale <= 0) ImageScale = 1.0F;
		return EnableResourceSize ?
			new FVector2D(ImageScale * image.Width, ImageScale * image.Height) :
			new FVector2D(ImageScale * RenderSize.X, ImageScale * RenderSize.Y);
	}
	#endregion
}