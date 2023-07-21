using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 重置区域对象
/// </summary>
[Signal("reset-zone-object")]
public sealed class ResetZoneObject : Reaction
{
	public byte zreg;
}