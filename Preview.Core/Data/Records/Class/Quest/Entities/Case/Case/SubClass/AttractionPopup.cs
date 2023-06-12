using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class AttractionPopup : CaseBase
	{
		[Signal("attraction-popup-env")]
		public ZoneEnv2 AttractionPopupEnv;
	}
}