using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Helpers;
public static partial class SeqExtension
{
	public static object CastSeq(this string value, string name)
	{
		var type = name.ToEnum<SequenceType>();
		if (type == SequenceType.None) return null;
		else if (type == SequenceType.KeyCap) return KeyCap.Cast(KeyCap.GetKeyCode(value));
		else if (type == SequenceType.KeyCommand) return KeyCommand.Cast(value.ToEnum<KeyCommandSeq>());

		throw new InvalidCastException($"Cast Failed: {name} > {value}");
	}

	public static string GetText<T>(this T value) where T : Enum
	{
		var name = value.GetAttribute<NameAttribute>()?.Name;
		if (name != null) return name.GetText();

		return value.ToString();
	}
}


public enum SequenceType
{
	None,

	KeyCap,
	KeyCommand,
}