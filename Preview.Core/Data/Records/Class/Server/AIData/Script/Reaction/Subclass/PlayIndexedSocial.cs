using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("play-indexed-social")]
	public sealed class PlayIndexedSocial : IReaction
	{
		public Script_obj From;

		/// <summary>
		/// 可缺省
		/// </summary>
		public Script_obj To;


		public byte Social;

		[Signal("play-social-delay")]
		public int PlaySocialDelay;
	}
}