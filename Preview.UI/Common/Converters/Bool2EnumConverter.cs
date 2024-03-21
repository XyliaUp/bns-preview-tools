using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Xylia.Preview.UI.Common.Converters;
public class Bool2EnumConverter : MarkupExtension, IValueConverter
{
	public override object ProvideValue(IServiceProvider serviceProvider) => this;

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value switch
		{
			false => 1,
			true => 2,
			_ => 0,
		};
	}

	public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value switch
		{
			1 => false,
			2 => true,
			_ => null,
		};
	}
}