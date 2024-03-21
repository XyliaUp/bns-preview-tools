using System.Text;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Objects.UObject;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models.Creature;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public abstract partial class Item : ModelElement
{
	#region Fields
	public Ref<ItemCombat>[] ItemCombat { get; set; }

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

	public int RandomOptionGroupId { get; set; }

	public int ImproveId { get; set; }
	public sbyte ImproveLevel { get; set; }
	public string ItemName => $"""<font name="00008130.Program.Fontset_ItemGrade_{ItemGrade}">{ItemNameOnly}</font>""";
	public string ItemNameOnly => Attributes["name2"].GetText();

	public int ClosetGroupId => Attributes.Get<int>("closet-group-id");
	#endregion


	#region Methods
	public ItemDecomposeInfo DecomposeInfo => new(this);

	public ImageProperty BackIcon => IconTexture.GetBackground(ItemGrade);

	public ImageProperty FrontIcon => IconTexture.Parse(Attributes.Get<string>("icon"));

	public FPackageIndex CanSaleItemImage => new MyFPackageIndex(
		AccountUsed ? "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon/SlotItem_privateSale.SlotItem_privateSale" :
		(Auctionable ? "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon/SlotItem_marketBusiness.SlotItem_marketBusiness" : null));

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

	public bool IsExpiration
	{
		get
		{
			var time = Attributes.Get<Record>("event-info")?.Attributes.Get<Time64>("event-expiration-time");
			if (time is null) return false;

			return time.Value < DateTimeOffset.Now.ToUnixTimeSeconds();
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