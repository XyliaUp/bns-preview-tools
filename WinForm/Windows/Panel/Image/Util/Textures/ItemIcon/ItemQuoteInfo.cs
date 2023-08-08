using Xylia.Extension;
using Xylia.Preview.UI.Extension;
using Xylia.Preview.UI.Resources;

using static Xylia.Preview.Data.Record.Item.Grocery;

namespace Xylia.Match.Util.Paks.Textures;
public sealed class ItemQuoteInfo : QuoteInfo
{
	#region Fields
	/// <summary>
	/// 指示是否有背景
	/// </summary>
	public bool NoBG = false;

	public sbyte Grade;

	public GroceryTypeSeq GroceryType;
	#endregion


	public override Bitmap ProcessImage(Bitmap bitmap)
	{
		if (bitmap is null) return null;

		//如果需要背景
		if (!NoBG)
		{
			var Background = Grade.GetBackGround();
			bitmap = Background.Combine(bitmap, DrawLocation.Centre);
		}

		//处理封印图标
		if (GroceryType == GroceryTypeSeq.Sealed)
		{
			bitmap = bitmap.Combine(Resource_BNSR.Weapon_Lock_04, DrawLocation.BottomLeft);
		}

		return bitmap;
	}
}