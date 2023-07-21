namespace Xylia.Preview.Common.Attribute;

[AttributeUsage(AttributeTargets.Field)]
public sealed class Repeat : System.Attribute
{
	public ushort Value;

	public Repeat(ushort value) => this.Value = value;
}