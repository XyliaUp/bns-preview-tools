using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class ZoneEnv2 : Record
{
	public string Alias;

	public Ref<Text> Name2;

	public Ref<Text> ActionName2;
	public Ref<Text> ActionDesc2;
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