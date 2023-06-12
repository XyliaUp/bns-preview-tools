using System.ComponentModel;

namespace Xylia.Preview.Data.Record.QuestData.TutorialCase
{
	public sealed class NpcBleeding : TutorialCaseBase
	{
		
	}

	public sealed class PcBleeding : TutorialCaseBase
	{
		[DefaultValue(null)]
		public byte Percent;
	}
}