using CUE4Parse.BNS.Conversion;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Assets.Exports.Texture;

using CUE4Parse_Conversion.Textures;

using SkiaSharp;

using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class IconTexture : Record
{
	public string Alias;

	public string iconTexture;

	public short IconHeight;
	public short IconWidth;

	public short TextureHeight;
	public short TextureWidth;
}

public static class IconTextureExt
{
	public static SKBitmap GetIcon(this string @ref, BnsDatabase set = null, DefaultFileProvider pak = null)
	{
		if (!string.IsNullOrWhiteSpace(@ref) && @ref.Contains(','))
		{
			var split = @ref.Split(',', 2);
			var alias = split[0];
			if (!short.TryParse(split[^1], out var index))
				throw new Exception("get icon index failed: " + @ref);

			return GetIcon(alias, index, set, pak);
		}

		return null;
	}

	public static SKBitmap GetIcon(this string alias, short index, BnsDatabase set = null, DefaultFileProvider pak = null)
	{
		if (alias is null) return null;

		set ??= FileCache.Data;
		return GetIcon(set.IconTexture[alias], index, pak);
	}

	public static SKBitmap GetIcon(this IconTexture record, short index, DefaultFileProvider pak = null)
	{
		if (record is null) return null;

		var raw = Task.Run(() => (pak ?? FileCache.Provider).LoadObject<UTexture2D>(record.iconTexture)).Result?.Decode();
		if (raw is null || index == 0) return raw;

		#region get sub
		// get index
		int amountRow = record.TextureWidth / record.IconWidth;
		int row = index % amountRow;
		int col = index / amountRow;

		if (row == 0)
		{
			row = amountRow;
			col--;
		}
		row--;

		return raw.Clone(row * record.IconWidth, col * record.IconHeight, record.IconWidth, record.IconHeight);
		#endregion
	}


	public static SKBitmap GetBackground(this sbyte grade, DefaultFileProvider pak = null)
	{
		pak ??= FileCache.Provider;
		return Task.Run(() => pak.LoadObject<UTexture2D>($"BNSR/Content/Art/UI/GameUI/Resource/GameUI_Window_R/ItemIcon_Bg_Grade_{grade}")).Result.Decode();
	}
}