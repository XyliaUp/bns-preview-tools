namespace Xylia.Preview.Data.Common.Attribute;

[AttributeUsage(AttributeTargets.Field)]
public sealed class Repeat : System.Attribute
{
    public ushort Value;

    public Repeat(ushort value) => Value = value;
}