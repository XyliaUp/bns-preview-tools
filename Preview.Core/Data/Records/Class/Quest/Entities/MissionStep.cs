using System;
using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record.QuestData.Enums;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record.QuestData
{
	[Signal("mission-step")]
	public class MissionStep : BaseRecord
	{
		[Signal("mission")]
		public List<Mission> Mission;

		[Signal("mission-step-success")]
		public List<MissionStepSuccess> MissionStepSuccess;

		[Signal("mission-step-fail")]
		public List<MissionStepFail> MissionStepFail;




		/// <summary>
		/// 步骤编号
		/// </summary>
		public byte id;

		[Signal("completion-type")]
		public OpCheck CompletionType;

		[Signal("giveup-warp-to-pcspawn")]
		public string GiveupWarpToPcSpawn;

		[Signal("giveup-zone-1")] public string GiveupZone1;
		[Signal("giveup-zone-2")] public string GiveupZone2;
		[Signal("giveup-zone-3")] public string GiveupZone3;

		[Signal("progress-talksocial")]
		public string ProgressTalkSocial;

		[Signal("progress-talksocial-delay")]
		public float ProgressTalkSocialDelay;

		/// <summary>
		/// 指示是否废弃步骤
		/// </summary>
		public bool Retired;

		[Signal("skip-dest-mission-step")]
		public bool SkipDestMissionStep;

		[Signal("time-limit-type")]
		public TimeLimitType TimeLimitType;

		[Signal("time-limit")]
		public short TimeLimit;

		public bool Hide;

		[Signal("mission-map-type")]
		public MissionMapType MissionMapType;



		[Side(ReleaseSide.Client)]
		public string Desc;

		[Side(ReleaseSide.Client)]
		[Signal("guide-message-category")]
		public GuideMessageCategory GuideMessageCategory;

		[Side(ReleaseSide.Client)]
		[Signal("guide-message")]
		public string GuideMessage;

		[Side(ReleaseSide.Client)]
		[Signal("guide-message-zone-1")]
		public string GuideMessageZone1;

		[Side(ReleaseSide.Client)]
		[Signal("guide-message-zone-2")]
		public string GuideMessageZone2;

		[Side(ReleaseSide.Client)]
		[Signal("location-x")]
		public float LocationX;

		[Side(ReleaseSide.Client)]
		[Signal("location-y")]
		public float LocationY;

		[Side(ReleaseSide.Client)]
		public string Map;

		[Obsolete]
		[Side(ReleaseSide.Client)]
		[Signal("map-zoom-rate")]
		public string MapZoomRate;


		[Side(ReleaseSide.Client)]
		[Signal("enable-navigation")]
		public bool EnableNavigation;

		/// <summary>
		/// 使用自动导航
		/// </summary>
		[Side(ReleaseSide.Client)]
		[Signal("use-auto-navigation")]
		public bool UseAutoNavigation;




		[Side(ReleaseSide.Server)]
		[Signal("quest-decision")] 
		public string QuestDecision;
		
		[Side(ReleaseSide.Server)] 
		[Signal("zone-1")] 
		public string Zone1;

		[Side(ReleaseSide.Server)] 
		[Signal("zone-2")] 
		public string Zone2;

		[Side(ReleaseSide.Server)] 
		[Signal("skip-quest-decision")] 
		public string SkipQuestDecision;

		[Side(ReleaseSide.Server)] 
		[Signal("skip-quest-decision-zone")] 
		public string SkipQuestDecisionZone;

		[Side(ReleaseSide.Server)]
		public RollBack RollBack;
	}
}