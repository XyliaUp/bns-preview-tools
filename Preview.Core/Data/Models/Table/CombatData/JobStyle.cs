using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class JobStyle : ModelElement
{
	public JobSeq Job;

	[Name("job-style")]
	public JobStyleSeq jobStyle;


	public Ref<Text> IntroduceJobStyleName;
}