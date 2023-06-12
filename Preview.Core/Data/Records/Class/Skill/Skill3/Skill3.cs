using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed partial class Skill : BaseRecord
	{
		#region Fields
		[Signal("variation-id")]
		public byte VariationId = 1;


		[Signal("revised-effect-equip-probability-in-exec-1")] public short RevisedEffectEquipProbabilityInExec1;
		[Signal("revised-effect-equip-probability-in-exec-2")] public short RevisedEffectEquipProbabilityInExec2;
		[Signal("revised-effect-equip-probability-in-exec-3")] public short RevisedEffectEquipProbabilityInExec3;
		[Signal("revised-effect-equip-probability-in-exec-4")] public short RevisedEffectEquipProbabilityInExec4;
		[Signal("revised-effect-equip-probability-in-exec-5")] public short RevisedEffectEquipProbabilityInExec5;


		[Signal("damage-rate-pvp")]
		public short DamageRatePvp = 1000;

		[Signal("damage-rate-standard-stats")]
		public short DamageRateStandardStats = 1000;


		public string Name;

		public Text Name2;


		[Signal("short-cut-key")]
		public KeyCommandSeq ShortCutKey;

		[Signal("short-cut-key-classic")]
		public KeyCommandSeq ShortCutKeyClassic;

		[Signal("short-cut-key-simple-context")]
		public KeyCommandSeq ShortCutKeySimpleContext;



		[Signal("icon-texture")]
		public string IconTexture;

		[Signal("icon-index")]
		public short IconIndex;
		#endregion

		#region Properties
		/// <summary>
		/// 当前快捷键
		/// </summary>
		public KeyCommand CurrentShortCutKey => KeyCommand.Cast(this.ShortCutKey);
		#endregion
	}
}