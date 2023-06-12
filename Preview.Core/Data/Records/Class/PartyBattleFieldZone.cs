using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class PartyBattleFieldZone : BaseRecord, Attraction
	{
		public string Group;

		[Signal("zone-name2")]
		public Text ZoneName2;

		[Signal("zone-desc")]
		public Text ZoneDesc;

		[Signal("arena-minimap")]
		public string ArenaMinimap;


		#region Interface Functions
		public string GetName() => this.ZoneName2.GetText();

		public string GetDescribe() => this.ZoneDesc.GetText();
		#endregion
	}
}