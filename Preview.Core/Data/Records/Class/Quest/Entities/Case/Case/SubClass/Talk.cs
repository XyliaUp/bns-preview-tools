using System;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	// 护送只能在 cave2 定义区域生成
	// cannot start convoy before progress mission
	// 无法在执行任务前开始护卫
	// acquisition 的子对象护卫Fields不生效
	public sealed class Talk : NpcTalkBase
	{
		[Obsolete]
		[Side(ReleaseSide.Server)]
		[Signal("convoy-member-1")]
		public int ConvoyMember1;

		[Obsolete]
		[Side(ReleaseSide.Server)]
		[Signal("convoy-member-2")]
		public int ConvoyMember2;


		/// <summary>
		/// 对话完成后进入护送状态
		/// </summary>
		[Side(ReleaseSide.Server)]
		public ZoneConvoy Convoy;

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
	}
}