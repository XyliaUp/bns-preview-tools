namespace Xylia.Preview.Data.Common.Attribute;
public class NameAttribute(string name) : System.Attribute
{
    public string Name { get; } = name;
}