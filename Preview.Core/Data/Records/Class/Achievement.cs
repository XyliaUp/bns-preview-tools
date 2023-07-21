using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
public sealed class Achievement : BaseRecord
{
	public short Id;
	public short Step;
	public JobSeq Job;




	public bool Deprecated;

	public string Picture;

	public Text Name;

	public Text Description2;


	[Signal("sort-no")]
	public short SortNo;

	[Signal("talk-social")]
	public TalkSocial TalkSocial;
}