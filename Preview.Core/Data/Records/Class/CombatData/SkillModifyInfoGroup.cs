using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public class SkillModifyInfoGroup : BaseRecord
{
	[Signal("job-style")]
	public JobStyleSeq JobStyle;

	[Signal("skill-modify-info") , Repeat(4)]
	public SkillModifyInfo[] SkillModifyInfo;



	#region Functions
	public override string ToString()
	{
		var objs = SkillModifyInfo.Select(o => o?.ToString()).Where(o => o is not null);
		if (!objs.Any()) return null;
		return $"<font name=\"00008130.UI.Label_Green03_12\">{ objs.Aggregate("<br/>") }</font>";
	}
	#endregion
}