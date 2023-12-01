using System;

using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.UI.Documents.Links;
/// <summary>
/// item-name:3d0a04.1.270F
/// </summary>
public sealed class ItemName : LinkId
{
	internal override void Load(ContentParams data)
	{
		var Id = Convert.ToInt32((string)data[1], 16);
		var Variant = Convert.ToInt32((string)data[2], 16);
		var StackCount = Convert.ToInt16((string)data[3], 16);

		//tagData.ClickEvent = new((o, e) => PreviewRegister.Preview(FileCache.Data.Item[Id, Variant]));
	}

	public static string CreateLink(string InnerText, Ref Ref, short Count = 1) => $"<link id=\"item-name:{Ref.Id:X}.{Ref.Variant}.{Count:X}\">{InnerText}</link>";
}