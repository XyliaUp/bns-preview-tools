
using System.Collections.Generic;
using System.ComponentModel;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Record.QuestData.Enums;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record.QuestData
{
	[Signal("tutorial-case")]
	public abstract class TutorialCaseBase : TypeBaseRecord<TutorialCaseType>
	{
		[DefaultValue(100)]
		public byte Prob = 100;

		[Side(ReleaseSide.Client)][Signal("mapunit-1")] public string MapUnit1;
		[Side(ReleaseSide.Client)][Signal("mapunit-2")] public string MapUnit2;
		[Side(ReleaseSide.Client)][Signal("mapunit-3")] public string MapUnit3;
		[Side(ReleaseSide.Client)][Signal("mapunit-4")] public string MapUnit4;
		[Side(ReleaseSide.Client)][Signal("mapunit-5")] public string MapUnit5;
		[Side(ReleaseSide.Client)][Signal("mapunit-6")] public string MapUnit6;
		[Side(ReleaseSide.Client)][Signal("mapunit-7")] public string MapUnit7;
		[Side(ReleaseSide.Client)][Signal("mapunit-8")] public string MapUnit8;
		[Side(ReleaseSide.Client)][Signal("mapunit-9")] public string MapUnit9;
		[Side(ReleaseSide.Client)][Signal("mapunit-10")] public string MapUnit10;

		[DefaultValue(-1)]
		[Signal("range-max")]
		public short RangeMax = -1;

		[DefaultValue(-1)]
		[Signal("range-min")]
		public short RangeMin = -1;

		[Signal("progress-mission")]
		public ProgressMission ProgressMission = ProgressMission.N;

		[Signal("valid-zone-1")]
		public Zone ValidZone1;

		[Signal("valid-zone-2")]
		public Zone ValidZone2;


		[Side(ReleaseSide.Server)]
		public string Zone;

		[Side(ReleaseSide.Server)]
		[Signal("quest-decision")]
		public string QuestDecision;



		public virtual List<string> AttractionObject { get; }
	}
}