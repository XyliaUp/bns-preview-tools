using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record.QuestData.Enums;

using static Xylia.Preview.Resources.Resource_Common;


namespace Xylia.Preview.Common.Extension
{
	public static class QuestExtension
	{
		public static Image FrontIcon(this Data.Record.Quest Quest)
		{
			bool IsRepeat = Quest.ResetType != ResetType.None;
			switch (Quest.Category)
			{
				case Category.Epic: return Map_Epic_Start;
				case Category.Job: return Map_Job_Start;
				case Category.Dungeon: return null;
				case Category.Attraction: return Map_attraction_start;
				case Category.TendencySimple: return Map_System_start;
				case Category.TendencyTendency: return Map_System_start;
				case Category.Mentoring: return mento_mentoring_start;
				case Category.Hunting: return IsRepeat ? Map_Hunting_repeat_start : Map_Hunting_start;
			}

			//faction quest
			if (Quest.MainFaction?.alias != null)
				return IsRepeat ? Map_Faction_repeat_start : Map_Faction_start;

			//content
			return Quest.ContentType switch
			{
				ContentType.Festival => IsRepeat ? Map_Festival_repeat_start : Map_Festival_start,
				ContentType.Duel or ContentType.PartyBattle => IsRepeat ? Map_Faction_repeat_start : Map_Faction_start,
				ContentType.SideEpisode => Map_side_episode_start,
				ContentType.Special => Map_Job_Start,

				_ => IsRepeat ? Map_Repeat_start : Map_Normal_Start,
			};
		}

	}
}
