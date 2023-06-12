using System.ComponentModel;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record.QuestData
{
	public class Destination : BaseRecord
	{
		[Signal("mission-step-id")]
		public byte MissionStepID;

		[DefaultValue(null)]
		[Signal("zone-index")]
		public byte ZoneIndex;


		[Side(ReleaseSide.Client)]
		public string Kismet;
	}
}