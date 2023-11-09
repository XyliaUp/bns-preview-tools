using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;

[Side(ReleaseSide.Client)]
public sealed class Text : Record
{
	public string Alias;
	public string text;
}

public static class TextExtension
{
	public static string GetText(this string alias, bool Nullable = false) => new Ref<Text>(alias).GetText(Nullable);

	public static string GetText(this Ref<Text> @ref, bool Nullable = false)
	{
		var record = @ref.Instance;

		return record?.text ?? (Nullable ? null : @ref.ToString());
	}
}