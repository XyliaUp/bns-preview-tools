using Xylia.Extension;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class Job : Record
{
	public JobSeq job;


	#region Functions
	public KeyCommand CurrentActionKey => KeyCommand.Cast(KeyCommandSeq.Action3);

	public static string GetStyleName(JobSeq Job, JobStyleSeq JobStyle)
	{
		if (Job == JobSeq.JobNone) return null;

		var o = FileCache.Data.JobStyle.FirstOrDefault(o => o.Job == Job && o.jobStyle == JobStyle);
		if (o != null) return o.IntroduceJobStyleName.GetText();

		return null;
	}

	public static List<JobSeq> GetPcJob() => Enum.GetValues<JobSeq>().Where(o => o > JobSeq.JobNone && o < JobSeq.PcMax).ToList();

	public static string GetName(JobSeq seq) => seq.GetDescription() ?? seq.ToString();

	public static List<string> GetPcJobName() => GetPcJob().Select(f => GetName(f)).ToList();

	public static JobSeq GetJob(string Name) => GetPcJob().FirstOrDefault(f => Name == GetName(f));
	#endregion
}