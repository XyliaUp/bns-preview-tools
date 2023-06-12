using System.Collections.Generic;
using System.Linq;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class SkillModifyInfoGroup : BaseRecord
	{
		[Signal("job-style")]
		public JobStyleSeq JobStyle;

		[Signal("skill-modify-info-1")]
		public SkillModifyInfo SkillModifyInfo1;

		[Signal("skill-modify-info-2")]
		public SkillModifyInfo SkillModifyInfo2;

		[Signal("skill-modify-info-3")]
		public SkillModifyInfo SkillModifyInfo3;

		[Signal("skill-modify-info-4")]
		public SkillModifyInfo SkillModifyInfo4;



		#region Functions
		public JobSeq SpecificJob { get; set; }

		public override string ToString()
		{
			var SkillModifyInfos = new List<string>
			{
				FileCache.Data.SkillModifyInfo[this.SkillModifyInfo1]?.ToString(),
				FileCache.Data.SkillModifyInfo[this.SkillModifyInfo2]?.ToString(),
				FileCache.Data.SkillModifyInfo[this.SkillModifyInfo3]?.ToString(),
				FileCache.Data.SkillModifyInfo[this.SkillModifyInfo4]?.ToString()

			}.Where(a => !string.IsNullOrWhiteSpace(a));


			if (!SkillModifyInfos.Any()) return null;
			return Job.GetStyleName(this.SpecificJob, this.JobStyle) + SkillModifyInfos.Aggregate((sum, now) => sum + "<br/>" + now);
		}
		#endregion
	}
}