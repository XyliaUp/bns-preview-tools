using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Record.CombatSequenceData.Enums;

namespace Xylia.Preview.Data.Record
{
	[Signal("combat-sequence")]
	public sealed class CombatSequence : BaseRecord
	{
		public string Alias;

		[Signal("hide-seq-start")]
		public bool HideSeqStart;

		/// <summary>
		/// 开始时社交
		/// </summary>
		[Signal("seq-start-social")]
		public byte SeqStartSocial;

		/// <summary>
		/// 激活调试模式
		/// </summary>
		[Signal("script-debug-enable")]
		public bool ScriptDebugEnable;

		/// <summary>
		/// 转移条件 1
		/// </summary>
		[Signal("transit-cond-1")]
		public TransitCond TransitCond1;

		/// <summary>
		/// 转移条件 2
		/// </summary>
		[Signal("transit-cond-2")]
		public TransitCond TransitCond2;

		[Signal("transit-action-1")]
		public TransitAction TransitAction1;

		[Signal("transit-action-2")]
		public TransitAction TransitAction2;

		[Signal("transit-social-1")]
		public byte TransitSocial1;

		[Signal("transit-social-2")]
		public byte TransitSocial2;



		public List<CombatSequenceData.Normal> Normal;

		public List<CombatSequenceData.Special> Special;
	}
}