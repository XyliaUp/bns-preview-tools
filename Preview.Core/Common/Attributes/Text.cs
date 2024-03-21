namespace Xylia.Preview.Common.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class TextAttribute(string alias) : Attribute
{
	public string Alias { get; } = alias;
}