using System.Drawing;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;

using Xylia.Preview.Resources;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Match.Util.Paks.Textures
{
	/// <summary>
	/// 追加内容
	/// </summary>
	public sealed class ItemQuoteInfo : QuoteInfo
	{
		#region 实例Fields
		/// <summary>
		/// 指示是否有背景
		/// </summary>
		public bool NoBG = false;

		public byte Grade;

		public GroceryTypeSeq GroceryType;
		#endregion

		public override Bitmap ProcessImage(Bitmap bitmap)
		{
			if (bitmap is null) return null;

			//如果需要背景
			if (!NoBG)
			{
				//获取物品等级背景
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
}