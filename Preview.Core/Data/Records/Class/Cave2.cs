using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class Cave2 : BaseRecord, IAttraction
{
	[Signal("ui-text-grade")]
	public sbyte UiTextGrade;

	[Signal("cave2-name2")]
	public Text Cave2Name2;

	[Signal("cave2-desc")]
	public Text Cave2Desc;

	[Signal("arena-entrance-zone")]
	public string ArenaEntranceZone;

	[Signal("arena-minimap")]
	public string ArenaMinimap;

	[Signal("arena-disable-zone-phase")]
	public bool ArenaDisableZonePhase;

	[Signal("required-level")]
	public sbyte RequiredLevel;

	[Signal("required-mastery-level")]
	public sbyte RequiredMasteryLevel;

	[Signal("quest-for-ignoring-required-level")]
	public string QuestForIgnoringRequiredLevel;


	#region Interface
	public string GetName() => this.Cave2Name2.GetText();

	public string GetDescribe() => this.Cave2Desc.GetText();
	#endregion
}