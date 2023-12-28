﻿using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Assets.Exports.Texture;

using CUE4Parse_Conversion.Textures;

using SkiaSharp;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;
using static Xylia.Preview.Data.Models.Item.Grocery;

namespace Xylia.Preview.Data.Models;
public abstract partial class Item : ModelElement
{
	public Ref<ItemBrand> Brand { get; set; }


	public GameCategory2Seq GameCategory2 { get; set; }

	public GameCategory3Seq GameCategory3 { get; set; }

	public MarketCategory2Seq MarketCategory2 { get; set; }

	public MarketCategory3Seq MarketCategory3 { get; set; }


	public bool Auctionable { get; set; }

	public bool AccountUsed { get; set; }

	public JobSeq[] EquipJobCheck { get; set; }

	public SexSeq2 EquipSex { get; set; }

	public Race EquipRace => Race.Get(Attributes["equip-race"].ToEnum<RaceSeq2>());

	public EquipType EquipType { get; set; }

	public sbyte ItemGrade { get; set; }



	public LegendGradeBackgroundParticleTypeSeq LegendGradeBackgroundParticleType => Attributes["legend-grade-background-particle-type"].ToEnum<LegendGradeBackgroundParticleTypeSeq>();
	public enum LegendGradeBackgroundParticleTypeSeq
	{
		None,

			TypeGold,

			TypeRedup,

			TypeGoldup,
	}


	public MainAbility MainAbility1 => Attributes["main-ability-1"].ToEnum<MainAbility>();
	public MainAbility MainAbility2 => Attributes["main-ability-2"].ToEnum<MainAbility>();


	public Msec UsableDuration => Attributes.Get<Msec>("usable-duration");
	public Ref<ItemEvent> EventInfo { get; set; }

	public int ImproveId => Attributes.Get<int>("improve-id");
	public sbyte ImproveLevel => Attributes.Get<sbyte>("improve-level");

	public string ItemName => $"<font name=\"00008130.Program.Fontset_ItemGrade_{this.ItemGrade}\">{ItemNameOnly}</font>";
	public string ItemNameOnly => Attributes["name2"].GetText();

	public int ClosetGroupId => Attributes.Get<int>("closet-group-id");

	public SKBitmap FrontIcon => Attributes["icon"].ToString().GetIcon();
	public SKBitmap Icon => ItemGrade.GetBackground().Compose(FrontIcon);
	public SKBitmap IconExtra
	{
		get
		{
			var bmp = Icon;
			if (bmp is null) return null;


			#region TopLeft
			//SKImage TopLeft = null;
			//var state = CustomDressDesignStateSeq.None;
			//if (this is Costume costume) state = costume.CustomDressDesignState;
			//else if (this is Accessory accessory) state = accessory.CustomDressDesignState;

			//if (state == CustomDressDesignStateSeq.Disabled) TopLeft = Resource_Common.Sewing;
			//else if (state == CustomDressDesignStateSeq.Activated) TopLeft = Resource_Common.Sewing2;

			//if (TopLeft != null) bmp = bmp.Combine(TopLeft, DrawLocation.TopLeft, false);
			#endregion

			#region TopRight
			SKBitmap TopRight = null;

			if (AccountUsed) TopRight = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/SlotItem_privateSale")?.Decode();
			else if (Auctionable) TopRight = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/SlotItem_marketBusiness")?.Decode();

			if (TopRight != null) bmp = bmp.Compose(TopRight);
			#endregion

			#region BottomLeft
			SKBitmap BottomLeft = null;
			if (EventInfo.Instance?.IsExpiration ?? false)
				BottomLeft = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/unuseable_olditem_3")?.Decode();
			else if (this is Grocery grocery && grocery.GroceryType == GroceryTypeSeq.Sealed)
				BottomLeft = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/Weapon_Lock_04")?.Decode();
			//else BottomLeft = new DecomposeInfo(item).GetExtra();

			if (BottomLeft != null) bmp = bmp.Compose(BottomLeft);
			#endregion

			return bmp;
		}
	}
}