using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData
{
	[Signal("mission-step-fail")]
	public sealed class MissionStepFail : CaseParent
	{
		[Signal("rollback-step-id")]
		public byte RollbackStepID;

		/// <summary>
		/// 直接放弃任务
		/// </summary>
		[Signal("dispose-quest")]
		public bool DisposeQuest;

		public byte Step;


		[Side(ReleaseSide.Client)]
		[Signal("fail-talksocial")]
		public TalkSocial FailTalksocial;

		[Side(ReleaseSide.Client)]
		[Signal("fail-talksocial-delay")]
		public float FailTalksocialDelay;





		[Side(ReleaseSide.Server)]
		[Signal("quest-decision")]
		public string QuestDecision;

		[Side(ReleaseSide.Server)] 
		[Signal("zone-1")] 
		public string Zone1;

		[Side(ReleaseSide.Server)] 
		[Signal("zone-2")] 
		public string Zone2;
	}
}