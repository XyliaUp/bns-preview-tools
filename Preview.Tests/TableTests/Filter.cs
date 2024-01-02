using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine;

namespace Xylia.Preview.Tests;
public partial class TableTests
{
	[TestMethod]
	public unsafe void Filter()
	{
		var rest = new DataArchiveWriter(true);

		rest.Write((uint)30);
		rest.Write((uint)30) ;

		Console.WriteLine(rest.ToArray().ToHex(true));


		for (int i = 1; i <= 25; i++)
		{
			var str = $"""	<record column="3" growth-category="etc1" item-equip-type="soul-2" node-type="seed-normal" row="{-1 - i}" seed-item-1="GB_General_Accessory_Soul2_MG_1001_1{i:00}" seed-item-2="GB_General_Accessory_Soul2_SG_1001_1{i:00}" seed-item-3="GB_General_Accessory_Soul2_NR_1001_1{i:00}" seed-item-4="GB_General_Accessory_Soul2_PG_1001_1{i:00}" seed-item-5="GB_General_Accessory_Soul2_WG_1001_1{i:00}" type="seed" use-improve="y" />""";
			Console.WriteLine(str);
		}
		

		//foreach (var record in Data.FileCache.Data.TextData.Where(r => 
		//	r.alias.StartsWith("UI.ItemTooltip.") ||
		//	r.alias.StartsWith("Name.Item.")))
		//{
		//	Trace.WriteLine(record.Attributes);
		//}
	}
}