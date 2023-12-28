using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Data.Models.Sequence;
public static partial class SequenceExtensions
{
	public static object CastSeq(this string value, string name)
	{
		var type = name.ToEnum<SequenceType>();
		if (type == SequenceType.None) return null;
		else if (type == SequenceType.KeyCap) return KeyCap.Cast(KeyCap.GetKeyCode(value));
		else if (type == SequenceType.KeyCommand) return KeyCommand.Cast(value.ToEnum<KeyCommandSeq>());

		throw new InvalidCastException($"Cast Failed: {name} > {value}");
	}

	public static bool CheckSeq<T>(this T[] seqs, T value) where T : Enum
	{
		T _default = default;

		if (value.Equals(_default)) return true;
		return seqs.Any(x => x.Equals(value)) || seqs.All(x => x.Equals(_default));
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