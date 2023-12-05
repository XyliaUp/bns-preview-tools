using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Common.Converters;
/// <summary>
/// Oneway converter for obtaining text of record 
/// </summary>
public class RecordNameConverter : MarkupExtension, IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is Record record)
		{
			var text = record.Attributes["name2"]?.GetText();
			if (text != null) return text;
		}

		return value.ToString();
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}

	public override object ProvideValue(IServiceProvider serviceProvider) => this;
}