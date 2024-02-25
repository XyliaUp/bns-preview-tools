using System.Xml.Linq;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Utils;
using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Objects.UObject;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Tests;
public static class XmlExtension
{
    public static void AddAttribute(this XElement element, string name, object value)
    {
        // valid dumplicate
        var attribute = element.Attribute(name);
        if (attribute != null) return;

        // valid value
        if (value.IsNullOrDefault()) return;
        else if (value is FVector2D vect2)
        {
            if (vect2.X == 0 && vect2.Y == 0) return;
            value = $"{vect2.X} {vect2.Y}";

        }
        else if (value is FPackageIndex packageIndex)
        {
            value = packageIndex.ResolvedObject?.GetPathName();
        }

        if (value != null) element.Add(new XAttribute(name, value));
    }

    public static XElement AddElement(this XElement parent, string name)
    {
        var element = new XElement(name);
        parent.Add(element);

        return element;
    }


    public static void Write(this XElement element, IUStruct stru)
    {
        foreach (var prop in stru.GetType().GetProperties().Where(p => p.CanWrite))
        {
            var name = prop.GetAttribute<NameAttribute>()?.Name ?? prop.Name;
            var value = prop.GetValue(stru);

            // struct
            if (value is FStructFallback) continue;
            else if (value.GetAttribute<StructFallback>() != null &&
                value is not TintColor tint)
            {
                if (value is StringProperty sp && string.IsNullOrEmpty(sp.LabelText.Text)) continue;

                element.AddElement($"{element.Name}.{name}")
                    .AddElement(prop.PropertyType.Name).Write((IUStruct)value);
            }
            else
            {
                // common
                element.AddAttribute(name, value);
            }
        }
    }
}