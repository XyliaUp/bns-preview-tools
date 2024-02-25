using System;
using System.Windows;
using CUE4Parse.BNS.Assets.Exports;

namespace Xylia.Preview.UI.Converters;
public static class LayoutExtension
{
    public static VerticalAlignment Parse(this VAlignment alignment)
    {
        return alignment switch
        {
            VAlignment.VAlign_Top => VerticalAlignment.Top,
            VAlignment.VAlign_Center => VerticalAlignment.Center,
            VAlignment.VAlign_Bottom => VerticalAlignment.Bottom,
            _ => throw new NotSupportedException()
        };
    }

    public static HorizontalAlignment Parse(this HAlignment alignment)
    {
        return alignment switch
        {
            HAlignment.HAlign_Left => HorizontalAlignment.Left,
            HAlignment.HAlign_Center => HorizontalAlignment.Center,
            HAlignment.HAlign_Right => HorizontalAlignment.Right,
            _ => throw new NotSupportedException()
        };
    }
}