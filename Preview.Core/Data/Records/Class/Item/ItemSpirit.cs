using System.Collections.Generic;
using System.Linq;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class ItemSpirit : BaseRecord
	{
		#region Fields
		[Signal("main-ingredient")]
		public Item MainIngredient;

		[Signal("applicable-part-1")]
		public EquipType ApplicablePart1;

		[Signal("applicable-part-2")]
		public EquipType ApplicablePart2;

		[Signal("applicable-part-3")]
		public EquipType ApplicablePart3;

		[Signal("applicable-part-4")]
		public EquipType ApplicablePart4;

		[Signal("use-random-ability-value")]
		public bool UseRandomAbilityValue;

		[Signal("money-cost")]
		public int MoneyCost;

		[Signal("attach-ability-1")]
		public MainAbility AttachAbility1;

		[Signal("attach-ability-2")]
		public MainAbility AttachAbility2;

		[Signal("ability-min-1")]
		public int AbilityMin1;

		[Signal("ability-min-2")]
		public int AbilityMin2;

		[Signal("ability-max-1")]
		public int AbilityMax1;

		[Signal("ability-max-2")]
		public int AbilityMax2;

		[Signal("once-attach-ability-min-1")]
		public int OnceAttachAbilityMin1;

		[Signal("once-attach-ability-min-2")]
		public int OnceAttachAbilityMin2;

		[Signal("once-attach-ability-max-1")]
		public int OnceAttachAbilityMax1;

		[Signal("once-attach-ability-max-2")]
		public int OnceAttachAbilityMax2;

		public WarningSeq Warning;
		public enum WarningSeq
		{
			None,

			Fail,
		}
		#endregion


		#region Functions
		public static ItemSpirit Query(Item ItemData) => FileCache.Data.ItemSpirit.FirstOrDefault(o => FileCache.Data.Item[o.MainIngredient] == ItemData);

		public static IEnumerable<ItemSpirit> Query(EquipType EquipType) => EquipType == EquipType.None ? null :
			FileCache.Data.ItemSpirit.Where(o => 
			EquipType == o.ApplicablePart1 || 
			EquipType == o.ApplicablePart2 || 
			EquipType == o.ApplicablePart3 || 
			EquipType == o.ApplicablePart4);
		#endregion
	}
}