using CUE4Parse.BNS.Conversion;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse_Conversion.Textures;
using SkiaSharp;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class IconTexture : ModelElement
{
	public string iconTexture { get; set; }

	public short IconHeight { get; set; }
	public short IconWidth { get; set; }

	public short TextureHeight { get; set; }
	public short TextureWidth { get; set; }


	#region Methods
	public SKBitmap GetIcon(short index, DefaultFileProvider pak = null)
	{
		var raw = Task.Run(() => (pak ?? FileCache.Provider).LoadObject<UTexture2D>(this.iconTexture)).Result?.Decode();
		if (raw is null || index == 0) return raw;

		var rect = this.GetRect(index);
		return raw.Clone(rect.Item1, rect.Item2, rect.Item3, rect.Item4);
	}

	public (float, float, float, float) GetRect(short index)
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

		return new(row * IconWidth, col * IconHeight, IconWidth, IconHeight);
	}

	public static IconTexture Parse(string value, out short index, BnsDatabase set = null)
	{
		if (!string.IsNullOrWhiteSpace(value) && value.Contains(','))
		{
			var split = value.Split(',', 2);
			var alias = split[0];
			if (!short.TryParse(split[^1], out index))
				throw new Exception("get icon index failed: " + value);

			set ??= FileCache.Data;
			return set.IconTexture[alias];
		}

		index = 0;
		return null;
	}
	#endregion
}

public static class IconTextureExt
{
	public static SKBitmap GetIcon(this string value, BnsDatabase set = null, DefaultFileProvider pak = null)
	{
		var record = IconTexture.Parse(value, out var index, set);
		return record?.GetIcon(index, pak);
	}

	public static SKBitmap GetBackground(this sbyte grade, DefaultFileProvider pak = null)
	{
		pak ??= FileCache.Provider;
		return Task.Run(() => pak.LoadObject<UTexture2D>($"BNSR/Content/Art/UI/GameUI/Resource/GameUI_Window_R/ItemIcon_Bg_Grade_{grade}")).Result?.Decode();
	}
}