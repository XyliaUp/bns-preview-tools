using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Xylia.Preview.UI.Common.Converters;
public class RatioConverter : MarkupExtension, IValueConverter
{
	public override object ProvideValue(IServiceProvider serviceProvider) => this;

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
       var size = System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter, culture);
        return size.ToString("G0", culture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}