using System;
using System.Collections.Generic;
using System.Linq;

using Xylia.Extension;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

public sealed class Job : BaseRecord
{
	public JobSeq job;


	public Text Name2;

	public string Icon;

	public Text Desc;



	#region Functions
	public KeyCommand CurrentActionKey => KeyCommand.Cast(KeyCommandSeq.Action3);

	/// <summary>
	/// 获得派系名称
	/// </summary>
	/// <param name="Job"></param>
	/// <param name="JobStyle"></param>
	/// <returns></returns>
	public static string GetStyleName(JobSeq Job, JobStyleSeq JobStyle)
	{
		if (Job == JobSeq.JobNone) return null;

		var o = FileCache.Data.JobStyle.FirstOrDefault(o => o.Job == Job && o.jobStyle == JobStyle);
		if (o != null) return o.IntroduceJobStyleName.GetText();

		return null;
	}


	/// <summary>
	/// 获得玩家职业
	/// </summary>
	/// <returns></returns>
	public static List<JobSeq> GetPcJob() => Enum.GetValues<JobSeq>().Where(o => o > JobSeq.JobNone && o < JobSeq.PcMax).ToList();

	public static string GetName(JobSeq seq) => seq.GetDescription() ?? seq.ToString();

	public static List<string> GetPcJobName() => GetPcJob().Select(f => GetName(f)).ToList();

	public static JobSeq GetJob(string Name) => GetPcJob().FirstOrDefault(f => Name == GetName(f));
	#endregion
}