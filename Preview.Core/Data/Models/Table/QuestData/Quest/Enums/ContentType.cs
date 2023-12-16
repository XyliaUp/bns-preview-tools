using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models.QuestData.Enums;
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


	[Name("pvp-reward")]
	PvPReward,

	/// <summary>
	/// 庆典
	/// </summary>
	Festival,

	/// <summary>
	/// 武功秘籍
	/// </summary>
	[Name("elite-skill")]
	EliteSkill,

	/// <summary>
	/// 比武
	/// </summary>
	Duel,

	/// <summary>
	/// 战场
	/// </summary>
	[Name("party-battle")]
	PartyBattle,

	/// <summary>
	/// 特殊任务（紫色）
	/// </summary>
	Special,

	/// <summary>
	/// 外传任务（橘红色）
	/// </summary>
	[Name("side-episode")]
	SideEpisode,
}
