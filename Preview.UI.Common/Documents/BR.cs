using System.Windows;

namespace Xylia.Preview.UI.Documents;
public class BR : Element
{
	protected override Size MeasureCore(Size availableSize) => new(0, FontSize);
}