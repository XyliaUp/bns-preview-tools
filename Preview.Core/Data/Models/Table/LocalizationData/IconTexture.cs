using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class IconTexture : ModelElement
{
	#region Attributes
	public string iconTexture { get; set; }
	public short IconHeight { get; set; }
	public short IconWidth { get; set; }
	public short TextureHeight { get; set; }
	public short TextureWidth { get; set; }
	#endregion

	#region Methods
	public (FVector2D, FVector2D) GetRect(short index)
	{
		int amountRow = this.TextureWidth / this.IconWidth;
		int row = index % amountRow;
		int col = index / amountRow;

		if (row == 0)
		{
			row = amountRow;
			col--;
		}
		row--;

		return (
			new FVector2D(row * IconWidth, col * IconHeight),
			new FVector2D(IconWidth, IconHeight));
	}

	public ImageProperty GetIcon(short index, IFileProvider pak = null)
	{
		var rect = this.GetRect(index);
		return new ImageProperty()
		{
			BaseImageTexture = new MyFPackageIndex(iconTexture, pak),
			ImageUV = rect.Item1,
			ImageUVSize = rect.Item2,
		};
	}


	public static ImageProperty Parse(string value, BnsDatabase db = null, IFileProvider pak = null)
	{
		if (!string.IsNullOrWhiteSpace(value) && value.Contains(','))
		{
			var split = value.Split(',', 2);
			var alias = split[0];
			if (!short.TryParse(split[^1], out var index))
				throw new Exception("get icon index failed: " + value);

			db ??= FileCache.Data;
			return db.Provider.GetTable<IconTexture>()[alias]?.GetIcon(index, pak);
		}

		return null;
	}

	public static ImageProperty GetBackground(sbyte grade, IFileProvider pak = null)
	{
		return new ImageProperty()
		{
			BaseImageTexture = new MyFPackageIndex($"BNSR/Content/Art/UI/GameUI/Resource/GameUI_Window_R/ItemIcon_Bg_Grade_{grade}", pak),
		};
	}
	#endregion
}