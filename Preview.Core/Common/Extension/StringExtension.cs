using System.Text;
using System.Text.RegularExpressions;

namespace Xylia.Preview.Common.Extension;
public static partial class String
{
	public static string RemovePrefixString(this string s, string str)
	{
		if (string.IsNullOrWhiteSpace(s))
			return null;

		string strRegex = @"^(" + Regex.Escape(str) + ")";
		return Regex.Unescape(Regex.Replace(s, strRegex, ""));
	}

	public static string RemoveSuffixString(this string s, string str)
	{
		if (string.IsNullOrWhiteSpace(s))
			return null;

		string strRegex = @"(" + Regex.Escape(str) + ")" + "$";
		return Regex.Unescape(Regex.Replace(s, strRegex, ""));
	}

	public static string RemovePrefixString(this string s, char str) => RemovePrefixString(s, str.ToString());

	public static string RemoveSuffixString(this string s, char str) => RemoveSuffixString(s, str.ToString());



	public static string TitleCase(this string line)
	{
		StringBuilder sb = new();
		for (int i = 0; i < line.Length; i++)
		{
			if ((i == 0 || (line[i - 1] == '-')) && line[i] >= 'a' && line[i] <= 'z') sb.Append((char)(line[i] - 32));
			else if (line[i] != '-') sb.Append(line[i]);
		}

		return sb.ToString();
	}

	public static string TitleLowerCase(this string line)
	{
		StringBuilder sb = new();
		for (int i = 0; i < line.Length; i++)
		{
			if (line[i] >= 'A' && line[i] <= 'Z')
			{
				if (i != 0) sb.Append('-');

			   sb.Append((char)(line[i] + 32));
			}
			else sb.Append(line[i]);
		}

		return sb.ToString();
	}
}