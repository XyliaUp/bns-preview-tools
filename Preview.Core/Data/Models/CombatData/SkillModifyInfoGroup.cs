using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public class SkillModifyInfoGroup : Record
{
	public string Alias;


	[Name("job-style")]
	public JobStyleSeq JobStyle;

	[Name("skill-modify-info") , Repeat(4)]
	public Ref<SkillModifyInfo>[] SkillModifyInfo;



	#region Methods
	public override string ToString()
	{
		var objs = SkillModifyInfo.Select(o => o.Instance?.ToString()).Where(o => o is not null);
		if (!objs.Any()) return null;
		return $"<font name=\"00008130.UI.Label_Green03_12\">{ objs.Aggregate("<br/>") }</font>";
	}
	#endregion
}