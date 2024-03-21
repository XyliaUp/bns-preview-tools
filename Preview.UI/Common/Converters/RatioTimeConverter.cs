using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Xylia.Preview.UI.Common.Converters;
public class RatioTimeConverter : MarkupExtension, IMultiValueConverter
{
	public override object ProvideValue(IServiceProvider serviceProvider) => this;

	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values.Length < 2) return null;
		if (values.Any(e => e == DependencyProperty.UnsetValue)) return DependencyProperty.UnsetValue;

		TimeSpan time = (TimeSpan)values[0];
		double percent = (double)values[1];
		return time * percent;
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}