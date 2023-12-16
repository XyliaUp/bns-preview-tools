using System.Xml.Linq;
using Xylia.Preview.Common.Attributes;

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
	public static string GetText(this string alias, bool Nullable = false) => new Ref<Text>(alias).GetText(Nullable);

	public static string GetText(this Ref<Text> @ref, bool Nullable = false)
	{
		var record = @ref.Instance;

		return record?.text ?? (Nullable ? null : @ref.ToString());
	}
}