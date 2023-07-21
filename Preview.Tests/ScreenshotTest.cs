using System.Data;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xylia.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Xml;

namespace Xylia.Preview.Tests;

[TestClass]
public class ScreenshotTest
{
	//[TestMethod]
	[DataRow(@"C:\Users\Xylia\Pictures\BnS\CharacterCustomize\外形_灵_女00001.jpg")]
	public void LoadData(string FilePath)
	{
		var bitmap = new Bitmap(FilePath);
		//foreach (PropertyItem property in bitmap.PropertyItems)
		//{
		//	//https://exiftool.org/TagNames/EXIF.html
		//	//https://blog.csdn.net/wllw7176/article/details/115548120
		//	Console.WriteLine($"ID:0x{property.Id:X2}, Type:{property.Type}, Length:{property.Len}");
		//}


		var ApplicationNotes = bitmap.GetPropertyItem(0x02bc);
		var xml = Encoding.UTF8.GetString(ApplicationNotes.Value);

		var o = xml.GetObject<ScreenShot>();
		Console.WriteLine(o.appearance.data);
	}



	private List<int> range { get; set; } = new List<int>()
	{
		1,2,4,5,6,7,8,9,
		//10,11,12,13,14,15,16,17,18,19,
		//20,21,22,23,24,24,26,27,28,29,
		
		//30,31,32,33,34,35,36,
		
		37,38,
		39,40,21,42,42,

		44,45,46,47,48,49,
		//50,51,52,53,54,55,56,57,58,59,
		
		60,61,


		91
	};

	private static void Diff()
	{
		byte[] Data1 = "01010106010c050403010201020102020b0102043500000000000a00000000050a00000000000f000000000000000000000000ec2800000000000000000000000000000000000000000000000000000000000000000a000000000000".ToBytes();
		byte[] Data2 = "01010106010c050403010201020105020b0102013500000000000a00000000050a00000000000f000000000000000000000000ec2800000000000000000000000000000000000000000000000000000000000000000a000000000000".ToBytes();
		for (int i = 0; i < Data1.Length; i++)
		{
			var b1 = (sbyte)Data1[i];
			var b2 = (sbyte)Data2[i];
			if (b1 != b2)
			{
				Trace.WriteLine($"param-{i + 1} ({b1} => {b2})");
			}
		}
	}


	private static void CreateAppearance(RaceSeq race, SexSeq sex, byte[] data)
	{
		var xe = new XElement("record");
		xe.SetAttributeValue("alias", "");
		xe.SetAttributeValue("anim-tree-name", "NewAnimTree.TwoLegsTreeForNonPC");
		xe.SetAttributeValue("body-material-name", "");
		xe.SetAttributeValue("body-mesh-name", "");
		xe.SetAttributeValue("decal-radius", "1.00");
		xe.SetAttributeValue("enable-physbrst", "y");
		xe.SetAttributeValue("face-anim-set-name", "");
		xe.SetAttributeValue("face-material-name", "");
		xe.SetAttributeValue("face-mesh-name", "");
		xe.SetAttributeValue("npc-dialogue-set", "");
		xe.SetAttributeValue("npcattach1", "CommonNPC_Acc_HandFan.Mesh.CommonNPC_Acc_HandFan_Open_Juria");
		xe.SetAttributeValue("pc-customize", "n");
		xe.SetAttributeValue("race", race);
		xe.SetAttributeValue("sex", sex);
		xe.SetAttributeValue("sound-attenuation-scale", "1.00");
		xe.SetAttributeValue("sound-volume-scale", "1.00");
		xe.SetAttributeValue("unique-soundcue", "");
		xe.SetAttributeValue("unique-soundculldist", "0.00");
		xe.SetAttributeValue("unique-soundfadetime", "0.00");
		xe.SetAttributeValue("voice-set-name", "00007975.NPC.Female_Young3");

		for (int idx = 1; idx <= 92; idx++)
			xe.SetAttributeValue("param8-" + idx, data[idx - 1]);

		Console.WriteLine(xe);
	}
}



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