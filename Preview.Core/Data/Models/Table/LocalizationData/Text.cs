using System.Xml.Linq;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;

[Side(ReleaseSide.Client)]
public sealed class Text : ModelElement
{
	public string alias { get; set; }

	public string text { get; set; }


	/// <summary>
	/// Convert XML text to record
	/// </summary>
	/// <param name="xml"></param>
	/// <returns></returns>
	public static Text Parse(string xml)
	{
		try
		{
			// determine if is element
			if (!xml.Contains('/')) return null;

			var element = XElement.Parse(xml);
			var reader = element.CreateReader();
			reader.MoveToContent();

			var alias = element.Attribute("alias")?.Value;
			var text = reader.ReadInnerXml();

			return new Text() { alias = alias, text = text };
		}
		catch
		{
			return null;
		}
	}
}

public static class TextExtension
{
	public static string GetText(this object obj)
	{
		if (obj is null) return null;
		else if (obj is Enum sequence) return SequenceExtensions.GetText(sequence);
		else if (obj is Record record) return record.Attributes["text"].ToString();
		else if (obj is Ref<Text> reference) return reference.Instance?.text;
		else return FileCache.Data.Get<Text>()[obj.ToString()]?.text;
	}
}