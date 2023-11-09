using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class SkillByEquipment : Record
{
	public string Alias;



	[Repeat(4), Name("skill3-id")]
	public int[] Skill3Id;

	[Repeat(4), Name("context-script")]
	public Ref<ContextScript>[] ContextScript;

	[Repeat(4), Name("tooltip-text")]
	public Ref<Text>[] TooltipText;
}