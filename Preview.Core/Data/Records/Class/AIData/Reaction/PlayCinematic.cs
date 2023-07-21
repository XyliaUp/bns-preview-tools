using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("play-cinematic")]
public sealed class PlayCinematic : Reaction
{
	public Cinematic Cinematic;

	public Sight sight;

	public Script_obj To;


	public enum Sight
	{
		None,

		One,

		Party,

		Team,

		Zone,
	}
}

