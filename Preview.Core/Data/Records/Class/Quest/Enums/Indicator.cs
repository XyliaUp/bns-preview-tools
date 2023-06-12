using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums
{
	[DefaultValue(Default)]
	public enum Indicator
	{
		Default,

		[Signal("quest-epic-can-complete")]
		QuestEpicCanComplete,

		[Signal("quest-epic-can-acquire")]
		QuestEpicCanAcquire,

		[Signal("quest-epic-can-progress")]
		QuestEpicCanProgress,

		[Signal("quest-epic-next-acquire")]
		QuestEpicNextAcquire,

		[Signal("quest-epic-completed")]
		QuestEpicCompleted,

		[Signal("quest-job-can-complete")]
		QuestJobCanComplete,

		[Signal("quest-job-can-acquire")]
		QuestJobCanAcquire,

		[Signal("quest-job-can-progress")]
		QuestJobCanProgress,

		[Signal("quest-job-next-acquire")]
		QuestJobNextAcquire,

		[Signal("quest-job-completed")]
		QuestJobCompleted,

		[Signal("quest-normal-can-complete")]
		QuestNormalCanComplete,

		[Signal("quest-normal-can-acquire")]
		QuestNormalCanAcquire,

		[Signal("quest-normal-can-progress")]
		QuestNormalCanProgress,

		[Signal("quest-normal-next-acquire")]
		QuestNormalNextAcquire,

		[Signal("quest-normal-completed")]
		QuestNormalCompleted,

		[Signal("quest-faction-can-complete")]
		QuestFactionCanComplete,

		[Signal("quest-faction-can-acquire")]
		QuestFactionCanAcquire,

		[Signal("quest-faction-can-progress")]
		QuestFactionCanProgress,

		[Signal("quest-faction-next-acquire")]
		QuestFactionNextAcquire,

		[Signal("quest-faction-not-progress")]
		QuestFactionNotProgress,

		[Signal("quest-faction-completed")]
		QuestFactionCompleted,

		[Signal("quest-festival-can-complete")]
		QuestFestivalCanComplete,

		[Signal("quest-festival-can-acquire")]
		QuestFestivalCanAcquire,

		[Signal("quest-festival-can-progress")]
		QuestFestivalCanProgress,

		[Signal("quest-festival-next-acquire")]
		QuestFestivalNextAcquire,

		[Signal("quest-festival-completed")]
		QuestFestivalCompleted,

		[Signal("quest-special-can-complete")]
		QuestSpecialCanComplete,

		[Signal("quest-special-can-acquire")]
		QuestSpecialCanAcquire,

		[Signal("quest-special-can-progress")]
		QuestSpecialCanProgress,

		[Signal("quest-special-next-acquire")]
		QuestSpecialNextAcquire,

		[Signal("quest-special-completed")]
		QuestSpecialCompleted,

		[Signal("quest-side-episode-can-complete")]
		QuestSideEpisodeCanComplete,

		[Signal("quest-side-episode-can-acquire")]
		QuestSideEpisodeCanAcquire,

		[Signal("quest-side-episode-can-progress")]
		QuestSideEpisodeCanProgress,

		[Signal("quest-side-episode-next-acquire")]
		QuestSideEpisodeNextAcquire,

		[Signal("quest-side-episode-completed")]
		QuestSideEpisodeCompleted,

		[Signal("quest-challenge-today-can-complete")]
		QuestChallengeTodayCanComplete,

		[Signal("quest-challenge-today-can-acquire")]
		QuestChallengeTodayCanAcquire,

		[Signal("quest-challenge-today-can-progress")]
		QuestChallengeTodayCanProgress,

		[Signal("quest-challenge-today-next-acquire")]
		QuestChallengeTodayNextAcquire,

		[Signal("quest-challenge-today-completed")]
		QuestChallengeTodayCompleted,


		[Signal("quest-normal-repeat-can-complete")]
		QuestNormalRepeatCanComplete,

		[Signal("quest-normal-repeat-can-acquire")]
		QuestNormalRepeatCanAcquire,

		[Signal("quest-normal-repeat-can-progress")]
		QuestNormalRepeatCanProgress,

		[Signal("quest-normal-repeat-next-acquire")]
		QuestNormalRepeatNextAcquire,

		[Signal("quest-normal-repeat-completed")]
		QuestNormalRepeatCompleted,

		[Signal("quest-faction-repeat-can-complete")]
		QuestFactionRepeatCanComplete,

		[Signal("quest-faction-repeat-can-acquire")]
		QuestFactionRepeatCanAcquire,

		[Signal("quest-faction-repeat-can-progress")]
		QuestFactionRepeatCanProgress,

		[Signal("quest-faction-repeat-next-acquire")]
		QuestFactionRepeatNextAcquire,

		[Signal("quest-faction-repeat-not-progress")]
		QuestFactionRepeatNotProgress,

		[Signal("quest-faction-repeat-completed")]
		QuestFactionRepeatCompleted,

		[Signal("quest-festival-repeat-can-complete")]
		QuestFestivalRepeatCanComplete,

		[Signal("quest-festival-repeat-can-acquire")]
		QuestFestivalRepeatCanAcquire,

		[Signal("quest-festival-repeat-can-progress")]
		QuestFestivalRepeatCanProgress,

		[Signal("quest-festival-repeat-next-acquire")]
		QuestFestivalRepeatNextAcquire,

		[Signal("quest-festival-repeat-completed")]
		QuestFestivalRepeatCompleted,

		[Signal("quest-side-episode-repeat-can-complete")]
		QuestSideEpisodeRepeatCanComplete,

		[Signal("quest-side-episode-repeat-can-acquire")]
		QuestSideEpisodeRepeatCanAcquire,

		[Signal("quest-side-episode-repeat-can-progress")]
		QuestSideEpisodeRepeatCanProgress,

		[Signal("quest-side-episode-repeat-next-acquire")]
		QuestSideEpisodeRepeatNextAcquire,

		[Signal("quest-side-episode-repeat-completed")]
		QuestSideEpisodeRepeatCompleted,

		[Signal("quest-normal-dayofweek-repeat-can-complete")]
		QuestNormalDayofweekRepeatCanComplete,

		[Signal("quest-normal-dayofweek-repeat-can-acquire")]
		QuestNormalDayofweekRepeatCanAcquire,

		[Signal("quest-normal-dayofweek-repeat-can-progress")]
		QuestNormalDayofweekRepeatCanProgress,

		[Signal("quest-normal-dayofweek-repeat-next-acquire")]
		QuestNormalDayofweekRepeatNextAcquire,

		[Signal("quest-normal-dayofweek-repeat-completed")]
		QuestNormalDayofweekRepeatCompleted,

		[Signal("quest-faction-dayofweek-repeat-can-complete")]
		QuestFactionDayofweekRepeatCanComplete,

		[Signal("quest-faction-dayofweek-repeat-can-acquire")]
		QuestFactionDayofweekRepeatCanAcquire,

		[Signal("quest-faction-dayofweek-repeat-can-progress")]
		QuestFactionDayofweekRepeatCanProgress,

		[Signal("quest-faction-dayofweek-repeat-next-acquire")]
		QuestFactionDayofweekRepeatNextAcquire,

		[Signal("quest-faction-dayofweek-repeat-not-progress")]
		QuestFactionDayofweekRepeatNotProgress,

		[Signal("quest-faction-dayofweek-repeat-completed")]
		QuestFactionDayofweekRepeatCompleted,

		[Signal("quest-festival-dayofweek-repeat-can-complete")]
		QuestFestivalDayofweekRepeatCanComplete,

		[Signal("quest-festival-dayofweek-repeat-can-acquire")]
		QuestFestivalDayofweekRepeatCanAcquire,

		[Signal("quest-festival-dayofweek-repeat-can-progress")]
		QuestFestivalDayofweekRepeatCanProgress,

		[Signal("quest-festival-dayofweek-repeat-next-acquire")]
		QuestFestivalDayofweekRepeatNextAcquire,

		[Signal("quest-festival-dayofweek-repeat-completed")]
		QuestFestivalDayofweekRepeatCompleted,

		[Signal("quest-side-episode-dayofweek-repeat-can-complete")]
		QuestSideEpisodeDayofweekRepeatCanComplete,

		[Signal("quest-side-episode-dayofweek-repeat-can-acquire")]
		QuestSideEpisodeDayofweekRepeatCanAcquire,

		[Signal("quest-side-episode-dayofweek-repeat-can-progress")]
		QuestSideEpisodeDayofweekRepeatCanProgress,

		[Signal("quest-side-episode-dayofweek-repeat-next-acquire")]
		QuestSideEpisodeDayofweekRepeatNextAcquire,

		[Signal("quest-side-episode-dayofweek-repeat-completed")]
		QuestSideEpisodeDayofweekRepeatCompleted,


		[Signal("quest-attraction-dayofweek-repeat-can-complete")]
		QuestAttractionCanComplete,

		[Signal("quest-attraction-dayofweek-repeat-can-acquire")]
		QuestAttractionCanAcquire,

		[Signal("quest-attraction-dayofweek-repeat-can-progress")]
		QuestAttractionCanProgress,

		[Signal("quest-attraction-dayofweek-repeat-next-acquire")]
		QuestAttractionNextAcquire,

		[Signal("quest-attraction-not-progress")]
		QuestAttractionNotProgress,

		[Signal("quest-attraction-dayofweek-repeat-completed")]
		QuestAttractionCompleted,

		[Signal("quest-retired")]
		QuestRetired,

		[Signal("convoy")]
		Convoy,

		[Signal("mark-1")]
		Mark1,

		[Signal("mark-2")]
		Mark2,

		[Signal("mark-3")]
		Mark3,

		[Signal("mark-4")]
		Mark4,

		[Signal("mark-5")]
		Mark5,

		[Signal("mark-6")]
		Mark6,

		[Signal("mark-7")]
		Mark7,

		[Signal("mark-8")]
		Mark8,


		[Signal("mark-timer-0")]
		MarkTimer0,

		[Signal("mark-timer-1")]
		MarkTimer1,

		[Signal("mark-timer-2")]
		MarkTimer2,

		[Signal("mark-timer-3")]
		MarkTimer3,

		[Signal("mark-timer-4")]
		MarkTimer4,

		[Signal("mark-timer-5")]
		MarkTimer5,

		[Signal("mark-timer-6")]
		MarkTimer6,

		[Signal("mark-timer-7")]
		MarkTimer7,

		[Signal("mark-timer-8")]
		MarkTimer8,

		[Signal("mark-timer-9")]
		MarkTimer9,

		[Signal("mark-timer-10")]
		MarkTimer10,

		[Signal("guild-mask-1")]
		GuildMask1,

		[Signal("guild-mask-2")]
		GuildMask2,

		[Signal("guild-mask-3")]
		GuildMask3,

		[Signal("guild-mask-4")]
		GuildMask4,

		[Signal("guild-mask-5")]
		GuildMask5,

		[Signal("axe")]
		Axe,

		[Signal("coin")]
		Coin,

		[Signal("quest-gadget-drop")]
		QuestGadgetDrop,

		[Signal("quest-gadget-required")]
		QuestGadgetRequired,

		[Signal("quest-board-start")]
		QuestBoardStart,

		[Signal("quest-vsboard-start")]
		QuestVsboardStart,

		[Signal("quest-vsboard-end")]
		QuestVsboardEnd,

		[Signal("quest-vsboard-repeat-start")]
		QuestVsboardRepeatStart,

		[Signal("quest-factionboard-start")]
		QuestFactionboardStart,

		[Signal("quest-factionboard-end")]
		QuestFactionboardEnd,

		[Signal("quest-factionboard-repeat-start")]
		QuestFactionboardRepeatStart,


		[Signal("attack-epic-kill")]
		AttackEpicKill,

		[Signal("attack-epic-kill-gadget")]
		AttackEpicKillGadget,

		[Signal("attack-job-kill")]
		AttackJobKill,

		[Signal("attack-job-kill-gadget")]
		AttackJobKillGadget,

		[Signal("attack-normal-kill")]
		AttackNormalKill,

		[Signal("attack-normal-kill-gadget")]
		AttackNormalKillGadget,

		[Signal("attack-faction-kill")]
		AttackFactionKill,

		[Signal("attack-faction-kill-gadget")]
		AttackFactionKillGadget,

		[Signal("attack-festival-kill")]
		AttackFestivalKill,

		[Signal("attack-festival-kill-gadget")]
		AttackFestivalKillGadget,

		[Signal("quest-simple-1")]
		QuestSimple1,

		[Signal("quest-simple-2")]
		QuestSimple2,

		[Signal("quest-simple-3")]
		QuestSimple3,

		[Signal("quest-simple-4")]
		QuestSimple4,

		[Signal("quest-simple-5")]
		QuestSimple5,

		[Signal("quest-tendency-1")]
		QuestTendency1,

		[Signal("quest-tendency-2")]
		QuestTendency2,

		[Signal("quest-tendency-3")]
		QuestTendency3,

		[Signal("quest-faction-challenge-today-can-complete")]
		QuestFactionChallengeTodayCanComplete,

		[Signal("quest-faction-challenge-today-can-acquire")]
		QuestFactionChallengeTodayCanAcquire,

		[Signal("quest-faction-challenge-today-can-progress")]
		QuestFactionChallengeTodayCanProgress,

		[Signal("quest-faction-challenge-today-next-acquire")]
		QuestFactionChallengeTodayNextAcquire,

		[Signal("quest-faction-challenge-today-not-progress")]
		QuestFactionChallengeTodayNotProgress,

		[Signal("quest-faction-challenge-today-completed")]
		QuestFactionChallengeTodayCompleted,

		[Signal("quest-mentoring-can-complete")]
		QuestMentoringCanComplete,

		[Signal("quest-mentoring-can-acquire")]
		QuestMentoringCanAcquire,

		[Signal("quest-mentoring-can-progress")]
		QuestMentoringCanProgress,

		[Signal("quest-mentoring-completed")]
		QuestMentoringCompleted,


		[Signal("quest-bind-challenge-today-completed")]
		QuestBindChallengeTodayCompleted,

		[Signal("quest-bind-duel-challenge-today-completed")]
		QuestBindDuelChallengeTodayCompleted,

		[Signal("quest-bind-festival-challenge-today-completed")]
		QuestBindFestivalChallengeTodayCompleted,

		[Signal("quest-bind-normal-completed")]
		QuestBindNormalCompleted,

		[Signal("quest-bind-duel-normal-completed")]
		QuestBindDuelNormalCompleted,

		[Signal("quest-bind-festival-normal-completed")]
		QuestBindFestivalNormalCompleted,


		[Signal("quest-hunting-can-complete")]
		QuestHuntingCanComplete,

		[Signal("quest-hunting-can-acquire")]
		QuestHuntingCanAcquire,

		[Signal("quest-hunting-can-progress")]
		QuestHuntingCanProgress,

		[Signal("quest-hunting-next-acquire")]
		QuestHuntingNextAcquire,

		[Signal("quest-hunting-completed")]
		QuestHuntingCompleted,

		[Signal("quest-hunting-repeat-can-complete")]
		QuestHuntingRepeatCanComplete,

		[Signal("quest-hunting-repeat-can-acquire")]
		QuestHuntingRepeatCanAcquire,

		[Signal("quest-hunting-repeat-can-progress")]
		QuestHuntingRepeatCanProgress,

		[Signal("quest-hunting-repeat-next-acquire")]
		QuestHuntingRepeatNextAcquire,

		[Signal("quest-hunting-repeat-completed")]
		QuestHuntingRepeatCompleted,
	}
}