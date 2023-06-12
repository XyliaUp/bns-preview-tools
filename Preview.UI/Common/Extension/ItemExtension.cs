using System.Collections.Concurrent;

using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Preview.Reward;
using Xylia.Preview.Resources;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.Common.Extension
{
	public static class ItemExtension
	{
		/// <summary>
		/// with background picture
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static Bitmap Icon(this Item item) => item.Attributes["icon"].GetIconWithGrade(item.ItemGrade);

		public static Bitmap Icon(this ItemBrandTooltip item) => item.Attributes["icon"].GetIconWithGrade(item.ItemGrade);

		/// <summary>
		/// with extra info
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static Bitmap IconExtra(this Item item)
		{
			var bmp = item.Icon();
			if (bmp is null) return null;


			#region TopLeft
			Bitmap TopLeft = null;
			if (item.CustomDressDesignState == CustomDressDesignStateSeq.Disabled) TopLeft = Resource_Common.Sewing;
			else if (item.CustomDressDesignState == CustomDressDesignStateSeq.Activated) TopLeft = Resource_Common.Sewing2;

			if (TopLeft != null) bmp = bmp.Combine(TopLeft, DrawLocation.TopLeft, false);
			#endregion

			#region TopRight
			Bitmap TopRight = null;

			if (item.AccountUsed) TopRight = Resource_BNSR.SlotItem_privateSale;
			else if (item.Auctionable) TopRight = Resource_BNSR.SlotItem_marketBusiness;

			if (TopRight != null) bmp = bmp.Combine(TopRight, DrawLocation.TopRight);
			#endregion

			#region BottomLeft
			Bitmap BottomLeft;
			if (item.EventInfo != null && item.EventInfo.IsExpiration) BottomLeft = Resource_BNSR.unuseable_olditem_3;   //判断是否过期			  
			else if (item.GroceryType == GroceryTypeSeq.Sealed) BottomLeft = Resource_BNSR.Weapon_Lock_04;  //判断是否是封印状态
			else BottomLeft = new DecomposeInfo(item).GetExtra();

			if (BottomLeft != null) bmp = bmp.Combine(BottomLeft, DrawLocation.BottomLeft);
			#endregion

			return bmp;
		}

		




		public static Bitmap GetBackGround(this byte Grade, bool IsUE4 = true) => Grade switch
		{
			2 => IsUE4 ? Resource_BNSR.ItemIcon_Bg_Grade_2 : Resource_Common.ItemIcon_Bg_Grade_2,
			3 => IsUE4 ? Resource_BNSR.ItemIcon_Bg_Grade_3 : Resource_Common.ItemIcon_Bg_Grade_3,
			4 => IsUE4 ? Resource_BNSR.ItemIcon_Bg_Grade_4 : Resource_Common.ItemIcon_Bg_Grade_4,
			5 => IsUE4 ? Resource_BNSR.ItemIcon_Bg_Grade_5 : Resource_Common.ItemIcon_Bg_Grade_5,
			6 => IsUE4 ? Resource_BNSR.ItemIcon_Bg_Grade_6 : Resource_Common.ItemIcon_Bg_Grade_6,
			7 => IsUE4 ? Resource_BNSR.ItemIcon_Bg_Grade_7 : Resource_Common.ItemIcon_Bg_Grade_7,
			8 => IsUE4 ? Resource_BNSR.ItemIcon_Bg_Grade_8 : Resource_Common.ItemIcon_Bg_Grade_9,
			9 => IsUE4 ? Resource_BNSR.ItemIcon_Bg_Grade_9 : Resource_Common.ItemIcon_Bg_Grade_8,

			1 or _ => IsUE4 ? Resource_BNSR.ItemIcon_Bg_Grade_1 : Resource_Common.ItemIcon_Bg_Grade_1,
		};

		public static Bitmap GetIconWithGrade(this string IconInfo, byte Grade) => string.IsNullOrWhiteSpace(IconInfo) ? null :
			Grade.GetBackGround(true).Combine(IconInfo.GetIcon());


		public static List<Item> GetItemInfo(this string rule, bool UseExt = false)
		{
			Item data;
			if (!rule.Contains('+') && int.TryParse(rule, out var id)) data = FileCache.Data.Item[id, 1];
			else data = FileCache.Data.Item[rule.Trim()];
			if (data != null) return new() { data };


			BlockingCollection<Item> lst = new();
			Parallel.ForEach(FileCache.Data.Item, Info =>
			{
				var ItemName = Info.Name2;
				if (ItemName != null)
				{
					if (UseExt)
					{
						if (ItemName.IndexOf(rule, StringComparison.OrdinalIgnoreCase) < 0) return;
					}
					else if (ItemName != rule) return;

					lst.Add(Info);
				}
			});

			return lst.ToList();
		}
	}
}