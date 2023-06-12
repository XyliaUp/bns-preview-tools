using System;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class TalkToSelf : CaseBase
	{
		/// <summary>
		/// 免费接收 (功能已废弃)
		/// </summary>
		[Obsolete]
		public bool Fee;

		[Side(ReleaseSide.Client)]
		public VirtualItem Item;

		[Side(ReleaseSide.Client)]
		public NpcTalkMessage Msg;



		[Signal("check-inventory-full")]
		public bool CheckInventoryFull;

		[Signal("check-equiped-durability-below")]
		public byte CheckEquipedDurabilityBelow;

		[Signal("check-exp-boost-normal-below")]
		public byte CheckExpBoostNormalBelow;

		[Signal("required-jumping-character-state")]
		public JumpingCharacterState RequiredJumpingCharacterState;
	}
}