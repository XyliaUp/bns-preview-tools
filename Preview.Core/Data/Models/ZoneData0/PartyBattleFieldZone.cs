using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;

namespace Xylia.Preview.Data.Models;
public sealed class PartyBattleFieldZone : Record, IAttraction
{
	public string Alias;



	public Ref<AttractionGroup> Group;

	public Ref<Text> ZoneName2;

	public Ref<Text> ZoneDesc;

	public string ArenaMinimap;


	#region Interface Methdos
	public override string GetText => this.ZoneName2.GetText();

	public string GetDescribe() => this.ZoneDesc.GetText();
	#endregion



	public enum PartyBattleFieldZoneType
	{
		None,

		[Name("occupation-war")]
		OccupationWar,

		[Name("capture-the-flag")]
		CaptureTheFlag,

		[Name("lead-the-ball")]
		LeadTheBall,
	}
}