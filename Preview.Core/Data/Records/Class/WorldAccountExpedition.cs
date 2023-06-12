using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper.Output;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class WorldAccountExpedition : BaseRecord, IOut
	{
		#region Fields
		public byte Step;

		[Signal("can-not-used")]
		public bool CanNotUsed;

		public byte Category;

		
		public bool Unknown;

		[Signal("ability-1")]
		public AttachAbility Ability1;

		[Signal("ability-2")]
		public AttachAbility Ability2;

		[Signal("ability-3")]
		public AttachAbility Ability3;

		[Signal("ability-4")]
		public AttachAbility Ability4;

		[Signal("ability-5")]
		public AttachAbility Ability5;

		[Signal("ability-value-1")]
		public int AbilityValue1;

		[Signal("ability-value-2")]
		public int AbilityValue2;

		[Signal("ability-value-3")]
		public int AbilityValue3;

		[Signal("ability-value-4")]
		public int AbilityValue4;

		[Signal("ability-value-5")]
		public int AbilityValue5;


		public Text Name;

		public Text Description;

		public Text Story;

	
		[Signal("tooltip-1")]
		public Text Tooltip1;

		[Signal("tooltip-2")]
		public Text Tooltip2;

		[Signal("tooltip-3")]
		public Text Tooltip3;

		[Signal("tooltip-4")]
		public Text Tooltip4;

		[Signal("tooltip-5")]
		public Text Tooltip5;
		#endregion




		OutSetTable IOut.OutTable()
		{
			OutSetTable table = new();
			table.type = typeof(WorldAccountExpedition).Name;
			table.attribute.Add(new("id"));
			table.attribute.Add(new("step"));
			table.attribute.Add(new("alias"));
			table.attribute.Add(new("category"));
			table.attribute.Add(new("name"));
			table.attribute.Add(new("description"));
			table.attribute.Add(new("tooltip-1"));
			table.attribute.Add(new("ability")
			{
				repeat = 5,
				extra = "ability-value"
			});


			return table;
		}
	}
}