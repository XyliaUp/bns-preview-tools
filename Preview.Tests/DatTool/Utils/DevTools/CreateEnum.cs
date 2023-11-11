using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using CUE4Parse.Utils;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Tests.DatTool.Utils.DevTools;
public static class CreateEnum
{
    public static string Instance(string text)
    {
        var xml = new XmlDocument();
        if (text.Contains(", "))
        {
            var table = xml.AppendChild(xml.CreateElement("table")) as XmlElement;

            var record = table.AppendChild(xml.CreateElement("attribute")) as XmlElement;
            foreach (var s in text.Split(", "))
            {
                var @case = xml.CreateElement("case");
                @case.SetAttribute("name", s);

                record.AppendChild(@case);
            }
        }
        else
        {
            xml.LoadXml($"<?xml version=\"1.0\"?>\n<table>{text}</table>");
        }

        return Instance(xml);
    }

    public static string Instance(XmlDocument xml)
    {
        StringBuilder result = new();
        foreach (XmlNode attribute in xml.SelectNodes("table/attribute"))
        {
            var AttributeName = attribute.Attributes["name"]?.Value;
            result.Append($"public enum {AttributeName?.TitleCase()}Seq\n{{\n");

            foreach (XmlNode Case in attribute.SelectNodes("./case"))
            {
                var name = Case.Attributes["name"]?.Value;
                var desc = Case.Attributes["desc"]?.Value;
                var IsDefault = Case.Attributes["default"]?.Value.ToBool();

				if (new Regex(@"-\d+$").Match(name).Success) result.AppendLine($"[Name(\"{name}\")]");
                if (!string.IsNullOrWhiteSpace(desc)) result.AppendLine($"[Description(\"{desc}\")]");

                result.AppendLine($"{name.TitleCase()},");
            }

            result.Append("}\n");
        }

        return result.ToString().SubstringBeforeLast("\n");
    }
}