using BnsBinTool.Core.DataStructs;

using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Common.Tag.Link;
public sealed class ItemName : LinkId
{
	internal override void Load(Params<string> data)
	{
		var Id = Convert.ToInt32(data[1], 16);
		var Variant = Convert.ToInt32(data[2], 16);
		var StackCount = Convert.ToInt16(data[3], 16);

		tagData.ClickEvent = new((o, e) => PreviewRegister.Preview(FileCache.Data.Item[Id, Variant]));
	}


	public static string CreateLink(string InnerText, Ref Ref, short Count = 1) => $"<link id=\"item-name:{Ref.Id:X}.{Ref.Variant}.{Count:X}\">{InnerText}</link>";
}