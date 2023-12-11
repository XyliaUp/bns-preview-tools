using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
public sealed class ZoneEnv2 : ModelElement { }


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

	[Name("step-1")]
	Step1,

	[Name("step-2")]
	Step2,

	[Name("step-3")]
	Step3,

	[Name("step-4")]
	Step4,

	[Name("step-5")]
	Step5,

	[Name("step-6")]
	Step6,

	[Name("step-7")]
	Step7,
}