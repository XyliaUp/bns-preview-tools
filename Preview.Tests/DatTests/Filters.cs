using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xylia.Preview.Data.Engine;
namespace Xylia.Preview.Tests.DatTests;

[TestClass]
public class Filters
{
	[TestMethod]
	public void Method()
	{
		//foreach (var record in Data.FileCache.Data.TextData.Where(r => 
		//	r.alias.StartsWith("UI.ItemTooltip.") ||
		//	r.alias.StartsWith("Name.Item.")))
		//{
		//	Trace.WriteLine(record.Attributes);
		//}
	}

	[TestMethod]
	public void Method2()
	{
		var archive = new DataArchive(File.ReadAllBytes("C:\\腾讯游戏\\bns_ob_prod\\Game\\BNSR\\Binaries\\Win64\\BNSR.exe"), true);
		archive.Position = 0x00;

		var temp = Encoding.Unicode.GetString(archive.ReadBytes((int)archive.Length));
		var index = 0;
		while (true)
		{
			index = temp.IndexOf("skill-build-up-group-list", index + 1, StringComparison.OrdinalIgnoreCase);
			if (index == -1) break;

			var index2 = index;
			Console.WriteLine(string.Concat($"[{index2 * 2:X8}] ", temp.AsSpan(index2, 0x100)));
		}
	}
}