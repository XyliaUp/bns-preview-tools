using System.Xml.Linq;

namespace Xylia.Preview.Tests;
public static class XmlExtension
{
	public static void AddAttribute(this XElement element, string name, object value)
	{
		if (value is null) return;

		element.Add(new XAttribute(name, value));
	}
}