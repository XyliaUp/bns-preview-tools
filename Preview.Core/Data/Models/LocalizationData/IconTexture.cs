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
	public static SKBitmap GetIcon(this string IconInfo, BnsDatabase set = null, DefaultFileProvider pak = null)
	{
		GetInfo(IconInfo, out string TextureAlias, out short IconIndex);
		return GetIcon(TextureAlias, IconIndex, set, pak);
	}

	public static SKBitmap GetIcon(this string TextureAlias, short IconIndex, BnsDatabase set = null, DefaultFileProvider pak = null)
	{
		if (TextureAlias is null) return null;

		set ??= FileCache.Data;
		return GetIcon(set.IconTexture[TextureAlias], IconIndex, pak);
	}

	public static SKBitmap GetIcon(this IconTexture record, short IconIndex, DefaultFileProvider pak = null)
	{
		if (record is null) return null;

		var TextureData = Task.Run(() => (pak ?? FileCache.Provider).LoadObject<UTexture2D>(record.iconTexture)).Result.Decode();
		if (TextureData is null) return null;

		#region get sub
		if (record.TextureWidth == record.IconWidth && record.TextureHeight == record.IconHeight)
			return TextureData;

		// get index
		int AmountRow = record.TextureWidth / record.IconWidth;
		int Row = IconIndex % AmountRow;
		int Col = IconIndex / AmountRow;

		if (Row == 0) Row = AmountRow;
		else Col += 1;

		return TextureData.Clone((Row - 1) * record.IconWidth, (Col - 1) * record.IconHeight, record.IconWidth, record.IconHeight);
		#endregion
	}


	private static void GetInfo(this string IconInfo, out string TextureAlias, out short IconIndex)
	{
		TextureAlias = null;
		IconIndex = 0;
		if (string.IsNullOrWhiteSpace(IconInfo)) return;

		//get index
		if (IconInfo.Contains(','))
		{
			var IconSplit = IconInfo.Split(',');
			TextureAlias = IconSplit[0];

			if (short.TryParse(IconSplit[^1], out IconIndex)) return;
			else throw new Exception("get icon index failed: " + IconInfo);
		}

		TextureAlias = IconInfo;
		IconIndex = 1;
		return;
	}



	public static SKBitmap GetBackground(this sbyte grade, DefaultFileProvider pak = null)
	{
		pak ??= FileCache.Provider;
		return Task.Run(() => pak.LoadObject<UTexture2D>($"BNSR/Content/Art/UI/GameUI/Resource/GameUI_Window_R/ItemIcon_Bg_Grade_{grade}")).Result.Decode();
	}
}