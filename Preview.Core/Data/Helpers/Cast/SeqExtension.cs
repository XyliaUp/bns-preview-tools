using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Helpers;
public static partial class SeqExtension
{
	public static object CastSeq(this string value, string name)
	{
		if (!name.TryParseToEnum<SeqType>(out var SeqType)) return null;
		else if (SeqType == SeqType.KeyCap) return KeyCap.Cast(KeyCap.GetKeyCode(value));
		else if (SeqType == SeqType.KeyCommand) return KeyCommand.Cast(value.ToEnum<KeyCommandSeq>());

		throw new InvalidCastException($"Cast Failed: {name} > {value}");
	}

	public static string GetText<T>(this T value) where T : Enum
	{
		var name = value.GetAttribute<NameAttribute>()?.Name;
		if (name != null) return name.GetText();

		return value.ToString();
	}
}


public enum SeqType
{
	None,

	KeyCap,
	KeyCommand,
}