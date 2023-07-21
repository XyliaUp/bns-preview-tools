using System.Text.RegularExpressions;

using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class Text : BaseRecord, IOut
{
	public string text;

	OutSetTable IOut.OutTable()
	{
		OutSetTable table = new();
		table.type = typeof(Text).Name;
		table.attribute.Add(new() { name = "alias", width = 50 });
		table.attribute.Add(new() { name = "text", width = 100 });

		return table;
	}
}

public static class TextExtension
{
	public static LocalDataTableSet data_replace = null; 

	public static string GetText(this string Alias, bool ReturnNull = false)
	{
		var Record = FileCache.Data.Text[Alias];
		if (Record is null) return ReturnNull ? null : Alias;

		return Record.GetText();
	}

	public static string GetText(this Text Record)
	{
		if (Record is null) return null;
		if (Record.Attributes is null) return Record.alias.GetText();
		
		// repelace text
		var tmpRecord = data_replace?.Text[Record.alias];
		if (tmpRecord is not null) Record = tmpRecord;

		return Record.Attributes["text"];
	}


	public static string CutText(this string Text)
	{
		if (Text is null) return null;

		var CopyTxt = Text.Decode();
		CopyTxt = new Regex(@"<\s*br\s*/\s*>").Replace(CopyTxt, "\n");
		return new Regex(@"<.*?>").Replace(CopyTxt, "");
	}
}