using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.AIData.ActSequence.Action
{
	/// <summary>
	/// 活动类型
	/// </summary>
	public enum ActionType
	{
		Despawn,

		Hide,

		[Signal("indexed-movearound")]
		IndexedMovearound,

		[Signal("indexed-pathway")]
		IndexedPathway,

		[Signal("indexed-social")]
		IndexedSocial,

		Loop,

		Movearound,

		[Signal("movearound-for-fieldbossspawn")]
		MovearoundForFieldBossSpawn,

		[Signal("movearound-for-randomspawn")]
		MovearoundForRandomSpawn,

		Pause,

		Pathway,

		Select,

		Social,

		Stay,
	}
}