using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 委托效果
/// </summary>
[Signal("invoke-effect")]
public sealed class InvokeEffect : Reaction
{
	public Script_obj Target;

	public string Effect;


	
	/// <summary>
	/// SoulMask 类型效果必须设置此Fields
	/// </summary>
	public Script_obj Caster;

	public Script_obj Invoker;


	public bool Immediately;

	public Script_obj From;
}