
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class JobStyle : BaseRecord
	{
		public JobSeq Job;							

		[Signal("job-style")]
		public JobStyleSeq jobStyle;


		[Signal("introduce-job-style-icon")]
		public string IntroduceJobStyleIcon;

		[Signal("introduce-job-style-name")]
		public string IntroduceJobStyleName;

		[Signal("introduce-job-style-play-desc")]
		public string IntroduceJobStylePlayDesc;

		[Signal("introduce-job-style-specialization-1")]
		public string IntroduceJobStyleSpecialization1;

		[Signal("introduce-job-style-specialization-2")]
		public string IntroduceJobStyleSpecialization2;

		[Signal("introduce-job-style-specialization-3")]
		public string IntroduceJobStyleSpecialization3;

		[Signal("introduce-job-style-specialization-4")]
		public string IntroduceJobStyleSpecialization4;

		[Signal("introduce-job-style-specialization-5")]
		public string IntroduceJobStyleSpecialization5;
	}
}