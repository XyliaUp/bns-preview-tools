using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class Job : ModelElement
{
	public JobSeq job => Attributes["job"].ToEnum<JobSeq>();

	public KeyCommand CurrentActionKey => KeyCommand.Cast(KeyCommandSeq.Action3);


	#region Methods
	public static string GetStyleName(JobSeq Job, JobStyleSeq JobStyle)
	{
		if (Job == JobSeq.JobNone) return null;

		var o = FileCache.Data.Get<JobStyle>().FirstOrDefault(o => o.Job == Job && o.jobStyle == JobStyle);
		if (o != null) return o.IntroduceJobStyleName;

		return null;
	}

	public static IEnumerable<JobSeq> GetPcJob() => Enum.GetValues<JobSeq>().Where(o => o > JobSeq.JobNone && o < JobSeq.PcMax);
	#endregion
}