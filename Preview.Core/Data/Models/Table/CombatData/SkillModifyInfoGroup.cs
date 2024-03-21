﻿using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public class SkillModifyInfoGroup : ModelElement
{
	[Name("job-style")]
	public JobStyleSeq JobStyle { get; set; }

	[Name("skill-modify-info")]
	public Ref<SkillModifyInfo>[] SkillModifyInfo { get; set; }



	#region Methods
	public override string ToString()
	{
		var objs = SkillModifyInfo.Select(o => o.Instance?.ToString()).Where(o => o is not null);
		if (!objs.Any()) return null;
		return $"<font name=\"00008130.UI.Label_Green03_12\">{string.Join("<br/>", objs)}</font>";
	}
	#endregion
}