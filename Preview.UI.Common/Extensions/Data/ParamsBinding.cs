using System;
using System.Globalization;
using System.Windows.Data;
using Xylia.Preview.UI.Documents;

namespace Xylia.Preview.UI.Extensions;
/// <summary>
///  Describes a collection of bindings attached to a single property.
///     The inner bindings contribute their values to the MultiBinding,
///     which combines/converts them into a resultant final value.
///     In the reverse direction, the target value is tranlated to
///     a set of values that are fed back into the inner bindings.
/// </summary>
public class ParamsBinding : MultiBinding
{
    public ParamsBinding()
    {
        Converter = new DataParamsConverter();
    }

    private class DataParamsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new DataParams(values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}