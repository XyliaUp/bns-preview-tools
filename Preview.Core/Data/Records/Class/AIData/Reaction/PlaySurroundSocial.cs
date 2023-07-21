using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("play-surround-social")]
public sealed class PlaySurroundSocial : Reaction
{
	public Script_obj From;

	public Script_obj To;

	public Social Social;

	[Signal("play-social-delay")]
	public int PlaySocialDelay;
}