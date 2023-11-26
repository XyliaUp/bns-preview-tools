using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class ContextScript : Record
{
	#region Fields
	public string Alias;

	public JobSeq Job;

	[Repeat(2)]
	public JobStyleSeq[] JobStyle;

	public RaceSeq Race;

	public bool ContextSimpleMode;


	public List<STANCE> Stance { get; set; }
	#endregion

	#region	Element
	public sealed class STANCE : Record
	{
		public List<Layer> Layer { get; set; }

		public StanceSeq Stance;
		public LinkType Link;
		public Flag AbnormalFlag;
		public bool UseBranchGroup;
	}

	public sealed class Layer : Record
	{
		public List<Decision> Decision { get; set; }
	}

	public sealed class Decision : Record
	{
		public List<Condition> Condition { get; set; }

		public List<Result> Result { get; set; }
	}

	public sealed class Condition : Record
	{
		public FieldSeq Field;
		public enum FieldSeq
		{
			None,
			All,
			State,
			Combo,
			Event,
			JobStyleOnly,
		}


		public JobStyleSeq JobStyle;

		public int Skill;

		public int VariationId;

		[Repeat(2)]
		public KeyCommandSeq[] CombinationKeyCommand;

		public bool SkipConditionTargetCheck;

		public bool SkipConditionMoveCheck;

		public bool SkipConditionLinkCheck;

		public EffectAttributeSeq ImmuneBreakerAttribute;
	}

	public sealed class Result : Record
	{
		public ControlModeSeq ControlMode;
		public enum ControlModeSeq
		{
			Classic,
			Bns,
		}

		public KeyStatusSeq KeyStatus;
		public enum KeyStatusSeq
		{
			Press,
			Unpress,
		}

		[Repeat(3)] public int[] Context;
		[Repeat(3)] public int[] BnsContext;
		[Repeat(2)] public int[] Special;
		public int Stance;
		[Repeat(12)] public int[] Skillbar;
		[Repeat(3)] public int[] Branch1Skillbar;
		[Repeat(3)] public int[] Branch2Skillbar;
		[Repeat(3)] public int[] Branch3Skillbar;
		[Repeat(3)] public int[] Branch4Skillbar;
		public int BranchEscapeComboDuration;
		public int CmdKeyUp;
		public int CmdKeyDown;
		public int CmdKeyLeft;
		public int CmdKeyRight;
		public int CmdKeyDoubleLeft;
		public int CmdKeyDoubleRight;
		[Repeat(5)] public int[] ExtraSkillbar;
		[Repeat(3)] public int[] ContextFallback;
		[Repeat(3)] public int[] BnsContextFallback;
		public int StanceFallback;
		[Repeat(12)] public int[] SkillbarFallback;
		public int CmdKeyUpFallback;
		public int CmdKeyDownFallback;
		public int CmdKeyLeftFallback;
		public int CmdKeyRightFallback;
		public int CmdKeyDoubleLeftFallback;
		public int CmdKeyDoubleRightFallback;
		public UiEffect StanceUiEffect;
		public UiEffect ContextUiEffect;
		public UiEffect SkillbarUiEffect;
		public UiEffect SpecialUiEffect;

		public enum UiEffect
		{
			None,
			KeyChange,
			Combo,
			Event,
			ImmuneBreak,
			ComboHighlight,
		}
	}
	#endregion
}