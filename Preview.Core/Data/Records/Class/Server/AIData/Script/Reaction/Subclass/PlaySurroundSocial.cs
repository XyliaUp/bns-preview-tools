using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("play-surround-social")]
	public sealed class PlaySurroundSocial : IReaction
	{
		public Script_obj From;

		public Script_obj To;

		public Social Social;

		[Signal("play-social-delay")]
		public int PlaySocialDelay;
	}
}