namespace Xylia.Preview.UI.Common;
public class NameObject<T>(T value, string text)
{
    public T Value { get; } = value;

	public override string ToString() => text;
}