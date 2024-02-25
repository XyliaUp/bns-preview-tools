using System.Xml.Serialization;

namespace Xylia.Preview.Data.Models.Config;
public abstract class ConfigTable : Group
{
	[XmlAttribute("major-version")]
	public ushort MajorVersion;

	[XmlAttribute("minor-version")]
	public ushort MinorVersion;

	[XmlAttribute("module")]
	public string Moudle;

	[XmlAttribute("release-module")]
	public string ReleaseMoudle;

	[XmlAttribute("release-side")]
	public string ReleaseSide;


	public static T LoadFrom<T>(Stream stream) where T : ConfigTable
	{
		if (stream is null) return null;
		return LoadFrom<T>(new StreamReader(stream).ReadToEnd());
	}

	public static T LoadFrom<T>(string xml) where T : ConfigTable
	{
		var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("config"));
		return serializer.Deserialize(new StringReader(xml)) as T;
	}
}


public class Option
{
	[XmlAttribute]
	public string name;

	[XmlAttribute]
	public string value;
}

public class Group
{
	[XmlAttribute]
	public string name;

	[XmlElement]
	public List<Option> option { get; set; } = [];

	[XmlElement]
	public List<Group> group { get; set; } = [];




	IReadOnlyDictionary<string, Option> _hash;

	public Option this[string index]
	{
		get
		{
			_hash ??= option.ToLookup(o => o.name, o => o).ToDictionary(o => o.Key, o => o.First());
			return _hash.GetValueOrDefault(index);
		}
	}
}