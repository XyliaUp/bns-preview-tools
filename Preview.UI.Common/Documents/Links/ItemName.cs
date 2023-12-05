using System;

using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.UI.Documents.Links;
public sealed class ItemName : LinkId
{
	public int Id;
	public int Variant;
	public short StackCount;

	internal override void Load(string text)
	{
		var data = text.Split('.');

		Id = Convert.ToInt32((string)data[0], 16);
		Variant = Convert.ToInt32((string)data[1], 16);
		StackCount = Convert.ToInt16((string)data[2], 16);
	}

	public static string Create(string InnerText, Ref Ref, short Count = 1) => $"<link id=\"item-name:{Ref.Id:X}.{Ref.Variant}.{Count:X}\">{InnerText}</link>";
}