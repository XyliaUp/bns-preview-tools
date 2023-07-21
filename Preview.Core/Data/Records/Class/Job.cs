using Xylia.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Data.Record;
public sealed class Job : BaseRecord
{
	public JobSeq job;


	public Text Name2;

	public string Icon;

	public Text Desc;



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