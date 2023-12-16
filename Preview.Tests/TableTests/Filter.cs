using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xylia.Preview.Tests;
public partial class TableTests
{
	[TestMethod]
	public void Filter()
	{
		//foreach (var record in Data.FileCache.Data.TextData.Where(r => 
		//	r.alias.StartsWith("UI.ItemTooltip.") ||
		//	r.alias.StartsWith("Name.Item.")))
		//{
		//	Trace.WriteLine(record.Attributes);
		//}


		//var tmp = "ItemSkill" + "Data";
		//foreach (var file in new DirectoryInfo(@"F:\Bns")
		//	.GetFiles(tmp + "*.xml"))
		//{
		//	var name = file.Name
		//		.RemoveSuffixString(file.Extension)
		//		.RemovePrefixString(tmp)
		//		.RemovePrefixString("_");


		//	XmlDocument doc = new();
		//	doc.Load(file.FullName);

		//	var record = doc.DocumentElement.ChildNodes.OfType<XmlElement>().First();
		//	Trace.WriteLine($"\t<file name=\"{name}\" alias=\"{record.Attributes["alias"].Value}\" />");
		//}
	}
}