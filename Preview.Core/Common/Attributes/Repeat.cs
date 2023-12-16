namespace Xylia.Preview.Common.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class Repeat : Attribute
{
    public ushort Value;

    public Repeat(ushort value) => Value = value;
}