namespace Xylia.Preview.Common.Attributes;

[Obsolete("repeat attribute no longer in use, read definition now.")]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class Repeat(ushort value) : Attribute
{
    public ushort Value = value;
}