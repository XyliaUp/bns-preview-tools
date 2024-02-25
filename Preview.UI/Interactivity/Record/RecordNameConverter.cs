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
	public object? Convert(object value, Type targetType, object? parameter, CultureInfo? culture)
	{
		if (value is Record record)
		{
			if (record.Owner.Name == "text") return record.Attributes["text"];
			else
			{
				var text = record.Attributes["name2"]?.GetText();
				if (text != null) return text;
			}
		}

		// if parameter exists and its value is BooleanBoxes.False means that return Null
		if (parameter is false) return null;
		return value?.ToString();
	}

	public string Convert(object value)
	{
		return Convert(value, typeof(string), null, null) as string ?? "";
	}

	public object ConvertBack(object value, Type targetType, object? parameter, CultureInfo? culture)
	{
		throw new NotImplementedException();
	}

	public override object ProvideValue(IServiceProvider serviceProvider) => this;
}