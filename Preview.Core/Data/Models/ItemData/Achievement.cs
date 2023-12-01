using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public sealed class Achievement : Record
{
	public short Id;
	public short Step;
	public JobSeq Job;

	public string Alias;




	public bool Deprecated;



	public Ref<Text> Name2;
	public Ref<Text> Description2;

	public short SortNo;

	public Ref<TalkSocial> TalkSocial;
}