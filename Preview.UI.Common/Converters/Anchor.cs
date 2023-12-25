using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Xylia.Preview.UI.Converters;

[TypeConverter(typeof(AnchorConverter))]
public struct Anchor
{
    public Vector Minimum;
    public Vector Maximum;
}

public class AnchorConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string)) return true;

        return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string s)
        {
            var anchor = new Anchor();

            try
            {
                var collection = VectorCollection.Parse(s);
                if (collection.Count == 1)
                {
                    anchor.Minimum = anchor.Maximum = collection[0];
                }
                else
                {
                    anchor.Minimum = collection[0];
                    anchor.Maximum = collection[1];
                }
            }
            catch
            {

            }

            return anchor;
        }

        return base.ConvertFrom(context, culture, value);
    }
}