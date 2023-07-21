using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums;
public enum ContentType
{
	None,

	/// <summary>
	/// 采集
	/// </summary>
	Gather,

	/// <summary>
	/// 制作
	/// </summary>
	Production,


	[Signal("pvp-reward")]
	PvPReward,

	/// <summary>
	/// 庆典
	/// </summary>
	Festival,

	/// <summary>
	/// 武功秘籍
	/// </summary>
	[Signal("elite-skill")]
	EliteSkill,

	/// <summary>
	/// 比武
	/// </summary>
	Duel,

	/// <summary>
	/// 战场
	/// </summary>
	[Signal("party-battle")]
	PartyBattle,

	/// <summary>
	/// 特殊任务（紫色）
	/// </summary>
	Special,

	/// <summary>
	/// 外传任务（橘红色）
	/// </summary>
	[Signal("side-episode")]
	SideEpisode,
}
