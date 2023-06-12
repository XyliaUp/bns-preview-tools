using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class NpcTalkMessage : BaseRecord
	{
		public Text Name2;

		[Signal("required-faction")]
		public Faction RequiredFaction;

		[Signal("required-complete-quest")]
		public string RequiredCompleteQuest;

		[Signal("function-step")]
		public byte FunctionStep;

		[Signal("end-talk-socia")]
		public Social EndTalkSocial;

		[Signal("end-talk-sound")]
		public string EndTalkSound;


		#region Functions
		public string GetStepShow(int index) => this.Attributes["step-show-" + index];
		#endregion
	}
}