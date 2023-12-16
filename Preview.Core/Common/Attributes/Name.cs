namespace Xylia.Preview.Common.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class NameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}