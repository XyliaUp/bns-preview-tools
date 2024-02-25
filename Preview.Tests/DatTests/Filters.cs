using System.Text;
using CUE4Parse.BNS.Assets.Exports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xylia.Preview.Data.Engine;
namespace Xylia.Preview.Tests.DatTests;

[TestClass]
public class Filters
{
	[TestMethod]
	public unsafe void Method()
	{
		//FileCache.Provider.LoadObject<UTexture>("BNSR/Content/Art/UI/GameUI/Resource/GameUI_Window_R/BNSR_Window.BNSR_Window").Decode()
		//	.Clone(7,7,49,49).Save(@"C:\Users\10565\Desktop\aaa.png");

		//foreach (var record in Data.FileCache.Data.TextData.Where(r => 
		//	r.alias.StartsWith("UI.ItemTooltip.") ||
		//	r.alias.StartsWith("Name.Item.")))
		//{
		//	Trace.WriteLine(record.Attributes);
		//}

		Console.WriteLine(TintColorConverter.ToLinearColor("FF6D7980", true));
	}

	[TestMethod]
	public unsafe void Method2()
	{
		var archive = new DataArchive(File.ReadAllBytes("C:\\腾讯游戏\\Blade_and_Soul\\BNSR\\Binaries\\Win64\\BNSR.exe"), true);
		archive.Position = 0x00;

		var temp = Encoding.Unicode.GetString(archive.ReadBytes((int)archive.Length));
		var index = 0;
		while (true)
		{
			index = temp.IndexOf("job-change-item-exchange-typ", index + 1, StringComparison.OrdinalIgnoreCase);
			if (index == -1) break;

			var index2 = index;
			Console.WriteLine(string.Concat($"[{index2 * 2:X8}] ", temp.AsSpan(index2, 0x100)));
		}
	}
}