using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Xylia.Preview.UI.Common.Converters;
public class EnumConverter : MarkupExtension, IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (targetType == typeof(int))
		{
			if (value.GetType().IsEnum) return (int)value;
		}

		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (targetType.IsEnum)
		{
			if (value is int) return Enum.ToObject(targetType , value);
		}

		return value;
	}

	public override object ProvideValue(IServiceProvider serviceProvider) => this;
}