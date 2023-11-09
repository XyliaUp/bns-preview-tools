using CUE4Parse.BNS.Conversion;

using SkiaSharp;

using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.QuestData;
using Xylia.Preview.Data.Models.QuestData.Enums;

namespace Xylia.Preview.Data.Models;
public sealed class Quest : Record
{
	#region Fields
	public int id;
	public string Alias;



	public sbyte MaxRepeat;

	[Side(ReleaseSide.Client)]
	public string Name;

	[Side(ReleaseSide.Client)]
	public Ref<Text> Name2;

	[Repeat(2)]
	public Ref<District>[] District;

	[Repeat(2)]
	public Ref<MapGroup1>[] Map_Group_1;

	[Side(ReleaseSide.Client)]
	public string Group;

	[Side(ReleaseSide.Client)]
	public Ref<Text> Group2;

	[Side(ReleaseSide.Client)]
	public Ref<Text> Desc;

	[Side(ReleaseSide.Client)]
	public Ref<Text> CompletedDesc;

	public Category Category;

	public bool CompletedList;

	[Side(ReleaseSide.Client)]
	public /*Grade*/ sbyte Grade;

	public bool Tutorial;

	[Side(ReleaseSide.Client)]
	public bool ShowTutorialTag;

	public sbyte LastMissionStep;

	public bool EffectExist;


	public QuestDayOfWeekSeq DayOfWeek;

	public enum QuestDayOfWeekSeq
	{
		None,

		Daily,

		Weekly,

		Monthly,

		Mon,

		Tue,

		Wed,

		The,

		Fri,

		Sat,

		Sun,

		[Name("sat-sun")]
		SatSun,

		[Name("fri-sat-sun")]
		FriSatSun,
	}



	public ResetType ResetType;
	public ResetType2 ResetByAcquireTime;
	public BDayOfWeek ResetDayOfWeek;
	public sbyte ResetDayOfMonth;

	public Ref<Faction> ActivatedFaction;
	public Ref<Faction> MainFaction;

	public ProductionType Production;

	public SaveType SaveType;

	[Side(ReleaseSide.Client)]
	public bool InvokeFxMsg;

	public Ref<Dungeon> Dungeon;

	public DungeonTypeSeq DungeonType;

	public enum DungeonTypeSeq
	{
		unbind,

		bind,
	}

	[Deprecated]
	public CraftType CraftType;

	public ContentType ContentType;

	public bool Retired;

	[Repeat(3)]
	public bool[] ProgressDifficultyType;

	public bool ProgressDifficultyTypeAlways = true;

	[Repeat(4)]
	public Ref<Record>[] Attraction;

	public Ref<Record> AttractionInfo;

	[Obsolete]
	public bool ResetEnable;

	[Obsolete]
	public bool ResetMoney;

	[Repeat(4), Obsolete]
	public Ref<Item>[] ResetItem;

	[Repeat(4), Obsolete]
	public sbyte[] ResetItemCount;

	[Side(ReleaseSide.Client)]
	public Ref<TalkSocial> AcquireTalksocial;

	[Side(ReleaseSide.Client)]
	public float AcquireTalksocialDelay;

	[Side(ReleaseSide.Client)]
	public Ref<TalkSocial> CompleteTalksocial;

	[Side(ReleaseSide.Client)]
	public float CompleteTalksocialDelay;

	public bool CheckVitality;

	public short ValidDateStartYear;
	public sbyte ValidDateStartMonth;
	public sbyte ValidDateStartDay;

	public short ValidDateEndYear;
	public sbyte ValidDateEndMonth;
	public sbyte ValidDateEndDay;

	public sbyte ValidTimeStartHour;
	public sbyte ValidTimeEndHour;

	public bool ValidDayofweekSun;
	public bool ValidDayofweekMon;
	public bool ValidDayofweekTue;
	public bool ValidDayofweekWed;
	public bool ValidDayofweekThu;
	public bool ValidDayofweekFri;
	public bool ValidDayofweekSat;

	[Deprecated]
	public Ref<Quest> ReplayEpicOriginal;
	public bool ReplayEpicStartPoint;

	public Ref<ZonePcSpawn> ReplayEpicPcspawn;

	public Ref<Dungeon> Dungeon2;

	public sbyte DuelMissionSteps;
	public sbyte DuelMissions;
	public sbyte DuelCases;
	public short DuelCaseSubtypes;

	public sbyte ExceedLevelNextLevel;

	public Ref<ContentsReset> ContentsReset;

	public bool CinemaCheck;

	public bool ReplayCheck;


	[Side(ReleaseSide.Server)]
	public BroadcastCategory BroadcastCategory;

	[Side(ReleaseSide.Server) , Repeat(3)]
	public Ref<Achievement>[] ExtraQuestCompleteAchievement;

	[Side(ReleaseSide.Server)]
	public Ref<Cinematic> ReplayEpicZoneLeaveCinematic;
	#endregion

	#region children element
	public List<Acquisition> Acquisition;

	public List<MissionStep> MissionStep;

	public List<Completion> Completion;

	public List<Transit> Transit;

	public List<GiveupLoss> GiveupLoss;
	#endregion


	#region	Properties
	public string Title => Group2.GetText();
	public string Text => Name2.GetText();
	public string Describe => Desc.GetText();

	public SKBitmap FrontIcon
	{
		get
		{
			string respath()
			{
				bool IsRepeat = ResetType != ResetType.None;
				switch (Category)
				{
					case Category.Epic: return "Map_Epic_Start";
					case Category.Job: return "Map_Job_Start";
					case Category.Dungeon: return null;
					case Category.Attraction: return "Map_attraction_start";
					case Category.TendencySimple: return "Map_System_start";
					case Category.TendencyTendency: return "Map_System_start";
					case Category.Mentoring: return "mento_mentoring_start";
					case Category.Hunting: return IsRepeat ? "Map_Hunting_repeat_start" : "Map_Hunting_start";
					case Category.Normal:
					{
						//faction quest
						if (!MainFaction.IsNull)
							return IsRepeat ? "Map_Faction_repeat_start" : "Map_Faction_start";

						return ContentType switch
						{
							ContentType.Festival => IsRepeat ? "Map_Festival_repeat_start" : "Map_Festival_start",
							ContentType.Duel or ContentType.PartyBattle => IsRepeat ? "Map_Faction_repeat_start" : "Map_Faction_start",
							ContentType.SideEpisode => "Map_side_episode_start",
							ContentType.Special => "Map_Job_Start",

							_ => IsRepeat ? "Map_Repeat_start" : "Map_Normal_Start",
						};
					}

					default: throw new NotImplementedException();
				}
			}


			var res = respath();
			if (res is null) return null;

			return FileCache.Provider.LoadObject($"BNSR/Content/Art/UI/GameUI/Resource/GameUI_Map_Indicator/{res}")?.GetImage();
		}
	}
	#endregion
}