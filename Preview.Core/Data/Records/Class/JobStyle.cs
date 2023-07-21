using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class JobStyle : BaseRecord
{
	public JobSeq Job;

	[Signal("job-style")]
	public JobStyleSeq jobStyle;


	[Signal("introduce-job-style-icon")]
	public string IntroduceJobStyleIcon;

	[Signal("introduce-job-style-name")]
	public Text IntroduceJobStyleName;

	[Signal("introduce-job-style-play-desc")]
	public Text IntroduceJobStylePlayDesc;

}