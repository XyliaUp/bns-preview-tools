using System.ComponentModel;

using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models.QuestData.Enums;
public enum Indicator
{
	Default,

	[Name("quest-epic-can-complete")]
	QuestEpicCanComplete,

	[Name("quest-epic-can-acquire")]
	QuestEpicCanAcquire,

	[Name("quest-epic-can-progress")]
	QuestEpicCanProgress,

	[Name("quest-epic-next-acquire")]
	QuestEpicNextAcquire,

	[Name("quest-epic-completed")]
	QuestEpicCompleted,

	[Name("quest-job-can-complete")]
	QuestJobCanComplete,

	[Name("quest-job-can-acquire")]
	QuestJobCanAcquire,

	[Name("quest-job-can-progress")]
	QuestJobCanProgress,

	[Name("quest-job-next-acquire")]
	QuestJobNextAcquire,

	[Name("quest-job-completed")]
	QuestJobCompleted,

	[Name("quest-normal-can-complete")]
	QuestNormalCanComplete,

	[Name("quest-normal-can-acquire")]
	QuestNormalCanAcquire,

	[Name("quest-normal-can-progress")]
	QuestNormalCanProgress,

	[Name("quest-normal-next-acquire")]
	QuestNormalNextAcquire,

	[Name("quest-normal-completed")]
	QuestNormalCompleted,

	[Name("quest-faction-can-complete")]
	QuestFactionCanComplete,

	[Name("quest-faction-can-acquire")]
	QuestFactionCanAcquire,

	[Name("quest-faction-can-progress")]
	QuestFactionCanProgress,

	[Name("quest-faction-next-acquire")]
	QuestFactionNextAcquire,

	[Name("quest-faction-not-progress")]
	QuestFactionNotProgress,

	[Name("quest-faction-completed")]
	QuestFactionCompleted,

	[Name("quest-festival-can-complete")]
	QuestFestivalCanComplete,

	[Name("quest-festival-can-acquire")]
	QuestFestivalCanAcquire,

	[Name("quest-festival-can-progress")]
	QuestFestivalCanProgress,

	[Name("quest-festival-next-acquire")]
	QuestFestivalNextAcquire,

	[Name("quest-festival-completed")]
	QuestFestivalCompleted,

	[Name("quest-special-can-complete")]
	QuestSpecialCanComplete,

	[Name("quest-special-can-acquire")]
	QuestSpecialCanAcquire,

	[Name("quest-special-can-progress")]
	QuestSpecialCanProgress,

	[Name("quest-special-next-acquire")]
	QuestSpecialNextAcquire,

	[Name("quest-special-completed")]
	QuestSpecialCompleted,

	[Name("quest-side-episode-can-complete")]
	QuestSideEpisodeCanComplete,

	[Name("quest-side-episode-can-acquire")]
	QuestSideEpisodeCanAcquire,

	[Name("quest-side-episode-can-progress")]
	QuestSideEpisodeCanProgress,

	[Name("quest-side-episode-next-acquire")]
	QuestSideEpisodeNextAcquire,

	[Name("quest-side-episode-completed")]
	QuestSideEpisodeCompleted,

	[Name("quest-challenge-today-can-complete")]
	QuestChallengeTodayCanComplete,

	[Name("quest-challenge-today-can-acquire")]
	QuestChallengeTodayCanAcquire,

	[Name("quest-challenge-today-can-progress")]
	QuestChallengeTodayCanProgress,

	[Name("quest-challenge-today-next-acquire")]
	QuestChallengeTodayNextAcquire,

	[Name("quest-challenge-today-completed")]
	QuestChallengeTodayCompleted,


	[Name("quest-normal-repeat-can-complete")]
	QuestNormalRepeatCanComplete,

	[Name("quest-normal-repeat-can-acquire")]
	QuestNormalRepeatCanAcquire,

	[Name("quest-normal-repeat-can-progress")]
	QuestNormalRepeatCanProgress,

	[Name("quest-normal-repeat-next-acquire")]
	QuestNormalRepeatNextAcquire,

	[Name("quest-normal-repeat-completed")]
	QuestNormalRepeatCompleted,

	[Name("quest-faction-repeat-can-complete")]
	QuestFactionRepeatCanComplete,

	[Name("quest-faction-repeat-can-acquire")]
	QuestFactionRepeatCanAcquire,

	[Name("quest-faction-repeat-can-progress")]
	QuestFactionRepeatCanProgress,

	[Name("quest-faction-repeat-next-acquire")]
	QuestFactionRepeatNextAcquire,

	[Name("quest-faction-repeat-not-progress")]
	QuestFactionRepeatNotProgress,

	[Name("quest-faction-repeat-completed")]
	QuestFactionRepeatCompleted,

	[Name("quest-festival-repeat-can-complete")]
	QuestFestivalRepeatCanComplete,

	[Name("quest-festival-repeat-can-acquire")]
	QuestFestivalRepeatCanAcquire,

	[Name("quest-festival-repeat-can-progress")]
	QuestFestivalRepeatCanProgress,

	[Name("quest-festival-repeat-next-acquire")]
	QuestFestivalRepeatNextAcquire,

	[Name("quest-festival-repeat-completed")]
	QuestFestivalRepeatCompleted,

	[Name("quest-side-episode-repeat-can-complete")]
	QuestSideEpisodeRepeatCanComplete,

	[Name("quest-side-episode-repeat-can-acquire")]
	QuestSideEpisodeRepeatCanAcquire,

	[Name("quest-side-episode-repeat-can-progress")]
	QuestSideEpisodeRepeatCanProgress,

	[Name("quest-side-episode-repeat-next-acquire")]
	QuestSideEpisodeRepeatNextAcquire,

	[Name("quest-side-episode-repeat-completed")]
	QuestSideEpisodeRepeatCompleted,

	[Name("quest-normal-dayofweek-repeat-can-complete")]
	QuestNormalDayofweekRepeatCanComplete,

	[Name("quest-normal-dayofweek-repeat-can-acquire")]
	QuestNormalDayofweekRepeatCanAcquire,

	[Name("quest-normal-dayofweek-repeat-can-progress")]
	QuestNormalDayofweekRepeatCanProgress,

	[Name("quest-normal-dayofweek-repeat-next-acquire")]
	QuestNormalDayofweekRepeatNextAcquire,

	[Name("quest-normal-dayofweek-repeat-completed")]
	QuestNormalDayofweekRepeatCompleted,

	[Name("quest-faction-dayofweek-repeat-can-complete")]
	QuestFactionDayofweekRepeatCanComplete,

	[Name("quest-faction-dayofweek-repeat-can-acquire")]
	QuestFactionDayofweekRepeatCanAcquire,

	[Name("quest-faction-dayofweek-repeat-can-progress")]
	QuestFactionDayofweekRepeatCanProgress,

	[Name("quest-faction-dayofweek-repeat-next-acquire")]
	QuestFactionDayofweekRepeatNextAcquire,

	[Name("quest-faction-dayofweek-repeat-not-progress")]
	QuestFactionDayofweekRepeatNotProgress,

	[Name("quest-faction-dayofweek-repeat-completed")]
	QuestFactionDayofweekRepeatCompleted,

	[Name("quest-festival-dayofweek-repeat-can-complete")]
	QuestFestivalDayofweekRepeatCanComplete,

	[Name("quest-festival-dayofweek-repeat-can-acquire")]
	QuestFestivalDayofweekRepeatCanAcquire,

	[Name("quest-festival-dayofweek-repeat-can-progress")]
	QuestFestivalDayofweekRepeatCanProgress,

	[Name("quest-festival-dayofweek-repeat-next-acquire")]
	QuestFestivalDayofweekRepeatNextAcquire,

	[Name("quest-festival-dayofweek-repeat-completed")]
	QuestFestivalDayofweekRepeatCompleted,

	[Name("quest-side-episode-dayofweek-repeat-can-complete")]
	QuestSideEpisodeDayofweekRepeatCanComplete,

	[Name("quest-side-episode-dayofweek-repeat-can-acquire")]
	QuestSideEpisodeDayofweekRepeatCanAcquire,

	[Name("quest-side-episode-dayofweek-repeat-can-progress")]
	QuestSideEpisodeDayofweekRepeatCanProgress,

	[Name("quest-side-episode-dayofweek-repeat-next-acquire")]
	QuestSideEpisodeDayofweekRepeatNextAcquire,

	[Name("quest-side-episode-dayofweek-repeat-completed")]
	QuestSideEpisodeDayofweekRepeatCompleted,


	[Name("quest-attraction-dayofweek-repeat-can-complete")]
	QuestAttractionCanComplete,

	[Name("quest-attraction-dayofweek-repeat-can-acquire")]
	QuestAttractionCanAcquire,

	[Name("quest-attraction-dayofweek-repeat-can-progress")]
	QuestAttractionCanProgress,

	[Name("quest-attraction-dayofweek-repeat-next-acquire")]
	QuestAttractionNextAcquire,

	[Name("quest-attraction-not-progress")]
	QuestAttractionNotProgress,

	[Name("quest-attraction-dayofweek-repeat-completed")]
	QuestAttractionCompleted,

	[Name("quest-retired")]
	QuestRetired,

	[Name("convoy")]
	Convoy,

	[Name("mark-1")]
	Mark1,

	[Name("mark-2")]
	Mark2,

	[Name("mark-3")]
	Mark3,

	[Name("mark-4")]
	Mark4,

	[Name("mark-5")]
	Mark5,

	[Name("mark-6")]
	Mark6,

	[Name("mark-7")]
	Mark7,

	[Name("mark-8")]
	Mark8,


	[Name("mark-timer-0")]
	MarkTimer0,

	[Name("mark-timer-1")]
	MarkTimer1,

	[Name("mark-timer-2")]
	MarkTimer2,

	[Name("mark-timer-3")]
	MarkTimer3,

	[Name("mark-timer-4")]
	MarkTimer4,

	[Name("mark-timer-5")]
	MarkTimer5,

	[Name("mark-timer-6")]
	MarkTimer6,

	[Name("mark-timer-7")]
	MarkTimer7,

	[Name("mark-timer-8")]
	MarkTimer8,

	[Name("mark-timer-9")]
	MarkTimer9,

	[Name("mark-timer-10")]
	MarkTimer10,

	[Name("guild-mask-1")]
	GuildMask1,

	[Name("guild-mask-2")]
	GuildMask2,

	[Name("guild-mask-3")]
	GuildMask3,

	[Name("guild-mask-4")]
	GuildMask4,

	[Name("guild-mask-5")]
	GuildMask5,

	[Name("axe")]
	Axe,

	[Name("coin")]
	Coin,

	[Name("quest-gadget-drop")]
	QuestGadgetDrop,

	[Name("quest-gadget-required")]
	QuestGadgetRequired,

	[Name("quest-board-start")]
	QuestBoardStart,

	[Name("quest-vsboard-start")]
	QuestVsboardStart,

	[Name("quest-vsboard-end")]
	QuestVsboardEnd,

	[Name("quest-vsboard-repeat-start")]
	QuestVsboardRepeatStart,

	[Name("quest-factionboard-start")]
	QuestFactionboardStart,

	[Name("quest-factionboard-end")]
	QuestFactionboardEnd,

	[Name("quest-factionboard-repeat-start")]
	QuestFactionboardRepeatStart,


	[Name("attack-epic-kill")]
	AttackEpicKill,

	[Name("attack-epic-kill-gadget")]
	AttackEpicKillGadget,

	[Name("attack-job-kill")]
	AttackJobKill,

	[Name("attack-job-kill-gadget")]
	AttackJobKillGadget,

	[Name("attack-normal-kill")]
	AttackNormalKill,

	[Name("attack-normal-kill-gadget")]
	AttackNormalKillGadget,

	[Name("attack-faction-kill")]
	AttackFactionKill,

	[Name("attack-faction-kill-gadget")]
	AttackFactionKillGadget,

	[Name("attack-festival-kill")]
	AttackFestivalKill,

	[Name("attack-festival-kill-gadget")]
	AttackFestivalKillGadget,

	[Name("quest-simple-1")]
	QuestSimple1,

	[Name("quest-simple-2")]
	QuestSimple2,

	[Name("quest-simple-3")]
	QuestSimple3,

	[Name("quest-simple-4")]
	QuestSimple4,

	[Name("quest-simple-5")]
	QuestSimple5,

	[Name("quest-tendency-1")]
	QuestTendency1,

	[Name("quest-tendency-2")]
	QuestTendency2,

	[Name("quest-tendency-3")]
	QuestTendency3,

	[Name("quest-faction-challenge-today-can-complete")]
	QuestFactionChallengeTodayCanComplete,

	[Name("quest-faction-challenge-today-can-acquire")]
	QuestFactionChallengeTodayCanAcquire,

	[Name("quest-faction-challenge-today-can-progress")]
	QuestFactionChallengeTodayCanProgress,

	[Name("quest-faction-challenge-today-next-acquire")]
	QuestFactionChallengeTodayNextAcquire,

	[Name("quest-faction-challenge-today-not-progress")]
	QuestFactionChallengeTodayNotProgress,

	[Name("quest-faction-challenge-today-completed")]
	QuestFactionChallengeTodayCompleted,

	[Name("quest-mentoring-can-complete")]
	QuestMentoringCanComplete,

	[Name("quest-mentoring-can-acquire")]
	QuestMentoringCanAcquire,

	[Name("quest-mentoring-can-progress")]
	QuestMentoringCanProgress,

	[Name("quest-mentoring-completed")]
	QuestMentoringCompleted,


	[Name("quest-bind-challenge-today-completed")]
	QuestBindChallengeTodayCompleted,

	[Name("quest-bind-duel-challenge-today-completed")]
	QuestBindDuelChallengeTodayCompleted,

	[Name("quest-bind-festival-challenge-today-completed")]
	QuestBindFestivalChallengeTodayCompleted,

	[Name("quest-bind-normal-completed")]
	QuestBindNormalCompleted,

	[Name("quest-bind-duel-normal-completed")]
	QuestBindDuelNormalCompleted,

	[Name("quest-bind-festival-normal-completed")]
	QuestBindFestivalNormalCompleted,


	[Name("quest-hunting-can-complete")]
	QuestHuntingCanComplete,

	[Name("quest-hunting-can-acquire")]
	QuestHuntingCanAcquire,

	[Name("quest-hunting-can-progress")]
	QuestHuntingCanProgress,

	[Name("quest-hunting-next-acquire")]
	QuestHuntingNextAcquire,

	[Name("quest-hunting-completed")]
	QuestHuntingCompleted,

	[Name("quest-hunting-repeat-can-complete")]
	QuestHuntingRepeatCanComplete,

	[Name("quest-hunting-repeat-can-acquire")]
	QuestHuntingRepeatCanAcquire,

	[Name("quest-hunting-repeat-can-progress")]
	QuestHuntingRepeatCanProgress,

	[Name("quest-hunting-repeat-next-acquire")]
	QuestHuntingRepeatNextAcquire,

	[Name("quest-hunting-repeat-completed")]
	QuestHuntingRepeatCompleted,
}