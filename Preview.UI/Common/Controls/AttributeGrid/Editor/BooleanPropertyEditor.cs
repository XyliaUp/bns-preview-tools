using System.Globalization;
using System.Windows.Data;

using HandyControl.Controls;

using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.UI.Common.Controls.AttributeGrid.Editor;
public class BooleanPropertyEditor : SwitchPropertyEditor, IValueConverter
{
	protected override IValueConverter GetConverter(PropertyItem propertyItem) => this;

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is BnsBoolean boolean) return (bool)boolean;

		throw new NotImplementedException();
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is bool b) return (BnsBoolean)b;

		throw new NotImplementedException();
	}
}