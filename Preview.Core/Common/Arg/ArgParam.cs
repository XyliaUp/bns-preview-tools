using Xylia.Extension;

namespace Xylia.Preview.Common.Arg;

public interface IArgParam
{
	/// <summary>
	/// get param value
	/// </summary>
	/// <param name="ParamName"></param>
	/// <returns></returns>
	object ParamTarget(string ParamName);
}

public class ArgParam<T> : IArgParam
{
	public T Value { get; set; }

	public override string ToString() => Value.ToString();



	object IArgParam.ParamTarget(string ParamName) => this.GetValue(ParamName, true);
}