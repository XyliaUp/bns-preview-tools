namespace Xylia.Preview.UI.Common.Documents;
public class BR : Element
{
	protected override Size MeasureCore(Size availableSize) => new(0, FontSize);
}