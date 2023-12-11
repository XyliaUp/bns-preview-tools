using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Xylia.Preview.Data.Common.DataStruct;
/// <summary>
/// Marshal.SizeOf(System.Boolean) is 4 size,
/// we need fix it 
/// </summary>
public readonly struct BnsBoolean : IComparable, IComparable<bool>, IEquatable<bool>
{
	// Member Variables
	private readonly byte m_value;

	// The internal string representation
	internal const string TrueLiteral = "y";
	internal const string FalseLiteral = "n";

	public BnsBoolean(bool value)
	{
		m_value = value ? (byte)1 : (byte)0;
	}


	public override string ToString() => m_value == 1 ? TrueLiteral : FalseLiteral;

	public override int GetHashCode() => m_value;

	public override bool Equals([NotNullWhen(true)] object obj)
	{
		// If it's not a boolean, we're definitely not equal
		if (obj is not BnsBoolean) return false;

		return m_value == ((BnsBoolean)obj).m_value;
	}

	public bool Equals(bool obj)
	{
		return m_value == (obj ? 1 : 0);
	}

	// Compares this object to another object, returning an integer that
	// indicates the relationship. For booleans, false sorts before true.
	// null is considered to be less than any instance.
	// If object is not of type boolean, this method throws an ArgumentException.
	//
	// Returns a value less than zero if this  object
	//
	public int CompareTo(object obj)
	{
		if (obj == null)
		{
			return 1;
		}
		if (obj is not BnsBoolean)
		{
			throw new ArgumentException(nameof(obj));
		}

		if (m_value == ((BnsBoolean)obj).m_value)
		{
			return 0;
		}
		else if (m_value == 0)
		{
			return -1;
		}
		return 1;
	}

	public int CompareTo(bool value)
	{
		if (m_value == (value ? 1 : 0))
		{
			return 0;
		}
		else if (m_value == 0)
		{
			return -1;
		}
		return 1;
	}

	//
	// Static Methods
	//
	internal static bool IsTrueStringIgnoreCase(ReadOnlySpan<char> value)
	{
		return value.Equals(TrueLiteral, StringComparison.OrdinalIgnoreCase);
	}

	internal static bool IsFalseStringIgnoreCase(ReadOnlySpan<char> value)
	{
		return value.Equals(FalseLiteral, StringComparison.OrdinalIgnoreCase);
	}


	// Determines whether a String represents true or false.
	public static BnsBoolean Parse(string value)
	{
		ArgumentNullException.ThrowIfNull(value);

		return Parse(value.AsSpan());
	}

	public static BnsBoolean Parse(ReadOnlySpan<char> value)
	{
		if (!TryParse(value, out bool result)) return false;	 
		return result;
	}

	// Determines whether a String represents true or false.
	public static BnsBoolean TryParse([NotNullWhen(true)] string value, out bool result) => TryParse(value.AsSpan(), out result);

	public static BnsBoolean TryParse(ReadOnlySpan<char> value, out bool result)
	{
		// Boolean.{Try}Parse allows for optional whitespace/null values before and
		// after the case-insensitive "true"/"false", but we don't expect those to
		// be the common case. We check for "true"/"false" case-insensitive in the
		// fast, inlined call path, and then only if neither match do we fall back
		// to trimming and making a second post-trimming attempt at matching those
		// same strings.

		if (IsTrueStringIgnoreCase(value))
		{
			result = true;
			return true;
		}

		if (IsFalseStringIgnoreCase(value))
		{
			result = false;
			return true;
		}

		return TryParseUncommon(value, out result);

		[MethodImpl(MethodImplOptions.NoInlining)]
		static bool TryParseUncommon(ReadOnlySpan<char> value, out bool result)
		{
			// With "true" being 4 characters, even if we trim something from <= 4 chars,
			// it can't possibly match "true" or "false".
			int originalLength = value.Length;
			if (originalLength >= 5)
			{
				value = TrimWhiteSpaceAndNull(value);
				if (value.Length != originalLength)
				{
					// Something was trimmed.  Try matching again.
					if (IsTrueStringIgnoreCase(value))
					{
						result = true;
						return true;
					}

					result = false;
					return IsFalseStringIgnoreCase(value);
				}
			}

			result = false;
			return false;
		}
	}

	private static ReadOnlySpan<char> TrimWhiteSpaceAndNull(ReadOnlySpan<char> value)
	{
		int start = 0;
		while (start < value.Length)
		{
			if (!char.IsWhiteSpace(value[start]) && value[start] != '\0')
			{
				break;
			}
			start++;
		}

		int end = value.Length - 1;
		while (end >= start)
		{
			if (!char.IsWhiteSpace(value[end]) && value[end] != '\0')
			{
				break;
			}
			end--;
		}

		return value.Slice(start, end - start + 1);
	}



	public static implicit operator BnsBoolean(bool value) => new(value);

	public static implicit operator bool(BnsBoolean value) => value.m_value == 1;
}