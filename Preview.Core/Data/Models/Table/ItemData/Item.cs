using System.Text;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse_Conversion.Textures;
using SkiaSharp;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Creature;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public abstract partial class Item : ModelElement
{
	#region Fields
	public Ref<ItemBrand> Brand { get; set; }

	public bool Auctionable { get; set; }

	public bool AccountUsed { get; set; }

	public GameCategory3Seq GameCategory3 => Attributes["game-category-3"].ToEnum<GameCategory3Seq>();

	public JobSeq[] EquipJobCheck { get; set; }

	public SexSeq2 EquipSex { get; set; }

	public Race EquipRace => Race.Get(Attributes["equip-race"].ToEnum<RaceSeq2>());

	public EquipType EquipType { get; set; }

	public sbyte ItemGrade { get; set; }
	public LegendGradeBackgroundParticleTypeSeq LegendGradeBackgroundParticleType => Attributes["legend-grade-background-particle-type"].ToEnum<LegendGradeBackgroundParticleTypeSeq>();

	public int ImproveId { get; set; }
	public sbyte ImproveLevel { get; set; }
	public string ItemName => $"""<font name="00008130.Program.Fontset_ItemGrade_{ItemGrade}">{ItemNameOnly}</font>""";
	public string ItemNameOnly => Attributes["name2"].GetText();

	public int ClosetGroupId => Attributes.Get<int>("closet-group-id");
	#endregion

	#region Methods
	public ItemDecomposeInfo DecomposeInfo => new(this);

	public bool IsExpiration
	{
		get
		{
			var Time = Attributes.Get<Record>("event-info")?.Attributes.Get<Time64>("event-expiration-time");
			if (Time is null) return false;

			return Time.Value < DateTimeOffset.Now.ToUnixTimeSeconds();
		}
	}

	public ImageProperty FrontIcon => IconTexture.Parse(Attributes.Get<string>("icon"));

	public SKBitmap Icon
	{
		get
		{
			var bmp = IconTexture.GetBackground(ItemGrade).Compose(FrontIcon?.Image);
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
			if (IsExpiration)
				BottomLeft = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/unuseable_olditem_3")?.Decode();
			else if (this is Grocery grocery && grocery.GroceryType == Grocery.GroceryTypeSeq.Sealed)
				BottomLeft = FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_Icon3_R/Weapon_Lock_04")?.Decode();
			//else BottomLeft = new DecomposeInfo(item).GetExtra();

			if (BottomLeft != null) bmp = bmp.Compose(BottomLeft);
			#endregion

			return bmp;
		}
	}

	public Tuple<string, string> CollectionSubstitute
	{
		get
		{
			StringBuilder Substitute1 = new(), Substitute2 = new();

			#region Info
			var MainInfo = Attributes.Get<Record>("main-info").GetText();
			var SubInfo = Attributes.Get<Record>("sub-info").GetText();
			if (MainInfo != null) Substitute1.AppendLine(MainInfo);
			if (SubInfo != null) Substitute2.AppendLine(SubInfo);
			#endregion

			#region Ability
			var data = new Dictionary<MainAbility, long>();

			var AttackPowerEquipMin = this.Attributes.Get<short>("attack-power-equip-min");
			var AttackPowerEquipMax = this.Attributes.Get<short>("attack-power-equip-max");
			data[MainAbility.AttackPowerEquipMinAndMax] = (AttackPowerEquipMin + AttackPowerEquipMax) / 2;

			var PveBossLevelNpcAttackPowerEquipMin = this.Attributes.Get<short>("pve-boss-level-npc-attack-power-equip-min");
			var PveBossLevelNpcAttackPowerEquipMax = this.Attributes.Get<short>("pve-boss-level-npc-attack-power-equip-max");
			data[MainAbility.PveBossLevelNpcAttackPowerEquipMinAndMax] = (PveBossLevelNpcAttackPowerEquipMin + PveBossLevelNpcAttackPowerEquipMax) / 2;

			var PvpAttackPowerEquipMin = this.Attributes.Get<short>("pvp-attack-power-equip-min");
			var PvpAttackPowerEquipMax = this.Attributes.Get<short>("pvp-attack-power-equip-max");
			data[MainAbility.PvpAttackPowerEquipMinAndMax] = (PvpAttackPowerEquipMin + PvpAttackPowerEquipMax) / 2;

			// HACK: Actually, the ability value is single get
			foreach (var seq in Enum.GetValues<MainAbility>())
			{
				if (seq == MainAbility.None) continue;

				var name = seq.ToString().TitleLowerCase();
				var value = Convert.ToInt32(this.Attributes[name]);
				if (value != 0) data[seq] = value;
				else if (seq != MainAbility.AttackAttributeValue)
				{
					var value2 = Convert.ToInt32(this.Attributes[name + "-equip"]);
					if (value2 != 0) data[seq] = value2;
				}
			}

			// HACK: Actually, the MainAbility is not this sequence
			var MainAbility1 = Attributes["main-ability-1"].ToEnum<MainAbility>();
			var MainAbility2 = Attributes["main-ability-2"].ToEnum<MainAbility>();

			foreach (var ability in data)
			{
				if (ability.Value == 0) continue;

				var text = ability.Key.GetName(ability.Value);
				if (ability.Key == MainAbility1 || ability.Key == MainAbility2) Substitute1.AppendLine(text);
				else Substitute2.AppendLine(text);
			}
			#endregion

			#region Equip
			for (int i = 1; i <= 4; i++)
			{
				var EffectEquip = Attributes.Get<Record>("effect-equip-" + i);
				if (EffectEquip is null) continue;

				var Name3 = EffectEquip.Attributes.Get<Record>("name3").GetText();
				var Description3 = EffectEquip.Attributes.Get<Record>("description3").GetText();

				if (Name3 != null) Substitute1.AppendLine(Name3);
				if (Description3 != null) Substitute2.AppendLine(Description3);
			}
			#endregion

			return new(
				Substitute1.ToString().TrimEnd('\n'),
				Substitute2.ToString().TrimEnd('\n'));
		}
	}

	public string AcquireRoute
	{
		get
		{
			// the original method is a little stupid
			// I want to retrieve the desc6 text

			// Item.DescTitle.0001
			return this.Attributes["description6"]?.GetText();
		}
	}
	#endregion
}

public class ItemDecomposeInfo
{
	#region Fields
	public bool DecomposeRewardByConsumeIndex;
	public int DecomposeMax = 1;
	public int DecomposeMoneyCost;

	public Reward[] DecomposeReward;
	public Reward DecomposeEventReward;
	public Dictionary<JobSeq, Reward> DecomposeJobRewards = [];

	public Tuple<Item, short>[] Decompose_By_Item2;
	public Tuple<Item, short>[] Job_Decompose_By_Item2;

	#endregion

	#region Constructor
	internal ItemDecomposeInfo(Item data)
	{
		var attributes = data.Attributes;

		DecomposeMax = attributes.Get<sbyte>("decompose-max");
		DecomposeMoneyCost = attributes.Get<int>("decompose-money-cost");
		DecomposeRewardByConsumeIndex = attributes.Get<BnsBoolean>("decompose-reward-by-consume-index");

		LinqExtensions.For(ref DecomposeReward, 7, (id) => attributes.Get<Record>("decompose-reward-" + id)?.As<Reward>());
		Job.GetPcJob().ForEach(job => DecomposeJobRewards[job] = attributes.Get<Record>("decompose-job-reward-" + job.GetDescription())?.As<Reward>());

		LinqExtensions.For(ref Decompose_By_Item2, 7, (id) => new(attributes.Get<Record>("decompose-by-item2-" + id)?.As<Item>(), attributes.Get<short>("decompose-by-item2-stack-count-" + id)));
		LinqExtensions.For(ref Job_Decompose_By_Item2, 7, (id) => new(attributes.Get<Record>("job-decompose-by-item2-" + id)?.As<Item>(), attributes.Get<short>("job-decompose-by-item2-stack-count-" + id)));
	}
	#endregion


	#region Methods
	//public Bitmap GetExtra()
	//{
	//	var result = GetExtra(this.Decompose_By_Item2[0].Item);
	//	result ??= GetExtra(this.Job_Decompose_By_Item2[0].Item);
	//	result ??= this.DecomposeMoneyCost == 0 ? null : Resource_BNSR.Weapon_Lock_04;

	//	return result;
	//}

	//private static Bitmap GetExtra(Item item2)
	//{
	//	if (item2.INVALID())
	//		return null;

	//	var Item2Info = FileCache.Data.Item[item2];
	//	if (Item2Info != null && Item2Info is Grocery grocery && grocery.GroceryType == GroceryTypeSeq.Key) return Resource_BNSR.unuseable_KeyLock;
	//	else return Resource_BNSR.Weapon_Lock_04;
	//}
	#endregion
}