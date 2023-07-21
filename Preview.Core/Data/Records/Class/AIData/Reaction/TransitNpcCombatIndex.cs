using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 转移战斗序列（指定）
/// </summary>
[Signal("transit-npc-combat-index")]
public sealed class TransitNpcCombatIndex : TransitNpcCombat
{
	/// <summary>
	/// 转移战斗序列索引 (智力参数索引)   索引从1开始
	/// </summary>
	public byte Index;
}