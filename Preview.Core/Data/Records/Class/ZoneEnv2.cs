using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class ZoneEnv2 : BaseRecord
{
	public Text Name2;

	[Signal("action-name2")]
	public Text ActionName2;

	[Signal("action-desc2")]
	public Text ActionDesc2;
}


public enum EnvOperation
{
	None,

	Open,

	Close,

	Enable,

	Disable,
}

public enum EnvState
{
	None,

	/// <summary>
	/// 开启
	/// </summary>
	Open,

	/// <summary>
	/// 关闭
	/// </summary>
	Close,

	Empty,

	[Signal("step-1")]
	Step1,

	[Signal("step-2")]
	Step2,

	[Signal("step-3")]
	Step3,

	[Signal("step-4")]
	Step4,

	[Signal("step-5")]
	Step5,

	[Signal("step-6")]
	Step6,

	[Signal("step-7")]
	Step7,
}