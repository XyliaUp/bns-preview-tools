using System.Xml.Serialization;

namespace Xylia.Preview.Data.Helper.Output;

[XmlRoot("table")]
public class OutSetTable
{
	[XmlElement]
	public float version;

	[XmlElement]
	public string type;

	[XmlElement]
	public List<Attribute> attribute = new();

	[XmlElement]
	public List<FileConfig> file = new();



	public class Attribute : ICloneable
	{
		[XmlAttribute]
		public string name;

		[XmlAttribute]
		public string text;

		[XmlAttribute]
		public int width = 10;

		[XmlAttribute]
		public int repeat = 1;

		[XmlAttribute]
		public string extra;


		#region Constructor
		public Attribute(string name) => this.name = name;

		public Attribute()
		{ 
		
		}
		#endregion

		#region ICloneable
		object ICloneable.Clone() => MemberwiseClone();

		public Attribute Clone() => (Attribute)MemberwiseClone();
		#endregion
	}

	public class FileConfig
	{
		[XmlAttribute]
		public string alias;

		[XmlAttribute]
		public string name;
	}
}