using System.Data;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Record.Test;
using Xylia.Xml;

namespace Xylia.Preview.Tests;

[TestClass]
public class ScreenshotTest
{
	[TestMethod]
	[DataRow(@"F:\Resources\Pictures\BnS\CharacterCustomize\外形_灵_女00000.jpg")]
	public void LoadData(string FilePath)
	{
		var bitmap = new Bitmap(FilePath);	

		//https://exiftool.org/TagNames/EXIF.html
		//foreach (PropertyItem property in bitmap.PropertyItems)
		//	Console.WriteLine($"ID:0x{property.Id:X2}, Type:{property.Type}, Length:{property.Len}");

		var screenshot = Encoding.UTF8.GetString(bitmap.GetPropertyItem(0x02bc).Value).GetObject<ScreenShot>();
		Console.WriteLine(screenshot.appearance.data);


		Param8 p1 = "01010106010c050403010201020102020b0102043500000000000a00000000050a00000000000f000000000000000000000000ec2800000000000000000000000000000000000000000000000000000000000000000a000000000000";
		Param8 p2 = "01010106010c050403010201020105020b0102013500000000000a00000000050a00000000000f000000000000000000000000ec2800000000000000000000000000000000000000000000000000000000000000000a000000000000";
		if (p1 != p2)
		{

		}
	}
}