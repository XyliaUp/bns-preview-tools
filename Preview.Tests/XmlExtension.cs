using System.Diagnostics;
using System.Xml.Linq;

namespace Xylia.Preview.Tests;
public static class XmlExtension
{
	public static void AddAttribute(this XElement element, string name, object value)
	{
		if (value is null) return;

		var attribute = element.Attribute(name);
		if (attribute != null)
		{
			Debug.WriteLine("dumplicate attribute: " + name);
			return;
		}

		element.Add(new XAttribute(name, value));
	}
}