using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("dispel-by-type")]
	public sealed class DispelByType : IReaction
	{
		public Script_obj Target;

		public Script_obj From;


		[Signal("dispel-force")]
		public bool DispelForce;

		[Signal("effect-type")]
		public string EffectType;
	}
}