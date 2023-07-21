using System.ComponentModel;
using System.Xml;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.CombatSequenceData;
public abstract class Sequence : BaseRecord
{
	#region Fields
	public List<Action> Actions = new();


	public short ID;

	[Signal("attack-limit")]
	public byte AttackLimit;

	[Signal("range-attack-limit")]
	public byte RangeAttackLimit;

	[Signal("combat-distance")]
	public byte CombatDistance;

	[DefaultValue(1)]
	[Signal("max-count")]
	public short MaxCount;

	[Signal("script-debug-enable")]
	public bool ScriptDebugEnable;

	/// <summary>
	/// 普通序列过程中调用特殊序列
	/// </summary>
	[Signal("special"), Repeat(5)]
	public byte[] Special;
	#endregion


	#region Functions
	public override void LoadData(XmlElement xe)
	{
		base.LoadData(xe);

		////Special1、Special2存在默认值, 当定义 Special1 后 Special2 会默认为空
		//if (this.Special[0] != 0) this.Special[1] = 0;

		//this.Actions = new List<Action>();
		//var Actions = xe.SelectNodes("./action");
		//for (int i = 0; i < Actions.Count; i++)
		//{
		//	var ActionNode = (XmlElement)Actions[i];
		//	this.Actions.Add(ActionNode.ActionFactory());
		//}
	}
	#endregion


	#region Sub
	public sealed class NormalSequence : Sequence
	{
		[Signal("change-min-msec")]
		public byte ChangeMinMsec;

		[Signal("except-summoned")]
		public bool ExceptSummoned;

		[Signal("hide-seq-start")]
		public bool HideSeqStart;

		[Signal("seq-start-social")]
		public byte SeqStartSocial;

		[Signal("setup-position")]
		public bool SetupPosition;

		[Signal("setup-dir")]
		public Direction SetupDir;
	}

	public sealed class SpecialSequence : Sequence
	{
		[Signal("condition"), Repeat(2)]
		public Flag Condition;

		/// <summary>
		/// 条件对象 默认是 event:npc-combat-action:target
		/// </summary>
		[Signal("condition-subscription"), Repeat(2)]
		public Script_obj ConditionSubscription;

		[Signal("start-delay")]
		public int StartDelay;

		[Signal("min-delay")]
		public int MinDelay;
	}
	#endregion
}