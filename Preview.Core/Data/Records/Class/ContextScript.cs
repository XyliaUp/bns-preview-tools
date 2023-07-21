using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class ContextScript : BaseRecord
{
	[Signal("context-simple-mode")]
	public bool ContextSimpleMode;

	public RaceSeq Race;

	public JobSeq Job;

	[Signal("job-style-1")]
	public JobStyleSeq JobStyle1;

	[Signal("job-style-2")]
	public JobStyleSeq JobStyle2;


	public List<Stance> stance;


	#region	ElementDef
	public sealed class Stance : BaseRecord
	{
		public List<Layer> Layer;


		[Signal("abnormal-flag")]
		public Flag AbnormalFlag;

		public StanceSeq stance;

		public LinkType Link;
	}

	public sealed class Layer : BaseRecord
	{
		public List<Decision> Decision;
	}

	public sealed class Decision : BaseRecord
	{
		public List<Condition> Condition;

		public List<Result> Result;
	}

	public sealed class Condition : BaseRecord
	{
		#region Enums
		public enum FieldSeq
		{
			[Signal("job-style-only")]
			JobStyleOnly,

			State,

			Combo,
		}
		#endregion



		public int Skill;

		[Signal("variation-id")]
		public byte VariationId;

		[Signal("job-style")]
		public JobStyleSeq JobStyle;

		[Signal("skip-condition-link-check")]
		public bool SkipConditionLinkCheck;

		[Signal("skip-condition-move-check")]
		public bool SkipConditionMoveCheck;

		[Signal("skip-condition-target-check")]
		public bool SkipConditionTargetCheck;

		public FieldSeq Field;

		[Signal("combination-key-command-1")]
		public KeyCommandSeq CombinationKeyCommand1;

		[Signal("combination-key-command-2")]
		public KeyCommandSeq CombinationKeyCommand2;
	}

	public sealed class Result : BaseRecord
	{
		#region Enums
		public enum ControlModeSeq
		{
			classic,
			bns,
		}

		public enum KeyStatusSeq
		{
			unpress,
		}


		public enum UiEffect
		{
			Event,

			Combo,

			[Signal("key-change")]
			KeyChange,

			[Signal("immune-break")]
			ImmuneBreak,
		}
		#endregion

		[Signal("control-mode")]
		public ControlModeSeq ControlMode;

		[Signal("key-status")]
		public KeyStatusSeq KeyStatus;


		[Signal("cmd-key-down")]
		public int CmdKeyDown;

		[Signal("cmd-key-left")]
		public int CmdKeyLeft;

		[Signal("cmd-key-double-left")]
		public int CmdKeyDoubleLeft;

		[Signal("cmd-key-right")]
		public int CmdKeyRight;

		[Signal("cmd-key-double-right")]
		public int CmdKeyDoubleRight;



		[Signal("context-ui-effect")]
		public UiEffect ContextUiEffect;

		[Signal("context-1")]
		public int Context1;

		[Signal("context-2")]
		public int Context2;

		[Signal("context-3")]
		public int Context3;



		[Signal("skillbar-ui-effect")]
		public UiEffect SkillbarUiEffect;

		[Signal("skillbar-1")]
		public int Skillbar1;

		[Signal("skillbar-2")]
		public int Skillbar2;

		[Signal("skillbar-3")]
		public int Skillbar3;

		[Signal("skillbar-4")]
		public int Skillbar4;

		[Signal("skillbar-5")]
		public int Skillbar5;

		[Signal("skillbar-6")]
		public int Skillbar6;

		[Signal("skillbar-7")]
		public int Skillbar7;

		[Signal("skillbar-8")]
		public int Skillbar8;


		[Signal("special-ui-effect")]
		public UiEffect SpecialUiEffect;


		[Signal("special-1")]
		public int Special1;

		[Signal("special-2")]
		public int Special2;


		[Signal("stance")]
		public int Stance;

		[Signal("stance-ui-effect")]
		public UiEffect StanceUiEffect;


		[Signal("extra-skillbar-1")]
		public int ExtraSkillbar1;

		[Signal("extra-skillbar-2")]
		public int ExtraSkillbar2;

		[Signal("extra-skillbar-3")]
		public int ExtraSkillbar3;

		[Signal("extra-skillbar-4")]
		public int ExtraSkillbar4;

		[Signal("extra-skillbar-5")]
		public int ExtraSkillbar5;
	}
	#endregion
}