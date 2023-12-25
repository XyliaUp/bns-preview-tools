using System.Windows.Data;
using HandyControl.Controls;

namespace Xylia.Preview.UI.Common.Controls.AttributeGrid.Editor;
internal class StringPropertyEditor : PlainTextPropertyEditor
{
	public override UpdateSourceTrigger GetUpdateSourceTrigger(PropertyItem propertyItem) => UpdateSourceTrigger.LostFocus;
}