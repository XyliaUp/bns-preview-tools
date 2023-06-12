using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("play-cinematic")]
	public sealed class PlayCinematic : IReaction
	{
		/// <summary>
		/// 引用 Cinematic 对象
		/// </summary>
		public Cinematic Cinematic;

		public Sight Sight;

		public Script_obj To;
	}

	public enum Sight
	{
		None,

		One,

		Party,

		Team,	

		Zone,
	}
}