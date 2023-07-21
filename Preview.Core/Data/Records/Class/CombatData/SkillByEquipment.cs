using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Models.BinData.Table.Record;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class SkillByEquipment : BaseRecord
{
	[Signal("skill3-id-1")]
	public int Skill3Id1;

	[Signal("skill3-id-2")]
	public int Skill3Id2;

	[Signal("skill3-id-3")]
	public int Skill3Id3;

	[Signal("skill3-id-4")]
	public int Skill3Id4;


	[Signal("context-script-1")]
	public ContextScript ContextScript1;

	[Signal("context-script-2")]
	public ContextScript ContextScript2;

	[Signal("context-script-3")]
	public ContextScript ContextScript3;

	[Signal("context-script-4")]
	public ContextScript ContextScript4;

	[Signal("tooltip-text-1")]
	public Text TooltipText1;

	[Signal("tooltip-text-2")]
	public Text TooltipText2;

	[Signal("tooltip-text-3")]
	public Text TooltipText3;

	[Signal("tooltip-text-4")]
	public Text TooltipText4;
}