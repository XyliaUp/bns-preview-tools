using System.ComponentModel;
using System.Reflection;
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



	public static object LoadSequence(Type type, string val)
	{
		return Enum.Parse(type, val.Replace('-', '_'), true);
	}

	public static object LoadPropSeq(Type type, string val)
	{
		foreach (string text in Enum.GetNames(type))
		{
			MemberInfo[] member = type.GetMember(text);
			if (member != null && member.Length != 0)
			{
				object[] customAttributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (customAttributes != null && customAttributes.Length != 0 && string.Compare(((DescriptionAttribute)customAttributes[0]).Description, val) == 0)
				{
					return Enum.Parse(type, text);
				}
			}
		}
		return null;
	}
}


public enum SequenceType
{
	None,

	KeyCap,
	KeyCommand,
}