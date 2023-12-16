using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class ContextScript : ModelElement
{
	#region Fields
	public string Alias { get; set; }

	public JobSeq Job { get; set; }

	[Repeat(2)]
	public JobStyleSeq[] JobStyle { get; set; }

	public RaceSeq Race { get; set; }

	public bool ContextSimpleMode { get; set; }


	public List<STANCE> Stance  { get; set; }
	#endregion

	#region	Element
	public sealed class STANCE : ModelElement
	{
		public List<Layer> Layer  { get; set; }

		public StanceSeq Stance { get; set; }
		public LinkType Link { get; set; }
		public Flag AbnormalFlag { get; set; }
		public bool UseBranchGroup { get; set; }
	}

	public sealed class Layer : ModelElement
	{
		public List<Decision> Decision  { get; set; }
	}

	public sealed class Decision : ModelElement
	{
		public List<Condition> Condition  { get; set; }

		public List<Result> Result  { get; set; }
	}

	public sealed class Condition : ModelElement
	{
		public FieldSeq Field { get; set; }
		public enum FieldSeq
		{
			None,
			All,
			State,
			Combo,
			Event,
			JobStyleOnly,
		}


		public JobStyleSeq JobStyle { get; set; }

		public int Skill { get; set; }

		public int VariationId { get; set; }

		[Repeat(2)]
		public KeyCommandSeq[] CombinationKeyCommand { get; set; }

		public bool SkipConditionTargetCheck { get; set; }

		public bool SkipConditionMoveCheck { get; set; }

		public bool SkipConditionLinkCheck { get; set; }

		public EffectAttributeSeq ImmuneBreakerAttribute { get; set; }
	}

	public sealed class Result : ModelElement
	{
		public ControlModeSeq ControlMode { get; set; }
		public enum ControlModeSeq
		{
			Classic,
			Bns,
		}

		public KeyStatusSeq KeyStatus { get; set; }
		public enum KeyStatusSeq
		{
			Press,
			Unpress,
		}

		[Repeat(3)] public int[] Context { get; set; }
		[Repeat(3)] public int[] BnsContext { get; set; }
		[Repeat(2)] public int[] Special { get; set; }
		public int Stance { get; set; }
		[Repeat(12)] public int[] Skillbar { get; set; }
		[Repeat(3)] public int[] Branch1Skillbar { get; set; }
		[Repeat(3)] public int[] Branch2Skillbar { get; set; }
		[Repeat(3)] public int[] Branch3Skillbar { get; set; }
		[Repeat(3)] public int[] Branch4Skillbar { get; set; }
		public int BranchEscapeComboDuration { get; set; }
		public int CmdKeyUp { get; set; }
		public int CmdKeyDown { get; set; }
		public int CmdKeyLeft { get; set; }
		public int CmdKeyRight { get; set; }
		public int CmdKeyDoubleLeft { get; set; }
		public int CmdKeyDoubleRight { get; set; }
		[Repeat(5)] public int[] ExtraSkillbar { get; set; }
		[Repeat(3)] public int[] ContextFallback { get; set; }
		[Repeat(3)] public int[] BnsContextFallback { get; set; }
		public int StanceFallback { get; set; }
		[Repeat(12)] public int[] SkillbarFallback { get; set; }
		public int CmdKeyUpFallback { get; set; }
		public int CmdKeyDownFallback { get; set; }
		public int CmdKeyLeftFallback { get; set; }
		public int CmdKeyRightFallback { get; set; }
		public int CmdKeyDoubleLeftFallback { get; set; }
		public int CmdKeyDoubleRightFallback { get; set; }
		public UiEffect StanceUiEffect { get; set; }
		public UiEffect ContextUiEffect { get; set; }
		public UiEffect SkillbarUiEffect { get; set; }
		public UiEffect SpecialUiEffect { get; set; }

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