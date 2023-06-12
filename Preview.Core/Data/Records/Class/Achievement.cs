using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

public sealed class Achievement : BaseRecord
{
	public short Step;

	public JobSeq Job;


	public bool Deprecated;



	[Signal("map-group-1")]
	public string MapGroup1;

	public string Picture;

	public Text Name;

	public Text Description2;


	[Signal("sort-no")]
	public short SortNo;

	[Signal("completed-game-message")]
	public string CompletedGameMessage;

	[Signal("talk-social")]
	public string TalkSocial;
}