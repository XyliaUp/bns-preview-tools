using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace Xylia.Preview.UI.Converters;
internal class CollectionConvert<T> : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
	{
		if (sourceType == typeof(string)) return true;

		return base.CanConvertFrom(context, sourceType);
	}

	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	{
		if (value is string s)
		{
			var array = s.Split(' ', ',');
			var data = new ObservableCollection<T>();

			for (int i = 0; i < array.Length; i++)
			{
				var item = array[i];
				if (string.IsNullOrEmpty(item)) continue;

				data.Add((T) Convert(array[i]));
			}

			return data;
		}

		return base.ConvertFrom(context, culture, value);
	}

	public static object Convert(string s)
	{
		var type = typeof(T);
		if (type == typeof(string)) return s;
		if (type == typeof(int)) return int.Parse(s);

		throw new NotSupportedException();
	}
}