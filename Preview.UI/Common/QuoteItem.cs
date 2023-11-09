namespace Xylia.Preview.UI.Common;
public class QuoteItem<T>
{
    public T Value;

    public string Text;


    public QuoteItem(T value, object text = null)
    {
        this.Value = value;
        this.Text = text?.ToString();
    }


	public static implicit operator QuoteItem<T>(T value) => new(value);

	public override string ToString() => Text;
}