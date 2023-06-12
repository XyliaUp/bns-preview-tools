using System;
using System.Drawing;

using CUE4Parse.BNS;

using Xylia.Preview.Common.Attribute;


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


	public Bitmap GetIcon(short IconIndex)
	{
		Bitmap TextureData = this.iconTexture.GetUObject().GetImage();
		if (TextureData is null) return null;


		#region 裁剪内容
		if (this.TextureWidth == this.IconWidth && this.TextureHeight == this.IconHeight)
			return TextureData;

		//获取行数与列数
		int AmountRow = this.TextureWidth / this.IconWidth;

		int RowID = IconIndex % AmountRow;
		int ColID = IconIndex / AmountRow;

		//计算行列索引
		//整除表示是最后一个对象
		if (RowID == 0) RowID = AmountRow;
		else ColID += 1;

		//System.Diagnostics.Debug.WriteLine($"{IconIndex} => {ColID} - {RowID}");
		lock (TextureData)
		{
			try
			{
				return TextureData.Clone(new Rectangle(
					(RowID - 1) * this.IconWidth,
					(ColID - 1) * this.IconHeight,
					this.IconWidth, this.IconHeight), TextureData.PixelFormat);
			}
			catch
			{
				return null;
			}
		}
		#endregion
	}
}

public static class IconTextureExt
{
	public static Bitmap GetIcon(this string IconInfo)
	{
		GetInfo(IconInfo, out string TextureAlias, out short IconIndex);
		if (TextureAlias is null) return null;

		return GetIcon(TextureAlias, IconIndex);
	}

	public static Bitmap GetIcon(this string TextureAlias, short IconIndex) => FileCache.Data.IconTexture[TextureAlias]?.GetIcon(IconIndex);


	public static void GetInfo(this string IconInfo, out string TextureAlias, out short IconIndex)
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