using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("play-social")]
public sealed class PlaySocial : Reaction
{
	public Script_obj From;

	/// <summary>
	/// 可缺省
	/// </summary>
	public Script_obj To;

	/// <summary>
	/// 引用 Social 对象
	/// </summary>
	public string Social;

	[Signal("play-social-delay")]
	public int PlaySocialDelay;
}