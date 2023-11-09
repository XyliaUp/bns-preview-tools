using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class JobStyle : Record
{
	public JobSeq Job;

	[Name("job-style")]
	public JobStyleSeq jobStyle;




	public string Alias;

	public string IntroduceJobStyleIcon;

	public Ref<Text> IntroduceJobStyleName;

	public Ref<Text> IntroduceJobStylePlayDesc;

}