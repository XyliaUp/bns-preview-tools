using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class TalkToItem : NpcTalkBase
	{
		/// <summary>
		/// 书信道具
		/// </summary>
		public Item Item;


		/// <summary>
		/// 检查包裹是否超出
		/// </summary>
		[Signal("check-inventory-full")]
		public bool CheckInventoryFull = false;

		/// <summary>
		/// 检查装备武器耐久度是否低于设定值
		/// </summary>
		[Signal("check-equiped-durability-below")]
		public byte CheckEquipedDurabilityBelow;

		[Signal("check-exp-boost-normal-below")]
		public byte CheckExpBoostNormalBelow;


		public override List<string> AttractionObject => new() { "item:" + Item };
	}
}