using System.Xml.Serialization;

namespace Xylia.Preview.Data.Record.Test;

[XmlRoot("screenshot")]
public class ScreenShot
{
	public HeadInfo head;

	public BodyInfo body;

	public AppearanceInfo appearance;


	public class HeadInfo
	{
		[XmlElement("major-version")]
		public ushort MajorVersion;

		[XmlElement("minor-version")]
		public ushort MinorVersion;

		public string gamename;

		[XmlElement("product-version")]
		public string ProductVersion;

		public string time;

		public string checksum;

		public string checksum2;
	}

	public class BodyInfo
	{

	}

	public class AppearanceInfo
	{
		[XmlElement("major-version")]
		public ushort MajorVersion;

		[XmlElement("minor-version")]
		public ushort MinorVersion;

		public byte race;

		public byte sex;

		public string data;
	}
}