using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Assets.Exports.Texture;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class IconTexture : BaseRecord
{
	[Signal("icon-texture")]
	public string iconTexture;

	[Signal("icon-height")]
	public short IconHeight;

	[Signal("icon-width")]
	public short IconWidth;

	[Signal("texture-height")]
	public short TextureHeight;

	[Signal("texture-width")]
	public short TextureWidth;
}

public static class IconTextureExt
{
	public static Bitmap GetIcon(this string IconInfo, DataTableSet set = null, PakData pak = null)
	{
		GetInfo(IconInfo, out string TextureAlias, out short IconIndex);
		return GetIcon(TextureAlias, IconIndex, set, pak);
	}

	public static Bitmap GetIcon(this string TextureAlias, short IconIndex, DataTableSet set = null, PakData pak = null)
	{
		if (TextureAlias is null) return null;

		set ??= FileCache.Data;
		return GetIcon(set.IconTexture[TextureAlias], IconIndex, pak);
	}

	public static Bitmap GetIcon(this IconTexture record, short IconIndex, PakData pak = null)
	{
		if (record is null) return null;

		Bitmap TextureData = (pak ?? FileCache.PakData).LoadObject<UTexture2D>(record.iconTexture).GetImage();
		if (TextureData is null) return null;

		#region get sub
		if (record.TextureWidth == record.IconWidth && record.TextureHeight == record.IconHeight)
			return TextureData;

		// get index
		int AmountRow = record.TextureWidth / record.IconWidth;
		int RowID = IconIndex % AmountRow;
		int ColID = IconIndex / AmountRow;

		if (RowID == 0) RowID = AmountRow;
		else ColID += 1;

		//System.Diagnostics.Debug.WriteLine($"{IconIndex} => {ColID} - {RowID}");
		lock (TextureData)
		{
			try
			{
				return TextureData.Clone(new Rectangle(
					(RowID - 1) * record.IconWidth,
					(ColID - 1) * record.IconHeight,
					record.IconWidth, record.IconHeight), TextureData.PixelFormat);
			}
			catch
			{
				return null;
			}
		}
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
}