namespace Xylia.Preview.UI.Common;
public class NameObject<T>(T value, string text)
{
    public T Value { get; set; } = value;

    public string Text { get; set; } = text;

	public override string ToString() => Text;
}