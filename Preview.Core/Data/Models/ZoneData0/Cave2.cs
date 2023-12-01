using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;

namespace Xylia.Preview.Data.Models;
public sealed class Cave2 : Record, IAttraction
{
	public string Alias;



	[Name("ui-text-grade")]
	public sbyte UiTextGrade;

	[Name("cave2-name2")]
	public Ref<Text> Cave2Name2;

	[Name("cave2-desc")]
	public Ref<Text> Cave2Desc;

	[Name("arena-entrance-zone")]
	public string ArenaEntranceZone;

	[Name("arena-minimap")]
	public string ArenaMinimap;

	[Name("arena-disable-zone-phase")]
	public bool ArenaDisableZonePhase;

	[Name("required-level")]
	public sbyte RequiredLevel;

	[Name("required-mastery-level")]
	public sbyte RequiredMasteryLevel;

	[Name("quest-for-ignoring-required-level")]
	public string QuestForIgnoringRequiredLevel;


	#region Interface
	public override string GetText => this.Cave2Name2.GetText();

	public string GetDescribe() => this.Cave2Desc.GetText();
	#endregion
}